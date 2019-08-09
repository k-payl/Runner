using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using LevelGeneration;
using Sound;

namespace GamePlay
{
	public abstract class BaseState : IControllerState, IEquatable<BaseState>
	{
		protected static float verticalSpeed;
		protected static bool wasInAirWhenAttacked = false;//for prevention of double-attack in air
		protected static TrackAbstract track;
		protected static Controller controller;
		protected static Transform controllerTransform;
		protected static Vector3 result;//??
		protected static CharacterController controllerCollider;
		private float XRaycastLenght;
		private static Int32 turningIgnoreMask;
		private static Int32 isGroundIgnoreMask;
		//protected static bool isGrounded;
		//protected static bool isStamered;
		//protected static bool canAttack;
		//protected static bool canJump;
		private float ZOffsetRayCast = 0.0f;   

		protected BaseState(Controller controller)
		{
			BaseState.controller = controller;
			BaseState.controllerTransform = controller.transform;
			track = Track3.GetInstance();
			controllerCollider = controller.GetComponent<CharacterController>();
			XRaycastLenght = Integrate(controller.ControllerParams.TurningCurve, 0f, 1f);
			turningIgnoreMask = ~((1 << controller.gameObject.layer) | (1 << 2)); // Everything except player and chunk layers. It works very strange
			isGroundIgnoreMask =~(1 << LayerMask.NameToLayer("IgnoreForIsGround"));
		}

		private float Integrate(AnimationCurve function, float x0, float x1)
		{
			float x = x0;
			float I = 0;														
			while(x<x1)
			{
				I += (function.Evaluate(x) + function.Evaluate(x + Time.deltaTime));
				x += Time.deltaTime;
			}
			return I *Time.deltaTime * 0.5f;
		}


		public virtual void Jump()
		{
				verticalSpeed = controller.ControllerParams.JumpVerticalSpeed;
				controller.SetState(controller.JumpingState);
				controller.soundEffects.PlayClip(PlayerClip.JumpUpSound);
				if (controller.footFlares != null)
					controller.footFlares.StartAndStopLastCrossfadable(controller.timeFlareAction, controller.timeFlaresCrossfade);
		}

		public virtual void Jump(float startSpeedY)
		{
			verticalSpeed = startSpeedY;
			controller.SetState(controller.JumpingState);
			controller.soundEffects.PlayClip(PlayerClip.JumpUpSound);
			if (controller.footFlares != null)
				controller.footFlares.StartAndStopLastCrossfadable(controller.timeFlareAction, controller.timeFlaresCrossfade);
		}
		public virtual void JumpDown(){}


		public virtual void Turn(TurnDirection direction)
		{
			bool isTurnAllow = IsTurnAllow(direction);
			if(isTurnAllow && track.Turn(direction))
			{
				 controller.TurningState.direction = direction;
				controller.SetState(controller.TurningState);
				controller.soundEffects.PlayClip(PlayerClip.TurnSound);
				if (controller.footFlares != null)
					controller.footFlares.StartAndStopLastCrossfadable(controller.timeFlareAction, controller.timeFlaresCrossfade);
			}
			else if (!isTurnAllow)
			{
				//Debug.Log("DisallowedTurningState");
				controller.DisallowedTurnState.Direction = direction;
				controller.SetState(controller.DisallowedTurnState);
				controller.soundEffects.PlayClip(PlayerClip.DissalowedTurnSound);
			}
		}

		protected bool IsTurnAllow(TurnDirection direction)
		{

			Vector3 topBound = new Vector3(controllerTransform.position.x,
										   controllerCollider.bounds.max.y - controllerCollider.radius, 
										   controllerTransform.position.z + ZOffsetRayCast);
			Vector3 downBound = new Vector3(controllerTransform.position.x,
											controllerCollider.bounds.min.y + controllerCollider.radius, 
											controllerTransform.position.z + ZOffsetRayCast);

			Vector3 raycastDirection = (direction == TurnDirection.Left)
				? Vector3.left * XRaycastLenght * controller.ControllerParams.TurningSpeed + Vector3.forward * controller.ControllerParams.RunSpeed
				: Vector3.right * XRaycastLenght * controller.ControllerParams.TurningSpeed + Vector3.forward * controller.ControllerParams.RunSpeed;
			
			// if in air then rotate raycastDirecton to 10 degree
			if (!IsGrounded())
			{
				Vector3 axisOfRotation = Vector3.Cross(raycastDirection, Vector3.up);
				raycastDirection = Quaternion.AngleAxis(10, axisOfRotation)*raycastDirection;
			}

			float lenghtCast = (direction == TurnDirection.Left)
				? Mathf.Abs(track.LineWidth / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(raycastDirection, Vector3.left)))
				: Mathf.Abs(track.LineWidth / Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(raycastDirection, Vector3.right)));

			Debug.DrawLine(topBound, topBound + raycastDirection.normalized * lenghtCast, Color.blue, 1f);
			Debug.DrawLine(downBound, downBound + raycastDirection.normalized * lenghtCast, Color.blue, 1f);

			RaycastHit[] hits = Physics.CapsuleCastAll(downBound  , topBound  ,
													   (controllerCollider as CharacterController).radius*1,
													   raycastDirection,
													   lenghtCast,
													   turningIgnoreMask);

			bool isTurnAllow = true;
			foreach (IPoolable poolable in hits.Select(hit => hit.collider.GetComponent(typeof (IPoolable))))//.OfType<IPoolable>())
			{
				if (poolable is Bonus || poolable is EnemyAbstract || poolable is PlaceForHill)
					continue;
				else
				{
					//Debug.Log("DisallowedTurn. " + poolable);
					isTurnAllow = false;
					break;
				}
			}
			return isTurnAllow;
		}
		
		public virtual void Run()
		{
			controller.SetState(controller.RuningState);
		}

		public virtual void Dead(DeadReason deadReason)
		{
		   // Debug.Log("Dead! "+deadReason);
			controller.DeadState.DeadReason = deadReason;
			controller.SetState(controller.DeadState);
			
		} 

		public virtual Vector3 UpdatePosition()
		{
			result = Vector3.zero;
			result.z = controller.ControllerParams.RunSpeed;
			
			//плавная корректировка по x-координате
			//TODO вырубить дл оптимизации
			result.x = ((track as Track3)!=null)? (track as Track3).CurrentXCoord - controllerTransform.position.x : 0;
			
			if(IsGrounded())
				verticalSpeed = 0;
			else
				verticalSpeed -= controller.ControllerParams.Gravity*Time.deltaTime;
			result.y = verticalSpeed;

		  
			
			return result;
		}
		public virtual void Attack(AttackType attackType)
		{
			switch (attackType)
			{
				case AttackType.Shield:
					if (controller.ShieldIn != null) controller.ShieldIn.enabled = true;
					if (controller.ShieldOut != null) controller.ShieldOut.enabled = true;
					controller.ShieldAttackState.startTime = Time.time;
					controller.SetState(controller.ShieldAttackState);
					controller.soundEffects.PlayClip(PlayerClip.ShieldSound);
					if (controller.footFlares != null) 
						controller.footFlares.StartAndStopLastCrossfadable(0.6f, controller.timeFlaresCrossfade);
					break;

				case AttackType.Sword:
					if (controller.WrenchInHand != null) controller.WrenchInHand.enabled = true;
					if (controller.WrenchOnBelt != null) controller.WrenchOnBelt.enabled = false;
					controller.SetState(controller.SwordAttackState);
					controller.SwordAttackState.startTime = Time.time;
					controller.soundEffects.PlayClip(PlayerClip.SwordSound);
					if (controller.footFlares != null)
						controller.footFlares.StartAndStopLastCrossfadable(0.6f, controller.timeFlaresCrossfade);
					break;					  
			}
		}

	   
		public bool IsGrounded()
		{
			return Physics.Raycast(
					new Vector3(controllerCollider.bounds.center.x, controllerCollider.bounds.min.y,controllerCollider.bounds.center.z),
					Vector3.down, 
					0.1f,
					isGroundIgnoreMask);

		}

		public virtual void Stamered()
		{
			CharacterCamera.Instance.Shake();
			controller.SetState(controller.StramedState);
		}


		public virtual void HandleCollide(Collider collider, ControllerColliderHit hit)
		{
			if(collider)
			{
				IPoolable obj = collider.GetComponent(typeof(IPoolable)) as IPoolable;
				if (obj != null)
				{
					if (obj is DestroyableObstcle)
						HandeleCollide(obj as DestroyableObstcle);
					if (obj is PlaceForObstacle)
						HandeleCollide(obj as PlaceForObstacle, hit);
					if (obj is Rope)
						HandeleCollide(obj as Rope);
					if (obj is PlaceForDangerZone)
						HandeleCollide(obj as PlaceForDangerZone);
					if (obj is EnemyAbstract)
						HandeleCollide(obj as EnemyAbstract, hit);
					if (obj is Bonus)
						HandeleCollide(obj as Bonus);
				}
				else
				{
					HealthDecrementer health = collider.GetComponent<HealthDecrementer>();
					if (health is HealthDecrementer)
					{
						if ((health as HealthDecrementer).active)
						{
							controller.life.DecreaseHP();
						}
					}
					FinishArea finish = collider.GetComponent<FinishArea>();
					if (finish != null)
					{
						Finish(finish);
					}   
				}
			}
		}

		protected virtual void HandeleCollide(Bonus bonus)
		{
			//говорим бонусу что его собрали
			bonus.Collect();

			//если бонус временный, добавляем его к игроку.
			if (bonus is TimePeriodBonus)
			{
				controller.timePeriodBonuses.Add(bonus as TimePeriodBonus);
			}
			else
			{
				// одноразовые бонусы
				if (bonus is Coin)
				{
					GameManager.Instance.info.bonuses.IncScore((bonus as Coin).Score);
				}
				else if (bonus is CreditCard)
				{
					GameManager.Instance.info.bonuses.IncScore((bonus as CreditCard).score);
				}
				else if (bonus is HalfBattery)
				{
					GameManager.Instance.info.bonuses.hasHalfBattery = true;
				}
				else if (bonus is Medikit)
				{
					controller.life.IncreaseHP(1);
				}
				else if (bonus is MemoryCard)
				{
					GameManager.Instance.info.bonuses.hasMemoryCard = true;
				}
				else if (bonus is SuperMedikit)
				{
					controller.life.RestoreHP();
				}
			}

			//генерируем событие
			GameManager.Instance.BonusCollectedEvent(bonus, GameManager.Instance.info.bonuses);
			
		}

		protected virtual void HandeleCollide(DestroyableObstcle obj)
		{
			if ( controller.IsStamered )
			{
				Debug.Log("Stamered of destroyable object");
				Stamered();
			}
			else
			{
				Dead(DeadReason.ForwardCollision);
			}
		}
		protected virtual void HandeleCollide(PlaceForObstacle obj, ControllerColliderHit hit)
		{
			if(controller.IsStamered)
			{
				Stamered();
			}
			else if (hit.controller.collisionFlags == CollisionFlags.Sides |
					hit.controller.collisionFlags == CollisionFlags.CollidedSides)
			{
				Dead(DeadReason.ForwardCollision);
			}
			else 
			{
			   // Debug.Log("непонятое в HandeleCollide(PlaceForObstacle obj, ControllerColliderHit hit)"); 
			}
		}

		protected virtual void HandeleCollide(Rope obj)
		{
			controller.SetState(controller.RopeRollingState);
			controller.RopeRollingState.rope = obj;
		}

		protected virtual void HandeleCollide(PlaceForDangerZone obj)
		{
			Dead(DeadReason.DangerZone);
		}

		protected virtual void HandeleCollide(EnemyAbstract obj, ControllerColliderHit hit)
		{
			obj.DeactivateHelathDecrementers();
				if (obj is MiddleEnemy)
					obj.Attack(controller, WeaponType.MiddleEnemyWeapon);
				else if (obj is BigEnemy)
					obj.Attack(controller, WeaponType.BigEnemyWeapon);
				else if (obj is ButterflyEnemy)
					obj.Attack(controller, WeaponType.ButterflyEnemyWeapon);
		}

		protected virtual void Finish(FinishArea finish)
		{
			controller.SetState(new FinishState(controller));
		}

		


		/// <summary>Делает нужные действия при переходе в отложенное в очередь состояние. 
		/// Принимает RuningState, либо TurningState, либо  JumpingState, либо SwordAttackState, либо ShieldAttackState</summary>
		protected virtual void ApplyLastState(BaseState state)
		{
			if (state is RuningState)
			{
				Run();
			}
			if (state is TurningState)
			{
				controller.TurningState = state as TurningState;
				Turn((state as TurningState).direction); 
			}
			if (state is JumpingState)
			{
				Jump();
			}
			if (state is SwordAttackState)
			{
				controller.SwordAttackState = state as SwordAttackState;
				Attack(AttackType.Sword);
			}
			if ( state is ShieldAttackState )
			{
				controller.ShieldAttackState = state as ShieldAttackState;
				Attack(AttackType.Shield);
			}
		}

		bool IEquatable<BaseState>.Equals(BaseState other)
		{
			if ( other == null ) return false;
			return (GetType().Equals(other.GetType()));
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			BaseState s = obj as BaseState;
			if (obj == null)
				return false;
			else
				return GetType().Equals(s.GetType());
		}

		public override int GetHashCode()
		{
			return GetType().GetHashCode();
		}
	}
}
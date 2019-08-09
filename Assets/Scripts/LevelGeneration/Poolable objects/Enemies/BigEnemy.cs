using UnityEngine;
using GamePlay;
using System.Collections;
namespace LevelGeneration
{
	[RequireComponent(typeof(Collider))]
	public class BigEnemy : EnemyAbstract
	{
		public AnimationClip DamageAnimation;

		private bool ShieldStrikeIsDone;
		private AnimationCurve curve;
		private Transform _transform;
		private float startTime;
		private float timeOfDamage;
		private float magicKoeff;
		private SlowMotionEffect slowMotionEffect;
		

		protected override void Start()
		{
			base.Start();
			ShieldStrikeIsDone = false; 
			_transform = transform;
			timeOfDamage = Controller.GetInstance().ControllerParams.ShieldAttackTime;
			curve = Controller.GetInstance().ControllerParams.AttackAcceleration;
			magicKoeff = CalculateMaximumAtCurve(curve)*2.85f;
			slowMotionEffect = FindObjectOfType<SlowMotionEffect>();
		}

		protected override void Update()
		{
			if (live && (Time.time - startTime) <= timeOfDamage && ShieldStrikeIsDone)
			{
				//linear law; it depends of max of Controller.GetInstance().ControllerParams.AttackAcceleration 
				_transform.Translate(Vector3.forward * (1f - (Time.time - startTime) / timeOfDamage) * Time.deltaTime * magicKoeff);
			}
				
		}

		public override void ResetState()
		{
			base.ResetState();
			ShieldStrikeIsDone = false;
		}

		public override void Attack( IAttackable target, WeaponType weaponType )
		{
			AttackEffect();
			target.ReciveDamage(weaponType);
			

		}
		
		public override void ReciveDamage( WeaponType weaponType )
		{
			switch (weaponType)
			{
				case WeaponType.Shield:
					 TryStartActAfterShield();
					break;
				case WeaponType.Sword: 
					if (! ShieldStrikeIsDone ) Attack(attackablePlayer, WeaponType.BigEnemyWeapon);
					break;
			}

		}

		private void TryStartActAfterShield()
		{
			if (!ShieldStrikeIsDone)
			{
				//≈ще удар
				ShieldStrikeIsDone = true;
				startTime = Time.time;
				ShowDamageEffect();
				if (slowMotionEffect != null) 
					slowMotionEffect.StartAndFinishLastAction(slowMotionEffect.slowMotionTime);
			}
			else
			{
				//удар по игроку
				AttackEffect();
			}
		}
		protected override void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player" && live)
			{
				if (Controller.GetInstance().IsInSwordAttackState)
				{
					if (ShieldStrikeIsDone)
					{
						Die();
					}
					else
					{
						Attack(attackablePlayer, WeaponType.BigEnemyWeapon);
					}
				}
				else if (Controller.GetInstance().IsInShieldAttackState && !ShieldStrikeIsDone)
				{
					TryStartActAfterShield();
				}
				else
				{
					Attack(attackablePlayer, WeaponType.BigEnemyWeapon);
				}
			}
		}
	 /*   protected override void OnTriggerStay( Collider other )
		{
			HandleHitWithPlayer(other);
		}
		*/

		private float CalculateMaximumAtCurve(AnimationCurve curve)
		{
			float max = 0;
			for (float i = 0; i<=1; i+=0.05f) if (max < curve.Evaluate(i)) max = curve.Evaluate(i);
			return max;
		}

		protected virtual void ShowDamageEffect()
		{
			if (DamageAnimation != null)
				animation.Play(DamageAnimation.name);
		}

		
	}
}
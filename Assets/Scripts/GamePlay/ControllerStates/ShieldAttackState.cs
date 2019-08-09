using UnityEngine;
using System.Collections;
using LevelGeneration;

namespace GamePlay
{   
	internal class ShieldAttackState : AttackState
	{	  
		 public ShieldAttackState( Controller controller, float maxTime ) : base(controller, maxTime) { }

		 public override void PlayAnimation()
		 {
			 if ( controller.Animations.ShieldAttack != null )
			 {
				 controller.ApplyAnimation(controller.Animations.ShieldAttack, controller.CrossfadeTimes.Attack);
			 }
		 }

		 protected override void HandeleCollide( EnemyAbstract obj, ControllerColliderHit hit )
		 {
				 obj.DeactivateHelathDecrementers();
				 controller.Attack(obj, WeaponType.Shield);
		 }
		protected override void HandeleCollide(DestroyableObstcle obj)
		{
			obj.Destroy(); 
		}

	}

}

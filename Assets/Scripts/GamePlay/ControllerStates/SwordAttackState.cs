using UnityEngine;
using System.Collections;
using LevelGeneration;

namespace GamePlay
{
	internal class SwordAttackState : AttackState
	{
		public SwordAttackState( Controller controller, float maxTime ) : base(controller, maxTime) { }

		public override void PlayAnimation()
		{
			if ( controller.Animations.SwordAttack != null )
			{
				controller.ApplyAnimation(controller.Animations.SwordAttack, controller.CrossfadeTimes.Attack);
			}
		}

		protected override void HandeleCollide( EnemyAbstract obj, ControllerColliderHit hit )
		{
				obj.DeactivateHelathDecrementers();
				controller.Attack(obj, WeaponType.Sword); 
			
		}
		protected override void HandeleCollide( DestroyableObstcle obj )
		{
			obj.Destroy();
		}

	}
}

using System.Collections.Generic;
using GamePlay;
using Gameplay;
using LevelGeneration;
using UnityEngine;

namespace GamePlay
{
	public partial class Controller : MonoBehaviour, IControllerForCamera, IAttackable
	{

		
		
		
		
		public void PrepareToReplay()
		{
			
			if (WrenchInHand != null) WrenchInHand.enabled = false;
			if (ShieldIn != null) ShieldIn.enabled = false;
			if (ShieldOut != null) ShieldOut.enabled = false;

			CanJump = true;
			timePeriodBonuses = new List<TimePeriodBonus>();
			SavedStates = new StatesQueue(1, 1f);

			life = new Life();
			characterController.center = colliderRelSaved;

			SetState(RuningState);
		}

		public void MoveToPosition(Vector3 pos)
		{
			transform.position = pos;
		}
	   
		

		public void EventLifeChanged(Life life)
		{
			if (LifeChanged != null)
				LifeChanged(life);
		}



		

		public void Attack(IAttackable target, WeaponType weaponType)
		{
			target.ReciveDamage(weaponType);
		}

		public void ReciveDamage(WeaponType weaponType)
		{
			switch (weaponType)
			{
					case WeaponType.BigEnemyWeapon:
					case WeaponType.ButterflyEnemyWeapon:
					case WeaponType.MiddleEnemyWeapon:
					Die(DeadReason.EnemyForward);
					life.Die();
					break;

			}
		}

		public void Die()
		{
			Die(DeadReason.EnemyForward);
			life.Die();
		}
	}

}
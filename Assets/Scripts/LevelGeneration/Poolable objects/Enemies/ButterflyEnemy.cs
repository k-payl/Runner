using UnityEngine;
using GamePlay;
using System.Collections;
namespace LevelGeneration
{
    public class ButterflyEnemy : EnemyAbstract
    {

        public override void Attack( IAttackable target, WeaponType weaponType )
        {
            AttackEffect();
            target.ReciveDamage(weaponType);
        }

        public override void ReciveDamage( WeaponType weaponType )
        {
            switch ( weaponType )
            {
                case WeaponType.Shield: Die();
                    break;
                case WeaponType.Sword: Die();
                    break;
            }
        }


    }
}

using UnityEngine;
using GamePlay;
using System.Collections;
 namespace LevelGeneration{

     public class MiddleEnemy : EnemyAbstract
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
                 case WeaponType.Shield: Attack(attackablePlayer, WeaponType.MiddleEnemyWeapon); 
                     break;
                 case WeaponType.Sword: Die();
                     break;
             }
         } 
     }
}

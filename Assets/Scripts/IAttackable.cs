using UnityEngine;
using System.Collections;

namespace GamePlay
{  

    public interface IAttackable
    {
        void Attack( IAttackable target, WeaponType weaponType );
        void ReciveDamage( WeaponType weaponType );
        void Die();
    }

    public enum WeaponType
    {
        Sword = 0,
        Shield = 1,

        MiddleEnemyWeapon = 3,
        BigEnemyWeapon = 4,
        ButterflyEnemyWeapon = 5

    }

}
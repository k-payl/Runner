using GamePlay;

namespace GamePlay
{
    /// <summary>
    /// ƒелегирована жизнь игрока
    /// </summary>
    public class Life  {

        public int maxHP = 3;
        public int HP; //TODO do private
    

        public Life()
        {
            RestoreHP();
        }
	
	
        public void DecreaseHP()
        {
            HP--;
            if (HP<=0) Controller.GetInstance().Die(DeadReason.HP);
            Controller.GetInstance().EventLifeChanged(this);
        }

        /// <summary>
        /// ”величить жизнь
        /// </summary>
        /// <returns>true - если прибавление не вышло за границы\n
        /// false - если прибавл€ем больше чем нужно</returns>
        public bool IncreaseHP(int hp)
        {
            if (HP == maxHP) return false;
            HP += hp;
            bool f;
            if (HP > maxHP)
            {
                f = false;
                HP = maxHP;
            }
            else
            {
                f = true;
            }
            Controller.GetInstance().EventLifeChanged(this);
            return f;


        }

        /// <summary>
        /// ¬осстановить здоровье
        /// </summary>
        public void RestoreHP()
        {
            if (HP != maxHP)
            {
                HP = maxHP;
                Controller.GetInstance().EventLifeChanged(this);
            }
        }

        public void Die()
        {
            if (HP != 0)
            {
                HP = 0;
                Controller.GetInstance().EventLifeChanged(this);
            }
        }
    }
}

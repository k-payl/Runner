using GamePlay;

namespace LevelGeneration
{
    public class Coin : OnceTimeBonus
    {
        public int Score = 1;

        void Start()
        {
            collider.isTrigger = true;
        }

    }
}
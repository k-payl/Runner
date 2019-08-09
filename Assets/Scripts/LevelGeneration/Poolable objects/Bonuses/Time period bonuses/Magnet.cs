using Sound;
using UnityEngine;
using GamePlay;

namespace LevelGeneration
{

    public class Magnet : TimePeriodBonus
    {
        public float influenceRadius;
        public float affectForce;

        public override void Affect()
        {
            Vector3 center = controller.transform.position;
            Vector3 directionOfForce;

            Collider[] influenceColliders = Physics.OverlapSphere(center, influenceRadius);

            foreach ( Collider influenceCollider in influenceColliders )
            {
                if ( influenceCollider.GetComponent<Coin>() != null )
                {
                    directionOfForce = (center  - influenceCollider.transform.position+Vector3.forward*0.5f).normalized;
                    //float force =1f;
                    //force= influenceRadius/(center - influenceCollider.transform.position).magnitude;
                    influenceCollider.transform.Translate(directionOfForce* affectForce * Time.deltaTime,Space.World);


                    //Debug.DrawRay(influenceCollider.transform.position, directionOfForce, Color.green, 0.5f);
                }
            }

        }

        protected override void LastEffect()
        {
        }

        protected override void FirstEffect()
        {
        }
    }
}

using GamePlay;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace LevelGeneration
{  
    [RequireComponent(typeof(Animation))]
    public abstract class EnemyAbstract : AbstractPoolableObject, IAttackable
    {
       
        public AnimationClip IdleAnimation;
        public AnimationClip DieAnimation;
        public AnimationClip AttackAnimation;
        public AudioClip ambientSound;
        public AudioClip attackSound;
        public AudioClip deathSound;

        [SerializeField, HideInInspector] protected bool live;
        protected IAttackable attackablePlayer;
        private float timeOfDeath;
        private AudioSource audioSource;

        //------------IAttackble-------------
        public abstract void Attack( IAttackable target, WeaponType weaponType ); 
        
        public abstract void ReciveDamage( WeaponType weaponType );
        
        public void Die()
        {
            live = false;
            DieEffect();
        }
        //-----------------------------------



        protected virtual void Start()
        {
            live = true;
            if ( IdleAnimation )
                animation.Play(IdleAnimation.name);
            attackablePlayer = controller;
            audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.loop = true;
                audioSource.clip = ambientSound;
                audioSource.Play();
            }
        }

        protected virtual void Update(){}

        protected virtual void AttackEffect()
        {
            if ( AttackAnimation )
                animation.Play(AttackAnimation.name);
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.clip = attackSound;
                audioSource.Play();
            }
        }
        protected virtual void DieEffect()
        {
            if ( DieAnimation )
                animation.Play(DieAnimation.name);
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.clip = deathSound;
                audioSource.Play();
            }
        }

        public override void ResetState()
        {
            base.ResetState();
            live = true;
            ActivateHelathDecrementers();
        }

        protected override void OnTriggerEnter( Collider other ) { }

        protected override void OnTriggerStay(Collider other){}

        public virtual void ActivateHelathDecrementers()
        {
            HealthDecrementer[] decs = GetComponentsInChildren<HealthDecrementer>();
            if (decs != null)
            {
                foreach (HealthDecrementer healthDecrementer in decs)
                {
                    healthDecrementer.active = true;
                }
            }
        }

        public virtual void DeactivateHelathDecrementers()
        {
            HealthDecrementer[] decs = GetComponentsInChildren<HealthDecrementer>();
            if (decs != null)
            {
                foreach (HealthDecrementer healthDecrementer in decs)
                {
                    healthDecrementer.active = false;
                }
            }
        }

        public override bool Equals(AbstractPoolableObject other)
        {
            return ((other is BigEnemy) && (this is BigEnemy)) || 
                    ((other is MiddleEnemy) && (this is MiddleEnemy)) ||
                   ((other is ButterflyEnemy) && (this is ButterflyEnemy));
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            DrawGizmo(Color.red);
        }
#endif
    }
}
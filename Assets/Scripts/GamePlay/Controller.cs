using Gameplay;
using LevelGeneration;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using Sound;

namespace GamePlay
{

    #region Event delegates
    public delegate void BonusHandler(Bonus bonus, BonusCollection collection);

    public delegate void BonusCollectionHandler(BonusCollection collection);

    public delegate void LifeChanged(Life life);
    #endregion

    #region Times
    [System.Serializable]       
	public class CrossfadeTimes
	{
		public float Run = 0.3f;
		public float Turning = 0.3f;
		public float JumpingUp = 0.3f;
		public float JumpingDown = 0.3f;
	    public float TurnRun = 0.4f;
		public float OnRope = 0.3f;
		public float Stramed = 0.3f;
		public float TurningStramed = 0.3f;
		public float Dead = 0.3f;
		public float Attack = 0.3f;
        public float AttackRun = 0.3f;
        public float DangerZoneDeath = 0.2f;
	}
	#endregion

	#region ControllerParams
	[System.Serializable]        
	public class ControllerParams
	{
		public float RunSpeed;
		public float RunAnimationSpeed = 1f;//TODO убрать перед билдом
		public float JumpHeight;
		[HideInInspector] public float JumpVerticalSpeed;
		public float JumpUpAnimationSpeed;//TODO убрать перед билдом
		public float JumpDownAnimationSpeed;//TODO убрать перед билдом
		public float TurningSpeed;
        public float RopeRollingSpeed;
		
		public AnimationCurve TurningCurve;
		public float TurningAnimationSpeed;//TODO убрать перед билдом
		public AnimationCurve AttackAcceleration;
		public float ShieldAttackTime=1f;
		public float SwordAttackTime = 1f;
		public float CanAttakIfHigherThenValue = 0.5f;
		public float JumpingAcceleration;
		public float StameredTime = 0.1f;
		public float Gravity = 9.81f;

        public AnimationCurve DangerZoneForwardSpeed;
        public float DangerZoneRunDelay;
		public float BounceCoef = 0.5f;
		public byte BounceCount = 3;
		public float HorizontBounceFrictionCoef = 0.5f;
	}
	#endregion

	#region Animations
	[System.Serializable]
	public class AnimationsCollection
	{
		public AnimationClip Walk;
		public AnimationClip Run;

		public AnimationClip[] JumpUp;
		public AnimationClip[] JumpDown;

		public AnimationClip TurnToLeft;
		public AnimationClip TurnToRight;
        public AnimationClip TurnToLeft1;
        public AnimationClip TurnToRight1;

		public AnimationClip Stammered;
		public AnimationClip LeftDiasllowedTurn;
		public AnimationClip RightDiasllowedTurn;

		public AnimationClip FallingBack;
	    public AnimationClip FallingForward;
		public AnimationClip GroundHeadUpDeath;
	    public AnimationClip GroundHeadDownDeath;
		public AnimationClip ElectroDead;
		public AnimationClip BataryLowDead;
		public AnimationClip DangerZoneDead;

		public AnimationClip SwordAttack;
		public AnimationClip ShieldAttack;
	    public AnimationClip StartAnimation;
	    public AnimationClip FinishAnimation;
	}
	#endregion


    /// <summary>
    /// Отвечает за состояние игрока.
    /// Паттерн состояния
    /// </summary>
	[RequireComponent(typeof(CharacterController))]
	public partial class Controller : MonoBehaviour, IControllerForCamera, IAttackable
	{
		public AnimationsCollection Animations;
		public ControllerParams ControllerParams;
		public CrossfadeTimes CrossfadeTimes;
	    public Renderer WrenchInHand;
		public Renderer WrenchOnBelt;
	    public Renderer ShieldIn;
	    public Renderer ShieldOut;
	    public static StatesQueue SavedStates;
        public bool IsStamered { get { return CalcStameredState(); } }
        public bool IsOnRope { get { return (currentState is RopeRollingState); } }
        public bool IsInShieldAttackState { get { return (currentState is ShieldAttackState); } }
        public bool IsInSwordAttackState { get { return (currentState is SwordAttackState); } }
        public bool CanJump;
        public List<TimePeriodBonus> timePeriodBonuses;
	    public PlayerSoundHandler soundEffects;
	    public FlareControl footFlares;
	    public float MaxFlireBrightness;
	    public float timeFlareAction;
        public float timeFlaresCrossfade;
        public Life life; //делегировано все что отвечает за жизнь

        public event LifeChanged LifeChanged;

		private IControllerState currentState;
		private JumpingState jumpingState;
		private RuningState runingState;
		private TurningState turningState;
		private DeadState deadState;
		private IControllerState stramedState;
		private DisallowedTurningState disallowedTurnState;
		private ShieldAttackState shieldAttackState;
		private SwordAttackState swordAttackState;
		private RopeRollingState ropeRollingState;

		internal ShieldAttackState ShieldAttackState        { get { return shieldAttackState; } set { value = shieldAttackState; }}
		internal SwordAttackState SwordAttackState          { get { return swordAttackState; } set { value = swordAttackState; }}
        internal TurningState TurningState                  { get { return turningState; } set { value = turningState; } }
        internal JumpingState JumpingState                  { get { return jumpingState; } set { value = JumpingState; } }
        internal DeadState DeadState                        { get { return deadState; } set { deadState = value; } }
        internal RuningState RuningState                    { get { return runingState; }}
        internal IControllerState StramedState              { get { return stramedState; }}
        internal DisallowedTurningState DisallowedTurnState { get { return disallowedTurnState; }}
        internal RopeRollingState RopeRollingState          { get { return ropeRollingState; }}

		private Animation _animation;
		private Transform _transform;
		private CharacterController characterController;
        private Vector3 colliderRelSaved; 
		[HideInInspector] public float ControllerColliderDeadHeight;
		private Vector3 move;   
		private TrackAbstract track;
		private static Controller instance;
		public static Controller GetInstance()
		{
			return instance ?? (instance = FindObjectOfType(typeof(Controller)) as Controller);
		}

        #region Unity Callbacks
        
        private void Start()
		{
			_animation = GetComponent<Animation>();
		    if (!_animation)
		        Debug.Log(
		            "The character you would like to control doesn't have animations. Moving her might look weird.");
		    else
		    {
		        _animation[Animations.Run.name].speed = ControllerParams.RunAnimationSpeed;
		        _animation[Animations.TurnToRight.name].speed = ControllerParams.TurningAnimationSpeed;
		        _animation[Animations.TurnToLeft.name].speed = ControllerParams.TurningAnimationSpeed;
		        for (int i = 0; i < Animations.JumpDown.Length; i++)
		            _animation[Animations.JumpDown[i].name].speed = ControllerParams.JumpDownAnimationSpeed;
		        for (int i = 0; i < Animations.JumpDown.Length; i++)
		            _animation[Animations.JumpUp[i].name].speed = ControllerParams.JumpUpAnimationSpeed;
		    }
		    track = TrackAbstract.GetInstance();
			if(!track)
			{
				track = null;
				Debug.Log("No Track found. Character can`t move without Track");
			}
			_transform = transform;
            characterController = GetComponent<CharacterController>();
			instance = this;

			ControllerParams.JumpVerticalSpeed = Mathf.Sqrt(2*ControllerParams.JumpHeight*ControllerParams.Gravity);
            ControllerColliderDeadHeight = characterController.center.y + characterController.height * 0.5f;

			runingState = new RuningState(this);
		    turningState = new TurningState(this);
			jumpingState = new JumpingState(this);
			disallowedTurnState = new DisallowedTurningState(this);
			stramedState = new StameredState(this);
			deadState = new DeadState(this);
			shieldAttackState = new ShieldAttackState(this, ControllerParams.ShieldAttackTime);
			swordAttackState = new SwordAttackState(this, ControllerParams.SwordAttackTime);                                                                             
			ropeRollingState = new RopeRollingState(this);
            colliderRelSaved = characterController.center;
            PrepareToReplay();
            
		    if (footFlares)
		    {
		        footFlares.maxBrightness = MaxFlireBrightness;
		        footFlares.TurnOff();
		    }
            
            life = new Life();
		}

        void OnDisable()
        {
            instance = null;
        }

        void OnDestory()
        {
            foreach (var periodBonuse in timePeriodBonuses)
            {
                GameManager.Instance.BonusMissedEvent(periodBonuse, GameManager.Instance.info.bonuses);
            }
        }

		private void Update()
        {

			move = currentState.UpdatePosition()*Time.deltaTime;
			characterController.Move(move);


            List<TimePeriodBonus> forDeleteList = new List<TimePeriodBonus>();
            foreach ( TimePeriodBonus bonus in timePeriodBonuses )
            {
                if ( !bonus.TimeIsFinished() )
                    bonus.Affect();
                else
                    forDeleteList.Add(bonus);
            }

            foreach ( TimePeriodBonus bonus in forDeleteList )
            {
                //событие
                GameManager.Instance.BonusMissedEvent(bonus, GameManager.Instance.info.bonuses);

                timePeriodBonuses.Remove(bonus);
                bonus.ResetState();
            }

		}


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.gameObject.layer != gameObject.layer)
            {
                currentState.HandleCollide(hit.collider, hit);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != gameObject.layer)
            {
                currentState.HandleCollide(other, null);
            }
        }
        #endregion

        public void Turn(TurnDirection direction) { currentState.Turn(direction); }
        public void Jump() { if (CanJump) currentState.Jump(); }
        public void JumpDown() { currentState.JumpDown(); }
        public void Attack(AttackType attackType) { currentState.Attack(attackType); }

        public void Die(DeadReason deadReason)
        { 
            life.Die();
            currentState.Dead(deadReason);
        }
       


		public void ApplyAnimation(AnimationClip clip, float crossfadeTime)
		{
		    if (clip != null)
		    {
		        _animation.CrossFade(clip.name, crossfadeTime);
		    }
		}

        //TODO удалить если не будет использована
        //public void ApplyAnimationQueued(AnimationClip clip, float crossfadeTime)
        //{
        //    if (clip != null)
        //    {
        //        _animation.CrossFadeQueued(clip.name, crossfadeTime);
        //    }
        //}

		public bool IsOnGround()
		{
			return currentState.IsGrounded();
		}

	    public Transform GetTransform()
	    {
	        return transform;
	    }


	    private bool CalcStameredState()
		{
                //boottom point is picked up under ground at dxHeight 
                float dxHeight = 0.1f;
				Vector3 bottomPoint = new Vector3(characterController.bounds.center.x,
												  characterController.bounds.min.y + dxHeight,
												  characterController.bounds.center.z);

				Vector3 middlePoint = new Vector3(characterController.bounds.center.x,
												  characterController.bounds.center.y - 0.2f,
												  characterController.bounds.center.z);
                float distance = 0.7f;//(characterController.bounds.max.x - characterController.bounds.min.x);

                //Debug.DrawLine(bottomPoint, bottomPoint + _transform.forward * distance,
                //               Color.yellow, duration);
                //Debug.DrawLine(middlePoint, middlePoint + _transform.forward * distance,
                //               Color.red, duration);
                //Debug.Log(string.Format("bottom: {0}, middle: {1}", Physics.Raycast(bottomPoint, _transform.forward, distance), Physics.Raycast(middlePoint, _transform.forward, distance)));

				return
					Physics.Raycast(bottomPoint, _transform.forward, distance,
									~(1 << gameObject.layer)) &&
					!Physics.Raycast(middlePoint, _transform.forward, distance,
									 ~(1 << gameObject.layer));
		}


		internal void SetState(IControllerState state)
		{
			currentState = state;
		}


        

    }

}
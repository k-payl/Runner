using UnityEngine;

namespace GamePlay
{

	public class FinishState : BaseState
	{
		public float timeOfBreaking = 2f;
		private float startTime;
		private float animationSavedSpeed;
		private float savedZSpeed;
		private Vector3 res;


		public FinishState(Controller controller) : base(controller)
		{
			startTime = Time.time;
			animationSavedSpeed = controller.ControllerParams.RunAnimationSpeed;
			res = base.UpdatePosition();
			savedZSpeed = res.z;
			//ihntGUI.Instance.HideIngameGUI();
			//ihntGUI.Instance.ShowEndLevelDialog("level 1");//TODO
			//GameManager.Instance.LevelCompleted();
		}

		public override Vector3 UpdatePosition()
		{
			
			//float s = 1f - (Time.time - startTime)/timeOfBreaking;
			res = Vector3.zero;

			

			//тормозит
			//if (s <= 1 && s >= 0)
			//{
			//	controller.ApplyAnimation(controller.Animations.Run, controller.CrossfadeTimes.Run);
			//	res = base.UpdatePosition();
			//	res.z = savedZSpeed*s;
			//	controller.ControllerParams.RunAnimationSpeed = animationSavedSpeed*s;
			//}
			//else
			//	//затормозил но не сделал что то последнее
			//	if (s < 0 && !finished)
			//	{
			//		finished = true;
			//		if (controller.Animations.FinishAnimation)
			//		{
			//			controller.ApplyAnimation(controller.Animations.FinishAnimation, 0.3f);
			//		}
			//		else
			//		{
			//			controller.animation.Stop();
			//		}
			//	}
			return res;
		}

		public override void Attack(AttackType attackType)
		{
		}

		public override void Jump()
		{
		}

		public override void JumpDown()
		{
		}

		public override void Turn(TurnDirection direction)
		{
		}
	}
}

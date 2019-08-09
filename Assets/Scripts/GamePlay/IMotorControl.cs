using UnityEngine;

namespace GamePlay
{
	public interface IMotorControl
	{

		
		
		
		
		void TurnSmoothly(float value);

		void Jump(JumpDrection jumpDrection);

		
		
		
		void Accelerate(float value);

	}

	public enum JumpDrection
	{
		Up = 0,
		Down=1
	}
}

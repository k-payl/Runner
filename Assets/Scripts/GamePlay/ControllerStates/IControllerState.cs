using LevelGeneration;
using UnityEngine;

namespace GamePlay
{
	internal interface IControllerState
	{  
		/// <summary> </summary>
		/// <returns>
		/// ���������� ��������, �� ������� ����� �������� transform � ������� �����. 
		/// �� �������� �� Time.deltaTime
		/// </returns>
		Vector3 UpdatePosition();
		void Jump();
		void JumpDown();
		void Turn(TurnDirection direction);
		void Run();
		void Dead(DeadReason deadReason);
		void Attack(AttackType attackType);
		bool IsGrounded();
		void Stamered();
		void HandleCollide(Collider collider, ControllerColliderHit hit);
	}
}
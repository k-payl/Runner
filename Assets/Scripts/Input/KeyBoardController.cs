using UnityEngine;
using System.Collections.Generic;
using GamePlay;
public class KeyBoardController : MonoBehaviour
{
	public Controller Player;
	void Start ()
	{
		Player = Controller.GetInstance();
	}
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftApple))
			Player.Turn(TurnDirection.Left);

		if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightApple))
			Player.Turn(TurnDirection.Right);

		if (Input.GetKeyDown(KeyCode.Space))
			Player.Jump(); 

		if (Input.GetKeyDown(KeyCode.S))
			Player.JumpDown();

		if (Input.GetKeyDown(KeyCode.LeftControl))
			Player.Attack(AttackType.Sword);

		if ( Input.GetKeyDown(KeyCode.LeftShift) )
			Player.Attack(AttackType.Shield);
	}
}
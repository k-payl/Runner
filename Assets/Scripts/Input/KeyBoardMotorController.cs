using GamePlay;
using UnityEngine;
using System.Collections;

public class KeyBoardMotorController : MonoBehaviour
{

    private IMotorControl controller;
	void Start ()
	{
	    controller = MotorController.GetInstance();
	}
	
	void Update () {

	    if (Input.GetKeyDown(KeyCode.Space))
            controller.Jump(JumpDrection.Up);

         if (Input.GetKeyDown(KeyCode.S))
             controller.Jump(JumpDrection.Down);

        controller.TurnSmoothly(Input.GetAxis("Horizontal"));
	}
}

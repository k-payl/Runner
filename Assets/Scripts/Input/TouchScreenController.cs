using GamePlay;
using UnityEngine;
using System.Collections;

public class TouchScreenController : MonoBehaviour {

    private Controller Player;
    void Start()
    {
        Player = Controller.GetInstance();
    }
    // Subscribe to events
    void OnEnable()
    {
        EasyTouch.On_Swipe += OnSwipeHandler;
        EasyTouch.On_SimpleTap += OnSimpleTapHandler;
    }
     
    void OnDisable()
    {
        UnsubscribeEvent();  
    }

    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UnsubscribeEvent()
    {
        EasyTouch.On_Swipe -= OnSwipeHandler;
        EasyTouch.On_SimpleTap -= OnSimpleTapHandler;
    }
    private void OnSwipeHandler( Gesture gesture )
    {  
         switch (gesture.swipe)
        {
            case EasyTouch.SwipeType.Left: Player.Turn(TurnDirection.Left); break;
            case EasyTouch.SwipeType.Right: Player.Turn(TurnDirection.Right); break;
            case EasyTouch.SwipeType.Down: Player.JumpDown(); break;
            case EasyTouch.SwipeType.Up: Player.Jump(); break;
        }
    }

    private void OnSimpleTapHandler(Gesture gesture)
    {
        if (gesture.touchCount == 1)
        {
            float ScreenMaxX = Screen.width;
            if (gesture.position.x < (int) ScreenMaxX/2)
            {
                Player.Attack(AttackType.Sword);
            }
            else
            {
                Player.Attack(AttackType.Shield);
            }
        }

    }
}

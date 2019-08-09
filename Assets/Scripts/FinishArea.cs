using GamePlay;
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FinishArea : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var color = Color.yellow;
        color.a = 0.1f;
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, collider.bounds.size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, collider.bounds.size);
    }
#endif

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ihntGUI.Instance.HideIngameGUI();
            ihntGUI.Instance.ShowEndLevelDialog("level 1");
            GameManager.Instance.LevelFinished(true);
        }
    }

}

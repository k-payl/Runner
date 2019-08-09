using System;
using System.Linq;
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class BlockDecorator : MonoBehaviour
{

    public static float ZSize = 70;
    public float cornerLenght;
    private Vector3 start, endX, endY, endZ;

	void OnDrawGizmos()
    {
        Vector3 center = transform.position;
        float lenghtX = 70f;
        float lenghtY = 70f;
        float lenghtZ = ZSize;
      for(int i = 0; i<=8; i++)
      {
          //if ( i == 2 || i == 3 || i == 6 || i==7)
          {
              start.x = center.x + lenghtX * Mathf.Pow(-1f, i);
              start.y = center.y + lenghtY * Mathf.Pow(-1f, i / 2);
              start.z = center.z + lenghtZ * Mathf.Pow(-1f, i / 4);

              endX = start + cornerLenght * Vector3.left * Mathf.Pow(-1f, i);
              endY = start + cornerLenght * Vector3.down * Mathf.Pow(-1f, i / 2);
              endZ = start + cornerLenght * Vector3.back * Mathf.Pow(-1f, i / 4);
              Color c = Color.gray;
              c.a = 0.5f;
              Gizmos.color = c;
              Gizmos.DrawLine(start, endX);
              Gizmos.DrawLine(start, endY);
              Gizmos.DrawLine(start, endZ);
          }
          
      }

      //  int STartInd = -1;
      //  for (int j = gameObject.name.Length - 1; j > 0; j--)
      //  {
      //      if (Char.IsDigit(gameObject.name[j]) && !Char.IsDigit(gameObject.name[j-1]))
      //      {
      //          STartInd = j;
      //          break;
      //      }
      //  }
            
      //if (STartInd!=-1) Gizmos.DrawIcon(center + Vector3.right * ZSize, "labels\\" + gameObject.name.Substring(STartInd, 2) + ".png");
    }
}

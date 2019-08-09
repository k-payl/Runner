using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
	[RequireComponent(typeof(BoxCollider)),ExecuteInEditMode, System.Serializable]
	public class Rope : AbstractPoolableObject
	{
		private float tension;
		[SerializeField] private float amplitude;  
		public LineRenderer line;
		[SerializeField] private Vector3 centerCircle;
		[SerializeField] private float lenght;
		[SerializeField] private float radius;
		private Vector3 vecForPlayer;

	   
		public float Lenght
		{
			get { return lenght; }
			set { 
					lenght = value; 
					ReCalculCentreAndRadius();
					SetDefaultStateRope();
			}
		}

		 
		private void ReCalculCentreAndRadius()
		{
			radius = amplitude / 2 + Lenght * Lenght / (8 * amplitude);
			float height = Mathf.Sqrt(radius * radius - (Lenght / 2) * (Lenght / 2));
			centerCircle = transform.position + Vector3.up * height;
		}
		 
		
		
 
		void Start()
		{
			amplitude = 0.7f;		  
			line = GetComponent<LineRenderer>();
			SetDefaultStateRope();
			ReCalculCentreAndRadius();
			tension = 0;
		}

		void OnEnable()
		{
			if (line == null) GetComponent<LineRenderer>();
		}
		

		public float CalculateYByZ( float z )
		{
			if (z < (transform.position.z - Lenght / 2))
			{
				 return transform.position.y;
			}
			else if (z > (transform.position.z + Lenght / 2)) 
			{
				return transform.position.y;
			}
			else
			{
				return 
					centerCircle.y - Mathf.Sqrt(radius * radius - (z - centerCircle.z) * (z - centerCircle.z));
			}
		}
		

		public float MaxZcoord() { return transform.position.z + Lenght / 2f; }
		public float MinZcoord() { return transform.position.z - Lenght / 2f; }

		public void SetDefaultStateRope(){
			if (line == null)
			{
				line = GetComponent<LineRenderer>();
			}

			if (transform.parent != null)
				{
					transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,  Lenght/ transform.parent.localScale.z);  
				}
				else
				{
					transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Lenght);
				}
				
				line.SetPosition(0, transform.position + Vector3.back * Lenght / 2f);
				line.SetPosition(1, transform.position + Vector3.down * amplitude);
				line.SetPosition(2, transform.position + Vector3.forward * Lenght / 2f);
		}
		
		
		public void TrySetKnotPosition(Vector3 pos)
		{
			Vector3 knotPos;
			if ((pos - (new Vector3(pos.x, CalculateYByZ(pos.z), pos.z))).magnitude <= tension)
			{
				knotPos.x =  transform.position.x;
				knotPos.y = CalculateYByZ(pos.z);
				knotPos.z = pos.z;
				line.SetPosition(1, knotPos);  
			}

		}

			
			
			
			
			
			
			
			
			
			
			

#if UNITY_EDITOR
		void OnDrawGizmos()
		{
			DrawGizmo(Color.white, 0.2f);
			for (float z = transform.position.z - Lenght / 2; z < transform.position.z + Lenght / 2; z += 0.2f)
			{
				Vector3 point = new Vector3(transform.position.x, CalculateYByZ(z), z);
				Debug.DrawLine(point, point + Vector3.forward * 0.1f, Color.red, 10f);
			}
		}
#endif
	}


   
}

using UnityEngine;

namespace LevelGeneration
{
	[RequireComponent(typeof(BoxCollider))]
	public class DestroyableObstcle : Obstacle
	{
		public float exploseValue;
		public ForceMode ForceMode;
		public bool useTorque;
		public float exploseRadius;
		public float zFacorOfForce;
		public Rigidbody[] Pieces; 
		private Vector3[] piceDefoltPositions;
		private Quaternion[] piceDefoltRotation;

	   
		[ContextMenu("Awake")]
		protected override void Awake()
		{
			base.Awake();
			renderer.enabled = true;
			piceDefoltPositions = new Vector3[Pieces.Length];
			piceDefoltRotation = new Quaternion[Pieces.Length];
			for(int i = 0; i < Pieces.Length; i++)
			{
				piceDefoltPositions[i] = Pieces[i].transform.localPosition;
				piceDefoltRotation[i] = Pieces[i].transform.localRotation;
				Pieces[i].Sleep();
				Pieces[i].renderer.enabled = false;
			}
			collider.isTrigger = true;
		}
		[ContextMenu("ResetState")]
		public override void ResetState()
		{
			base.ResetState();
			for(int i = 0; i < Pieces.Length; i++)
			{
				Pieces[i].transform.localPosition = piceDefoltPositions[i];
				Pieces[i].transform.localRotation = piceDefoltRotation[i];
				Pieces[i].Sleep();
				Pieces[i].renderer.enabled = false;
			}
			renderer.enabled = true;
			collider.enabled = true;
		}
		[ContextMenu("Explosion")]
		public void Destroy()
		{
			
			renderer.enabled = false;
			collider.enabled = false;
			for(int i = 0; i < Pieces.Length; i++)
			{
				Pieces[i].renderer.enabled = true;
				Pieces[i].AddExplosionForce(exploseValue,transform.position +Vector3.back*zFacorOfForce,exploseRadius,1f,ForceMode);
				if (useTorque) Pieces[i].AddRelativeTorque(new Vector3(Random.Range(-3f,3f), Random.Range(-2f,2f), Random.Range(-10f,10f)), ForceMode);
				
			}

		}

		
		
		
		


	 
	}
}

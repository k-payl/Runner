using UnityEngine;
using System.Collections;
using GamePlay;

namespace LevelGeneration
{
   
	public class PlaceForDangerZone : PlaceForPolable
	{

		protected override void Awake()
		{
			base.Awake();
		//	collider.isTrigger = true;
		}
		void OnDrawGizmos()
		{
			DrawGizmo(Color.Lerp(Color.red, Color.yellow, 0.5f));
		}

		

		
	}
}
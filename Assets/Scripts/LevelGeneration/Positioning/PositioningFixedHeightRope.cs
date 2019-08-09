using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
	[ExecuteInEditMode]
	public class PositioningFixedHeightRope : Positioning
	{
		protected float Height;
#if UNITY_EDITOR
		public override void Update()
		{
			if (!Application.isPlaying)
			{
				base.Update();
			}
		}
#endif
		public void BindToFloor()
		{
			transform.position = new Vector3(transform.position.x, Height, transform.position.z);	 
		}
	   
	}
}
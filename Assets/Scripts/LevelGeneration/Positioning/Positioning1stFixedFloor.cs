using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
	[ExecuteInEditMode]
	public class Positioning1stFixedFloor : PositioningFixedFloor
	{
		 public void Awake()
			 {
			Floor =1;
			}
#if UNITY_EDITOR
		public override void Update()
		{
			if (!Application.isPlaying)
			{
				base.Update();
				BindToFloor();
			}
		}
#endif
	}
}

using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
	[ExecuteInEditMode]
	public class Positioning2ndFixedFloor : PositioningFixedFloor
	{

		public void Awake()
		{
			Floor = 2;
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

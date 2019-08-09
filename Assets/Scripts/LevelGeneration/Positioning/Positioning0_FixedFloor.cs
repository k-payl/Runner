using System.Runtime.Serialization.Formatters;
using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
	[ExecuteInEditMode]
	public class Positioning0_FixedFloor : PositioningFixedFloor
	{

		public void Awake()
		{
			Floor = 0;
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

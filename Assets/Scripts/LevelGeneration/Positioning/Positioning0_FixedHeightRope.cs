using UnityEngine;
using System.Collections;
namespace LevelGeneration
{
	public class Positioning0_FixedHeightRope : PositioningFixedHeightRope
	{
		void Awake()
		{
			Height = HeightInfos.HeightGroundFloorRope;
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

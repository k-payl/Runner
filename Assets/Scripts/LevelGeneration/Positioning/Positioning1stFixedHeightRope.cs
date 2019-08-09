using UnityEngine;
using System.Collections;
namespace LevelGeneration
{
	public class Positioning1stFixedHeightRope : PositioningFixedHeightRope
	{

		void Awake()
		{
			Height = HeightInfos.HeightFirstFloorRope;
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

using System;
using LevelGeneration;
using UnityEngine;
using System.Collections.Generic;
using GamePlay;

namespace LevelGeneration
{		
	public abstract class TimePeriodBonus : Bonus 
	{
		public float ActiveTime;
		private float startTime;
		private bool isCollected;

		public override void Collect()
		{
			base.Collect();
			isCollected = true;
			startTime = Time.time;
			FirstEffect();
			
		}

		public override void ResetState()
		{
			if (TimeIsFinished())
			{
				isCollected = false;
				LastEffect();
				base.ResetState();
				
			}
		}


		public bool TimeIsFinished()
		{
			if ( isCollected )
			{
				return (Time.time - startTime) > ActiveTime;
			}
			return true;
		}

		
		
		
		public abstract void Affect();
		
		
		
		
		
		protected abstract void LastEffect();

		
		
		
		protected abstract void FirstEffect();

	}
}
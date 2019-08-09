using System;
using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;
using System.Collections;

namespace GamePlay
{
	[ExecuteInEditMode, Serializable]
	public class BonusCollection
	{
		public int score; 
		public bool hasHalfBattery; 
		
		public bool hasMemoryCard; 
		public bool hasCoinMultiplier; 
		[SerializeField]private float batareyCharge;

		public float BatareyCharge
		{
			get { return batareyCharge; }
		   set
			{

				if (value + batareyCharge > 1)
					batareyCharge = 1;
				else
				{
					if (batareyCharge + value <= 0)
					{
						if (!hasHalfBattery)
						{
							Controller.GetInstance().CanJump = false;
						}
						else
						{
							batareyCharge += (0.5f + value);
							hasHalfBattery = false;
							Controller.GetInstance().CanJump = true;
						}
					}
					else
						batareyCharge += value;
				}
			}
		}

		
		
		
		
		public BonusCollection()
		{
			score = 0;
			batareyCharge = 1f;
			hasHalfBattery = false;
			hasCoinMultiplier = false;
		}

		public void Load()
		{
			batareyCharge = 1f;
			hasCoinMultiplier = false;
			hasHalfBattery = false;
		}

		public void Save()
		{
			
		}


		public void LevelFinished(bool success)
		{
			hasHalfBattery = false;
			hasCoinMultiplier = false;
		}

		public void IncScore(int value)
		{
			if (hasCoinMultiplier)
			{
				score += value * 2;
				
			}
			else
			{
				score += value;
				
			}
		}

	}
}
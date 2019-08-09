using GamePlay;
using Sound;
using UnityEngine;
using System.Collections;

namespace LevelGeneration
{ 
	public class CoinMultiplier : TimePeriodBonus
	{
		public override void Affect()
		{
		}

		protected override void LastEffect()
		{
			GameManager.Instance.info.bonuses.hasCoinMultiplier = false;
		}

		protected override void FirstEffect()
		{
			GameManager.Instance.info.bonuses.hasCoinMultiplier = true;
		}
	}
}

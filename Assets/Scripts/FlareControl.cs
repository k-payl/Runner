using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TemporaryActions;

namespace GamePlay
{

	public class FlareControl : TemporaryCrossfadableAction
	{
		[HideInInspector]
		public float maxBrightness;
		private List<LensFlare> flares;
		private void Start()
		{
			flares = GetComponentsInChildren<LensFlare>().ToList();
			if (flares == null) flares = new List<LensFlare>();
		}

		public void TurnOn()
		{
			Fade(1);
		}

		public void TurnOff()
		{
			Fade(0);
		}


		protected override void Effect()
		{
		}

		protected override void FirstEffect()
		{
			TurnOn();
		}

		protected override void LastEffect()
		{
			TurnOff();
		}

		public override void Fade(float state)
		{
			foreach (LensFlare flare in flares)
			{
				flare.brightness = state * maxBrightness;
			}
		}
	}
}

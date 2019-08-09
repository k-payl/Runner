using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace TemporaryActions
{


	
	
	
	
	
	
	public abstract class TemporaryCrossfadableAction : TemporaryAction
	{
		
		
		
		private float crossfadeTime;

		
		
		
		
		
		
		public void StartAndStopLastCrossfadable(float time, float _crossfadeTime)
		{
			crossfadeTime = _crossfadeTime;
			StartAndFinishLastAction(time);
		}


		public abstract void Fade(float state);

		protected override IEnumerator Coroutine()
		{
			currentTime = 0;
			LastEffectIsDone = false;

			
			if (crossfadeTime > float.Epsilon)
				while (true)
				{
					float s = currentTime/crossfadeTime;
					Fade(s);
					currentTime += Time.deltaTime;
					if (currentTime > crossfadeTime)
					{
						
						Fade(1f);
						break;
					}
					yield return null;
				}
			currentTime = 0;

			FirstEffect();

			
			while (true)
			{
				currentTime += Time.deltaTime;
				if (currentTime > time) break;
				Effect();
				yield return null;
			}
			currentTime = 0;

			LastEffect();
			LastEffectIsDone = true;

			
			if (crossfadeTime > float.Epsilon)
				while (true)
				{
					float s = 1 - currentTime/crossfadeTime;
					Fade(s);
					currentTime += Time.deltaTime;
					if (currentTime > crossfadeTime)
					{
						Fade(0);
						break;
					}
					yield return null;
				}


		}
	}
}

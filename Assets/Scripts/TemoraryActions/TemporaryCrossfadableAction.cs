using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace TemporaryActions
{


	/// <summary>
	/// Может запускать что то на некоторое время с кроссфейдами
	/// Нужно унаследовать от этого класса 
	/// и реализовать Effect(), FirstEffect(), LastEffect(), Fade(float t)
	/// Это MonoBehaviour
	/// </summary>
	public abstract class TemporaryCrossfadableAction : TemporaryAction
	{
		/// <summary>
		/// время длительности двух(в начале и в конце) кроссфейдов
		/// </summary>
		private float crossfadeTime;

		/// <summary>
		/// Запустить действие с кроссфейдами в начале и в конце
		/// Таким образом, общее время = time + 2 * _crossafdeTime  
		/// </summary>
		/// <param name="time">время действия</param>
		/// <param name="_crossfadeTime">время кроссфейдов в начале и в конце</param>
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

			//начальный кроссфейд
			if (crossfadeTime > float.Epsilon)
				while (true)
				{
					float s = currentTime/crossfadeTime;
					Fade(s);
					currentTime += Time.deltaTime;
					if (currentTime > crossfadeTime)
					{
						//последний кроссфейд 
						Fade(1f);
						break;
					}
					yield return null;
				}
			currentTime = 0;

			FirstEffect();

			//сам прецесс
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

			//конечный кроссфейд
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

using System.Collections;
using UnityEngine;

namespace TemporaryActions
{

	
	
	
	
	/
	
	public abstract class TemporaryAction : MonoBehaviour
	{
		protected float time;
		protected float currentTime;
		protected bool LastEffectIsDone;


		public void StartAndFinishLastAction(float _time)
		{
			time = _time;
			StopCoroutine("Coroutine");
			if (!LastEffectIsDone)
				LastEffect();

			StartCoroutine("Coroutine");
		}

		protected virtual IEnumerator Coroutine()
		{
			currentTime = 0;
			LastEffectIsDone = false;
			FirstEffect();

			
			while (true)
			{
				Effect();
				currentTime += Time.fixedDeltaTime;
				if (currentTime > time) break;
				yield return new WaitForFixedUpdate();
			}

			LastEffect();
			LastEffectIsDone = true;

		}

		
		
		
		
		protected abstract void Effect();

		
		
		
		
		protected abstract void FirstEffect();

		
		
		
		
		protected abstract void LastEffect();

	}
}
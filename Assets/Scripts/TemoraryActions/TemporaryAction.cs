using System.Collections;
using UnityEngine;

namespace TemporaryActions
{

	/// <summary>
	/// Абстрактный класс позволяет запускать некое временное действие 
	/// Нужно унаследовать от этого класса и реализовать Effect(), FirstEffect(), LastEffect()
	/// <example>StartAndFinishLastAction(5)</example> 
	/// /// Это MonoBehaviour
	/// </summary>
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

			//сам прецесс
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

		/// <summary>
		/// Некое полезное действие
		/// Выполняется каждый кадр 
		/// </summary>
		protected abstract void Effect();

		/// <summary>
		/// Выполняется сразу после вызова <see cref="StartAndFinishLastAction()"/> один раз
		/// Всякую инициализацию писать здесь
		/// </summary>
		protected abstract void FirstEffect();

		/// <summary>
		/// Выполняется в самом конце действия или после прерывания
		/// Всякую очистку и сброс состояний писать здесь
		/// </summary>
		protected abstract void LastEffect();

	}
}
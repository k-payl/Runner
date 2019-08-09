using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelGeneration
{

	public interface IPoolable
	{
		bool IsUsedNow { get; set; }
		
		/// <summary>
		/// Должна сбрасывать соостояние объекта в некое промежуточное(не готовое к использованию). 
		/// Желательно что бы отключались все Update скрипты
		/// </summary>
		void ResetState();

		/// <summary>
		/// Старт всего всего в объекте. 
		/// После вызова должен быть готов к использованию
		/// </summary>
		void Init();

		GameObject GetGameObject { get; }
	}
}
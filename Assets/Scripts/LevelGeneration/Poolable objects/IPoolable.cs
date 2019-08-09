using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelGeneration
{

	public interface IPoolable
	{
		bool IsUsedNow { get; set; }
		
		
		
		
		
		void ResetState();

		
		
		
		
		void Init();

		GameObject GetGameObject { get; }
	}
}
using UnityEngine;
using LevelGeneration;
using System.Collections;

namespace LevelGeneration
{
	public class HealthDecrementer : MonoBehaviour
	{
		public new bool active;

		private void Awake()
		{
			active = true;
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.DrawIcon(transform.position, "icon_enemy.png");
		}
#endif
	}
}

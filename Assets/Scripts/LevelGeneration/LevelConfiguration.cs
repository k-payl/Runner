using System.Collections.Generic;
using UnityEngine;
using Sound;

[ExecuteInEditMode]
public class LevelConfiguration : MonoBehaviour
{
	public bool IsSerializedLevel;
	public bool IsMotocycleLevel;
	public float batteryConsumption;
	public bool AvaliableShieldAttack;
	public bool AviliableSwordAttack;
	public SpawnPoint defaulPoint;
	//при добавлении полей, нужно обновлять SetDefaultSettings(LevelSettengs settings);

	public static LevelConfiguration instance;
	public static LevelConfiguration Instance
	{
		get
		{
			if (instance == null)
				instance = FindObjectOfType<LevelConfiguration>();
			return instance;
		}
	}
	
	void Awake()
	{
		if (instance == null)
			instance = this;
		else 
			Destroy(gameObject);
	}
	//private static void SetDefaultSettings(LevelLocalSettings localSettings)
	//{
	//	localSettings.IsSerializedLevel = false;
	//	localSettings.IsMotocycleLevel = false;
	//	localSettings.batteryConsumption = 0.001f;
	//	localSettings.AvaliableShieldAttack = true;
	//	localSettings.AviliableSwordAttack = true;
	//}

	///// <summary>
	///// Положение ближайшего чекпоинта 
	///// </summary>
	///// <param name="target">цель</param>
	//public Vector3 NearestPointPos(Vector3 target)
	//{
	//	if (defaulPoint == null || checkPoints == null)
	//	{
	//		if (defaulPoint != null)
	//			return defaulPoint.Position;

	//		return Vector3.zero;
	//	}
	//	else if (checkPoints.Count == 0)
	//	{
	//		if (defaulPoint==null)
	//			return Vector3.zero;
	//		return defaulPoint.Position;
	//	}
	//	else
	//	{
	//		float minDist = float.PositiveInfinity;
	//		Vector3 res = checkPoints[0].Position;
	//		foreach (var point in checkPoints)
	//		{
	//			if ((Vector3.Distance(point.Position, target)) < minDist)
	//			{
	//				minDist = (Vector3.Distance(point.Position, target));
	//				res = point.Position;
	//			}
	//		}
	//		return res;
	//	}
	//}


	/// <summary>
	/// Положение игрока при старте по умолчанию
	/// </summary>
	public Vector3 DefaultPosition
	{
		get
		{
			if (defaulPoint != null) return defaulPoint.Position;
			return Vector3.zero;
		}
	}
	
	
}

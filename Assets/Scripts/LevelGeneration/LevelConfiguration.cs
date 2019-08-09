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
	
	
	
	
	
	
	
	

	
	
	
	
	
	
	
	
	
	

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	


	
	
	
	public Vector3 DefaultPosition
	{
		get
		{
			if (defaulPoint != null) return defaulPoint.Position;
			return Vector3.zero;
		}
	}
	
	
}

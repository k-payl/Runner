using System;
using GamePlay;
using LevelGeneration;
using UnityEngine;
using System.Collections;

public abstract class ihntBonusElementAbstract : MonoBehaviour
{

	public string obsevableBonusType;

	protected virtual void Awake()
	{
		GameManager.Instance.BonusCollected += BonusCollectedHandler;
		GameManager.Instance.BonusMissed += BonusMissedHandler;
		GameManager.Instance.CollectionInitialized += JustUpdate;

	}

	
	private void BonusCollectedHandler(Bonus bonus, BonusCollection collection)
	{
		if (bonus.GetType() == Type.GetType("LevelGeneration." + obsevableBonusType))
		{
			HandleBonusCollected(bonus, collection);
		}
	}

	private void BonusMissedHandler(Bonus bonus, BonusCollection collection)
	{
		if (bonus.GetType() == Type.GetType("LevelGeneration." + obsevableBonusType))
		{
			HandleBonusMissed(bonus, collection);
		}
	}

	public void OnDestroy()
	{
		GameManager.Instance.BonusCollected -= BonusCollectedHandler;
		GameManager.Instance.BonusMissed -= BonusMissedHandler;
		GameManager.Instance.CollectionInitialized -= JustUpdate;
	}

	public abstract void HandleBonusCollected(Bonus bonus, BonusCollection collection);
	public abstract void HandleBonusMissed(Bonus bonus, BonusCollection collection);
	public abstract void JustUpdate(BonusCollection collection);

}

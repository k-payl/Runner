using GamePlay;
using LevelGeneration;
using UnityEngine;
using System.Collections;

public class ihntMagnetElement : ihntBonusElementAbstract {
	protected override void Awake()
	{
		base.Awake();
		dfControl magnetGUI = GetComponent<dfControl>();
		magnetGUI.Hide();
	}

	public override void HandleBonusCollected(Bonus bonus, BonusCollection collection)
	{
		dfControl magnetGUI = GetComponent<dfControl>();
		magnetGUI.Show();
	   
	}

	public override void HandleBonusMissed(Bonus bonus, BonusCollection collection)
	{
		dfControl magnetGUI = GetComponent<dfControl>();
		magnetGUI.Hide();
		
	}

	public override void JustUpdate(BonusCollection collection)
	{
		
	}
}

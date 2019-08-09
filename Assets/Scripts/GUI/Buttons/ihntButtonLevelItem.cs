using System;
using Loading;
using GamePlay;
using UnityEngine;
using System.Collections;

public class ihntButtonLevelItem : ihntButtonBase, IComparable<ihntButtonLevelItem>
{

	public string levelName;
	public int index;
	public dfButton owner;

	protected override void Start()
	{
		base.Start();
		owner = GetComponent<dfButton>();
	}
	
	public int CompareTo(ihntButtonLevelItem other)
	{
		return (index-other.index);
	}

	public override void OnClick(dfControl control, dfMouseEventArgs args)
	{
		LevelFactory factory = new LevelFactory(levelName, loadingBar, switchPanelBehaviour);
		factory.LoadLevel();
	}
}

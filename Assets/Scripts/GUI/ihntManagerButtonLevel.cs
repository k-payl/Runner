using System;
using System.Collections.Generic;
using System.Linq;
using GamePlay;
using UnityEngine;
using System.Collections;

public class ihntManagerButtonLevel : MonoBehaviour
{
	public ihntPanelBase panelToHideOnButtonPressed;
	public dfProgressBar bar;
	private List<ihntButtonLevelItem> btnsLevels;

	
	void Start ()
	{
		if (GetComponent<dfPanel>() != null)
			GetComponent<dfPanel>().IsVisibleChanged += IsVisibleChangedHandler;
		Repaint();
	}

	public void IsVisibleChangedHandler(dfControl control, bool isHidden)
	{
		if (isHidden)
		{
			Repaint();
		}
	}

	private void Repaint()
	{
		btnsLevels = GetComponentsInChildren<ihntButtonLevelItem>().ToList();
		btnsLevels.Sort();
		
		for (int i = 0; (i < (GameManager.Instance.info.completedLevels + 1)) && (i < btnsLevels.Count); i++)
		{
			btnsLevels[i].owner.Show();
			btnsLevels[i].owner.Enable();
		}
		for (int i = GameManager.Instance.info.completedLevels + 1; i < btnsLevels.Count; i++)
		{
			btnsLevels[i].owner.Hide();
			btnsLevels[i].owner.Disable();
		}

	}


	
	
}

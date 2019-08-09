using System;
using UnityEngine;
using System.Collections;

public class ihntGUI : Singleton<ihntGUI>
{

	public ihntPanelBase ingameGUI;
	public ihntPanelEndLevel endLevelGUI;
	public ihntPanelBase gameOverGUI;
	public ihntPanelBase lastDialogGUI;
	public dfLanguageManager languageManager;

	

	

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	

	

	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);
	}

	public void ShowIngameGUI()
	{
		if (ingameGUI!=null) ingameGUI.Show();
	}

	public void HideIngameGUI()
	{
		if (ingameGUI!=null) ingameGUI.Hide();
	}

	
	
	
	
	public void ShowEndLevelDialog(string title)
	{
		if (endLevelGUI!=null) 
		{
			endLevelGUI.SetLevelRepresentation(title);
			endLevelGUI.Show();
		}
	}

	public void HideEndLevelDialog()
	{
		if (endLevelGUI!=null) endLevelGUI.Hide();
	}

	public void ShowGameOverGUI(string title)
	{
		
		if (gameOverGUI!=null)
			gameOverGUI.Show();
	}

	public void LastDialog()
	{
		if (lastDialogGUI!=null)
			lastDialogGUI.Show();
	}


}

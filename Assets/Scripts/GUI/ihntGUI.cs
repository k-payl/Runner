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

	//#region Instance

	//private static ihntGUI instance;

	//public static ihntGUI Instance
	//{
	//	get
	//	{
	//		if (instance == null)
	//			instance = FindObjectOfType(typeof (ihntGUI)) as ihntGUI;
	//		if (instance == null)
	//		{
	//			instance = new GameObject("GUIManager").AddComponent<ihntGUI>();
	//			Debug.Log("ihntGUI new created");
	//			DontDestroyOnLoad(instance.gameObject);
	//		}
	//		return instance;
	//	}
	//}

	//#endregion

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

	/// <summary>
	/// Показывает диалог в конце левела
	/// </summary>
	/// <param name="title">имя левела</param>
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
		//TODO with title
		if (gameOverGUI!=null)
			gameOverGUI.Show();
	}

	public void LastDialog()
	{
		if (lastDialogGUI!=null)
			lastDialogGUI.Show();
	}


}

using System;
using UnityEngine;
using System.Collections;

public class ihntPanelEndLevel : ihntPanelBase
{

	public dfLabel title;
	private string word1 = "LEVEL";
	private string word2 = "COMPLETED";

	protected override void Start()
	{
		base.Start();
		
		
	}

	public void SetLevelRepresentation(string levelRepresentation)
	{
		
		if (title != null)
		{
			title.Text = word1 + " " + levelRepresentation + " " + word2+"!";
		}

	}
}

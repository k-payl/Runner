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
		//TODO multilang
		//if (title!=null) title.Text = 
	}

	public void SetLevelRepresentation(string levelRepresentation)
	{
		//TODO multilang
		if (title != null)
		{
			title.Text = word1 + " " + levelRepresentation + " " + word2+"!";
		}

	}
}

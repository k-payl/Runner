using System.Collections.Generic;
using GamePlay;
using GamePlay;
using UnityEngine;
using System.Collections;

public class ihntLifeElement : MonoBehaviour
{

	public List<dfControl> lifeItems;


	void OnLevelWasLoaded()
	{
		if (Controller.GetInstance() != null) 
			Controller.GetInstance().LifeChanged += HandleChangingLife;

	}

	private void HandleChangingLife(Life life)
	{
		//Debug.Log("ihntLifeElement.HandleChangingLife(): HP=" + life.HP); 
			for (int i = 0; i < lifeItems.Count; i++)
			{
				if (i < life.HP) lifeItems[i].Show();
				else lifeItems[i].Hide();
			}
	}
}

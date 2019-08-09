using Loading;
using GamePlay;
using UnityEngine;
using System.Collections;

public class ihntButtonMainMenu : ihntButtonBase {

	public override void OnClick(dfControl control, dfMouseEventArgs args)
	{
		base.OnClick(control, args);

		LevelFactory factory = new LevelFactory(GameManager.Instance.defaultLevel , loadingBar, switchPanelBehaviour);
		factory.LoadLevel();
	}
}

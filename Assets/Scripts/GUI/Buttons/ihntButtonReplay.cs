using Loading;
using GamePlay;
using UnityEngine;
using System.Collections;

public class ihntButtonReplay : ihntButtonBase {
	public override void OnClick(dfControl control, dfMouseEventArgs args)
	{
		base.OnClick(control, args);

		LevelFactory factory = new LevelFactory(Application.loadedLevelName , loadingBar, switchPanelBehaviour);
		factory.LoadLevel();
	}
}

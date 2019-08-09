using UnityEngine;
using System.Collections;
using GamePlay;

namespace Loading
{
	internal class ReloadCurrent : LevelLoader
	{
		protected ihntPanelBase showPanel;
		public ReloadCurrent(string levelName, dfProgressBar _loadingBar, ISwitchPanelBehaviour _switchableBehaviour)
			: base(levelName, _loadingBar, _switchableBehaviour)
		{
		}

		public override bool Check()
		{
			return (Application.loadedLevelName == LevelName);
		}

		public override void Execute()
		{
			
			if (GameManager.Instance.trackChunkManager != null)
			{
				GameManager.Instance.trackChunkManager.DeInitialize();
				GameManager.Instance.trackChunkManager.Initialize(
				GameManager.Instance.currentLevelConfiguration.DefaultPosition);
			}

			
			TrackAbstract.GetInstance().CalculateTrackState(GameManager.Instance.currentLevelConfiguration.DefaultPosition);

			
			Controller.GetInstance().MoveToPosition(GameManager.Instance.currentLevelConfiguration.DefaultPosition);
			
			Controller.GetInstance().PrepareToReplay();

			if (switchableBehaviour != null)
				switchableBehaviour.Switch();
		}
	}

}
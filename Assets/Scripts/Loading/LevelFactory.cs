using System.Collections;
using GamePlay;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loading
{
	public class LevelFactory
	{
		private readonly List<LevelLoader> _loaders;
		public LevelFactory(string levelName, dfProgressBar loadingBar, ISwitchPanelBehaviour panelBehaviour)
		{
			_loaders = new List<LevelLoader>
			{
				new ReloadCurrent(levelName, loadingBar, panelBehaviour),
				new LoadNewLevel(levelName, loadingBar, panelBehaviour),
			};

		}

		public void LoadLevel()
		{
			foreach (LevelLoader loader in _loaders)
			{
				if (loader.Check())
				{
					loader.Execute();
					return;
				}
			}
			throw new NotSupportedException("There are not siuteble LevelLoader");
		}
	}

	internal abstract class LevelLoader
	{
		protected readonly string LevelName;
		protected dfProgressBar loadingBar;
		protected ISwitchPanelBehaviour switchableBehaviour;

		protected LevelLoader(string levelName, dfProgressBar _loadingBar, ISwitchPanelBehaviour _switchableBehaviour)
		{
			LevelName = levelName;
			loadingBar = _loadingBar;
			switchableBehaviour = _switchableBehaviour;
		}

		public abstract bool Check();

		public abstract void Execute();
	}

   

	
}

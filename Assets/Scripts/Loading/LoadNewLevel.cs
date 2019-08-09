using System.Reflection.Emit;
using UnityEngine;
using System.Collections;
using GamePlay;

namespace Loading
{
	internal class LoadNewLevel : LevelLoader
	{
		private AsyncOperation async = null;

		public LoadNewLevel(string levelName, dfProgressBar _loadingBar, ISwitchPanelBehaviour _switchableBehaviour)
			: base(levelName, _loadingBar, _switchableBehaviour)
		{
		}

		public override bool Check()
		{
			return Application.loadedLevelName != LevelName;
		}

		public override void Execute()
		{
			
			//Application.LoadLevel(LevelName);
			
			GameObject b = new GameObject{name = "Coroutine_loading"};
			MonoBehaviour objMonoBehaviour = b.AddComponent<Empty>();
			GameObject.DontDestroyOnLoad(objMonoBehaviour.gameObject);
			if (loadingBar != null)
			{
				loadingBar.Value = 0;
				loadingBar.Show(); 
			}
			objMonoBehaviour.StartCoroutine(coroutineLoading(LevelName, loadingBar, switchableBehaviour));
		}



		IEnumerator coroutineLoading(string levelName, dfProgressBar bar, ISwitchPanelBehaviour switchPanel)
		{
			async = Application.LoadLevelAsync(levelName);

			again:
			if (async.isDone)
			{
				Debug.Log("completed");
				if (switchPanel!=null)
					switchPanel.Switch();
			}
			else
			{
				Debug.Log("LoadNewLevel: async.progress=" + async.progress);
				if (bar != null) bar.Value = async.progress;
				yield return new WaitForEndOfFrame();
				goto again;
				
			}
			
		}


	}
}
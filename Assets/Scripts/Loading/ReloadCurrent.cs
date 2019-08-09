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
            //перезагружаем чанки
            if (GameManager.Instance.trackChunkManager != null)
            {
                GameManager.Instance.trackChunkManager.DeInitialize();
                GameManager.Instance.trackChunkManager.Initialize(
                GameManager.Instance.currentLevelConfiguration.DefaultPosition);
            }

            //готовим трек
            TrackAbstract.GetInstance().CalculateTrackState(GameManager.Instance.currentLevelConfiguration.DefaultPosition);

            //перемещаем игрока
            Controller.GetInstance().MoveToPosition(GameManager.Instance.currentLevelConfiguration.DefaultPosition);
            //готовим игрока к новому забегу
            Controller.GetInstance().PrepareToReplay();

            if (switchableBehaviour != null)
                switchableBehaviour.Switch();
        }
    }

}
using UnityEngine;
using System.Collections;
using GamePlay;

namespace LevelGeneration
{

	[ExecuteInEditMode, System.Serializable]
	public class Positioning : MonoBehaviour
	{

		 [HideInInspector]
		public int LineNumber;

		//должен быть Track
		protected static TrackAbstract track;
		public virtual void OnEnable()
		{
			track = TrackAbstract.GetInstance();
		}
		protected virtual void OnDisable()
		{
			track = null;
		}

		
		public virtual void BindToLine()
		{
			if ( track == null )
				track = Track3.GetInstance();
			if (null != (track as Track3))
			{
				transform.position = new Vector3(
					(track as Track3).XCoordOfLine(LineNumber),
					transform.position.y,
					transform.position.z);
			}
		}
		
#if UNITY_EDITOR
		public virtual void Update()
		{
			if (!Application.isPlaying)
			{
				BindToLine();
			}
		}
#endif
	}

}
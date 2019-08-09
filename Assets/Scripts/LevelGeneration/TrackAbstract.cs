using UnityEngine;

namespace GamePlay
{
	[RequireComponent(typeof (BoxCollider)), ExecuteInEditMode]
	public abstract class TrackAbstract : MonoBehaviour
	{

		public abstract float LineWidth { get; set; }
		protected static TrackAbstract instance;

		public static TrackAbstract GetInstance()
		{
			if (instance == null)
				instance = FindObjectOfType(typeof (TrackAbstract)) as TrackAbstract;
			return instance;
		}

		public virtual bool Turn(TurnDirection direction)
		{
			return true;
		}

		public abstract float MaxX();
		
		public abstract float MinX();

		public abstract void CalculateTrackState(Vector3 pointAtTrack); 

		protected virtual void Awake()
		{
			
			if (instance != null && instance != this)
			{
				Debug.LogWarning("One track is alredy exists. See " + instance.gameObject.name);
				DestroyImmediate(this);
			}
			else if (instance == null) instance = this;
		}

		protected virtual void OnEnable()
		{
		}

		private void OnApplicationQuit()
		{
			instance = null;
		}

#if UNITY_EDITOR
		protected virtual void OnDrawGizmos()
		{
		}
#endif
	}
}

using GamePlay;
using UnityEngine;








public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

	public static T Instance
	{
		get
		{
			if (applicationIsQuitting)
			{
				Debug.LogWarning("Singletone<" + typeof(T) + ">: Instance '" + typeof(T) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return _instance;
			}

			lock ( _lock )
			{
				if ( _instance == null )
				{
					_instance = (T) FindObjectOfType(typeof(T));

					if ( FindObjectsOfType(typeof(T)).Length > 1 )
					{
						Debug.LogError("Singletone<"+typeof(T)+">: Something went really wrong " +
							" - there should never be more than 1 singleton!" +
							" Reopenning the scene might fix it.");
						return _instance;
					}

					if ( _instance == null )
					{
						if (!applicationIsQuitting)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = "(singleton) " + typeof (T).ToString();

							DontDestroyOnLoad(singleton);

							Debug.Log("Singletone<" + typeof (T) + ">: An instance of " + typeof (T) +
									  " is needed in the scene, so '" + singleton +
									  "' was created with DontDestroyOnLoad.");
						}
						else
						{
							Debug.Log("Singletone<"+typeof(T)+">:Destroying duo application is Quitting");
							return null;
						}
					}
					else
					{
						Debug.Log("Singletone<"+typeof(T)+">: Using instance already created: " +
							_instance.gameObject.name);
					}
				}

				return _instance;
			}
		}
	}

	public bool IsCanUse;

	protected virtual void Awake()
	{
		GameManager g = GameManager.Instance;
		if (_instance != null)
		{
			if ((_instance as Singleton<T>) != this)
			{
				IsCanUse = false;
				Destroy(gameObject);
				Debug.Log("Singletone<"+typeof(T)+">: " + gameObject.name + " destroyed");
			}
			else
			{
				IsCanUse = true;
			}
		}
		else
		{
			IsCanUse = false;
			Debug.Log("Singletone<"+typeof(T)+">: Something went wrong: _instance = null in Awake()");
		}
	}
	protected virtual void Update()
	{
	}

	private static bool applicationIsQuitting = false;
	
	
	
	
	
	
	
	
	public void OnApplicationQuit()
	{
		applicationIsQuitting = true;
	}
}
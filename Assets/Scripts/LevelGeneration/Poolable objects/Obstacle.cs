using UnityEngine;

namespace LevelGeneration
{
	public class Obstacle : AbstractPoolableObject
	{

		public override void ResetState()
		{
			base.ResetState();
			transform.position = defaultPosition;

			//выключаем для оптимизации
			//TODO мб вынести в Float как static...
			Floater f = GetComponent<Floater>();
			Rotator r = GetComponent<Rotator>();
			Floater f_c = GetComponentInChildren<Floater>();
			Rotator r_c = GetComponentInChildren<Rotator>();
			if (f != null) f.Deactivate();
			if (r != null) r.Deactivate();
			if (f_c != null) f_c.Deactivate();
			if (r_c != null) r_c.Deactivate();
		}

		public override void Init()
		{
			Floater f = GetComponent<Floater>();
			Rotator r = GetComponent<Rotator>();
			Floater f_c = GetComponentInChildren<Floater>();
			Rotator r_c = GetComponentInChildren<Rotator>();
			if (f != null) f.Activate();
			if (r != null) r.Activate();
			if (f_c != null) f_c.Activate();
			if (r_c != null) r_c.Activate();
		}
	}
}

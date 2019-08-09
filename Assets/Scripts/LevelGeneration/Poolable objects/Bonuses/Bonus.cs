using UnityEngine;
using System.Collections.Generic;
using GamePlay;

namespace LevelGeneration
{

	public abstract class Bonus : AbstractPoolableObject, ICollectable
	{
		

		
		
		
		public virtual void Collect()
		{
			if ( collider )
				collider.enabled = false;
			if (renderer)
				renderer.enabled = false;
			Floater f = GetComponent<Floater>();
			Rotator r = GetComponent<Rotator>();
			if (f != null) f.Deactivate();
			if (r != null) r.Deactivate();
		}
		public override void ResetState()
		{
			base.ResetState();		   
			if (collider)
				collider.enabled = true;
			if (renderer)
				renderer.enabled = true;
			transform.position = defaultPosition;
			Floater f = GetComponent<Floater>();
			Rotator r = GetComponent<Rotator>();
			if (f != null) f.Deactivate();
			if (r != null) r.Deactivate();
		}

		public override void Init()
		{
			Floater f = GetComponent<Floater>();
			Rotator r = GetComponent<Rotator>();
			if (f != null) f.Activate();
			if (r != null) r.Activate();
		}
	}
}
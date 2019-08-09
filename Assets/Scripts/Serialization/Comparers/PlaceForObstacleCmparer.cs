using System;
using System.Collections.Generic;
using LevelGeneration;

namespace Serialization
{
	class PlaceForObstacleCmparer: IEqualityComparer<IPoolable>
	{
		public bool Equals(IPoolable x, IPoolable y)
		{
			return true;
		}

		public int GetHashCode(IPoolable obj)
		{
			return 1;
		}
	}
}

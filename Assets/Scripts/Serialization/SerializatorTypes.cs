using UnityEngine;
using System;
using System.Collections.Generic;

namespace Serialization
{ 


	public static class SerializatorTypes
	{
		private static Dictionary<string, Type> serializeMap;// "Class Name" -> "Serialization Class Name"
		static SerializatorTypes()
		{
			serializeMap = new Dictionary<string,Type>();

			//----------------StandartSerializator------------------
			serializeMap.Add("DestroyableObstacle", Type.GetType("Serialization.StandartSerializator"));
			//Bonuses
			serializeMap.Add("HalfBattery", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("Coin", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("CreditCard", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("Medikit", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("MemoryCard", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("SuperMedikit", Type.GetType("Serialization.FloatRotateSerializator"));
				
			serializeMap.Add("CoinMultiplier", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("CrazyBattery", Type.GetType("Serialization.FloatRotateSerializator"));
			serializeMap.Add("Magnet", Type.GetType("Serialization.FloatRotateSerializator"));

			//Enemies
			serializeMap.Add("MiddleEnemy", Type.GetType("Serialization.StandartSerializator"));
			serializeMap.Add("BigEnemy", Type.GetType("Serialization.StandartSerializator"));
			serializeMap.Add("ButterflyEnemy", Type.GetType("Serialization.StandartSerializator"));



			//-------------RopeSerializator------------------
			serializeMap.Add("Rope", Type.GetType("Serialization.RopeSerializator"));

			//-------------FloatRotateSerializator--------------
			serializeMap.Add("Obstacle", Type.GetType("Serialization.FloatRotateSerializator"));

			//-----------------Scalaable-----------------------
			serializeMap.Add("PlaceForObstacle", Type.GetType("Serialization.ScalaableSerializator"));
			serializeMap.Add("PlaceForHill", Type.GetType("Serialization.ScalaableSerializator"));
			serializeMap.Add("PlaceForDangerZone", Type.GetType("Serialization.ScalaableSerializator"));

			//----------------InsideObjsAnimatedContainerSerializator--------------
			serializeMap.Add("InsideObjsAnimatedContainer", Type.GetType("Serialization.InsideObjsAnimatedContainerSerializator"));

			
		}
	   public static Type SerializatorFor(string className)
	   {
		   return serializeMap[className];
	   }

	}
}
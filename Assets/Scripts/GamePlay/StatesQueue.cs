﻿using System;
using System.Security.Cryptography.X509Certificates;
using GamePlay;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{

	/// <summary>
	/// Очередь (для данной задачи "отложенных") состояний.
	/// Можно положить\взять состояние.
	/// </summary>
	public class StatesQueue
	{
		public float stateLifeTime;
		public int maxStates;
		private Queue<TemporaryState> states;
		private string toString;

		internal class TemporaryState  
		{
			public BaseState state;
			public float startTime;

			public TemporaryState( BaseState state, float time )
			{
				this.state = state;
				this.startTime = time;
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="_maxStateCount">максимальное количество состояний в очереди</param>
		/// <param name="stateLifeTimeInQueue">максимальное время жизни одного сосотояния</param>
		public StatesQueue(int _maxStateCount = 1, float stateLifeTimeInQueue = 1f)
		{
			states = new Queue<TemporaryState>(_maxStateCount);
			stateLifeTime = stateLifeTimeInQueue;
			maxStates = _maxStateCount;
			toString = "";
		}

		public int Count {get { return states.Count; }}

		public BaseState GetLastState()
		{
			BaseState res;
			if (states.Count == 0)
			{
				res = Controller.GetInstance().RuningState;
			}
			else
			{
				TemporaryState expectedValidState = states.Dequeue();
				
				while ( (Time.time - expectedValidState.startTime) > stateLifeTime )
				{
					if (states.Count >= 1)
						expectedValidState = states.Dequeue();
					else
					{
						expectedValidState.state = new RuningState(Controller.GetInstance());
						break;
					}
				}
				res = expectedValidState.state;
			}
			return res;
		}

		public void PutState(BaseState newState)
		{
			int countValidStates = 0;
			// Ищем, сколько непросроченных состояний в очереди
			foreach (TemporaryState one in states)
			{
				if ((Time.time - one.startTime) < stateLifeTime) countValidStates++;
			}

			//если еще можно добавить сосотояние
			if (countValidStates < maxStates)
			{
				
				TemporaryState containerNewState = new TemporaryState(newState, Time.time);
				bool existSameState = false;
				float timeOfExistedSame = 0f;
				// поиск такого же состояния в очереди
				foreach (TemporaryState one in states)
				{
					if (one.state.Equals(newState))
					{
						existSameState = true;
						timeOfExistedSame = Time.time - one.startTime;
						break;
					}
				}

				// если существует такое же но просроченое состояние ИЛИ не существует такого же сосотояния,
				// то вставить 
				if ((existSameState && (timeOfExistedSame > stateLifeTime)) || !existSameState)
					states.Enqueue(containerNewState); 
			}


			   
		}

		
		public override string ToString()
		{
			toString = "StatesQueue("+ states.Count+")\n";
			foreach (TemporaryState one in states)
			{
				toString += one.state.GetType().ToString();
				toString += (" (time: " + (Time.time - one.startTime)+")");
				toString+= "\n";
			}
			return toString;
		}
	}

	

}


using System;
using LevelGeneration;
using UnityEngine;
using System.Collections.Generic;
using GamePlay;

namespace LevelGeneration
{		
	public abstract class TimePeriodBonus : Bonus 
	{
		public float ActiveTime;
		private float startTime;
		private bool isCollected;

		public override void Collect()
		{
			base.Collect();
			isCollected = true;
			startTime = Time.time;
			FirstEffect();
			//Debug.Log("Time period bonus collected");
		}

		public override void ResetState()
		{
			if (TimeIsFinished())
			{
				isCollected = false;
				LastEffect();
				base.ResetState();
				//Debug.Log("Time period bonus is removed");
			}
		}


		public bool TimeIsFinished()
		{
			if ( isCollected )
			{
				return (Time.time - startTime) > ActiveTime;
			}
			return true;
		}

		/// <summary>
		/// ����������, �������� �������� ������.���������� ���-������ �� ���������� ��������� �������
		/// </summary>
		public abstract void Affect();
		
		/// <summary>
		/// ��������� ����� �������� � ����� ����� ��������� ������
		/// ���������� ���� ���
		/// </summary>
		protected abstract void LastEffect();

		/// <summary>
		/// ������������� ������. ���������� � ������ ���� ���
		/// </summary>
		protected abstract void FirstEffect();

	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelGeneration
{

	public interface IPoolable
	{
		bool IsUsedNow { get; set; }
		
		/// <summary>
		/// ������ ���������� ���������� ������� � ����� �������������(�� ������� � �������������). 
		/// ���������� ��� �� ����������� ��� Update �������
		/// </summary>
		void ResetState();

		/// <summary>
		/// ����� ����� ����� � �������. 
		/// ����� ������ ������ ���� ����� � �������������
		/// </summary>
		void Init();

		GameObject GetGameObject { get; }
	}
}
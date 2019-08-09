using System;
using LevelGeneration;
using UnityEngine;
using System.Collections;

namespace GamePlay
{
	[Serializable]
	public class TrackLine
	{
		/// <summary>
		/// Координата центра линии, относительно центра трэка.
		/// </summary>
		public float XCoord; 

		public TrackLine(float x)
		{
			XCoord = x;
		}
	}

	public class Track3 : TrackAbstract
	{
		public override float LineWidth
		{
			get { return lineWidth; }
			set
			{
				lineWidth = value;
				if ( lines == null )
					lines = new[] { new TrackLine(-lineWidth), new TrackLine(0f), new TrackLine(lineWidth) };
				else
				{
					lines[0].XCoord = -lineWidth;
					lines[1].XCoord = 0f;
					lines[2].XCoord = lineWidth;
				}
			}
		}
		public float CurrentXCoord
		{
			get { return XCoordOfLine(currentLine); }
		}

		private int currentLine;
		[SerializeField] private TrackLine[] lines;
		[SerializeField] private float lineWidth = 2.5f;

		//X-координата линии lineNumber 
		public float XCoordOfLine(int lineNumber)
		{

			if(lineNumber >= 0 && lineNumber < lines.Length)
				return lines[lineNumber].XCoord + collider.bounds.center.x; //Средняя линия должна находится в центре коллайдера. 
			else
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		public override bool Turn(TurnDirection direction)
		{
			bool f = false;
			switch(direction)
			{
				case TurnDirection.Left:
					if (currentLine > 0)
					{
						currentLine--;
						f = true;
					}
					break;
				case TurnDirection.Right:
					if (currentLine < lines.Length - 1)
					{
						currentLine++;
						f = true;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException("direction");
			}
			return f;

		}

		public override float MaxX()
		{
			float x = (lines.Length > 0) ? XCoordOfLine(lines.Length - 1) : transform.position.x;
			return x;
		}

		public override float MinX()
		{
			return XCoordOfLine(0);
		}

		public override void CalculateTrackState(Vector3 pointAtTrack)
		{
			float x = pointAtTrack.x;
			float d1 = Mathf.Abs(x - XCoordOfLine(0));
			float d2 = Mathf.Abs(x - XCoordOfLine(1));
			float d3 = Mathf.Abs(x - XCoordOfLine(2));
			byte i = 0;
			float min = d1;
			if (d2 < d1) {i = 1;min = d2;}
			if (d3 < min)i = 2;
			currentLine = i;
			//Debug.Log("Calculated number line ="+i);
		}


		protected override void Awake()
		{
			base.Awake();
			LineWidth = lineWidth;
		}

		
#if UNITY_EDITOR
		protected override void OnDrawGizmos()
		{
			Vector3 startPoint = new Vector3(0, collider.bounds.max.y, collider.bounds.min.z);
			Vector3 ednPoint = new Vector3(0, collider.bounds.max.y, collider.bounds.max.z);
			Gizmos.color = Color.red;
			for(int i = 0; i < lines.Length; i++)
			{
				Gizmos.DrawLine(startPoint + new Vector3(XCoordOfLine(i), 0f, 0),
								ednPoint + new Vector3(XCoordOfLine(i), 0.01f, 0));
			}
			//   Gizmos.DrawLine(startPoint + new Vector3(LineWidth * lines.Length + collider.bounds.min.x, 0f, 0f), ednPoint + new Vector3(LineWidth * lines.Length + collider.bounds.min.x, 0f, 0f));
		}
#endif

		public static string[] LinesLiteralPresenation = new[] { "first", "second", "third" };
	}
}
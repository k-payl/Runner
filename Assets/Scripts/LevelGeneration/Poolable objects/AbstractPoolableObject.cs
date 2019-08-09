using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using GamePlay;

namespace LevelGeneration
{
	public abstract class AbstractPoolableObject : MonoBehaviour, IPoolable, IEquatable<AbstractPoolableObject>
	{
		protected static Controller controller;
		protected static readonly Vector3 defaultPosition = new Vector3(-100f, -100f, -100f);
		private const float gizmoLineLength = 0.8f;
		private int hash;

		public static int h;

		protected virtual void Awake()
		{
			if (controller == null) controller = Controller.GetInstance();
			IsUsedNow = false;
			
			//делаем хэш для быстрого сравнения объетов
			//как сумму кодов букв в имени типа
			string nameType;nameType = GetType().Name;
			for (int i = 0; i < nameType.Length; i++)
			{
				hash += nameType[i];
			}
		}

		private bool isUsedNow;

		public virtual void ResetState()
		{
			isUsedNow = false;
			transform.position = defaultPosition;
			Collider coll = collider;
			if (coll != null)
				coll.enabled = false;
		}

		public virtual void Init()
		{
			Collider coll = collider;
			if (coll != null)
				coll.enabled = true;
		}


		public bool IsUsedNow
		{
			get { return isUsedNow; }
			set { isUsedNow = value; }

		}

		public GameObject GetGameObject
		{
			get{return gameObject;}
		}


		public override int GetHashCode()
		{
			return hash;
		}

		public virtual bool Equals(AbstractPoolableObject other)
		{
		   // Debug.Log("AbstractPoolableObject: GetType().Name=" + GetType().Name + "GetType().GetHashCode=" + GetType().GetHashCode());
		   // bool fastExit = this.GetGameObject.GetComponents<Component>().Length == other.GetGameObject.GetComponents<Component>().Length;
		   //if (!fastExit)
		   //   return false;

			//Compare this object meshes
			MeshFilter xm = this.GetComponent<MeshFilter>();
			MeshFilter ym = other.GetComponent<MeshFilter>();
			if (xm == null ^ ym == null)
			{
				return false;
			}
			if (xm != null && ym != null)
			{
				if (xm.sharedMesh != ym.sharedMesh)
					return false;
			}
			

			//meshes equals =>
			//compare materials
			bool hasRenderer1 = this.renderer !=null;
			bool hasRenderer2 = other.renderer != null;
			if (hasRenderer1 ^ hasRenderer2)
				return false;
			if (hasRenderer1 && hasRenderer2)
				if (other.renderer.sharedMaterial  != this.renderer.sharedMaterial);
			
			
			//parents - Equals =>
			//compare meshes childs
			MeshFilter xm_c = this.GetComponentInChildren<MeshFilter>();
			MeshFilter ym_c;
			MeshFilter[] meshes = other.GetComponentsInChildren<MeshFilter>(true);
			if (meshes.Length != 0)
				ym_c = meshes[0];
			else
				ym_c = null;

			if (xm_c == null && ym_c == null)
			{
				//Debug.Log("AbstractPoolableObject: child meshes are equals for "+gameObject.name+" and "+other.gameObject.name);
				return true;
			}
			if ((xm_c == null) ^ (ym_c == null))
			{
				/*
			   // !!!!!
			   //ошибка. в child почему то находит MeshFilter
				if ((h < 5) && xm_c!=null)
				{
					GameObject kk = new GameObject {name = "generated_from_"+xm_c.name};
					kk.AddComponent<MeshFilter>();
					kk.GetComponent<MeshFilter>().mesh = xm_c.sharedMesh;
					kk.AddComponent<MeshRenderer>();
					kk.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
					h++;
				}
				 */
				return false;
			}
			
			bool child_meshes_IsEquals = (xm_c.sharedMesh == ym_c.sharedMesh);
		  //  if (!child_meshes_IsEquals) return false;


		   return child_meshes_IsEquals;

		   // //materials equals =>
		   // //compare materials
		   // hasRenderer1 = (this.GetComponentInChildren<Renderer>() != null);
		   // Renderer[] renders = other.GetComponentsInChildren<Renderer>(true);
		   // if (renders.Count() != null)
		   //	 hasRenderer2 = true;
		   // else
		   //	 hasRenderer2 = null;

		   // hasRenderer2 = other.GetComponentInChildren<Renderer>();
		   // if (hasRenderer1 ^ hasRenderer2)
		   //	 return false;
		   // if (!hasRenderer1 && !hasRenderer2)
		   //	 return true;

		   // return (this.GetComponentInChildren<Renderer>().sharedMaterial ==
		   //			 other.GetComponentInChildren<Renderer>().sharedMaterial);

		   

		}



		protected virtual void OnTriggerEnter( Collider other ) { }
		protected virtual void OnTriggerStay( Collider other ) { }

		protected virtual void DrawGizmo(Color color,float alpha = 0.5f)
		{
			if ( this.enabled )
			{
				color.a = alpha;
				Gizmos.color = color;
				Gizmos.matrix = transform.localToWorldMatrix;
				Gizmos.DrawCube(Vector3.zero, Vector3.one + new Vector3(0.01f, 0.01f, 0.01f));
				Gizmos.color = Color.black;

				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
				Gizmos.matrix = new Matrix4x4();

				Gizmos.color = color;
				Vector3 start = new Vector3(collider.bounds.center.x, collider.bounds.min.y, collider.bounds.center.z);
				Vector3 end = start;
				end.y = 0f;
				int gizmoLineCount = Mathf.FloorToInt(Mathf.Abs((start - end).y) / gizmoLineLength);
				for ( int i = 0; i < gizmoLineCount; i++ )
					Gizmos.DrawLine(start - new Vector3(0f, gizmoLineLength * i, 0f),
									start - new Vector3(0f, gizmoLineLength * (i + 0.5f), 0f));
				if ( start.y - gizmoLineCount * gizmoLineLength > end.y )
					Gizmos.DrawLine(start - new Vector3(0f, gizmoLineLength * gizmoLineCount, 0f), end);
			}
		}
	}
}
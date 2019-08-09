using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    public class MeshFilterDeepComparer : IEqualityComparer<GameObject>
    {
        public bool Equals(GameObject x, GameObject y)
        {
            bool fastExit =
                x.transform.childCount != y.transform.childCount ||
                x.GetComponents<Component>().Length != y.GetComponents<Component>().Length;
            if (fastExit)
            {
                return false;
            }
            Renderer[] xRenderers = x.GetComponentsInChildren<Renderer>(true);
            Renderer[] yRenderers = y.GetComponentsInChildren<Renderer>(true);
            bool result = xRenderers.Length == yRenderers.Length;
            int i = 0;
            while (result == true && i<xRenderers.Length)
            {
                result = IsEqualRenderers(xRenderers[i], yRenderers[i]);
                i++;
            }

            return result;


        }

        public int GetHashCode(GameObject obj)
        {
            throw new System.NotImplementedException();
        }

        private bool IsEqualRenderers(Renderer x, Renderer y)
        {
            MeshFilter xm = x.GetComponent<MeshFilter>();
            MeshFilter ym = y.GetComponent<MeshFilter>();
            bool result = xm.sharedMesh.vertexCount == ym.sharedMesh.vertexCount
                          && xm.renderer.sharedMaterials.Length == ym.renderer.sharedMaterials.Length;
            int i = 0;
            while (result && i < xm.sharedMesh.vertexCount)
            {
                result = xm.sharedMesh.vertices[i] == ym.sharedMesh.vertices[i];
                i++;
            }
            i = 0;
            while (result && i < xm.renderer.sharedMaterials.Length)
            {
                result = x.renderer.sharedMaterials[i] == y.renderer.sharedMaterials[i];
                i++;
            }

            return result;
        }
    }
}
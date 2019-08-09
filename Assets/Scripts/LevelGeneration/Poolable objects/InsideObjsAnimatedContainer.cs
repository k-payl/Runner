using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
    [RequireComponent(typeof (BoxCollider))]
    public class InsideObjsAnimatedContainer : AbstractPoolableObject
    {
        private Animation[] anims;
        [ContextMenu("TryToAnimateObjectInside")]
        public void TryToAnimateObjectInside()
        {
            anims = GetComponentsInChildren<Animation>();
            foreach (Animation concreteAnimation in anims)
            {
                if (concreteAnimation.clip != null)
                {
                    concreteAnimation[concreteAnimation.clip.name].speed = 1;
                    concreteAnimation.Play(concreteAnimation.clip.name);
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            anims = GetComponentsInChildren<Animation>();
            foreach (Animation concreteAnimation in anims)
            {
                if (concreteAnimation.clip != null)
                {
                    concreteAnimation.Play(concreteAnimation.clip.name);
                    concreteAnimation[concreteAnimation.clip.name].speed = -1;
                    concreteAnimation[concreteAnimation.clip.name].time = 0;
                }
            }
        }

        public override void ResetState()
        {
            if (anims!=null)
                foreach (Animation concreteAnimation in anims)
                {
                    concreteAnimation.Play(concreteAnimation.clip.name);
                    concreteAnimation[concreteAnimation.clip.name].speed = -1;
                    concreteAnimation[concreteAnimation.clip.name].time = 0;
                }
            collider.enabled = true;
            //TODO нужно устанавливать на первый кадр
            base.ResetState();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                collider.enabled = false;
                TryToAnimateObjectInside();
            }
        }


        public override bool Equals(AbstractPoolableObject other)
        {
            bool Is_base_Equals = base.Equals(other);
            if (Is_base_Equals && other is InsideObjsAnimatedContainer)
            {
                //string msg = "";
                //Debug.Log("InsideObjsAnimatedContainer.Equals(InsideObjsAnimatedContainer other): Comparision this.name="+this.name+" other.name="+other.name);
                Animation xa = GetComponentInChildren<Animation>();
                Animation ya;

                //Какой-то баг (не хочет выполнять other.GetComponentInChildren<Animation>())
                //поэтому приходится так извращаться...
                Animation[] comps2 = other.GetGameObject.GetComponentsInChildren<Animation>(true);
                if (comps2.Length != 0)
                    ya = comps2[0];
                else
                    ya = null;
                //end-of извращение-------
                 


                if ((xa == null) ^ (ya == null))
                {
                    //Debug.Log(this.name + " and "+other.name + " are NOT equals(duo Animation component):\n");
                   // Component[] anims1 = GetComponentsInChildren<Component>(true);
                    //comps2 = other.GetGameObject.GetComponentsInChildren<Component>(true);
                    //msg += "children of this components:\n";
                    //foreach (Component anim in anims1)
                    //{
                    //    msg += (anim.GetType()+"\n");
                    //}
                    //msg += "-------\n of other components:\n";
                    //foreach (Component anim in comps2)
                    //{
                    //    msg += (anim.GetType() + "\n");
                    //}
                    //Debug.Log(msg);
                    return false;
                }
                if ((xa == null) && (ya == null))
                {
                    //Debug.Log(this.name + " and " + other.name + " are equals (duo (xa == null) && (ya == null))");
                    return true;
                }
                //Debug.Log(this.name + " and " + other.name + " are equals");
                return (xa.clip == ya.clip);
            }
            else
            {
                //Debug.Log(this.name + " and " + other.name + " are not equals (duo base isn't equals or other isn't InsideObjsAnimatedContainer)");
                return false;
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.TransformPoint(GetComponent<BoxCollider>().center), "RedA.tif");
        }
#endif
        
    }
}

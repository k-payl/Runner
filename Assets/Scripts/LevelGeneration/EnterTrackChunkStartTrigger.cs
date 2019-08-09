using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelGeneration
{
    public class EnterTrackChunkStartTrigger : MonoBehaviour
    {
        private static TrackChunkManager trackChunkManager;
        public TrackChunk chunk;
        void Start()
        {
                trackChunkManager = TrackChunkManager.GetInstance();
        }

            void OnTriggerEnter(Collider other)
            {
                if (other.tag == "Player")
                {
                    trackChunkManager.CheckForNewChunks(chunk);
                }
            }

    }
}
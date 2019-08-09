using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GamePlay;
using LevelGeneration;
using UnityEngine;

namespace Sound
{
    public class SoundUtils : MonoBehaviour
    {

        public static AudioClip getRandomAudioClip(List<AudioClip> clips)
        {
            if (clips.Count != 0)
            {
                int rand = Random.Range(0, clips.Count);

                int i = rand;
                while (clips[i] == null && i > 0)
                    i--;
                if (clips[i] == null)
                {
                    i = rand;
                    while (clips[i] == null && i < clips.Count - 1)
                        i++;
                }
                return clips[i];
            }
            else
                return null;
        }
    }

    public class BonusSoundHandler : MonoBehaviour
    {
        public AudioSource audioSource;
        public List<AudioClip> Coins;
        public AudioClip CreditCard;
        public AudioClip HalfBattery;
        public AudioClip Medkit;
        public AudioClip MemeoryCard;
        public AudioClip SuperMedkit;

        public AudioClip CoinMultiplier;
        public AudioClip CrazyBattery;
        public AudioClip Magnet;

        //#region Instance
        //private static BonusSoundHandler instance;
        //public static BonusSoundHandler GetInstance()
        //{
        //    return instance ?? (instance = FindObjectOfType(typeof(BonusSoundHandler)) as BonusSoundHandler);
        //}

        //private void Start()
        //{
        //    instance = this;
        //    if (!audioSource && audio)
        //        audioSource = audio;
        //    if (!audioSource)
        //        audioSource = GetComponentInChildren<AudioSource>();
        //}
        //#endregion

        void Start()
        {
            GameManager.Instance.BonusCollected += PlayClip;
        }

        public void PlayClip(Bonus bonus, BonusCollection bonuses)
        {
            if (audioSource)
            {
                audioSource.loop = false;
                if (bonus is Coin)
                {
                    audioSource.clip = SoundUtils.getRandomAudioClip(Coins);
                }
                else if (bonus is CoinMultiplier)
                {
                    audioSource.clip = CoinMultiplier;
                }
                else if (bonus is CrazyBattery)
                {
                    audioSource.clip = CrazyBattery;
                }
                else if (bonus is CreditCard)
                {
                    audioSource.clip = CreditCard;
                }
                else if (bonus is HalfBattery)
                {
                    audioSource.clip = HalfBattery;
                }
                else if (bonus is Magnet)
                {
                    audioSource.clip = Magnet;
                }
                else if (bonus is Medikit)
                {
                    audioSource.clip = Medkit;
                }
                else if (bonus is MemoryCard)
                {
                    audioSource.clip = MemeoryCard;
                }
                else if (bonus is SuperMedikit)
                {
                    audioSource.clip = SuperMedkit;
                }


                if (audioSource.clip != null)
                    audioSource.Play();
            }
        }
    }
}


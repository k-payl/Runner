using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace Sound
{
	public enum PlayerClip
	{
		JumpUpSound = 0,
		JumpRunClip,
		TurnSound,
		ShieldSound,
		SwordSound,
		DissalowedTurnSound

	}


	/// <summary>
	/// Этому классу делегирована обработка звуков от игрока
	/// </summary>
	public class PlayerSoundHandler : MonoBehaviour
	{
		public AudioSource audioSource;
		public List<AudioClip> JumpUpClips;
		public AudioClip JumpRunClip;
		public AudioClip TurnClip;
		public AudioClip ShieldAttackClip;
		public AudioClip SwordAttackClip;
		public List<AudioClip> DissalowedTurnClips;

		public AudioClip ForwardCollision;
		public AudioClip EnemyDeadth;
		public AudioClip DangerZone;
		public AudioClip BatteryLow;
		public AudioClip HP;

		#region Instance
		private static PlayerSoundHandler instance;
		public static PlayerSoundHandler GetInstance()
		{
			return instance ?? (instance = FindObjectOfType(typeof(PlayerSoundHandler)) as PlayerSoundHandler);
		}

		private void Start()
		{
			instance = this;
			if (!audioSource && audio)
				audioSource = audio;
			if (!audioSource)
				audioSource = GetComponentInChildren<AudioSource>();
		}
		#endregion


		public void PlayDeadSound(DeadReason reason)
		{
			if (audioSource)
			{
				audioSource.Stop();
				audioSource.loop = false;
				switch (reason)
				{
					case DeadReason.BatareyLow:
							audioSource.clip = BatteryLow;
						break;
					case DeadReason.DangerZone:
							audioSource.clip = DangerZone;
						break;
					case DeadReason.EnemyForward:
					case DeadReason.EnemySide:
							audioSource.clip = EnemyDeadth;
						break;
					case DeadReason.ForwardCollision:
							audioSource.clip = ForwardCollision;
						break;
					case DeadReason.HP:
							audioSource.clip = HP;
						break;
					default:
							audioSource.clip = ForwardCollision;
						break;
				}
				if (audioSource.clip)
					audioSource.Play();
				
			}
		}

		public void PlayClip(PlayerClip clipType)
		{
			if (audioSource)
			{
				audioSource.loop = false;
				switch (clipType)
				{
						case PlayerClip.JumpRunClip:
							audioSource.clip = JumpRunClip;
							break;
						case PlayerClip.JumpUpSound:
							//рандомим
							audioSource.clip =  SoundUtils.getRandomAudioClip(JumpUpClips);
							break;
						case PlayerClip.ShieldSound:
							audioSource.clip = ShieldAttackClip;
							break;
						case PlayerClip.SwordSound:
							audioSource.clip = SwordAttackClip;
							break;
						case PlayerClip.TurnSound:
							audioSource.clip = TurnClip;
							break;
						case PlayerClip.DissalowedTurnSound:
							audioSource.clip =  SoundUtils.getRandomAudioClip(DissalowedTurnClips);
							break;
				}
				if (audioSource.clip)
					audioSource.Play();
			}
		}

	   



	}
}

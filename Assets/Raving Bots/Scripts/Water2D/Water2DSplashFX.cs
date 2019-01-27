using UnityEngine;
using System.Collections;

namespace RavingBots.Water2D
{
	public class Water2DSplashFX : MonoBehaviour
	{
		public ParticleSystem DropParticles;
		public ParticleSystem BubbleParticles;

		public int DropCount = 30;
		[Range(0f, 1f)] public float RandDropLifetime = 1f;
		[Range(0f, 1f)] public float RandDropSpeed = 0.8f;
		public int BubbleCount = 30;
		[Range(0f, 1f)] public float RandBubbleLifetime = 1f;

		ParticleSystem.Particle[] _drops;
		ParticleSystem.Particle[] _bubbles;

		AudioSource _audioSource;
		float _gravityModifier;
		
		void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			_gravityModifier = DropParticles.gravityModifier;
		}

		public void Play(float scale, AudioClip sound, float volume, float pitch)
		{
			PlayDrops(scale);
			PlayBubbles(scale);

			_audioSource.Stop();
            _audioSource.clip = sound;
			_audioSource.volume = volume;
			_audioSource.pitch = pitch;
			_audioSource.Play();
        }

		void PlayDrops(float scale)
		{
			DropParticles.gravityModifier = _gravityModifier * scale;
			DropParticles.Emit(Mathf.RoundToInt(scale * DropCount));

			PrepareTable(DropParticles, ref _drops);
			DropParticles.GetParticles(_drops);

			var s = Mathf.Sqrt(scale);

			for (var i = 0; i < _drops.Length; i++)
			{
				_drops[i].startLifetime *= (1f - Random.value * RandDropLifetime);
				_drops[i].velocity *= (1f - Random.value * RandDropSpeed) * scale;
				_drops[i].rotation = GetAngle(_drops[i].velocity);
				_drops[i].startSize *= s;
			}

			DropParticles.SetParticles(_drops, _drops.Length);
		}

		void PlayBubbles(float scale)
		{
			BubbleParticles.Emit(Mathf.RoundToInt(scale * BubbleCount));

			PrepareTable(BubbleParticles, ref _bubbles);
			BubbleParticles.GetParticles(_bubbles);

			var s = Mathf.Sqrt(scale);

			for (var i = 0; i < _bubbles.Length; i++)
			{
				_bubbles[i].startLifetime *= (1f - Random.value * RandBubbleLifetime);
				_bubbles[i].startSize *= s;
			}

			BubbleParticles.SetParticles(_bubbles, _bubbles.Length);
		}

		void PrepareTable(ParticleSystem particleSystem, ref ParticleSystem.Particle[] particles)
		{
			if (particles == null || particles.Length != particleSystem.particleCount)
				particles = new ParticleSystem.Particle[particleSystem.particleCount];
		}

		float GetAngle(Vector2 v)
		{
			return Vector2.Angle(Vector2.up, v) * Mathf.Sign(Vector2.Dot(v, Vector2.right));
		}
	}
}

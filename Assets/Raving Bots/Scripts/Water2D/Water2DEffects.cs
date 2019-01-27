using UnityEngine;
using System.Collections;

namespace RavingBots.Water2D
{
	[RequireComponent(typeof(BuoyancyEffector2D))]
	public class Water2DEffects : MonoBehaviour
	{
		public Water2DSplashFX SplashFXPrefab;
		public int SplashFXPrecache = 30;
		public float SplashFXPowerScale = 0.1f;
		public float SplashFXPowerThreshold = 0.1f;
		public float SplashFXOffset = 0.2f;
		public AudioClip[] SplashFXSounds;
		public float SplashFXPowerToVolume = 1;
		public float SplashFXPowerToPitch = 1;

		public float FloatingSpeed = 1f;
		public float FloatingRange = 1f;

		BuoyancyEffector2D _buoyancyEffector2D;
		float _surfaceLevel;

		Water2DSplashFX[] _splashCache;
		int _splash;

		void Awake()
		{
			_buoyancyEffector2D = GetComponent<BuoyancyEffector2D>();
			_surfaceLevel = _buoyancyEffector2D.surfaceLevel;

			_splashCache = new Water2DSplashFX[SplashFXPrecache];
			var container = new GameObject("Splash Container").transform;

			for (var i = 0; i < _splashCache.Length; i++)
			{
				var splash = Instantiate(SplashFXPrefab);
				splash.transform.parent = container;

				_splashCache[i] = splash;
            }
        }

		void FixedUpdate()
		{
			_buoyancyEffector2D.surfaceLevel = _surfaceLevel - FloatingRange * 0.5f * (Mathf.Sin(Mathf.PI * 2f * FloatingSpeed * Time.fixedTime) + 1f);
		}

		public void OnTriggerEnter2D(Collider2D other)
		{
			var rigidbody = other.transform.parent.GetComponent<Rigidbody2D>();
			var power = SplashFXPowerScale * Vector2.Dot(rigidbody.velocity, Vector2.down) * rigidbody.mass;

			if (power < SplashFXPowerThreshold)
				return;

			var splash = _splashCache[_splash];
			splash.transform.position = new Vector2(other.bounds.center.x, other.bounds.min.y - SplashFXOffset);
			splash.Play(power, SplashFXSounds[Random.Range(0, SplashFXSounds.Length)], power * SplashFXPowerToVolume, SplashFXPowerToPitch / power);

			_splash = (_splash + 1) % _splashCache.Length;
        }
	}
}
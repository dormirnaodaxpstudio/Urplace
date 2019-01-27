using UnityEngine;
using System.Collections;

namespace RavingBots.Water2D
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshRenderer))]
	public class Water2DMaterialScaler : MonoBehaviour
	{
		public string SortingLayerName = "Water";

		public bool DisableInGame = true;
		public bool UpdateEachFrame = true;

		// material input parameters

		[Range(0f, 1f)] public float Transparency = 0.5f;
		[Range(0f, 1f)] public float RefractionIntensity = 0.02f;
		public float BumpMapTilling = 0.1f;
		public float TextureTilling = 1f;
		public float WaveDensity = 1f;
		public float WaveAmplitude = 0.2f;
		public float WaveSpeed = 0.5f;
		[Range(0.001f, 1f)] public float WaveEdgeSoftness = 0.7f;
		[Range(0f, 1f)] public float WaveBlendLevel = 0.5f;
		
		public float CurrentSpeed = -0.15f;

		// shader keywords

		const string _DISABLE_WAVES = "DISABLE_WAVES";
		const string _DISABLE_REFRACTION = "DISABLE_REFRACTION";
		public bool DISABLE_WAVES;
		public bool DISABLE_REFRACTION;

		MeshRenderer _renderer;
		
		void Awake()
		{
			_renderer = GetComponent<MeshRenderer>();
			_renderer.sortingLayerName = SortingLayerName;

			if (Application.isPlaying)
			{
				if (DisableInGame)
					enabled = false;
				else
					Debug.LogWarning("Material is updated each frame (check DisableInGame to increase performance)");
			}
		}

		void Update()
		{
			if (UpdateEachFrame)
			{
				UpdateMaterial();
				UpdateShader();
            }
        }

		[ContextMenu("Update Material")]
		public void UpdateMaterial()
		{
			var m = _renderer.sharedMaterial;
			var lx = transform.lossyScale.x;
			var ly = transform.lossyScale.y;

			var c = m.color;
			c.a = Transparency;
			m.color = c;

			m.SetFloat("_Intensity", RefractionIntensity);

			var wave = m.GetVector("_Wave");
			wave.x = WaveDensity * lx;
			wave.y = WaveAmplitude / ly;
			wave.z = WaveSpeed;
			wave.w = WaveEdgeSoftness;

			m.SetVector("_Wave", wave);
			m.SetFloat("_Level", WaveBlendLevel);
			m.SetFloat("_Current", CurrentSpeed / lx);

            m.SetTextureScale("_MainTex", new Vector2(TextureTilling * ly, lx / ly));
			m.SetTextureScale("_Refraction", new Vector2(BumpMapTilling * lx, BumpMapTilling * ly));
		}

		[ContextMenu("Update Shader")]
		public void UpdateShader()
		{
			var m = _renderer.sharedMaterial;

			SetKeyword(m, DISABLE_WAVES, _DISABLE_WAVES);
			SetKeyword(m, DISABLE_REFRACTION, _DISABLE_REFRACTION);
		}

		void SetKeyword(Material m, bool state, string name)
		{
			if (state == m.IsKeywordEnabled(name))
				return;

			if (state)
				m.EnableKeyword(name);
			else
				m.DisableKeyword(name);
		}
	}
}

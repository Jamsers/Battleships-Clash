using UnityEngine;

namespace UnityStandardAssets.Utility
{
	[RequireComponent(typeof(GUIText))]
	public class FPSCounter : MonoBehaviour
	{
		private const float fpsMeasurePeriod = 0.5f;

		private const string display = "{0} FPS";

		private int m_FpsAccumulator;

		private float m_FpsNextPeriod;

		private int m_CurrentFps;

		private GUIText m_GuiText;

		private void Start()
		{
			m_FpsNextPeriod = Time.realtimeSinceStartup + 0.5f;
			m_GuiText = GetComponent<GUIText>();
		}

		private void Update()
		{
			m_FpsAccumulator++;
			if (Time.realtimeSinceStartup > m_FpsNextPeriod)
			{
				m_CurrentFps = (int)((float)m_FpsAccumulator / 0.5f);
				m_FpsAccumulator = 0;
				m_FpsNextPeriod += 0.5f;
				m_GuiText.text = m_CurrentFps.ToString() + " FPS";
            }
		}
	}
}

using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class TimedObjectActivator : MonoBehaviour
	{
		public enum Action
		{
			Activate,
			Deactivate,
			Destroy,
			ReloadLevel,
			Call
		}

		[Serializable]
		public class Entry
		{
			public GameObject target;

			public Action action;

			public float delay;
		}

		[Serializable]
		public class Entries
		{
			public Entry[] entries;
		}

		public Entries entries = new Entries();

		private void Awake()
		{
			Entry[] array = entries.entries;
			foreach (Entry entry in array)
			{
				switch (entry.action)
				{
				case Action.Activate:
					StartCoroutine(Activate(entry));
					break;
				case Action.Deactivate:
					StartCoroutine(Deactivate(entry));
					break;
				case Action.Destroy:
					UnityEngine.Object.Destroy(entry.target, entry.delay);
					break;
				case Action.ReloadLevel:
					StartCoroutine(ReloadLevel(entry));
					break;
				}
			}
		}

		private IEnumerator Activate(Entry entry)
		{
			yield return new WaitForSeconds(entry.delay);
			entry.target.SetActive(value: true);
		}

		private IEnumerator Deactivate(Entry entry)
		{
			yield return new WaitForSeconds(entry.delay);
			entry.target.SetActive(value: false);
		}

		private IEnumerator ReloadLevel(Entry entry)
		{
			yield return new WaitForSeconds(entry.delay);
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
	}
}

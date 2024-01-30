using UnityEngine;

public class AudioPlay : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void debug()
	{
		UnityEngine.Debug.Log("yo wtf");
	}

	private void PlayAudio()
	{
		AudioSource component = GetComponent<AudioSource>();
		component.Play();
	}
}

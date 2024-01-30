using UnityEngine;

public class DeleteOutOfBounds : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerExit(Collider other)
	{
		Object.Destroy(other.gameObject);
	}
}

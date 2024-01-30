using UnityEngine;

public class DeleteMissile : MonoBehaviour
{
	public GameObject explosion;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Missile")
		{
			Object.Instantiate(explosion, other.transform.position, explosion.transform.rotation);
			UnityEngine.Object.Destroy(other.gameObject);
		}
	}
}

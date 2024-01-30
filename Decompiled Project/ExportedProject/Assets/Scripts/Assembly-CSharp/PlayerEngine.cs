using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
	public GameObject explosion;

	public GameEngine gameEngine;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy" || other.tag == "Sub")
		{
			Object.Instantiate(explosion, other.transform.position, explosion.transform.rotation);
			Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
			Object.Destroy(other.gameObject);
			Object.Destroy(base.gameObject);
			gameEngine.GameOver();
		}
		if (other.tag == "Island")
		{
			Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
			Object.Destroy(base.gameObject);
			gameEngine.GameOver();
		}
	}
}

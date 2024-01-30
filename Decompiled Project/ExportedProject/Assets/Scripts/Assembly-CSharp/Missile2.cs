using UnityEngine;

public class Missile2 : MonoBehaviour
{
	public GameObject explosion;

	public int missileSpeed;

	public GameObject debugBox;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 vector = new Vector3(0f, 0f, missileSpeed);
		base.transform.position = base.transform.position + vector * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player2")
		{
			Object.Instantiate(explosion, other.transform.position, explosion.transform.rotation);
			Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
			GameObject gameObject = GameObject.FindWithTag("Player");
			Object.Destroy(gameObject.gameObject);
			Object.Destroy(base.gameObject);
			GameObject gameObject2 = GameObject.FindWithTag("Game Engine");
			GameEngine component = gameObject2.GetComponent<GameEngine>();
			component.GameOver();
		}
	}
}

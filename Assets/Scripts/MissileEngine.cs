using UnityEngine;

public class MissileEngine : MonoBehaviour
{
	public GameObject explosion;

	public int missileSpeed;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 a = new Vector3(0f, 0f, missileSpeed);
		base.transform.position = base.transform.position + a * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			GameObject gameObject = GameObject.FindWithTag("Game Engine");
			GameEngine component = gameObject.GetComponent<GameEngine>();
			component.AddToScore(1);
			Object.Instantiate(explosion, other.transform.position, explosion.transform.rotation);
			Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
			UnityEngine.Object.Destroy(other.gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		if (other.tag == "Sub")
		{
			SubHitCounter component2 = other.GetComponent<SubHitCounter>();
			if (component2.wasHit)
			{
				GameObject gameObject2 = GameObject.FindWithTag("Game Engine");
				GameEngine component3 = gameObject2.GetComponent<GameEngine>();
				component3.AddToScore(2);
				Object.Instantiate(explosion, other.transform.position, explosion.transform.rotation);
				Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
				UnityEngine.Object.Destroy(other.gameObject);
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (!component2.wasHit)
			{
				component2.wasHit = true;
				Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (other.tag == "Island")
		{
			Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}

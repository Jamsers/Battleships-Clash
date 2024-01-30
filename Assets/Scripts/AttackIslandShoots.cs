using UnityEngine;

public class AttackIslandShoots : MonoBehaviour
{
	private float shootTime;

	public float shootRate;

	public float missileYOffset;

	public GameObject missile;

	private void Start()
	{
		shootTime = Time.time;
	}

	private void Update()
	{
		if (Time.time - shootTime > shootRate)
		{
			Vector3 b = new Vector3(0f, missileYOffset, 0f);
			Quaternion rhs = Quaternion.Euler(0f, 0f, 180f);
			Object.Instantiate(missile, base.transform.position + b, missile.transform.rotation * rhs);
			shootTime = Time.time;
		}
	}
}

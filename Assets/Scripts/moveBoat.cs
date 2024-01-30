using UnityEngine;

public class moveBoat : MonoBehaviour
{
	public float upMovement;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 a = new Vector3(0f, 0f, upMovement);
		base.transform.position = base.transform.position + a * Time.deltaTime;
	}
}

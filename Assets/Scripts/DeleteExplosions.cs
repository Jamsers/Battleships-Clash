using UnityEngine;

public class DeleteExplosions : MonoBehaviour
{
	public float explosionLife;

	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, explosionLife);
	}

	private void Update()
	{
	}
}

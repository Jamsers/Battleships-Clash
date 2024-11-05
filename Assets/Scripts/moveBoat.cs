using UnityEngine;
using UnityStandardAssets.Effects;

public class moveBoat : MonoBehaviour
{
    public GameObject explosion;

    public float upMovement;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 a = new Vector3(0f, 0f, upMovement);
		base.transform.position = base.transform.position + a * Time.deltaTime;
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Sub" || other.gameObject.tag == "Island")
        {
            Object.Instantiate(explosion, base.transform.position, explosion.transform.rotation);
            UnityEngine.Object.Destroy(base.gameObject);
        }
    }
}

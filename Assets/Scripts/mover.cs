using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
	public float speed = 15;

    private void FixedUpdate()
	{
		GetComponent<Rigidbody>().velocity = transform.up * speed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletKiller")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace space_program.Game
{
    public class Asteroid : Obstacle
    {

        public AudioSource explode;

        // Start is called before the first frame update
        void Start()
        {
            explode = GameObject.FindGameObjectWithTag("explodeSound").GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, 25 * Time.deltaTime, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Bolt")
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                if(explode != null)
                {
                    explode.Play();
                }
            }
        }
    }
}

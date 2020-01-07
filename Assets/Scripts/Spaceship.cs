﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using space_program.UI;

namespace space_program.Game{
    [System.Serializable]
    /// <summary>
    /// Stage boundary, zMin and zMax may be used later
    /// </summary>
    public class Boundary
    {
	    public float xMin, xMax, zMin, zMax;
    }

    public class Spaceship : MonoBehaviour
    {
        /// <summary>
        /// The spaceship's movement speed from side to side
        /// </summary>
        private float _speedSide = 7.0f;

        /// <summary>
        /// The spaceships's movement speed upwards
        /// </summary>
        private float _speedUp = 5.0f;

        private float _fuel;

        /// <summary>
        /// Property for accessing fuel and keeping the fuel value under 100
        /// </summary>
        public float Fuel {
            get
            {
                return _fuel;
            }
            set
            {
                if (value < 100.0)
                    _fuel = value;
                else
                {
                    _fuel = 100;
                }
            }
        }

        private bool _alive = true;

        /// <summary>
        /// True if the ship has reached the end planet
        /// </summary>
        private bool _arrived = false;

        /// <summary>
        /// The game UI
        /// </summary>
        public UIManager uiManager;

        /// <summary>
        /// The audio clip that will play when the ship hits an obstacle
        /// </summary>
        public AudioSource crashSound;

        /// <summary>
        /// The audio clip that will play when the ship hits a collectible
        /// </summary>
        public AudioSource collectSound;

        /// <summary>
        /// The audio clip that will play when the ship hits a collectible
        /// </summary>
        public AudioSource gameOverSound;	

        /// <summary>
	    /// Tilt
	    /// </summary>
	    public float tilt;

	    /// <summary>
	    /// Boundary
	    /// </summary>
	    public Boundary boundary;

        public GameObject shot;
        public Transform shotSpawn;
        public float fireRate = 0.1f;
        private float nextFire = 0.0f;
        public float space = 0.1f;

        private void Start()
        {
            Fuel = 100;

            if (uiManager == null)
            {
                uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
            }
        }
		
        void FixedUpdate()
        {
            if (_alive && !_arrived)
            {
                // Move the ship up side to side (based on user input)
                float translation = Input.GetAxis("Horizontal") * _speedSide * Time.fixedDeltaTime;

                float translationUp = _speedUp * Time.fixedDeltaTime;

                transform.Translate(translation, translationUp, 0);

                //adding boundary
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                    transform.position.y,
                    8.0f
                    );
                //adding tilt
                transform.rotation = Quaternion.Euler(0.0f, translation * -tilt, 0.0f);

                Fuel -= 0.05f;
            }

            if (Fuel <= 0)
            {
                Lose();
            }
            uiManager.UpdateFuelSlider(Fuel);
        }

        /// <summary>
        /// For shooting
        /// </summary>
        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire && Fuel > 15)
            {
                // Spend fuel to shoot
                Fuel -= 10;
                uiManager.UpdateFuelSlider(Fuel);

                nextFire = Time.time + fireRate;
                Vector3 temp = shotSpawn.position;
                float f = shotSpawn.position.x - 1;
                Quaternion rotation = shotSpawn.rotation;
                for (int i = 1; f < shotSpawn.position.x + 1; i++)
                {
                    rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z += -i * 3 + 30);
                    temp.x = f;
                    Instantiate(shot, temp, rotation);
                    f += space;
                }
            }
        }

        /// <summary>
        /// Detect collisions
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Obstacle")
            {
                // Subtract fuel if they hit an obstacle
                Fuel -= 10;
                uiManager.UpdateFuelSlider(Fuel);
                crashSound.Play();
                Destroy(other.gameObject);
            }
            else if (other.tag == "Collectible")
            {
                // Add fuel if they collected fuel
                Fuel += 20;
                uiManager.UpdateFuelSlider(Fuel);
                collectSound.Play();
                Destroy(other.gameObject);
            }
            else if (other.tag == "Goal")
            {
                Win();
            }

            // Determine if the user has died
            if (Fuel <= 0)
            {
                Lose();
            }
        }

        /// <summary>
        /// Set arrived to true and display the winPanel
        /// </summary>
        private void Win()
        {
            collectSound.Play();
            _arrived = true;
            uiManager.Win();
        }

        /// <summary>
        /// Set alive to false and display the losePanel
        /// </summary>
        private void Lose()
        {
            _alive = false;
            gameOverSound.Play();
            uiManager.Lose();
        }
    }
}


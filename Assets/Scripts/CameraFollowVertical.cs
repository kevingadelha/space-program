using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace space_program.Game
{
    /// <summary>
    /// Makes the camera follow the player's spaceship in the u axis
    /// </summary>
    public class CameraFollowVertical : MonoBehaviour
    {
        /// <summary>
        /// Vector for camera movement
        /// </summary>
        private Vector3 cameraTarget;

        /// <summary>
        /// The target (spaceship)
        /// </summary>
        private Transform target;

        void Start()
        {
            // find the target by its tag
            target = GameObject.FindGameObjectWithTag("Spaceship").transform;
        }

        void FixedUpdate()
        {
            // move the camera with the target
            if (target != null)
            {
                cameraTarget = new Vector3(transform.position.x, target.position.y, transform.position.z);
                transform.position = cameraTarget;
            }

        }
    }
}

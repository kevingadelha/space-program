using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace space_program.Game
{
    public class Fuel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.left, 30 * Time.deltaTime, Space.Self);
        }
    }
}

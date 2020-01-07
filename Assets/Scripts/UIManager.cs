using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using space_program.Game;
using UnityEngine.UI;

namespace space_program.UI
{
    public class UIManager : MonoBehaviour
    {

        /// <summary>
        /// The slider represeting the fuel level
        /// </summary>
        public Slider fuelSlider;

        /// <summary>
        /// The panel that is displayed when the user wins
        /// </summary>
        public GameObject winPanel;

        /// <summary>
        /// The panel that is displayed when the user loses
        /// </summary>
        public GameObject losePanel;

        // Start is called before the first frame update
        void Start()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Updates the fuel slider to match the player's fuel level
        /// </summary>
        /// <param name="fuelValue"></param>
        public void UpdateFuelSlider(float fuelValue)
        {
            fuelSlider.value = fuelValue;
        }

        /// <summary>
        /// Displays the winPanel
        /// </summary>
        public void Win()
        {
            winPanel.SetActive(true);
        }

        /// <summary>
        /// Displays the losePanel
        /// </summary>
        public void Lose()
        {
            losePanel.SetActive(true);
        }
    }
}


using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is linked to the option screen. It contains a volume control and an overview for the standard key mapping.
    /// </summary>
    public class OptionScreen : MonoBehaviour
    {
        /// <summary>
        /// The variable saves the current sound volume.
        /// </summary>
        private float volume = 1.0f;

        /// <summary>
        /// The variable contains the image for the standard key mapping overview.
        /// </summary>
        public Texture controls;

        /// <summary>
        /// This method is called once per frame and checks if the escape button is pressed for going back to the main screen.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick button 6"))
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }
        }

        /// <summary>
        /// The method is responsible for the GUI and therefore creates the shown elements like the button, the volume control and the image for the key overview.
        /// </summary>
        public void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - Screen.width * 0.2f, Screen.height / 2 - Screen.height * 0.45f, Screen.width * 0.4f, Screen.height * 0.45f));
            GUILayout.Box("Volume");
            volume = GUILayout.HorizontalSlider(volume, 0.0f, 1.0f);
            AudioListener.volume = volume;
            GUILayout.EndArea();

            GUI.DrawTexture(new Rect(Screen.width / 2 - Screen.width * 0.4f, Screen.height / 2 - Screen.height * 0.3f, Screen.width * 0.8f, Screen.height * 0.7f), controls, ScaleMode.ScaleToFit);

            if (GUI.Button(new Rect(0, Screen.height - 30, 70, 30), "Back"))
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }
        }
    }
}
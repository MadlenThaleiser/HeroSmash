using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is linked to the main screen at game start. It creates the buttons for starting the game, loading the option screen and viewing the credits.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// The background image.
        /// </summary>
        public Texture background;

        /// <summary>
        /// The original screen width which is being changed by the matrix.
        /// </summary>
        private const int SCREEN_WIDTH = 1024;

        /// <summary>
        /// The original screen height which is being changed by the matrix.
        /// </summary>
        private const int SCREEN_HEIGHT = 768;

        /// <summary>
        /// Draws the background image and the buttons for the credits and options.
        /// </summary>
        public void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
            initMenu();

            if (GUI.Button(new Rect(Screen.width - 80, Screen.height - 30, 80, 30), "Options"))
            {
                Application.LoadLevel((int)Constants.Levels.OPTIONS);
            }

            // Reset the screen dimensions again for the rest of the game
            GUI.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// Creates the buttons for starting and quitting the game.
        /// </summary>
        private void initMenu()
        {
            GUIStyle menuStyle = new GUIStyle(GUI.skin.button);
            menuStyle.fontSize = 20;
            menuStyle.fontStyle = FontStyle.Bold;
            const int BUTTON_HEIGHT = 50;
            const int BUTTON_WIDTH = 200;

            var startButton = GUI.Button(
                new Rect(
                    Screen.width / 2 - BUTTON_WIDTH / 2,
                    Screen.height / 2 - BUTTON_HEIGHT / 2 + 50,
                    BUTTON_WIDTH,
                    BUTTON_HEIGHT),
                "Start!",
                menuStyle);

            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 100, 80, 30), "Quit Game"))
            {
                Application.Quit();
            }

            // Start the character selection when the button is clicked
            if (startButton)
            {
                Application.LoadLevel((int)Constants.Levels.CHARACTER_SELECTION);
            }
        }

        /// <summary>
        /// Sets the music to the menu sound
        /// </summary>
        public void Start()
        {
            GameMusic.topical = GameMusic.Screen.MENU;
        }

        /// <summary>
        /// Checks if the return key is pressed for starting the character selection screen.
        /// </summary>
        public void Update()
        {
            // Navigation to the next Screen
            if (Input.GetKeyDown("return") || Input.GetKeyDown("joystick button 7"))
            {
                Application.LoadLevel((int)Constants.Levels.CHARACTER_SELECTION);
            }
        }
    }
}
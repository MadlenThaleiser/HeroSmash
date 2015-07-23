using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The class is linked to the credit screen scene and shows the text which describes who created this game.
    /// </summary>
    public class CreditScreen : MonoBehaviour
    {
        /// <summary>
        /// The offset is responsible for the position of the shown text. It is changed in the update method.
        /// </summary>
        private float offset;

        /// <summary>
        /// The speed for the text.
        /// </summary>
        public float speed = 25.0f;

        /// <summary>
        /// The style of the GUI.
        /// </summary>
        public GUIStyle style;

        /// <summary>
        /// Initializes the offset for the text and sets the game music to the credit sound.
        /// </summary>
        public void Start()
        {
            GameMusic.topical = GameMusic.Screen.OPTIONS;
            this.offset = Screen.height;
        }

        /// <summary>
        /// Update is called once per frame, lets the text scroll over the screen and checks if the escape key is pressed.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }

            this.offset -= Time.deltaTime * this.speed;
        }

        /// <summary>
        /// This creates the button for going back and creates a label for the shown text.
        /// </summary>
        public void OnGUI()
        {
            if (GUI.Button(new Rect(0, Screen.height - 30, 70, 30), "Back"))
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }

            if (Screen.width < 500)
            {
                style.fontSize = 15;
            }
            else if (Screen.width < 1200)
            {
                style.fontSize = 20;
            }
            else if (Screen.width < 2000)
            {
                style.fontSize = 28;
            }

            var position = new Rect(75, this.offset, Screen.width, Screen.height);
            var text = "Head of Synchronisation: Michael Decker\n\nGreat pixel bender: Madlen Thaleiser\n\nMaster of animation: Fares Mokrani\n\nHead of ultimate perfection: Josephine Mertens\n\nBackbone of logic implementation: Jakob Warkotsch\n\nScreen design officer: Paul Kunze\n\nMaster Chief: Gero Leinemann\n\nProduct Owning Product Owner: Hoang Viet Do\n\nAssisting Assistant: Hoang Ha Do\n\nThis game was created in a software project at\nFreie Universität Berlin in the year 2013.";
            GUI.Label(position, text, this.style);
        }
    }
}
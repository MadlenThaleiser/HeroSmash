using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The class is linked to the level selection scene. It creates the GUI and provides the functionality for choosing a level.
    /// </summary>
    public class LevelSelection : MonoBehaviour
    {
        /// <summary>
        /// The spotlight which shows the currently chosen level.
        /// </summary>
        public GameObject Light;

        /// <summary>
        /// Saves the currently chosen level.
        /// </summary>
        public static int level = 1;

        /// <summary>
        /// Shows the level information label with the text 'coming soon'.
        /// </summary>
        private GameObject comingSoon;

        /// <summary>
        /// Shows the level information label for the demo level.
        /// </summary>
        private GameObject demoLevel;

        /// <summary>
        /// Shows the level information label for the school level.
        /// </summary>
        private GameObject schoolLevel;

        /// <summary>
        /// Position for the Spotlight when Level 1 (DemoLevel) is choosen
        /// </summary>
        private Vector3 lightLevel1 = new Vector3(-7.233704f, 4.022607f, -10.1002f);

        /// <summary>
        /// Position for the Spotlight when Level 2 (SchoolLevel) is choosen
        /// </summary>
        private Vector3 lightLevel2 = new Vector3(-3.381073f, 4.022607f, -10.1002f);

        /// <summary>
        /// Position for the Spotlight when Level 3 (ComingSoon) is choosen
        /// </summary>
        private Vector3 lightLevel3 = new Vector3(1.06303f, 4.022607f, -10.1002f);

        /// <summary>
        /// Position for the Spotlight when Level 4 (ComingSoon) is choosen
        /// </summary>
        private Vector3 lightLevel4 = new Vector3(5.287646f, 4.022607f, -10.1002f);

        /// <summary>
        /// Checks whether a joystick axis is already in use.
        /// </summary>
        private bool axisInUse = false;

        /// <summary>
        /// Initializes variables like <c>coming_soon</c>, <c>level1_inf</c> and <c>school_level_info</c>.
        /// </summary>
        public void Start()
        {
            GameMusic.topical = GameMusic.Screen.MENU;

            // init gameobjects and visibility
            comingSoon = GameObject.Find("comingsoon");
            comingSoon.GetComponent<MeshRenderer>().enabled = false;
            demoLevel = GameObject.Find("leve1inf");
            schoolLevel = GameObject.Find("school_level_info");
            schoolLevel.GetComponent<MeshRenderer>().enabled = false;
            spotLightPosition();
        }

        /// <summary>
        /// Update is called once per frame and checks if the keys for choosing a level are pressed.
        /// </summary>
        public void Update()
        {
            // move left for selection
            if (Input.GetKeyDown("left") || (Input.GetAxisRaw("menu_horizontal") < -0.5 && axisInUse == false))
            {
                if (level == 1)
                {
                    Light.transform.position = lightLevel4;
                    level = 4;
                    comingSoon.GetComponent<MeshRenderer>().enabled = true;
                    demoLevel.GetComponent<MeshRenderer>().enabled = false;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (level == 2)
                {
                    Light.transform.position = lightLevel1;
                    level = 1;
                    comingSoon.GetComponent<MeshRenderer>().enabled = false;
                    demoLevel.GetComponent<MeshRenderer>().enabled = true;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (level == 3)
                {
                    Light.transform.position = lightLevel2;
                    level = 2;
                    comingSoon.GetComponent<MeshRenderer>().enabled = false;
                    demoLevel.GetComponent<MeshRenderer>().enabled = false;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = true;
                }
                else if (level == 4)
                {
                    Light.transform.position = lightLevel3;
                    level = 3;
                    comingSoon.GetComponent<MeshRenderer>().enabled = true;
                    demoLevel.GetComponent<MeshRenderer>().enabled = false;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = false;
                }

                axisInUse = true;
            }

            if (Input.GetAxisRaw("menu_horizontal") == 0)
            {
                axisInUse = false;
            }

            // move right for selection
            if (Input.GetKeyDown("right") || (Input.GetAxisRaw("menu_horizontal") > 0.5 && axisInUse == false))
            {
                if (level == 3)
                {
                    Light.transform.position = lightLevel4;
                    level = 4;
                    comingSoon.GetComponent<MeshRenderer>().enabled = true;
                    demoLevel.GetComponent<MeshRenderer>().enabled = false;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (level == 4)
                {
                    Light.transform.position = lightLevel1;
                    level = 1;
                    comingSoon.GetComponent<MeshRenderer>().enabled = false;
                    demoLevel.GetComponent<MeshRenderer>().enabled = true;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (level == 1)
                {
                    Light.transform.position = lightLevel2;
                    level = 2;
                    comingSoon.GetComponent<MeshRenderer>().enabled = false;
                    demoLevel.GetComponent<MeshRenderer>().enabled = false;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = true;
                }
                else if (level == 2)
                {
                    Light.transform.position = lightLevel3;
                    level = 3;
                    comingSoon.GetComponent<MeshRenderer>().enabled = true;
                    demoLevel.GetComponent<MeshRenderer>().enabled = false;
                    schoolLevel.GetComponent<MeshRenderer>().enabled = false;
                }

                axisInUse = true;
            }

            // enter selection
            if (Input.GetKeyDown("return") || Input.GetKeyDown("joystick button 7"))
            {
                if (level == 1)
                {
                    Application.LoadLevel((int)Constants.Levels.DEMO_LEVEL);
                }
                else
                {
                    if (level == 2)
                    {
                        Application.LoadLevel((int)Constants.Levels.SCHOOL_LEVEL);
                    }
                }
            }

            if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick button 6"))
            {
                Application.LoadLevel((int)Constants.Levels.CHARACTER_SELECTION);
            }
        }

        /// <summary>
        /// Draws the GUI buttons for going back and confirming.
        /// </summary>
        public void OnGUI()
        {
            if (GUI.Button(new Rect(0, Screen.height - 30, 80, 30), "Back"))
            {
                Application.LoadLevel((int)Constants.Levels.CHARACTER_SELECTION);
            }

            if (GUI.Button(new Rect(Screen.width - 80, Screen.height - 30, 80, 30), "Next"))
            {
                if (level == 1)
                {
                    Application.LoadLevel((int)Constants.Levels.DEMO_LEVEL);
                }
                else if (level == 2)
                {
                    Application.LoadLevel((int)Constants.Levels.SCHOOL_LEVEL);
                }
            }
        }

        /// <summary>
        /// Sets the position of the spotlight when the screen is shown
        /// </summary>
        public void spotLightPosition()
        {
            if (level == 1)
            {
                Light.transform.position = lightLevel1;
                comingSoon.GetComponent<MeshRenderer>().enabled = false;
                demoLevel.GetComponent<MeshRenderer>().enabled = true;
                schoolLevel.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (level == 2)
            {
                Light.transform.position = lightLevel2;
                comingSoon.GetComponent<MeshRenderer>().enabled = false;
                demoLevel.GetComponent<MeshRenderer>().enabled = false;
                schoolLevel.GetComponent<MeshRenderer>().enabled = true;
            }
            else if (level == 3)
            {
                Light.transform.position = lightLevel3;
                comingSoon.GetComponent<MeshRenderer>().enabled = true;
                demoLevel.GetComponent<MeshRenderer>().enabled = false;
                schoolLevel.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (level == 4)
            {
                Light.transform.position = lightLevel4;
                comingSoon.GetComponent<MeshRenderer>().enabled = true;
                demoLevel.GetComponent<MeshRenderer>().enabled = false;
                schoolLevel.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
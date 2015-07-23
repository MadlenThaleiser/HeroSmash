using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The class is linked to the character selection scene. It creates the GUI and provides the functionality for choosing a character for up to 4 players.
    /// </summary>
    public class CharacterSelection : MonoBehaviour
    {
        /// <summary>
        /// The spotlight lightens the topical character.
        /// </summary>
        private GameObject spotlight;

        /// <summary>
        /// Says which character is chosen at the moment.
        /// </summary>
        private int character;

        /// <summary>
        /// Says which player is choosing at the moment.
        /// </summary>
        private int player;

        /// <summary>
        /// Contains all GameObjects of the characters.
        /// </summary>
        private GameObject[] chars;

        /// <summary>
        /// Contains the start position of each character.
        /// </summary>
        private Vector3[] charPlatformPos;

        /// <summary>
        /// Contains the 4 positions of the platforms for each player.
        /// </summary>
        private Vector3[] playerPlatformPos;

        /// <summary>
        /// Contains the position offsets of each character so that they stand at the right place.
        /// </summary>
        private Vector3[] charOffset;

        /// <summary>
        /// The style of the GUI.
        /// </summary>
        public GUIStyle style;

        /// <summary>
        /// Checks whether a joystick axis is already in use.
        /// </summary>
        private bool axisInUse = false;

        /// <summary>
        /// This initializes all of the variables.
        /// </summary>
        public void Start()
        {
            GameMusic.topical = GameMusic.Screen.MENU;
            character = 1;
            player = 1;
            chars = new GameObject[]
        {
            GameObject.Find("harry"), GameObject.Find("snail"),
            GameObject.Find("kirb"), GameObject.Find("captain_murica"),
            GameObject.Find("yoda"), GameObject.Find("mr_ed")
        };
            charPlatformPos = new Vector3[]
        {
            new Vector3(-30.52f, 3.82f, -0.85f), new Vector3(-26.23f, 3.82f, -0.85f),
            new Vector3(-21.95f, 3.82f, -0.85f),   new Vector3(-17.7f, 3.82f, -0.85f),
            new Vector3(-13.5f, 3.82f, -0.85f), new Vector3(-9.15f, 3.82f, -0.85f)
        };

            playerPlatformPos = new Vector3[]
        {
            new Vector3(-26.43f, -5.3f, -0.85f), new Vector3(-22.2f, -5.3f, -0.85f),
            new Vector3(-17.95f, -5.3f, -0.85f), new Vector3(-13.68f, -5.3f, -0.85f)
        };

            charOffset = new Vector3[]
        {
            new Vector3(2f, 0.02f, 0), new Vector3(1.73f, 0.98f, 0),
            new Vector3(2f, 1.76f, 0),   new Vector3(2f, 0f, 0),
            new Vector3(2f, 1.45f, 0), new Vector3(2f, 2.36f, 0)
        };

            spotlight = GameObject.Find("Spotlight_Characters");

            for (int i = 0; i < chars.Length; i++)
            {
                for (int j = 0; j < GameController.charOfPlayer.Length; j++)
                {
                    if (chars[i] != null && GameController.charOfPlayer[j] == i + 1)
                    {
                        chars[i].transform.position = playerPlatformPos[j] + charOffset[i];
                        break;
                    }

                    if (chars[i] != null && !GameController.charOfPlayer.Equals(i + 1))
                    {
                        chars[i].transform.position = charPlatformPos[i] + charOffset[i];
                    }
                }
            }
        }

        /// <summary>
        /// Update is called once per frame and checks if the keys for choosing a character or a player are pressed.
        /// </summary>
        public void Update()
        {
            if (Input.GetKeyDown("right") || ((Input.GetAxis("menu_horizontal") > 0.5) && (axisInUse == false)))
            {
                if (character == 4)
                {
                    spotlight.transform.position = new Vector3(-28.7f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 1;
                }
                else if (character == 3)
                {
                    spotlight.transform.position = new Vector3(-16f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 4;
                }
                else if (character == 2)
                {
                    spotlight.transform.position = new Vector3(-20f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 3;
                }
                else if (character == 1)
                {
                    spotlight.transform.position = new Vector3(-24.2f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 2;
                }

                axisInUse = true;
            }

            if (Input.GetKeyDown("left") || ((Input.GetAxis("menu_horizontal") < -0.5) && (axisInUse == false)))
            {
                if (character == 1)
                {
                    spotlight.transform.position = new Vector3(-15.6f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 4;
                }
                else if (character == 2)
                {
                    spotlight.transform.position = new Vector3(-28.6f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 1;
                }
                else if (character == 3)
                {
                    spotlight.transform.position = new Vector3(-24.2f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 2;
                }
                else if (character == 4)
                {
                    spotlight.transform.position = new Vector3(-20f, spotlight.transform.position.y, spotlight.transform.position.z);
                    character = 3;
                }

                axisInUse = true;
            }

            if (Input.GetKeyDown("1") || Input.GetKeyDown("joystick button 0"))
            {
                player = 1;
                GameObject.Find("Arrow").transform.position = new Vector3(-24.5f, GameObject.Find("Arrow").transform.position.y, GameObject.Find("Arrow").transform.position.z);
            }

            if ((Input.GetKeyDown("2") || Input.GetKeyDown("joystick button 1")) && GameController.playerNum > 0)
            {
                player = 2;
                GameObject.Find("Arrow").transform.position = new Vector3(-20.3f, GameObject.Find("Arrow").transform.position.y, GameObject.Find("Arrow").transform.position.z);
            }

            if ((Input.GetKeyDown("3") || Input.GetKeyDown("joystick button 2")) && GameController.playerNum > 1)
            {
                player = 3;
                GameObject.Find("Arrow").transform.position = new Vector3(-16.3f, GameObject.Find("Arrow").transform.position.y, GameObject.Find("Arrow").transform.position.z);
            }

            if ((Input.GetKeyDown("4") || Input.GetKeyDown("joystick button 3")) && GameController.playerNum > 2)
            {
                player = 4;
                GameObject.Find("Arrow").transform.position = new Vector3(-11.8f, GameObject.Find("Arrow").transform.position.y, GameObject.Find("Arrow").transform.position.z);
            }

            if (Input.GetKeyDown("down") || (Input.GetAxis("menu_vertical") > 0.5))
            {
                // check if the character is already chosen from someone else
                bool abort = false;
                for (int i = 0; i < GameController.charOfPlayer.Length; i++)
                {
                    if (GameController.charOfPlayer[i] == character)
                    {
                        abort = true;
                    }
                }

                if (abort == false)
                {
                    // if the player already chose a character
                    if (GameController.charOfPlayer[player - 1] != 0)
                    {
                        // set this character back to his main position
                        if (chars[GameController.charOfPlayer[player - 1] - 1] != null)
                        {
                            chars[GameController.charOfPlayer[player - 1] - 1].transform.position = charPlatformPos[GameController.charOfPlayer[player - 1] - 1] + charOffset[GameController.charOfPlayer[player - 1] - 1];
                        }
                    }
                    else
                    {
                        // increase the number of players
                        GameController.playerNum++;
                    }

                    // bind the new character to the player
                    GameController.charOfPlayer[player - 1] = character;

                    // save current character to current player 
                    int tempPlayer = player - 1;
                    int tempChar = character - 1;
                    PlayerPrefs.SetInt(tempPlayer.ToString(), tempChar);

                    // set the character to the platform of the player
                    if (chars[character - 1] != null)
                    {
                        chars[character - 1].transform.position = playerPlatformPos[player - 1] + charOffset[character - 1];
                    }
                }
            }

            if ((Input.GetKeyDown("up") == true) || (Input.GetAxisRaw("menu_vertical") < -0.5))
            {
                int tempPlayer = player - 1;

                // if the player already chose a character
                if (GameController.charOfPlayer[tempPlayer] != 0 && chars[GameController.charOfPlayer[tempPlayer] - 1] != null)
                {
                    // set this character back to his main position
                    chars[GameController.charOfPlayer[tempPlayer] - 1].transform.position = charPlatformPos[GameController.charOfPlayer[tempPlayer] - 1] + charOffset[GameController.charOfPlayer[tempPlayer] - 1];

                    // delete the entry
                    GameController.charOfPlayer[tempPlayer] = 0;

                    // decrease the number of players
                    GameController.playerNum--;
                }

                if (GameController.playerNum - tempPlayer > 0)
                {
                    GameController.charOfPlayer[tempPlayer] = GameController.charOfPlayer[tempPlayer + 1];
                    GameController.charOfPlayer[tempPlayer + 1] = 0;
                }

                if (GameController.playerNum - tempPlayer > 1)
                {
                    GameController.charOfPlayer[tempPlayer + 1] = GameController.charOfPlayer[tempPlayer + 2];
                    GameController.charOfPlayer[tempPlayer + 2] = 0;
                }

                if (GameController.playerNum - tempPlayer > 2)
                {
                    GameController.charOfPlayer[tempPlayer + 2] = GameController.charOfPlayer[tempPlayer + 3];
                    GameController.charOfPlayer[tempPlayer + 3] = 0;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (GameController.charOfPlayer[i] != 0)
                    {
                        chars[GameController.charOfPlayer[i] - 1].transform.position = playerPlatformPos[i] + charOffset[GameController.charOfPlayer[i] - 1];
                    }

                    PlayerPrefs.SetInt(i.ToString(), GameController.charOfPlayer[i] - 1);
                }
            }

            if (Input.GetKeyDown("return") || Input.GetKeyDown("joystick button 7"))
            {
                if (GameController.playerNum > 0)
                {
                    Application.LoadLevel((int)Constants.Levels.STAGE_SELECT);
                }
            }

            if (Input.GetKeyDown("escape") || Input.GetKeyDown("joystick button 6"))
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }

            if ((Input.GetAxisRaw("menu_horizontal") < 0.5) && (Input.GetAxisRaw("menu_horizontal") > -0.5) && (Input.GetAxisRaw("menu_vertical") < 0.5) && (Input.GetAxisRaw("menu_vertical") > -0.5))
            {
                axisInUse = false;
            }
        }

        /// <summary>
        /// This creates the buttons for going back and forward in the menu.
        /// </summary>
        public void OnGUI()
        {
            if (GUI.Button(new Rect(Screen.width - 80, Screen.height - 30, 80, 30), "Next") && GameController.playerNum > 0)
            {
                Application.LoadLevel((int)Constants.Levels.STAGE_SELECT);
            }

            if (GUI.Button(new Rect(0, Screen.height - 30, 80, 30), "Back"))
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }

            GUILayout.BeginArea(new Rect(10, Screen.height / 2 - Screen.height / 10, Screen.width / 6, Screen.height / 2));

            if (Screen.width < 500)
            {
                style.fontSize = 8;
            }
            else if (Screen.width < 1200)
            {
                style.fontSize = 10;
            }
            else if (Screen.width < 1900)
            {
                style.fontSize = 20;
            }
            else if (Screen.width < 2600)
            {
                style.fontSize = 26;
            }

            style.wordWrap = true;
            GUILayout.Label("Select a character with the left and right arrow keys and confirm/ reset with the down/ up key.\n", style);
            GUILayout.Label("Select a player with the number keys 1 to 4.", style);
            GUILayout.EndArea();
        }
    }
}
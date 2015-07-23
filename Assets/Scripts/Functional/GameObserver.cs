using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is created every time when a level starts and observes all actions of the players. It controls the spawning, dying and respawning of all players.
    /// </summary>
    public class GameObserver : MonoBehaviour
    {
        /// <summary>
        /// Says which player chose which character to play.
        /// </summary>
        public GameObject[] players;

        /// <summary>
        /// In this string, the character names are saved.
        /// </summary>
        private string[] charNames = new string[4] { "Harry", "Punkry", "Kirb", "Captain 'Murricah" };

        /// <summary>
        /// Contains the GameObjects of all chosen characters.
        /// </summary>
        private GameObject[] characters;

        /// <summary>
        /// Contains the PictureTextures for the battle gui of all chosen characters.
        /// </summary>
        public Texture[] guiPictures;

        /// <summary>
        /// Contains the scripts of all currently playing characters.
        /// </summary>
        private BasicCharacter[] scripts;

        /// <summary>
        /// True for a player when he is playing and not dead or respawning
        /// </summary>
        private bool[] inGame;

        /// <summary>
        /// The personal cameras for every player which are necessary to zoom in.
        /// </summary>
        private Camera[] cams;

        /// <summary>
        /// A camera which shows close-up views of the characters at the beginning of the current match.
        /// </summary>
        public Camera miniCam;

        /// <summary>
        /// The main camera which shows the level and all of the characters during the game.
        /// </summary>
        public Camera mainCam;

        /// <summary>
        /// Contains the spawn points for all characters.
        /// </summary>
        private Vector3[] spawnPoints;

        /// <summary>
        /// Contains the script for the GUI which is shown during the game to be able to update the screen.
        /// </summary>
        public FirstGui gui;

        /// <summary>
        /// Contains the GameObject of the wings which appear when a character dies.
        /// </summary>
        public GameObject wings;

        /// <summary>
        /// Tells whether the game is currently in development mode.
        /// </summary>
        private bool devMode = false;

        /// <summary>
        /// The bottom border of the level. When being below, the character dies.
        /// </summary>
        private const float DOWNLIMIT = -100f;

        /// <summary>
        /// Object array for respawn points.
        /// </summary>
        public GameObject[] points;

        /// <summary>
        /// The invincibility rate.
        /// </summary>
        private const float APPEARANCE_RATE = 0.3f;

        /// <summary>
        /// The invincibility duration.
        /// </summary>
        public const float INVINCIBILITY_DURATION = 15f;

        /// <summary>
        /// Time the wings stay at the character after respawn in seconds
        /// </summary>
        private const float WINGS_STAY_TIME = 2f;

        /// <summary>
        /// Time in seconds until a character is respawned.
        /// </summary>
        public const float RESPAWN_DELAY = 3f;

        /// <summary>
        /// Initializes the spawn points and spawns the players.
        /// </summary>
        public void Start()
        {
            if (GameController.playerNum == 0)
            {
                devMode = true;
            }

            getSpawnPoints();
            spawnPlayers();

            gui = (FirstGui)this.GetComponent(typeof(FirstGui));

            points = new GameObject[] { GameObject.Find("point1"), GameObject.Find("respawn_ground"), GameObject.Find("point3"), GameObject.Find("point4") };

            // Allocation for invisibility
            points[0].GetComponent<MeshRenderer>().enabled = false;
            points[0].collider.isTrigger = true;
            points[1].GetComponent<MeshRenderer>().enabled = false;
            points[1].collider.isTrigger = true;
            points[2].GetComponent<MeshRenderer>().enabled = false;
            points[2].collider.isTrigger = true;
            points[3].GetComponent<MeshRenderer>().enabled = false;
            points[3].collider.isTrigger = true;
        }

        /// <summary>
        /// Update is called once per frame and provides the respawn process and the camera zoom.
        /// </summary>
        public void Update()
        {
            if (GameController.isPlaying)
            {
                // Check if a player has to be killed
                checkDeath();

                // Update characters
                CameraZoom zoom = mainCam.GetComponent<CameraZoom>();
                zoom.setBounds(characters);
            }
        }

        /// <summary>
        /// Updates the damage inside the GUI.
        /// </summary>
        /// <param name="id">Wich Player is hit?</param>
        /// <param name="newWeight">The new damage value of the player.</param>
        public void updateGUI(int id, int newWeight)
        {
            gui.damage[id] = newWeight;
        }

        /// <summary>
        /// Used to get the spawn points from the current level
        /// </summary>
        private void getSpawnPoints()
        {
            spawnPoints = new Vector3[4];
            switch ((int)LevelSelection.level)
            {
                case 1:
                    spawnPoints = Constants.SpawnPoint[(int)Constants.Levels.DEMO_LEVEL];
                    break;
                case 2:
                    spawnPoints = Constants.SpawnPoint[(int)Constants.Levels.SCHOOL_LEVEL];
                    break;
            }
        }

        /// <summary>
        /// Spawns the players by starting their introduction.
        /// </summary>
        private void spawnPlayers()
        {
            characters = new GameObject[GameController.playerNum];
            scripts = new BasicCharacter[GameController.playerNum];
            cams = new Camera[GameController.playerNum];
            inGame = new bool[GameController.playerNum];

            if (devMode)
            {
                initDevMode();
            }
            else
            {
                StartCoroutine(intro());
            }
        }

        /// <summary>
        /// Initializes the level in development mode without the introduction and menu.
        /// </summary>
        private void initDevMode()
        {
            const int PLAYER_COUNT = 2;
            GameController.playerNum = PLAYER_COUNT;
            devMode = true;
            characters = new GameObject[PLAYER_COUNT];
            scripts = new BasicCharacter[PLAYER_COUNT];

            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                // grab saved characternumber for each player
                int charNr = PlayerPrefs.GetInt(i.ToString());
                spawnPlayer(i, charNr);
            }

            GameController.isPlaying = true;
        }

        /// <summary>
        /// Plays the introduction animations.
        /// </summary>
        /// <returns>The seconds to wait in the coroutine.</returns>
        private IEnumerator intro()
        {
            for (int i = 0; i < GameController.playerNum; i++)
            {
                // grab saved characternumber for each player
                int charNr = PlayerPrefs.GetInt(i.ToString());
                spawnPlayer(i, charNr);

                // Introduces the player
                cams[i] = (Camera)Instantiate(
                      miniCam,
                      new Vector3(spawnPoints[i].x, spawnPoints[i].y + 10, spawnPoints[i].z - 50),
                      Quaternion.Euler(0f, 0f, 0f));
                mainCam.gameObject.SetActive(false);
                cams[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                characters[i].gameObject.playAnimationIfExists("Provocation");
                characters[i].gameObject.playSoundIfExists(scripts[i].introSound, 1f);
                yield return new WaitForSeconds(10f);
                cams[i].gameObject.SetActive(false);
                mainCam.gameObject.SetActive(true);
            }

            gui.countdown = 6;
            for (int i = 0; i < 8; i++)
            {
                gui.countdown--;
                yield return new WaitForSeconds(2f);
            }

            GameController.isPlaying = true;
        }

        /// <summary>
        /// Spawns the player's character.
        /// </summary>
        /// <param name='playerNr'>
        /// The player's ID.
        /// </param>
        /// <param name="charNr">
        /// The Character ID
        /// </param>
        private void spawnPlayer(int playerNr, int charNr)
        {
            Debug.Log(playerNr.ToString());
            Debug.Log(charNr.ToString());
            if (charNr == 2)
            {
                characters[playerNr] = (GameObject)Instantiate(
                players[charNr],
                spawnPoints[playerNr],
                Quaternion.Euler(180f, 90f, 0f));
            }
            else
            {
                characters[playerNr] = (GameObject)Instantiate(
                    players[charNr],
                    spawnPoints[playerNr],
                    Quaternion.Euler(0f, 90f, 0f));
            }
            FirstGui.playerPictures[playerNr] = guiPictures[charNr];
            FirstGui.playerNames[playerNr] = charNames[charNr];
            characters[playerNr].GetComponent<BasicCharacter>().playerID = playerNr + 1;
            scripts[playerNr] = (BasicCharacter)characters[playerNr].GetComponent<BasicCharacter>();
            scripts[playerNr].observer = this;

            inGame[playerNr] = true;
        }

        /// <summary>
        /// Checks whether one of the players is to be killed
        /// </summary>
        private void checkDeath()
        {
            for (int i = 0; i < GameController.playerNum; i++)
            {
                if (isFallingOutside(i) && inGame[i])
                {
                    killPlayer(i);
                    StartCoroutine(RespawnPlayer(i));
                }
            }
        }

        /// <summary>
        /// Kills the player identified by id and updates the scores/deaths
        /// </summary>
        /// <param name="id">The player id to kill</param>
        private void killPlayer(int id)
        {
            // KILL!
            scripts[id].state = BasicCharacter.DEAD;
            inGame[id] = false;

            // Play die sound
            characters[id].gameObject.playSoundIfExists(scripts[id].dieSound);

            // Set deadWitdh
            scripts[id].deadWidth = Mathf.Max(Mathf.Abs(characters[id].gameObject.transform.position.x), scripts[id].deadWidth);
            this.gui.deathDistance[id] = scripts[id].deadWidth;

            // Update stats
            int enemy = this.scripts[id].lastHitBy - 1;
            if (enemy != id)
            {
                gui.score[enemy]++;
            }
            else
            {
                gui.score[id]--;
            }

            gui.deads[id]++;
        }

        /// <summary>
        /// Waits for <c>RESPAWN_DELAY</c> seconds, allocates the respawn-point to the player who died,
        /// plays the respawn sound, adds (and removes) the wings and resets the player properties
        /// (like weight factor, velocity, etc)
        /// </summary>
        /// <param name="id">The id of the player who died.</param>
        /// <returns>The invinciblity coroutine</returns>
        public IEnumerator RespawnPlayer(int id)
        {
            // Wait for respawn delay
            yield return new WaitForSeconds(RESPAWN_DELAY * Time.timeScale);

            // Play respawn sound
            characters[id].gameObject.playSoundIfExists(scripts[id].respawnSound);

            // Reset players rotation
            scripts[id].resetThrowRotation();

            // Add wings to character
            float[] wingsPlayerGap;
            wingsPlayerGap = new float[]
        {
            14f, 8f, 8f, 16f, 0f, 0f
        };
            points[id].GetComponent<MeshRenderer>().enabled = true;
            points[id].collider.isTrigger = false;
            scripts[id].transform.position = points[id].gameObject.transform.position + Vector3.up * 15;

            var instance = (GameObject)Instantiate(wings, new Vector3(scripts[id].transform.position.x, scripts[id].transform.position.y + wingsPlayerGap[GameController.charOfPlayer[id] - 1], scripts[id].transform.position.z), Quaternion.Euler(0, 90, 0));
            var wingTransform = instance.transform;
            wingTransform.parent = scripts[id].transform; // make the wings to a child of the character

            // Make characters wings invisible
            StartCoroutine(TurnWingsInvisible(id));

            // Reset character properties
            this.scripts[id].setWeightFactor(1f);
            this.characters[id].rigidbody.velocity = new Vector3(0f, 0f, 0f);
            updateGUI(id, 100);

            // Start invincibility mode
            StartCoroutine(InvincibleBlink(this.characters[id]));
        }

        /// <summary>
        /// Detects the point where the player dies and saves it. 
        /// </summary>
        /// <param name="id">The id of the player who is falling out.</param>
        /// <returns>True if the player's position is below the <c>DOWNLIMIT</c> of the level.</returns>
        public bool isFallingOutside(int id)
        {
            // The limit is only the lower bound of the level
            return scripts[id].transform.position.y <= DOWNLIMIT;
        }

        /// <summary>
        /// Makes respawnpoints invisible.
        /// </summary>
        /// <param name="id">The id of the player and of the respawn point.</param>
        /// <returns>The number of seconds to wait in the coroutine.</returns>
        private IEnumerator TurnWingsInvisible(int id)
        {
            yield return new WaitForSeconds(WINGS_STAY_TIME * Time.timeScale);
            points[id].collider.isTrigger = true;
            points[id].GetComponent<MeshRenderer>().enabled = false;
            Destroy(GameObject.Find("wings_animated(Clone)"));
        }

        /// <summary>
        /// Makes the Gameobject blinking.
        /// </summary>
        /// <param name="waitTime">Says how long the GameObject will blink.</param>
        /// <returns>The seconds to wait in the coroutine.</returns>
        private IEnumerator flash(float waitTime)
        {
            while (Time.deltaTime < waitTime)
            {
                characters[0].transform.renderer.enabled = false;

                yield return new WaitForSeconds(0.2f);

                characters[0].transform.renderer.enabled = true;

                yield return new WaitForSeconds(0.2f);
            }
        }

        /// <summary>
        /// The player blinks for a short time and is invincible after respawn.
        /// </summary>
        /// <param name="player">The player who is blinking.</param>
        /// <returns>The seconds to wait in the coroutine.</returns>
        public IEnumerator InvincibleBlink(GameObject player)
        {
            float invincibilityCounter = 0;
            var playerScript = (BasicCharacter)player.GetComponent<BasicCharacter>();
            playerScript.state = BasicCharacter.INVINCIBLE;
            while (invincibilityCounter <= INVINCIBILITY_DURATION)
            {
                var renderers = player.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    r.enabled = false;
                }

                yield return new WaitForSeconds(APPEARANCE_RATE);
                var renderers2 = player.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers2)
                {
                    r.enabled = true;
                }

                yield return new WaitForSeconds(APPEARANCE_RATE);
                invincibilityCounter++;
            }

            playerScript.state = BasicCharacter.READY;

            // Player is back in game and can die!
            inGame[playerScript.playerID - 1] = true;
        }
    }
}
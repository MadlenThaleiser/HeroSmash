using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is being created as a singleton in the main menu and exists during the whole game.
    /// It saves the information which player chose which character.
    /// It is used by the character selection scene and the level which is being played.
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        /// <summary>
        /// Says which character belongs to which player.
        /// </summary>
        public static int[] charOfPlayer;

        /// <summary>
        /// Saves the number of players who chose a character and how many will be able to play.
        /// </summary>
        public static int playerNum;

        /// <summary>
        /// Says if the game is running.
        /// </summary>
        public static bool isPlaying = false;

        /// <summary>
        /// Creates the singleton properties for the class by setting the own instance to null.
        /// </summary>
        private static GameController instance = null;

        /// <summary>
        /// Creates the singleton properties for the class by returning the own instance.
        /// </summary>
        public static GameController Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Destroys all new instances of the class to retain the singleton properties.
        /// </summary>
        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
        }

        /// <summary>
        /// Speeds up the game by the factor specified in <c>AnimationHelper.TIME_SCALE</c> and initializes the variables <c>playerNum</c> and <c>charOfPlayer</c>.
        /// </summary>
        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            Time.timeScale = AnimationHelper.TIME_SCALE;
            playerNum = 0;
            charOfPlayer = new int[] { 0, 0, 0, 0 };
        }
    }
}
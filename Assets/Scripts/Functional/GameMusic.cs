using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is playing the game music as long as the game is running. It is being instantiated in the main menu and exists all the time.
    /// The music to be played is set in each scene script in the Start-method.
    /// </summary>
    public class GameMusic : MonoBehaviour
    {
        /// <summary>
        /// Audio clips for each situation with different music.
        /// </summary>
        public AudioClip menuSound, creditSound, gameSound;

        /// <summary>
        /// Enum types for all situations with different music.
        /// </summary>
        public enum Screen
        {
            MENU, INGAME, OPTIONS
        }

        /// <summary>
        /// Saves the topical situation for the music which has to be chosen.
        /// </summary>
        public static Screen topical;

        /// <summary>
        /// Says which sound is already playing.
        /// </summary>
        private Screen playing;

        /// <summary>
        /// Creates the singleton properties for the class by setting the own instance to null.
        /// </summary>
        private static GameMusic instance = null;

        /// <summary>
        /// Creates the singleton properties for the class by returning the own instance.
        /// </summary>
        public static GameMusic Instance
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

            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Update is called once per frame and changes the music if a new situation was reached and the variable <c>topical</c> changed.
        /// </summary>
        public void Update()
        {
            // if necessary stop the current sound and play a new one
            if (topical == Screen.OPTIONS && playing != Screen.OPTIONS)
            {
                this.audio.Stop();
                this.audio.clip = creditSound;
                this.audio.Play();
                playing = Screen.OPTIONS;
            }

            if (topical == Screen.MENU && playing != Screen.MENU)
            {
                this.audio.Stop();
                this.audio.clip = menuSound;
                this.audio.Play();
                playing = Screen.MENU;
            }

            if (topical == Screen.INGAME && playing != Screen.INGAME)
            {
                this.audio.Stop();
                this.audio.clip = gameSound;
                this.audio.Play();
                playing = Screen.INGAME;
            }
        }
    }
}
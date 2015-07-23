using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is being created every time when a level starts. It shows the GUI when playing the game by creating and changing the fields for the scores, deads and the time.
    /// </summary>
    public class FirstGui : MonoBehaviour
    {
        /// <summary>
        /// Saves the Death distances.
        /// </summary>
        public float[] deathDistance;

        /// <summary>
        /// The original height which is being changed by the matrix to offer resolution independence.
        /// </summary>
        private float oHeight;

        /// <summary>
        /// The original width which is being changed by the matrix to offer resolution independence.
        /// </summary>
        private float oWidth;

        /// <summary>
        /// Array for the current player damage values.
        /// </summary>
        public int[] damage = new int[4] { 100, 100, 100, 100 };

        /// <summary>
        /// Array for the font colors which are changed dependent on the countdown.
        /// </summary>
        private Color[] color = new Color[7] { Color.red, Color.yellow, Color.magenta, Color.green, Color.cyan, Color.blue, Color.white };

        /// <summary>
        /// Saves the Character Names for the GUI;
        /// </summary>
        public static string[] playerNames = new string[4] { "", "", "", "" };

        /// <summary>
        /// Array for currents deads of each player.
        /// </summary>
        public int[] deads = new int[4] { 0, 0, 0, 0 };

        /// <summary>
        /// Current countdown value.
        /// </summary>
        public int countdown = 0;

        /// <summary>
        /// Array for current score values.
        /// </summary>
        public int[] score = new int[4] { 0, 0, 0, 0 };

        /// <summary>
        /// Style for font manipulation.
        /// </summary>
        public GUIStyle style;

        /// <summary>
        /// Pictures for the battle gui 
        /// </summary>
        public static Texture[] playerPictures = new Texture[4];

        /// <summary>
        /// The width of the label which shows the time.
        /// </summary>
        private const float TIMER_LABEL_WIDTH = 20f;

        /// <summary>
        /// The height of the label which shows the time.
        /// </summary>
        private const float TIMER_LABEL_HEIGHT = 20f;

        /// <summary>
        /// The length of a single match in seconds.
        /// </summary>
        private const float BATTLE_LENGTH = 180f;

        /// <summary>
        /// The position of the label which shows the time.
        /// </summary>
        private Rect battleTimerLabelRect = new Rect(
                730,
                TIMER_LABEL_HEIGHT / 2,
                TIMER_LABEL_WIDTH,
                TIMER_LABEL_HEIGHT);

        /// <summary>
        /// Says which amount of time is left for the current match.
        /// </summary>
        private float battleCounter;

        /// <summary>
        /// Says the counter when to start counting.
        /// </summary>
        private bool timeToStart;

        /// <summary>
        /// The style of the GUI label for the time.
        /// </summary>
        public GUIStyle timerStyle;

        /// <summary>
        /// The winner of the current battle.
        /// </summary>
        private int battleWinner = 0;

        /// <summary>
        /// The width for the score screen at the end of the match.
        /// </summary>
        private const float SCORE_SCREEN_WIDTH = 700f;

        /// <summary>
        /// The height for the score screen at the end of the match.
        /// </summary>
        private const float SCORE_SCREEN_HEIGHT = 550f;

        /// <summary>
        /// The font size for the score screen at the end of the match.
        /// </summary>
        private const int SCORE_SCREEN_FONTSIZE = 20;

        /// <summary>
        /// The position for the score screen at the end of the match.
        /// </summary>
        private Rect scoreScreenRect = new Rect(
            425,
            100,
            SCORE_SCREEN_WIDTH,
            SCORE_SCREEN_HEIGHT);

        /// <summary>
        /// The id for the score screen at the end of the match.
        /// </summary>
        private const int SCORE_WINDOW_ID = 1;

        /// <summary>
        /// The window style for the score screen at the end of the match.
        /// </summary>
        public GUIStyle scoreScreenWindowStyle;

        /// <summary>
        /// The text style for the score screen at the end of the match.
        /// </summary>
        public GUIStyle scoreScreenTextStyle;

        /// <summary>
        /// Says that the match is over and that the score screen can be shown.
        /// </summary>
        private bool showScreen;

        /// <summary>
        /// The width of the labels which show the current play data and scores of each player.
        /// </summary>
        private const float PLAYER_SCORE_LABEL_WIDTH = 100f;

        /// <summary>
        /// The height of the labels which show the current play data and scores of each player.
        /// </summary>
        private const float PLAYER_SCORE_HEIGHT = 50f;

        /// <summary>
        /// The horizontal alignment of the labels which show the current play data and scores of each player.
        /// </summary>
        private const float PLAYER_SCORE_HORIZONTAL_ALIGNMENT = 50f;

        /// <summary>
        /// The vertical alignment of the labels which show the current play data and scores of each player.
        /// </summary>
        private const float PLAYER_SCORE_VERTICAL_ALIGNMENT = 50f;

        /// <summary>
        /// The vertical gap between the labels which show the current play data and scores of each player.
        /// </summary>
        private const float VERTICAL_GAP_BETWEEN_LABELS = 70f;

        /// <summary>
        /// The horizontal gap between the labels which show the current play data and scores of each player.
        /// </summary>
        private const float HORIZONTAL_GAP_BETWEEN_LABELS = 150f;

        /// <summary>
        /// The horizontal alignment for the score screen buttons at the end of the match.
        /// </summary>
        private const float MENU_BUTTON_HORIZONTAL_ALIGNMENT = 50f;

        /// <summary>
        /// The vertical alignment for the score screen buttons at the end of the match.
        /// </summary>
        private const float MENU_BUTTON_VERTICAL_ALIGNMENT = 450f;

        /// <summary>
        /// The width for the score screen buttons at the end of the match.
        /// </summary>
        private const float MENU_BUTTON_WIDTH = 150f;

        /// <summary>
        /// The height for the score screen buttons at the end of the match.
        /// </summary>
        private const float MENU_BUTTON_HEIGHT = 40f;

        /// <summary>
        /// The horizontal gap between the score screen buttons at the end of the match.
        /// </summary>
        private const float HORIZONTAL_GAP_BETWEEN_BUTTONS = 220f;

        /// <summary>
        /// The position of the box for the player data and scores of the first player.
        /// </summary>
        private Rect playerOneScoreLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS * 2,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the box for the player data and scores of the second player.
        /// </summary>
        private Rect playerTwoScoreLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS * 3,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the box for the player data and scores of the third player.
        /// </summary>
        private Rect playerThreeScoreLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS * 4,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the box for the player data and scores of the fourth player.
        /// </summary>
        private Rect playerFourScoreLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS * 5,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the label for the scores of each player.
        /// </summary>
        private Rect scoreLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT + HORIZONTAL_GAP_BETWEEN_LABELS,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the label for the deads of each player.
        /// </summary>
        private Rect deadsLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT + HORIZONTAL_GAP_BETWEEN_LABELS * 2,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the label for the farthest dead flight of each player.
        /// </summary>
        private Rect flightLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT + HORIZONTAL_GAP_BETWEEN_LABELS * 3,
                PLAYER_SCORE_VERTICAL_ALIGNMENT + VERTICAL_GAP_BETWEEN_LABELS,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the label for the winner in the score screen at the end of the match.
        /// </summary>
        private Rect winnerLabelRect = new Rect(
            PLAYER_SCORE_HORIZONTAL_ALIGNMENT + HORIZONTAL_GAP_BETWEEN_LABELS,
                PLAYER_SCORE_VERTICAL_ALIGNMENT,
                PLAYER_SCORE_LABEL_WIDTH,
                PLAYER_SCORE_HEIGHT);

        /// <summary>
        /// The position of the battle again button in the score screen at the end of the match.
        /// </summary>
        private Rect battleAgainButtonRect = new Rect(
            MENU_BUTTON_HORIZONTAL_ALIGNMENT,
                MENU_BUTTON_VERTICAL_ALIGNMENT,
                MENU_BUTTON_WIDTH,
                MENU_BUTTON_HEIGHT);

        /// <summary>
        /// The position of the main menu button in the score screen at the end of the match.
        /// </summary>
        private Rect mainMenuButtonRect = new Rect(
            MENU_BUTTON_HORIZONTAL_ALIGNMENT + HORIZONTAL_GAP_BETWEEN_BUTTONS * 2,
                MENU_BUTTON_VERTICAL_ALIGNMENT,
                MENU_BUTTON_WIDTH,
                MENU_BUTTON_HEIGHT);

        /// <summary>
        /// The position of the character selection button in the score screen at the end of the match.
        /// </summary>
        private Rect characterSelectionButtonRect = new Rect(
            MENU_BUTTON_HORIZONTAL_ALIGNMENT + HORIZONTAL_GAP_BETWEEN_BUTTONS,
                MENU_BUTTON_VERTICAL_ALIGNMENT,
                MENU_BUTTON_WIDTH,
                MENU_BUTTON_HEIGHT);

        /// <summary>
        /// The skin for the GUI.
        /// </summary>
        public GUISkin firstGuiSkin;

        /// <summary>
        /// Initializes variables like <c>countdown</c>, <c>timeToStart</c>, <c>battleCounter</c> and <c>showScreen</c> and sets the music to the in-game sound.
        /// </summary>
        public void Start()
        {
            deathDistance = new float[4] { 0, 0, 0, 0 };
            countdown = 0;
            GameMusic.topical = GameMusic.Screen.INGAME;
            timeToStart = false;
            battleCounter = BATTLE_LENGTH;
            showScreen = false;
        }

        /// <summary>
        /// Checks if the escape button is pressed for leaving the match and counts down the time of the <c>battleCounter</c>.
        /// </summary>
        public void Update()
        {
            if (timeToStart)
            {
                battleCounter -= Time.deltaTime / Time.timeScale;

                // End of the countdown
                if (battleCounter <= 0)
                {
                    battleCounter = 0;
                    showScreen = true;
                }
            }
        }

        /// <summary>
        /// Called Whenever GUI is updated and shows the GUI boxes with the player information.
        /// </summary>
        public void OnGUI()
        {
            oHeight = 730;
            oWidth = 1600;

            GUI.skin = firstGuiSkin;

            Vector2 ratio = new Vector2(Screen.width / oWidth, Screen.height / oHeight);
            Matrix4x4 guiMatrix = Matrix4x4.identity;
            guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
            GUI.matrix = guiMatrix;

            intro();

            battleGUI();

            battleTimer();

            // If the Battle End Condition is met, show the Score Screen
            if ((showScreen || Input.GetKeyDown("escape")) && timeToStart)
            {
                if (battleCounter > 0)
                {
                    showScreen = true;
                    battleCounter = 0;
                }

                // The characters don't move anymore
                GameController.isPlaying = false;
                scoreScreenRect = GUI.Window(SCORE_WINDOW_ID, scoreScreenRect, ScoreScreen, "BATTLE RESULT");
            }

            GUI.matrix = Matrix4x4.identity;
        }

        /// <summary>
        /// Updates the timer label on the screen.
        /// </summary>
        private void battleTimer()
        {
            // Timer Style
            timerStyle.normal.textColor = Color.yellow;
            timerStyle.fontSize = 50;

            // Minute:Seconds Format
            float minutes = Mathf.Floor(battleCounter / 60);
            float seconds = Mathf.Floor(battleCounter % 60);

            GUI.Label(battleTimerLabelRect, minutes.ToString("00") + ":" + seconds.ToString("00"), timerStyle);
        }

        /// <summary>
        /// Renders the GUI for the battle.
        /// </summary>
        private void battleGUI()
        {
            if (GameController.isPlaying)
            {
                // Begin UI Player1
                GUI.BeginGroup(new Rect(100, 590, 200, 60));
                GUI.Box(new Rect(0, 0, 200, 140), playerNames[0] + ":");
                style.fontSize = 20;
                style.normal.textColor = Color.green;
                GUI.Label(new Rect(20, 25, 50, 35), " " + score[0], style);
                style.normal.textColor = Color.red;
                GUI.Label(new Rect(80, 25, 50, 35), " " + deads[0], style);
                style.normal.textColor = Color.white;
                GUI.Label(new Rect(125, 25, 75, 35), " " + damage[0] + "%", style);
                GUI.EndGroup();
                GUI.DrawTexture(new Rect(100, 650, 200, 60), playerPictures[0], ScaleMode.StretchToFill);

                // Begin UI Player2
                if (GameController.playerNum > 1)
                {
                    GUI.BeginGroup(new Rect(500, 590, 200, 60));
                    GUI.Box(new Rect(0, 0, 200, 140), playerNames[1] + ":");
                    style.fontSize = 20;
                    style.normal.textColor = Color.green;
                    GUI.Label(new Rect(20, 25, 50, 35), " " + score[1], style);
                    style.normal.textColor = Color.red;
                    GUI.Label(new Rect(80, 25, 50, 35), " " + deads[1], style);
                    style.normal.textColor = Color.white;
                    GUI.Label(new Rect(135, 25, 75, 35), " " + damage[1] + "%", style);
                    GUI.EndGroup();
                    GUI.DrawTexture(new Rect(500, 650, 200, 60), playerPictures[1], ScaleMode.StretchToFill);
                }

                // Begin UI Player3
                if (GameController.playerNum > 2)
                {
                    GUI.BeginGroup(new Rect(900, 590, 200, 60));
                    GUI.Box(new Rect(0, 0, 200, 140), playerNames[2] + ":");
                    style.fontSize = 20;
                    style.normal.textColor = Color.green;
                    GUI.Label(new Rect(20, 25, 50, 35), " " + score[2], style);
                    style.normal.textColor = Color.red;
                    GUI.Label(new Rect(80, 25, 50, 35), " " + deads[2], style);
                    style.normal.textColor = Color.white;
                    GUI.Label(new Rect(135, 25, 75, 35), " " + damage[2] + "%", style);
                    GUI.EndGroup();
                    GUI.DrawTexture(new Rect(900, 650, 200, 60), playerPictures[2], ScaleMode.StretchToFill);
                }

                // Begin UI Player4
                if (GameController.playerNum > 3)
                {
                    GUI.BeginGroup(new Rect(1300, 590, 400, 60));
                    GUI.Box(new Rect(0, 0, 200, 140), playerNames[3] + ":");
                    style.fontSize = 20;
                    style.normal.textColor = Color.green;
                    GUI.Label(new Rect(20, 25, 50, 35), " " + score[3], style);
                    style.normal.textColor = Color.red;
                    GUI.Label(new Rect(80, 25, 50, 35), " " + deads[3], style);
                    style.normal.textColor = Color.white;
                    GUI.Label(new Rect(135, 25, 75, 35), " " + damage[3] + "%", style);
                    GUI.EndGroup();
                    GUI.DrawTexture(new Rect(1300, 650, 200, 60), playerPictures[3], ScaleMode.StretchToFill);
                }
            }
        }

        /// <summary>
        /// Renders the countdown for the introduction.
        /// </summary>
        private void intro()
        {
            style.fontSize = 50;
            if (countdown > 0)
            {
                style.normal.textColor = color[countdown];
                GUI.Label(new Rect(750, 50, 200, 35), "" + countdown, style);
            }

            if (countdown == -1)
            {
                style.normal.textColor = color[0];
                GUI.Label(new Rect(750, 50, 200, 35), "" + "GO", style);
                timeToStart = true;
            }

            style.normal.textColor = Color.white;
        }

        /// <summary>
        /// Initializes the score screen by calling <see cref="initLabel"/> and <see cref="initButton"/>.
        /// </summary>
        /// <param name="id">The screen id (is needed by the screen but not used).</param>
        private void ScoreScreen(int id)
        {
            // Score Screen Labels
            initLabel();

            // Menu Buttons
            initButton();
        }

        /// <summary>
        /// Initializes the score screen labels.
        /// </summary>
        private void initLabel()
        {
            scoreScreenTextStyle.normal.textColor = Color.white;
            scoreScreenTextStyle.fontSize = SCORE_SCREEN_FONTSIZE;

            // Winner Label
            battleWinner = whoWon(damage, deads, score);
            if (battleWinner != 0)
            {
                GUI.Label(winnerLabelRect, "            Player : " + battleWinner + " won", scoreScreenTextStyle);
            }
            else
            {
                GUI.Label(winnerLabelRect, "                 Draw", scoreScreenTextStyle);
            }

            // Score and Deads Labels
            GUI.Label(scoreLabelRect, "Player Score", scoreScreenTextStyle);
            GUI.Label(deadsLabelRect, "Player Deaths", scoreScreenTextStyle);
            GUI.Label(flightLabelRect, "Player Flight", scoreScreenTextStyle);

            // Shows First Player Score
            GUI.Label(
                playerOneScoreLabelRect,
                playerNames[0] + " :           " + score[0] + "                       " + deads[0] + "                       " + deathDistance[0],
                scoreScreenTextStyle);

            // Shows Second Player Score
            if (GameController.playerNum > 1)
            {
                GUI.Label(
                    playerTwoScoreLabelRect,
                    playerNames[1] + " :           " + score[1] + "                       " + deads[1] + "                       " + deathDistance[1],
                    scoreScreenTextStyle);
            }

            // Shows Third Player Score
            if (GameController.playerNum > 2)
            {
                GUI.Label(
                    playerThreeScoreLabelRect,
                    playerNames[2] + " :           " + score[2] + "                       " + deads[2] + "                       " + deathDistance[2],
                    scoreScreenTextStyle);
            }

            // Shows Fourth Player Score
            if (GameController.playerNum > 3)
            {
                GUI.Label(
                    playerFourScoreLabelRect,
                    playerNames[3] + " :           " + score[3] + "                       " + deads[3] + "                       " + deathDistance[3],
                    scoreScreenTextStyle);
            }
        }

        /// <summary>
        /// Initializes the battle end buttons and their functions.
        /// </summary>
        private void initButton()
        {
            var battleAgainButton = GUI.Button(battleAgainButtonRect, "Battle Again");
            var characterSelectionButton = GUI.Button(characterSelectionButtonRect, "Character Selection");
            var mainMenuButton = GUI.Button(mainMenuButtonRect, "Main Menu");

            if (battleAgainButton)
            {
                Application.LoadLevel(Application.loadedLevel);
            }

            if (characterSelectionButton)
            {
                Application.LoadLevel((int)Constants.Levels.CHARACTER_SELECTION);
            }

            if (mainMenuButton)
            {
                Application.LoadLevel((int)Constants.Levels.MAIN_MENU);
            }
        }

        /// <summary>
        /// Decides who won the battle depending on the game results.
        /// </summary>
        /// <param name="damagesHere">The damages of all players.</param>
        /// <param name="deadsHere">The number of deads of all players.</param>
        /// <param name="scoresHere">The scores of all players.</param>
        /// <returns>The number of the player who won.</returns>
        private int whoWon(int[] damagesHere, int[] deadsHere, int[] scoresHere)
        {
            List<int> winners = new List<int>();
            for (int i = 0; i < GameController.playerNum; i++)
            {
                winners.Add(i);
            }

            // Score
            int winnerScore = scoresHere.Max();
            winners = winners.FindAll(delegate(int player) { return scoresHere[player] == winnerScore; });

            // Dead
            if (winners.Count() > 1)
            {
                int winnerDead = deadsHere.Min();
                winners = winners.FindAll(delegate(int player) { return deadsHere[player] == winnerDead; });
            }

            // Damage
            if (winners.Count() > 1)
            {
                int winnerDamage = damagesHere.Min();
                winners = winners.FindAll(delegate(int player) { return damagesHere[player] == winnerDamage; });
            }

            switch (winners.Count())
            {
                case 1:
                    // Winner!
                    return winners[0] + 1;  // index to player number

                default:
                    // Draw
                    return 0;
            }
        }
    }
}
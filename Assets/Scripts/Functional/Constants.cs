using System;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is a collection of constants which are used in the menu and in the game.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// This enum helps to identify all of the scenes.
        /// </summary>
        public enum Levels
        {
            MAIN_MENU = 0,
            CHARACTER_SELECTION,
            STAGE_SELECT,
            DEMO_LEVEL,
            SCHOOL_LEVEL,
            CREDITS,
            OPTIONS
        }

        /// <summary>
        /// The array contains information about all spawn points in every level.
        /// </summary>
        public static Vector3[][] SpawnPoint;

        /// <summary>
        /// Initializes the positions of the spawn points.
        /// </summary>
        static Constants()
        {
            SpawnPoint = new Vector3[6][];
            SpawnPoint[(int)Levels.DEMO_LEVEL] = new Vector3[4];

            SpawnPoint[(int)Levels.DEMO_LEVEL][0] = new Vector3(-25, 10, 0);
            SpawnPoint[(int)Levels.DEMO_LEVEL][1] = new Vector3(25, 10, 0);
            SpawnPoint[(int)Levels.DEMO_LEVEL][2] = new Vector3(-60, 70, 0);
            SpawnPoint[(int)Levels.DEMO_LEVEL][3] = new Vector3(60, 70, 0);

            SpawnPoint[(int)Levels.SCHOOL_LEVEL] = new Vector3[4];
            SpawnPoint[(int)Levels.SCHOOL_LEVEL][0] = new Vector3(32.83376f, 381.5925f, -37.4941f);
            SpawnPoint[(int)Levels.SCHOOL_LEVEL][1] = new Vector3(-50.02605f, 385.5925f, -37.4941f);
            SpawnPoint[(int)Levels.SCHOOL_LEVEL][2] = new Vector3(-137.5023f, 394.249f, -37.4941f);
            SpawnPoint[(int)Levels.SCHOOL_LEVEL][3] = new Vector3(-176.6333f, 311.5846f, -37.4941f);
        }
    }
}
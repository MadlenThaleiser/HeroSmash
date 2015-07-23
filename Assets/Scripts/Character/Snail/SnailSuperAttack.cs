using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is linked to the snail character and is created when the snail is being played in the game. It initializes the range and speed of the super attack.
    /// </summary>
    public class SnailSuperAttack : SuperAttack
    {
        /// <summary>
        /// Initializes the range of the super attack.
        /// </summary>
        protected override int range
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Initializes the speed of the super attack.
        /// </summary>
        protected override int speed
        {
            get
            {
                return 20;
            }
        }
    }
}
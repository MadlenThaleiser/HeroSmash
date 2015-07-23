using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Throw Attack.
    /// </summary>
    public class Throw : BasicAttack
    {
        /// <summary>
        /// Range of the Throw Attack.
        /// </summary>
        protected override int range
        {
            get { return 4; }
        }

        /// <summary>
        /// Speed of the Throw Attack.
        /// </summary>
        protected override int speed
        {
            get { return 20; }
        }

        /// <summary>
        /// Called by Unity each frame.
        /// </summary>
        public void Update()
        {
            moveAttack();
        }
    }
}
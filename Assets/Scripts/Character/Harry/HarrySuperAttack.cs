using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Class for Harrys super attack.
    /// </summary>
    public class HarrySuperAttack : SuperAttack
    {
        /// <summary>
        /// Range of the super Attack
        /// </summary>
        protected override int range
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Speed of the attack.
        /// </summary>
        protected override int speed
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// Intentional left blank.
        /// </summary>
        public new void Start()
        {
        }
    }
}
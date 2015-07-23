using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Class for Captain 'Murica super attack.
    /// </summary>
    public class CaptainMuricaSuperAttack : SuperAttack
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
        /// Super Attack initial position
        /// </summary>
        protected override Vector3 attackOffset
        {
            get
            {
                return new Vector3(8f, +2f, 0);
            }
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
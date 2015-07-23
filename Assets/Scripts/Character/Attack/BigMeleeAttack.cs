using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The Big Melee Attack
    /// </summary>
    public class BigMeleeAttack : BasicAttack
    {
        /// <summary>
        /// The range of the BMA.
        /// </summary>
        protected override int range
        {
            get
            {
                return 7;
            }
        }

        /// <summary>
        /// The speed of the BMA.
        /// </summary>
        protected override int speed
        {
            get
            {
                return 15;
            }
        }

        /// <summary>
        /// Called by Unity when instantiating.
        /// </summary>
        public void Start()
        {
            this.gameObject.tag = "bMAttack";
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
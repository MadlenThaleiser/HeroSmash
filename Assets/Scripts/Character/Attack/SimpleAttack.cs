using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Simple Attack
    /// </summary>
    public class SimpleAttack : BasicAttack
    {
        /// <summary>
        /// Range of the Attack
        /// </summary>
        protected override int range
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Speed of the Attack
        /// </summary>
        protected override int speed
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// Called by Unity when beeing instantiated.
        /// </summary>
        public void Start()
        {
            this.gameObject.tag = "sAttack";
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
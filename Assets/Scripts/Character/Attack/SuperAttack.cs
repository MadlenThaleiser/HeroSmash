using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The Standard Super Attack.
    /// </summary>
    public class SuperAttack : BasicAttack
    {
        /// <summary>
        /// Range of the Super Attack.
        /// </summary>
        protected override int range
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// Speed of the Super Attack.
        /// </summary>
        protected override int speed
        {
            get
            {
                return 35;
            }
        }

        /// <summary>
        /// Sets this attack to be a super attack.
        /// </summary>
        public override bool isSuperAttack
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Called by Unity when this Object is beeing instantiated.
        /// </summary>
        public void Start()
        {
            this.gameObject.tag = "SPAttack";
        }
    }
}
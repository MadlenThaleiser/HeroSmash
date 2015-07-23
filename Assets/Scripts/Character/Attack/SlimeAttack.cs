using System;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The ranged attack of the snail
    /// </summary>
    public class SlimeAttack : RangeAttack
    {
        /// <summary>
        /// Gets called when colliding with an object.
        /// </summary>
        /// <param name="c"> The Collider Object.</param>
        public new void OnTriggerEnter(Collider c)
        {
            base.OnTriggerEnter(c);

            var victim = c.GetComponent<BasicCharacter>();
            if (victim && victim.playerID != ownerID)
            {
                victim.slowDown();
            }
        }
    }
}
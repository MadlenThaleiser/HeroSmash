using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is linked to the kirb character and is created when kirb is being played in the game. It helps to perform the super attack.
    /// </summary>
    public class SmokeAttack : SuperAttack
    {
        /// <summary>
        /// Sets the tag to "SPAttack".
        /// </summary>
        public new void Start()
        {
            this.tag = "SPAttack";
        }

        /// <summary>
        /// Slows down the enemies when they get hit by the smoke.
        /// </summary>
        public void OnParticleCollision(GameObject v)
        {
            var victim = v.GetComponent<BasicCharacter>();
            if (victim && victim.playerID != ownerID)
            {
                victim.slowDown();
            }
        }
    }
}
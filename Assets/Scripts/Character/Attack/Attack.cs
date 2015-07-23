using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Interface for implemanting an attack.
    /// </summary>
    public interface Attack
    {
        /// <summary>
        /// Damage value of the attack.
        /// </summary>
        float damage
        {
            get;
        }

        /// <summary>
        /// Force a character expiriences when hit by this attack.
        /// </summary>
        int force
        {
            get;
        }

        /// <summary>
        /// Player ID of the attacking player.
        /// </summary>
        int ownerID
        {
            get;
        }

        /// <summary>
        /// Checks wether or not this is a super attack.
        /// </summary>
        bool isSuperAttack
        {
            get;
        }
    }
}
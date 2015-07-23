using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This is the interface which is implemented by the class <see cref="BasicCharacter"/>. 
    /// It defines the style of the attacks and the basic behaviour of all characters.
    /// </summary>
    public interface Character
    {
        /// <summary>
        /// The standard attack for the character which has a very small range.
        /// </summary>
        void standardAttack();

        /// <summary>
        /// The big melee attack for the character which has a little more range than the standard attack.
        /// </summary>
        void bigMeleeAttack();

        /// <summary>
        /// The defense of the character.
        /// </summary>
        void defend();

        /// <summary>
        /// The range attack for the character.
        /// </summary>
        void rangeAttack();

        /// <summary>
        /// The super attack which is very special for each character and which does a lot of damage.
        /// </summary>
        void superAttack();

        /// <summary>
        /// Says that every character has to be able to change its state (every implementing class must have such a method).
        /// </summary>
        int state
        {
            set;
        }
    }
}
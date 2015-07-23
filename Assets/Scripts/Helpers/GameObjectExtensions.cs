using System;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class helps to start playing sounds and animations for a specified GameObject and to set the animation speed to a specific value.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Starts an animation if the GameObject owns the specified animation.
        /// </summary>
        /// <param name="g">The GameObject which plays the animation.</param>
        /// <param name="name">The name of the animation which should be played.</param>
        public static void playAnimationIfExists(this GameObject g, string name)
        {
            if (g.animation && g.animation[name])
            {
                g.animation.Play(name);
            }
        }

        /// <summary>
        /// Starts a sound and sets its volume if the GameObject owns the specified sound.
        /// </summary>
        /// <param name="g">The GameObject which plays the sound.</param>
        /// <param name="clip">The name of the sound which should be played.</param>
        /// <param name="volume">The volume of the sound which should be played.</param>
        public static void playSoundIfExists(this GameObject g, AudioClip clip, float volume)
        {
            if (g.audio && clip)
            {
                g.audio.PlayOneShot(clip, volume);
            }
        }

        /// <summary>
        /// Starts a sound if the GameObject owns the specified sound. Sets the volume to 1.
        /// </summary>
        /// <param name="g">The GameObject which plays the sound.</param>
        /// <param name="clip">The name of the sound which should be played.</param>
        public static void playSoundIfExists(this GameObject g, AudioClip clip)
        {
            g.playSoundIfExists(clip, 1f);
        }

        /// <summary>
        /// Changes the speed of the specified animation.
        /// </summary>
        /// <param name="g">The GameObject which owns the animation to be changed.</param>
        /// <param name="name">The name of the animation which should be changed.</param>
        /// <param name="speed">The new speed for the animation.</param>
        public static void setAnimationSpeed(this GameObject g, string name, float speed)
        {
            if (g.animation && g.animation[name])
            {
                g.animation[name].speed = speed;
            }
        }
    }
}
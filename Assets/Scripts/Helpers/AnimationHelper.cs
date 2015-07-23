using UnityEngine;
using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class helps to play all animations correctly when playing a match by normalizing the animation speed.
    /// </summary>
    public class AnimationHelper
    {
        /// <summary>
        /// The scale which defines the speed for the animations.
        /// </summary>
        public const float TIME_SCALE = 4f;

        /// <summary>
        /// Scales down the speed for the animations of a single GameObject.
        /// </summary>
        /// <param name="g">The games Object whose animation gets scaled down</param>
        public static void normalizeSpeed(GameObject g)
        {
            if (g.animation)
            {
                foreach (AnimationState a in g.animation)
                {
                    a.speed = 1 / TIME_SCALE;
                }
            }
        }
    }
}
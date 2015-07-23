using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Class used for Explosions.
    /// </summary>
    public class Explosion : MonoBehaviour
    {
        /// <summary>
        /// The corrosponding Particle System.
        /// </summary>
        public ParticleSystem ps;

        /// <summary>
        /// Called by Unity when Explosion gets instantiated.
        /// </summary>
        public void Start()
        {
            ps = this.gameObject.GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Called by Unity each frame.
        /// </summary>
        public void Update()
        {
            if (ps)
            {
                if (!ps.IsAlive())
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
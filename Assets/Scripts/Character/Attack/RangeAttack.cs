using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// A Ranged Attack.
    /// </summary>
    public class RangeAttack : BasicAttack
    {
        /// <summary>
        /// Explosion of the ranged Attack.
        /// </summary>
        public GameObject Explosion;

        /// <summary>
        /// Range of the Attack.
        /// </summary>
        protected override int range
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Speed of the Attack
        /// </summary>
        protected override int speed
        {
            get
            {
                return 35;
            }
        }

        /// <summary>
        /// Called by Unity when instantiated.
        /// </summary>
        public void Start()
        {
            this.gameObject.tag = "rgAttack";
        }

        /// <summary>
        /// Called by Unity each frame.
        /// </summary>
        public void Update()
        {
            const float EXPLOSION_X_POSITION = 5f;
            const float EXPLOSION_Y_POSITION = 3f;

            Vector3 explosionPosition = new Vector3(
                this.gameObject.transform.position.x + EXPLOSION_X_POSITION,
                this.gameObject.transform.position.y + EXPLOSION_Y_POSITION,
                this.gameObject.transform.position.z + this.gameObject.transform.localScale.z / 2);

            moveAttack();

            // Explosion of the Super Attack
            Instantiate(Explosion, explosionPosition, Quaternion.identity);
        }
    }
}
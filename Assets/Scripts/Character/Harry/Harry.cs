using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// The Behaviour script of the "Harry" Character.
    /// Is a component of the Harry prefab.
    /// Implements things like the special attack, and everything "Harry" specific.
    /// </summary>
    public class Harry : BasicCharacter
    {
        /// <summary>
        /// Animation for special attack (cannon ball + explosion).
        /// </summary>
        public GameObject LandingExplosion;

        /// <summary>
        /// Animation for special attack (cannon ball + explosion).
        /// </summary>
        private GameObject landingExplosion;

        /// <summary>
        /// Checks if Harry is superattacking
        /// </summary>
        private bool superAttacking = false;

        /// <summary>
        /// Called by Unity on intantiation.
        /// </summary>
        public new void Start()
        {
            base.Start();
            gameObject.setAnimationSpeed("Provocation", 0.11f);
            gameObject.setAnimationSpeed("Range Attack", 0.1f);
        }

        /// <summary>
        /// Range attack: Harry shoots a fire ball out of his behind.
        /// </summary>
        public override void rangeAttack()
        {
            const float ATTACK_ROTATION = 90f;
            const float ATTACK_DELAY = 3f;
            const float ANIMATION_DELAY = 4f;
            const int RANGE_ATTACK_FORCE = 200;
            const float RANGE_ATTACK_DAMAGE = 1f;

            // Animation Delay
            StartCoroutine(rangeAttackCharginTime(ANIMATION_DELAY));

            GameObject rangeAttack = (GameObject)Instantiate(rgAttack, this.transform.position, Quaternion.Euler(0f, 0f, ATTACK_ROTATION));

            this.attack = (RangeAttack)rangeAttack.GetComponent(typeof(RangeAttack));
            this.attack.init(playerID, RANGE_ATTACK_FORCE, RANGE_ATTACK_DAMAGE, ATTACK_DELAY);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;
        }

        /// <summary>
        /// Super attack: Harry executes an explosion by taking a canon ball shape and hitting the ground.
        /// </summary>
        public override void superAttack()
        {
            if (!this.isGrounded())
            {
                this.superAttacking = true;
                this.state = ATTACKING;
                this.gameObject.playAnimationIfExists("Super Attack");
            }
        }

        /// <summary>
        /// Range attack TimeOut to synchronize the animation with the attack.
        /// </summary>
        /// <param name="timeOut">Triggers when the attack disapears.</param>
        /// <returns>Returns IEnumerator</returns>
        private IEnumerator rangeAttackCharginTime(float timeOut)
        {
            this.gameObject.playAnimationIfExists("Range Attack");
            yield return new WaitForSeconds(timeOut);
        }

        /// <summary>
        /// Update is called once per frame and checks if Harry perfoms a superattack
        /// </summary>
        public new void Update()
        {
            base.Update();

            if (this.isGrounded() && this.superAttacking)
            {
                triggerExplosion();
            }
        }

        /// <summary>
        /// Triggers the explosion unless Harry got spammed.
        /// </summary>
        private void triggerExplosion()
        {
            this.superAttacking = false;
            if (this.state != ATTACKING)
            {
                return;
            }

            const float EXPLOSION_X_POSITION = 5f;
            const float EXPLOSION_Y_POSITION = 3f;

            Vector3 explosionPosition = new Vector3(
                this.gameObject.transform.position.x + EXPLOSION_X_POSITION,
                this.gameObject.transform.position.y + EXPLOSION_Y_POSITION,
                this.gameObject.transform.position.z + this.gameObject.transform.localScale.z / 2);

            // Explosion of the Super Attack
            landingExplosion = (GameObject)Instantiate(LandingExplosion, explosionPosition, this.gameObject.transform.rotation);
            this.attack = landingExplosion.GetComponentInChildren<HarrySuperAttack>();
            this.attack.init(playerID, 100, 0.5f);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;
            state = READY;
        }
    }
}
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is linked to the snail character and is created when the snail is being played in a match. It inherits from the class <see cref="BasicCharacter"/> and overwrites the unique animations and attacks.
    /// </summary>
    public class Snail : BasicCharacter
    {
        /// <summary>
        /// The GameObject for the particle effect which is visible when performing the special attack.
        /// </summary>
        public GameObject GaryRollingEffect;

        /// <summary>
        /// The Instance of the particle effect which is visible when performing the special attack.
        /// </summary>
        private GameObject garyRollingEffect;

        /// <summary>
        /// The super attack script of the snail which is used to perform the super attack.
        /// </summary>
        private SnailSuperAttack snailSuperAttack = null;

        /// <summary>
        /// Initializes the animation speeds for the attacks and the provocation.
        /// </summary>
        public new void Start()
        {
            base.Start();
            gameObject.setAnimationSpeed("Simple Attack", 0.6f);
            gameObject.setAnimationSpeed("Provocation", 0.11f);
            gameObject.setAnimationSpeed("Super Attack", 0.05f);
            gameObject.setAnimationSpeed("Range Attack", 0.1f);
        }

        /// <summary>
        /// Initializes the range attack.
        /// </summary>
        public override void rangeAttack()
        {
            const float ATTACK_ROTATION = 90f;
            const float ATTACK_DELAY = 3f;
            const float ANIMATION_DELAY = 4f;
            const int RANGE_ATTACK_FORCE = 80;
            const float RANGE_ATTACK_DAMAGE = 0.5f;

            // Animation Delay
            StartCoroutine(rangeAttackCharginTime(ANIMATION_DELAY));

            GameObject rangeAttack = (GameObject)Instantiate(rgAttack, new Vector3(this.transform.position.x, this.transform.position.y - 4.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, ATTACK_ROTATION));

            this.attack = (SlimeAttack)rangeAttack.GetComponent(typeof(SlimeAttack));
            this.attack.init(playerID, RANGE_ATTACK_FORCE, RANGE_ATTACK_DAMAGE, ATTACK_DELAY);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;
        }

        /// <summary>
        /// Initializes the super attack (rolling on the ground).
        /// </summary>
        public override void superAttack()
        {
            // Delaytime for loading of other routines
            const float LOAD_FACTOR = 4f;

            this.state = ATTACKING;
            snailSuperAttack = this.gameObject.AddComponent<SnailSuperAttack>();
            snailSuperAttack.init(this.playerID, 0, 0.8f);
            snailSuperAttack.setDirection(movingLeft);
            snailSuperAttack.attacker = this;
            this.attack = snailSuperAttack;
            this.attack.ownerID = this.playerID;

            // start loading the superAttack
            // Coroutine will also starts the other animationcoroutines for the superattack
            StartCoroutine(loadSuperAttack(LOAD_FACTOR));
        }

        /// <summary>
        /// Waits some time until the range attack can start.
        /// </summary>
        /// <param name="timeOut">The time to wait until starting the range attack.</param>
        /// <returns>The time which is being waited.</returns>
        private IEnumerator rangeAttackCharginTime(float timeOut)
        {
            this.gameObject.playAnimationIfExists("Range Attack");
            yield return new WaitForSeconds(timeOut);
        }

        /// <summary>
        /// Waits some time until the super attack can start.
        /// </summary>
        /// <param name="time">The time to wait until starting the super attack.</param>
        /// <returns>The time which is being waited.</returns>
        private IEnumerator superAttackCharginTime(float time)
        {
            // snail is rolling only        
            animation["Super Attack Rolling"].speed = 2f;
            animation["Super Attack Rolling"].wrapMode = WrapMode.Loop;

            // snail can't be hit
            state = INVINCIBLE;
            this.gameObject.playAnimationIfExists("Super Attack Rolling");
            this.gameObject.rigidbody.velocity = transform.forward * 100f;

            const float EFFECT_X_POSITION = 5f;
            const float EFFECT_Y_POSITION = 3f;

            // Position of the Flames
            Vector3 explosionPosition = new Vector3(
                this.gameObject.transform.position.x + EFFECT_X_POSITION,
                this.gameObject.transform.position.y + EFFECT_Y_POSITION,
                this.gameObject.transform.position.z + this.gameObject.transform.localScale.z / 2);

            // Flames of the out of the Garry While doing Super Attack
            garyRollingEffect = (GameObject)Instantiate(GaryRollingEffect, explosionPosition, this.gameObject.transform.rotation);
            garyRollingEffect.rigidbody.velocity = this.gameObject.rigidbody.velocity;

            yield return new WaitForSeconds(time);

            // stop superattack after a time
            gameObject.animation.Stop();
            this.gameObject.playAnimationIfExists("Super Attack Out");
            state = READY;
            Destroy(this.gameObject.GetComponent<SnailSuperAttack>());
            snailSuperAttack = null;
        }

        /// <summary>
        /// Waits some time until the snail can start rolling for performing the super attack.
        /// </summary>
        /// <param name="timer">The time to wait until starting the super attack.</param>
        /// <returns>The time which is being waited.</returns>
        private IEnumerator loadSuperAttack(float timer)
        {
            animation["Super Attack In"].wrapMode = WrapMode.ClampForever;
            this.gameObject.playAnimationIfExists("Super Attack In");
            this.gameObject.playSoundIfExists(superAttackSound);
            yield return new WaitForSeconds(timer);

            // after a time the snail starts rolling
            if (this.state == ATTACKING)
            {
                StartCoroutine(superAttackCharginTime(4f));
            }
        }

        /// <summary>
        /// Pushes enemies away and does damage to them when performing the super attack.
        /// </summary>
        /// <param name="c">The collision with another character for pushing it away.</param>
        public new void OnCollisionEnter(Collision c)
        {
            base.OnCollisionEnter(c);

            BasicCharacter bchar = c.gameObject.GetComponent<BasicCharacter>();
            if (this.state == INVINCIBLE && bchar != null && (bchar.tag == "harry" || bchar.tag == "kirb" || bchar.tag == "captain_murica"))
            {
                // enemy got damage
                if (snailSuperAttack == null)
                {
                    // Super attack is already over
                    return;
                }
                bchar.gotHit(this.gameObject, snailSuperAttack);

                // push enemy up
                var body = bchar.collider.attachedRigidbody;
                Vector3 newVelocity = new Vector3(
                    body.velocity.x,
                    body.velocity.y + 50,
                    body.velocity.z);
                body.velocity = newVelocity;
            }
        }

        /// <summary>
        /// The character cannot be hit by normal attacks when defending.
        /// </summary>
        public override void specialDefending()
        {
            BasicCharacter.isInvicibleDefending = true;
        }
    }
}
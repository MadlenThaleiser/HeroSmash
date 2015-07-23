using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class is linked to the kirb character and is created when kirb is being played in a match. It inherits from the class <see cref="BasicCharacter"/> and overwrites the unique animations and attacks.
    /// </summary>
    public class Kirb : BasicCharacter
    {
        /// <summary>
        /// The prefab of Kirb's special smoke attack.
        /// </summary>
        public GameObject smokeAttackPrefab;

        /// <summary>
        /// Initializes the animation speeds for the attacks and the provocation.
        /// </summary>
        public new void Start()
        {
            base.Start();
            gameObject.setAnimationSpeed("Book Melee Attack", 0.6f);
            gameObject.setAnimationSpeed("Provocation", 0.11f);
            gameObject.setAnimationSpeed("Super Attack", 0.05f);
            gameObject.setAnimationSpeed("Super Move Attack", 0.1f);
        }

        /// <summary>
        /// Initializes the range attack.
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

            Vector3 startPos = this.transform.position;
            startPos.y -= 3;
            GameObject rangeAttack = (GameObject)Instantiate(rgAttack, startPos, Quaternion.Euler(0f, 0f, ATTACK_ROTATION));

            this.attack = (RangeAttack)rangeAttack.GetComponent(typeof(RangeAttack));
            this.attack.init(playerID, RANGE_ATTACK_FORCE, RANGE_ATTACK_DAMAGE, ATTACK_DELAY);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;
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
        /// Initializes the super attack (the smoke ball).
        /// </summary>
        public override void superAttack()
        {
            const int DELAY = 5;

            // TODO: maybe play an animation here to show that he initiated the attack
            this.state = ATTACKING;
            Invoke("startSuperAttack", DELAY);
        }

        /// <summary>
        /// Starts the super attack (the smoke ball) when enough time passed since pressing the super attack button.
        /// </summary>
        private void startSuperAttack()
        {
            if (this.state != ATTACKING)
            {
                return;
            }

            var smoke = (GameObject)Instantiate(smokeAttackPrefab, this.transform.position, Quaternion.identity);
            this.attack = smoke.GetComponentInChildren<SmokeAttack>();
            this.attack.init(playerID, 30, 0.3f);
            this.attack.attacker = this;

            this.state = READY;
        }
    }
}
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Captain 'Murica's Script: Range Attack and Super Attack instantiation
    /// </summary>
    public class CaptainMurica : BasicCharacter
    {
        /// <summary>
        /// Ketchup ball GameObject
        /// </summary>
        public GameObject KetchupBall;

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
        /// Range attack: Captain Murica shoots a fires out of his behind.
        /// </summary>
        public override void rangeAttack()
        {
            const float ATTACK_ROTATION = 90f;
            const float ATTACK_DELAY = 3f;
            const float ANIMATION_DELAY = 4f;
            const int RANGE_ATTACK_FORCE = 200;
            const float RANGE_ATTACK_DAMAGE = 1f;

            // Animation Delay
            StartCoroutine(rangeAttackCharginTime(ANIMATION_DELAY, "Range Attack"));

            GameObject rangeAttack = (GameObject)Instantiate(rgAttack, Vector3.zero, Quaternion.Euler(0f, 0f, ATTACK_ROTATION));

            this.attack = (RangeAttack)rangeAttack.GetComponent(typeof(RangeAttack));
            this.attack.init(playerID, RANGE_ATTACK_FORCE, RANGE_ATTACK_DAMAGE, ATTACK_DELAY);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;
        }

        /// <summary>
        /// Super attack: Captain Murica throws a Ketchup Ball.
        /// </summary>
        public override void superAttack()
        {
            const float ATTACK_ROTATION = 90f;
            const float ATTACK_DELAY = 3f;
            const float ANIMATION_DELAY = 2f;
            const int SUPER_ATTACK_FORCE = 200;
            const float SUPER_ATTACK_DAMAGE = 1f;

            // Animation Delay
            StartCoroutine(rangeAttackCharginTime(ANIMATION_DELAY, "Super Attack"));

            GameObject superAttack = (GameObject)Instantiate(KetchupBall, Vector3.zero, Quaternion.Euler(0f, 0f, ATTACK_ROTATION));

            Vector3 newPos = superAttack.transform.position;
            newPos.y += 20f;
            superAttack.transform.position = newPos;

            this.attack = (SuperAttack)superAttack.GetComponent(typeof(SuperAttack));
            this.attack.init(playerID, SUPER_ATTACK_FORCE, SUPER_ATTACK_DAMAGE, ATTACK_DELAY);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;
        }

        /// <summary>
        /// Range attack TimeOut to synchronize the animation with the attack.
        /// </summary>
        /// <param name="timeOut">Triggers when the attack disapears.</param>
        /// <returns>Returns IEnumerator</returns>
        private IEnumerator rangeAttackCharginTime(float timeOut, string animationName)
        {
            this.gameObject.playAnimationIfExists(animationName);
            yield return new WaitForSeconds(timeOut);
        }
    }
}
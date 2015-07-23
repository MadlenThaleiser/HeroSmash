using System;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// Defines the basic behaviour of all attacks.
    /// Attack Classes should inherit from it.
    /// </summary>
    public abstract class BasicAttack : MonoBehaviour, Attack
    {
        /// <summary>
        /// Properties of the attack.
        /// ownerID: contains the ID of the attacking character.
        /// </summary>
        protected int _ownerID;

        /// <summary>
        /// Defines the attacks force.
        /// </summary>
        protected int _force;

        /// <summary>
        /// Defines the attacks direction.
        /// </summary>
        protected int direction;

        /// <summary>
        /// Defines the attacks damage.
        /// </summary>
        protected float _damage;

        /// <summary>
        /// Timer to  measure the delay.
        /// </summary>
        protected float delayTimer;

        /// <summary>
        /// Delay for the attack.
        /// </summary>
        protected float delay;

        /// <summary>
        /// Attacker, who fired this attack.
        /// </summary>
        public int ownerID
        {
            get
            {
                return _ownerID;
            }

            set
            {
                _ownerID = value;
            }
        }

        /// <summary>
        /// Force a character expiriences when getting attacked.
        /// </summary>
        public int force
        {
            get
            {
                return _force;
            }
        }

        /// <summary>
        /// Sets the damage a characters recieves.
        /// </summary>
        public float damage
        {
            get
            {
                return _damage;
            }
        }

        /// <summary>
        /// Was this attack spammed?
        /// </summary>
        protected bool _spammed = false;

        /// <summary>
        /// Makes sure weather or not a attack was interupted
        /// </summary>
        public bool spammed
        {
            get { return _spammed; }
            set { _spammed = value; }
        }

        /// <summary>
        /// The attacking character.
        /// </summary>
        protected Character _attacker;

        /// <summary>
        /// The attacking character.
        /// </summary>
        public Character attacker
        {
            protected get
            {
                return _attacker;
            }

            set
            {
                value.state = BasicCharacter.ATTACKING;

                // set the attack's position according to the characters position.
                var bs = (BasicCharacter)value;
                initialX = bs.transform.position.x;
                this.transform.position = bs.collider.bounds.center;

                _attacker = value;
            }
        }

        /// <summary>
        /// Maximum range
        /// </summary>
        protected abstract int range { get; }

        /// <summary>
        /// Attack speed
        /// </summary>
        protected abstract int speed { get; }

        /// <summary>
        /// Offset to move the position of range attacks
        /// </summary>
        protected virtual Vector3 attackOffset
        {
            get
            {
                return Vector3.zero;
            }
        }

        /// <summary>
        ///  The initial position of the attack.
        /// </summary>
        protected float initialX;

        /// <summary>
        /// This is not a super attack.
        /// </summary>
        public virtual bool isSuperAttack
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// This is called by unity when clliding with something.
        /// </summary>
        /// <param name="c">The colliding object.</param>
        public void OnTriggerEnter(Collider c)
        {
            var victim = c.GetComponent<BasicCharacter>();
            if (victim)
            {
                victim.gotHit(this.gameObject.collider);
            }
            else if ((c.tag == "level" || c.tag == "Wall") && !(this.gameObject.GetComponent<BasicCharacter>() is Snail))
            {
                Destroy(this.gameObject);
                this.attacker.state = BasicCharacter.READY;
            }
        }

        /// <summary>
        /// Initiates the attack with basic properties.
        /// </summary>
        /// <param name='ownerID'>
        /// The ID of the attacking character.
        /// </param>
        /// <param name='force'>
        /// The force that hits the victim.
        /// </param>
        /// <param name='damage'>
        /// The damage that will be added to the character's weight factor.
        /// </param>
        public void init(int ownerID, int force, float damage)
        {
            _ownerID = ownerID;
            _force = force;
            _damage = damage;
        }

        /// <summary>
        /// Initiates the attack with basic properties.
        /// </summary>
        /// <param name='ownerID'>
        /// The ID of the attacking character.
        /// </param>
        /// <param name='force'>
        /// The force that hits the victim.
        /// </param>
        /// <param name='damage'>
        /// The damage that will be added to the character's weight factor.
        /// </param>
        /// <param name="delay">
        /// The delay of the attack.
        /// </param>
        public void init(int ownerID, int force, float damage, float delay)
        {
            init(ownerID, force, damage);
            this.delay = delay;
        }

        /// <summary>
        /// Sets the direction of the attack from the attacker's move direction.
        /// </summary>
        public void setDirection(int direction)
        {
            this.direction = direction;
        }

        /// <summary>
        /// Moves the attack to a new position.
        /// </summary>
        protected void moveAttack()
        {
            if (delayTimer < delay)
            {
                delayTimer += 1 * Time.deltaTime;
                return;
            }

            this.attacker.state = BasicCharacter.READY;

            // Destroy the attack if the attacker got spammed or when it leaves the attack's range
            if (spammed || Mathf.Abs(initialX - transform.position.x) - ((BasicCharacter)attacker).collider.bounds.size.z / 2 > this.range)
            {
                Destroy(this.gameObject);
                delayTimer = 0;
            }

            // Quickly move the attack in the direction of its parent object (the character)
            this.transform.position = new Vector3(transform.position.x + this.speed * Time.deltaTime * direction, transform.position.y, transform.position.z);
        }
    }
}
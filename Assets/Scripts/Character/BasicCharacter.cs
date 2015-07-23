using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class defines the mainbehaviour of the characters. It implements the interface <see cref="Character"/> and inherits from the class <see cref="MonoBehavior"/>.
    /// </summary>
    public abstract class BasicCharacter : MonoBehaviour, Character
    {
        /// <summary>
        /// The Stars that are shown, when the character got hit.
        /// </summary>
        public GameObject stars;

        /// <summary>
        /// The max width a character has been pushed out of the game.
        /// </summary>
        public float deadWidth;

        /// <summary>
        /// The Sound Clips of all character moves
        /// </summary>
        /// <summary>
        /// Audio Object of the player when he is running/moving
        /// </summary>
        public AudioClip runSound;

        /// <summary>
        /// Audio Object for the standardattack of the player
        /// </summary>
        public AudioClip simpleMeleeAttackSound;

        /// <summary>
        /// Audio Object for the big melee attack of the player
        /// </summary>
        public AudioClip bigMeleeAttackSound;

        /// <summary>
        /// Audio Object for the rangeattack of the player
        /// </summary>
        public AudioClip rangeAttackSound;

        /// <summary>
        /// Audio Object for the superattack of the player
        /// </summary>
        public AudioClip superAttackSound;

        /// <summary>
        /// Audio Object of the player when he jumps
        /// </summary>
        public AudioClip jumpSound;

        /// <summary>
        /// Audio Object of the player when he dies
        /// </summary>
        public AudioClip dieSound;

        /// <summary>
        /// Audio Object of the player when he gets hit
        /// </summary>
        public AudioClip getHitSound;

        /// <summary>
        /// Audio Object for the defending of the player
        /// </summary>
        public AudioClip defendSound;

        /// <summary>
        /// Audio Object for the respawn of the player
        /// </summary>
        public AudioClip respawnSound;

        /// <summary>
        /// Audio Object for the provocation of the player
        /// </summary>
        public AudioClip provocationSound;

        /// <summary>
        /// Audio Object for the playerintroducionsound
        /// </summary>
        public AudioClip introSound;

        /// <summary>
        /// Audio Object for the sound when the player throws an enemy
        /// </summary>
        public AudioClip throwAttackSound;

        /// <summary>
        /// Audio Object for the sound when the player was thrown
        /// </summary>
        public AudioClip beThrownSound;

        /// <summary>
        /// Informs if the player used the double jump
        /// </summary>
        private bool secondJump;

        /// <summary>
        /// How much of the force goes into the y-axe of the character velocity. (see gotHit)
        /// </summary>
        private const float Y_FACTOR = 0.5f;

        /// <summary>
        /// Standard attack force (property)
        /// </summary>
        private const int STANDARD_ATTACK_FORCE = 150;

        /// <summary>
        /// Standard attack damage (property)
        /// </summary>
        private const float STANDARD_ATTACK_DAMAGE = 0.1f;

        /// <summary>
        /// Acceleration for the movement
        /// </summary>
        private const float SPEED = 270;

        /// <summary>
        /// Defines the maxspeed a player can has
        /// </summary>
        private const float MAXSPEED = 18;

        /// <summary>
        /// Defines the height a player can jump
        /// </summary>
        private const int JUMP_HEIGHT = 30;

        /// <summary>
        /// Big Melee attack force (property)
        /// </summary>
        private const int BIG_MELEE_ATTACK_FORCE = 300;

        /// <summary>
        /// Big Melee Attack damage (property)
        /// </summary>
        private const float BIG_MELEE_DAMAGE = 0.4f;

        /// <summary>
        /// Super Attack Properties (Delay, nextFire, fireRate)
        /// </summary>
        private const float FIRE_RATE = 5f;

        /// <summary>
        /// Delaytimer for superattack and rangeattack
        /// </summary>
        private float nextFire = 0f;

        /// <summary>
        /// Reference to the GameObserver
        /// </summary>
        public GameObserver observer;

        /// <summary>
        /// Defines the factor that accelerates the player when it gets hit. (lower is better)
        /// </summary>
        private float _weightFactor = 1f;

        /// <summary>
        /// Getter/Setter for the weightFactor 
        /// </summary>
        private float weightFactor
        {
            get
            {
                return _weightFactor;
            }

            set
            {
                _weightFactor = value;
                this.observer.updateGUI(this._playerID - 1, (int)Mathf.Round(100 / weightFactor));
            }
        }

        /// <summary>
        /// Defines the factor that speeds up or slows down a character's maximum speed.
        /// </summary>    
        private float speedFactor = 1f;

        /// <summary>
        /// Tracks the player's distance to the ground
        /// needed for jumping
        /// </summary>
        private float distToGround;

        /// <summary>
        /// A reference for the landing animation (dust)
        /// </summary> 
        public GameObject LandingDust;

        /// <summary>
        /// Informs if the player was jumping or not
        /// </summary>
        public bool wasJumping = false;

        /// <summary>
        /// A reference for the attack needed for spamming.
        /// </summary>
        protected BasicAttack attack;

        /// <summary>
        /// Counter for the provocation delay
        /// </summary>
        private float counter = 3f;

        /// <summary>
        /// Timer for playing the movementsound
        /// </summary>
        private float mPlayed = 0;

        /// <summary>
        /// Length of the movementtime of the movementsound
        /// </summary>
        private const float M_LENGTH = 0.42f;

        /// <summary>
        /// ID for identifying the character attacks and keys
        /// </summary> 
        private int _playerID;

        /// <summary>
        /// Getter/Setter for the playerID
        /// </summary>
        public int playerID
        {
            get
            {
                return _playerID;
            }

            set
            {
                _playerID = value;
            }
        }

        /// <summary>
        /// Detects whether the object is moving left or right
        /// Using an integer for convenience when moving or placing objects related to that object (e.g. shield)
        /// </summary>
        protected int movingLeft = 1;

        /// <summary>
        /// Simple Attack Object for the instatiation - an attack in front of the player
        /// </summary> 
        public GameObject simpleAttack;

        /// <summary>
        /// Big Melee Attack for the instatiation - is a longer and heavier attack
        /// </summary>
        public GameObject bMeleeAttack;

        /// <summary>
        /// Range Attack Object for the instatiation
        /// </summary>
        public GameObject rgAttack;

        /// <summary>
        /// Special Attack Object for the instatiation
        /// </summary>
        public GameObject sprAttack;

        /// <summary>
        /// Current State Implemantation (will be changed to State mashine, if too many states)
        /// </summary>
        private int _state = READY;

        /// <summary>
        /// Getter/Setter for the character state
        /// </summary>
        public int state
        {
            get
            {
                return _state;
            }

            set
            {
                // The character should not be trigger when it changes its state while being
                if (_state == THROWN)
                {
                    this.collider.isTrigger = false;
                }

                switch (value)
                {
                    case PARALYZED:
                        paralysisTimer = PARALYSIS_TIME;
                        if (tooFast())
                        {
                            fall();
                        }
                        else
                        {
                            this.gameObject.playAnimationIfExists("Getting Hit");
                        }

                        break;
                }

                _state = value;
            }
        }

        /// <summary>
        /// The player is ready to fight 
        /// </summary>
        public const int READY = 1;

        /// <summary>
        /// The player is dead and the respawn will be initialize
        /// </summary>
        public const int DEAD = 2;

        /// <summary>
        /// The player is defending with his own defending behaviour
        /// </summary>
        public const int DEFENDING = 3;

        /// <summary>
        /// The player is paralyzed
        /// </summary>
        public const int PARALYZED = 4;

        /// <summary>
        /// The player provokes the other players
        /// </summary>
        public const int PROVOKING = 5;

        /// <summary>
        /// The player is attacking and can spam his attacks
        /// </summary>
        public const int ATTACKING = 6;

        /// <summary>
        /// The player was dead and respawns or he has a special behaviour during defending or attacking
        /// </summary>
        public const int INVINCIBLE = 7;

        /// <summary>
        /// The player is thrown by an other character
        /// </summary>
        public const int THROWN = 8;

        /// <summary>
        /// Saves the attacker
        /// </summary>
        public int lastHitBy;

        /// <summary>
        /// Managaer for the Keybindings
        /// </summary>
        private InputManager myInput;

        /// <summary>
        /// Time for paralysis after getting hit
        /// </summary> 
        private const int PARALYSIS_TIME = 1;

        /// <summary>
        /// Timer for the paralysis after getting hit
        /// </summary>
        private int paralysisTimer = 0;

        /// <summary>
        /// Timer going from 0 to 1 for the throw transition
        /// </summary>
        private float throwTimer = 0;

        /// <summary>
        /// Position while the player is thrown
        /// </summary>
        private Vector3 throwPosition;

        /// <summary>
        /// Rotation while the player is thrown
        /// </summary>
        private Quaternion throwRotation;

        /// <summary>
        /// Defines the Side where the throw comes from
        /// </summary>
        private int throwSide;

        /// <summary>
        /// The side the character that was being thrown has to be rotated back to.
        /// (can be different from the throwSide when the character is being thrown twice from two different sides.)
        /// </summary>
        private int throwRotationSide;

        /// <summary>
        /// Is use for the unique defense behaviour
        /// </summary>
        public static bool isInvicibleDefending = false;

        /// <summary>
        /// Use this for initialization
        /// </summary> 
        public void Start()
        {
            deadWidth = 0f;
            secondJump = false;
            lastHitBy = playerID;
            myInput = new InputManager(playerID);

            distToGround = collider.bounds.extents.y;

            AnimationHelper.normalizeSpeed(this.gameObject);
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        public void Update()
        {
            if (GameController.isPlaying)
            {
                switch (state)
                {
                    case READY:
                    case INVINCIBLE:
                        if (isGrounded() && this.animation && !this.animation.isPlaying)
                        {
                            this.gameObject.playAnimationIfExists("Ready Stance");
                        }

                        // Big melee attack
                        // Has to be checked before the simple attack to avoid triggering both big melee and standard attack.
                        if (myInput.getBMAttackKey())
                        {
                            bigMeleeAttack();
                        }
                        else if (myInput.getSAttackKey())
                        {
                            // Standard attack.
                            standardAttack();
                        }

                        // Super attack
                        // Has to be checked before the range attack to avoid triggering both super and range attack.
                        if (myInput.getSuperAttackKey() && Time.time > nextFire)
                        {
                            nextFire = FIRE_RATE + Time.time;
                            superAttack();
                        }
                        else if (myInput.getRangeAttackKey() && Time.time > nextFire)
                        {
                            nextFire = FIRE_RATE + Time.time;
                            this.gameObject.rigidbody.velocity = Vector3.zero;
                            rangeAttack();
                        }

                        // throw
                        if (myInput.getThrowKey())
                        {
                            throwEnemy();
                        }

                        // moving
                        if (myInput.getHorizontalKey() != 0)
                        {
                            move();
                        }

                        // jumping
                        if (myInput.getJumpKey())
                        {
                            jump();
                        }

                        // Defending
                        if (myInput.getDefendKey())
                        {
                            defend();
                        }

                        // Provoke
                        if (myInput.getProvocationKey())
                        {
                            this.audio.PlayOneShot(this.provocationSound, 1f);
                            this.gameObject.playAnimationIfExists("Provocation");
                            this.state = PROVOKING;
                        }

                        break;
                    case DEAD:
                        // Nothing to do here
                        break;
                    case DEFENDING:
                        // Defense Logic HERE
                        if (myInput.getDefendKeyUp())
                        {
                            holdUpDefense();
                        }

                        break;
                    case ATTACKING:
                        // Character is attacking and can be spammed.
                        break;
                    case PARALYZED:
                        unParalyze();
                        break;
                    case THROWN:
                        // isTrigger means it's being thrown and not yet released
                        if (this.collider.isTrigger)
                        {
                            beingThrown();
                        }

                        break;
                    case PROVOKING:
                        counter -= Time.deltaTime;
                        if (counter <= 0)
                        {
                            counter = 3f;
                            this.state = READY;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Function for moving the Character when Horizontal Keys are pressed
        /// </summary>
        protected void move()
        {
            if (myInput.getHorizontalKey() > 0)
            {
                if (movingLeft != 1)
                {
                    // transform.Rotate(new Vector3(0f,180f,0f));
                    transform.localEulerAngles = new Vector3(
                        this.transform.localEulerAngles.x,
                        this.tag == "kirb" ? 270 : 90,
                        this.transform.localEulerAngles.z);
                }

                movingLeft = 1;
            }
            else if (myInput.getHorizontalKey() < 0)
            {
                if (movingLeft != -1)
                {
                    // transform.Rotate(new Vector3(0f, 180f, 0f));
                    transform.localEulerAngles = new Vector3(
                        this.transform.localEulerAngles.x,
                        this.tag == "kirb" ? 90 : 270,
                        this.transform.localEulerAngles.z);
                }

                movingLeft = -1;
            }

            playMoveSound();
            if (movingLeft * myInput.getHorizontalKey() <= 0)
            {
                this.rigidbody.velocity = new Vector3(0, this.rigidbody.velocity.y, 0);
            }

            var maxSpeed = MAXSPEED * speedFactor;
            if (this.gameObject.rigidbody.velocity.x * movingLeft < maxSpeed)
            {
                this.gameObject.rigidbody.AddForce(myInput.getHorizontalKey() * SPEED, 0f, 0f);
            }

            if (isGrounded())
            {
                this.gameObject.playAnimationIfExists("Running");
            }
        }

        /// <summary>
        /// Plays movementsound when the character is moving on the ground
        /// </summary>
        protected void playMoveSound()
        {
            if (isGrounded())
            {
                if (mPlayed == 0)
                {
                    this.gameObject.playSoundIfExists(runSound, 0.3f);
                }
                else if (mPlayed > M_LENGTH)
                {
                    this.gameObject.playSoundIfExists(runSound, 0.3f);
                    mPlayed = 0;
                }

                mPlayed += Time.deltaTime;
            }
        }

        /// <summary>
        /// Defense Function (While not defending)
        /// </summary>
        public void defend()
        {
            state = DEFENDING;

            this.gameObject.playSoundIfExists(defendSound, 0.8f);
            this.gameObject.rigidbody.velocity = new Vector3(0f, this.gameObject.rigidbody.velocity.y, 0f);

            // the Character can't be moved
            this.gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            this.gameObject.playAnimationIfExists("Defense Up");
            specialDefending();
        }

        /// <summary>
        /// Each character has an own defense behaviour which must be implement by the specific character 
        /// </summary>
        public virtual void specialDefending()
        {
        }

        /// <summary>
        /// Drops the defense
        /// </summary>
        protected void holdUpDefense()
        {
            state = READY;
            this.gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            this.gameObject.playAnimationIfExists("Defense Down");
            isInvicibleDefending = false;
        }

        /// <summary>
        /// Pre-function for the gotHit-function, convert the collider into a gameobject
        /// </summary>
        /// <param name="enemy">
        /// Enemy which attacks the player
        /// </param>
        public void gotHit(Collider enemy)
        {
            gotHit(enemy.gameObject);
        }

        /// <summary>
        /// Pre-function for the gotHit-function, creates an attack-object which is also passed to the gotHitfunction
        /// </summary>
        /// <param name="enemy">
        /// Enemy which attacks the player
        /// </param>
        public void gotHit(GameObject enemy)
        {
            var attack = (Attack)enemy.GetComponent(typeof(Attack));
            if (attack != null)
            {
                gotHit(enemy, attack);
            }
            else
            {
                Debug.Log("Not an attack!!!!11elf");
            }
        }

        /// <summary>
        /// Enemy and the attack influence the behaviour and the damage of the player
        /// </summary>
        /// <param name="enemy">
        /// enemy which attacks the player
        /// </param>
        /// <param name="attack">
        /// Attack which hits the player
        /// </param>
        public void gotHit(GameObject enemy, Attack attack)
        {
            if (attack.ownerID != this.playerID)
            {
                Transform enemyTransform = enemy.transform.parent == null ? enemy.transform : enemy.transform.parent;
                int side = (enemyTransform.position.x < this.gameObject.transform.position.x) ? 1 : -1;
                int attackHeight = (enemyTransform.position.y < this.gameObject.transform.position.y) ? 1 : -1;
                if (attack is Throw)
                {
                    gotThrown(enemy, side);
                    lastHitBy = attack.ownerID;
                    return;
                }

                // Destroy shield if hit from behind
                if (this.state == DEFENDING && side == movingLeft && !isInvicibleDefending)
                {
                    holdUpDefense();
                }
                else if (state == DEFENDING && side != movingLeft && !attack.isSuperAttack)
                {
                    // If the player is currently in defending mode and the attack is coming from the side where the shield is raised, the attack should not have an effect on it.
                    return;
                }
                else if (state == INVINCIBLE || isInvicibleDefending)
                {
                    // The player is invincible, the enemy attacks don't have any effect
                    return;
                }
                else if (state == DEFENDING || attack.isSuperAttack)
                {
                    // If the player is currently in defending mode and the enemey attacks him with a superattack, he will get hit and his shield will be destroyed
                    holdUpDefense();
                    this.state = PARALYZED;
                }

                if (state == ATTACKING)
                {
                    if (this.attack != null)
                    {
                        this.attack.spammed = true;
                    }
                }

                lastHitBy = attack.ownerID;
                this.gameObject.playSoundIfExists(getHitSound, 1f);

                const int BASIC_FORCE = 6;
                this.weightFactor += attack.damage;
                GameObject star;
                if (enemy.collider != null)
                {
                    Vector3 pos = new Vector3(enemy.collider.bounds.center.x, enemy.collider.bounds.center.y, enemy.collider.bounds.center.z - 3);
                    star = (GameObject)Instantiate(stars, pos, this.transform.rotation);
                }
                else
                {
                    Vector3 pos = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z - 3);
                    star = (GameObject)Instantiate(stars, pos, this.transform.rotation);
                }

                star.transform.parent = this.transform;
                this.gameObject.rigidbody.AddForce(BASIC_FORCE * attack.force * side * this.weightFactor, Y_FACTOR * BASIC_FORCE * attackHeight * attack.force * this.weightFactor, 0f);
                state = PARALYZED;
            }
        }

        /// <summary>
        /// Character got thrown by an enemy and will be rotated to the other side
        /// </summary>
        /// <param name="enemy">
        /// Player who throws the character
        /// </param>
        /// <param name="side">
        /// The side where the throw comes
        /// </param>
        protected void gotThrown(GameObject enemy, int side)
        {
            this.throwPosition = new Vector3(
                        enemy.transform.position.x,
                        this.gameObject.transform.position.y + this.collider.bounds.size.y + enemy.collider.bounds.size.y + 5,
                        this.transform.position.z);

            // the character is rotated by 90° while being thrown if it's not already rotated
            if (this.throwSide == 0 && this.state != THROWN)
            {
                this.throwRotation = this.gameObject.transform.rotation * Quaternion.Euler(90 * -side, 0, 0);
                throwRotationSide = side;
            }

            this.throwSide = side;
            this.state = THROWN;
            this.collider.isTrigger = true; // to avoid collisions and funny effects with the attacker
        }

        /// <summary>
        /// Gets called when the character is being thrown.
        /// </summary>
        protected void beingThrown()
        {
            this.throwTimer += 0.1f;
            if (this.throwTimer >= 1)
            {
                releaseThrow();
                this.gameObject.playSoundIfExists(beThrownSound, 0.1f);
            }
            else
            {
                this.transform.position = Vector3.Lerp(
                    this.transform.position,
                    this.throwPosition,
                    throwTimer);
                this.transform.rotation = Quaternion.Lerp(
                    this.transform.rotation,
                    this.throwRotation,
                    throwTimer);
            }
        }

        /// <summary>
        /// Gets called when the character is being released from an attackers throw.
        /// The character gets accelerated and resets some throw properties.
        /// </summary>
        protected void releaseThrow()
        {
            this.transform.rigidbody.velocity = new Vector3(10 * throwSide * this.weightFactor, 6 * this.weightFactor, 0);
            this.collider.isTrigger = false; // enable collisions again
            this.throwTimer = 0;
        }

        /// <summary>
        /// Throws the enemy.
        /// </summary>
        public void throwEnemy()
        {
            this.gameObject.playSoundIfExists(throwAttackSound, 0.8f);
            GameObject tossRange = (GameObject)Instantiate(simpleAttack, this.transform.position, this.gameObject.transform.rotation);
            Destroy(tossRange.GetComponent<SimpleAttack>());
            this.attack = (Throw)tossRange.AddComponent("Throw");
            this.attack.init(playerID, 0, 0);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;

            this.gameObject.playAnimationIfExists("Throw Attack");
        }

        /// <summary>
        /// Resets the throw rotation back to the original.
        /// </summary>
        public void resetThrowRotation()
        {
            if (this.throwSide == 0)
            {
                return;
            }

            // Make sure it's really rotated all the way to the throwRotation
            this.transform.rotation = this.throwRotation;

            this.transform.Rotate(Vector3.right, 90 * this.throwRotationSide);
            state = READY;
            this.throwSide = 0;
            this.throwRotationSide = 0;
        }

        /// <summary>
        /// Character can be hit by a special attack which uses an explosion effect
        /// </summary>
        /// <param name="explosion">
        /// explosion which hits the character
        /// </param>
        public void OnParticleCollision(GameObject explosion)
        {
            if (explosion.tag.Equals("SPAttack"))
            {
                gotHit(explosion);
            }
        }

        /// <summary>
        /// Starts the falling and staning up animation.
        /// </summary>
        protected void fall()
        {
            this.gameObject.playAnimationIfExists("Falling and Getting Up");
        }

        /// <summary>
        /// Leaves the player paralyzed and increases paralysisTimer while it's is less than PARALYSIS_TIME. Afterwards the player goes back to READY.
        /// </summary>
        protected void unParalyze()
        {
            if ((++paralysisTimer * Time.deltaTime) >= PARALYSIS_TIME)
            {
                state = READY;
                paralysisTimer = PARALYSIS_TIME;
            }
        }

        /// <summary>
        /// Checks whether the character is currently standing on the ground.
        /// </summary>
        /// <returns>
        /// The grounded.
        /// </returns>
        protected bool isGrounded()
        {
            // We need a buffer to detect if the player is grounded since the distance is not constantly == 0 even though it's standing.
            const float GROUND_BUFFER = 1.5f;
            RaycastHit hitr;
            RaycastHit hitl;
            Vector3 left;
            Vector3 right;
            left = new Vector3(collider.transform.position.x - 3f, collider.transform.position.y, collider.transform.position.z);
            right = new Vector3(collider.transform.position.x + 3f, collider.transform.position.y, collider.transform.position.z);

            if (Physics.Raycast(new Ray(left, -Vector3.up), out hitl))
            {
                if (Physics.Raycast(new Ray(right, -Vector3.up), out hitr))
                {
                    return (hitl.distance < GROUND_BUFFER + distToGround) || (hitr.distance < GROUND_BUFFER + distToGround);
                }

                return hitl.distance < GROUND_BUFFER + distToGround;
            }
            else if (Physics.Raycast(new Ray(right, -Vector3.up), out hitr))
            {
                return hitr.distance < GROUND_BUFFER + distToGround;
            }

            return false;
        }

        /// <summary>
        /// Starts the jumpinganimation for a simple and a double jump
        /// </summary>
        protected void jump()
        {
            // simple jump
            if (isGrounded() && !wasJumping)
            {
                transform.rigidbody.velocity = new Vector3(
                transform.rigidbody.velocity.x,
                 JUMP_HEIGHT,
                transform.rigidbody.velocity.z);
                this.gameObject.playSoundIfExists(jumpSound, 0.3f);
                this.gameObject.playAnimationIfExists("Jump");
                wasJumping = true;
            }

            // double jump
            if (!secondJump && !isGrounded())
            {
                transform.rigidbody.velocity = new Vector3(
                transform.rigidbody.velocity.x,
                 JUMP_HEIGHT,
                transform.rigidbody.velocity.z);
                this.gameObject.playSoundIfExists(jumpSound, 0.3f);
                this.gameObject.playAnimationIfExists("Jump");
                wasJumping = true;
                secondJump = true;
            }

            // landing
            StartCoroutine(landingCheck());
        }

        /// <summary>
        /// Checks if the player was jumping and is now landing
        /// Plays the landinganimation and creates the landing dust
        /// </summary>
        /// <returns>
        /// The time which is being waited.
        /// </returns>
        protected IEnumerator landingCheck()
        {
            yield return new WaitForSeconds(0.4f);

            bool landed = false;
            while (!landed)
            {
                if (isGrounded() && this.wasJumping)
                {
                    const float LANDING_DUST_Z_AXIS_POSITION = 5f;
                    Vector3 position = new Vector3(
                        this.gameObject.transform.position.x,
                        this.gameObject.transform.position.y,
                        this.gameObject.transform.position.z - LANDING_DUST_Z_AXIS_POSITION);
                    Instantiate(LandingDust, position, Quaternion.identity);
                    wasJumping = false;
                    secondJump = false;
                    landed = true;
                    this.gameObject.playAnimationIfExists("Land");
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        /// <summary>
        /// The player can attack his enemy with a standard attack
        /// </summary>
        public void standardAttack()
        {
            GameObject sA = (GameObject)Instantiate(simpleAttack, this.transform.position, this.gameObject.transform.rotation);
            this.attack = (SimpleAttack)sA.GetComponent(typeof(SimpleAttack));
            this.attack.init(playerID, STANDARD_ATTACK_FORCE, STANDARD_ATTACK_DAMAGE);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;

            this.gameObject.playAnimationIfExists("Simple Attack");
            this.gameObject.playSoundIfExists(simpleMeleeAttackSound, 1f);
        }

        /// <summary>
        /// The player can attack his enemy with a Big Melee Attack
        /// </summary>
        public void bigMeleeAttack()
        {
            const int ATTACK_DELAY = 1;

            GameObject bMA = (GameObject)Instantiate(bMeleeAttack, this.transform.position, this.gameObject.transform.rotation);
            this.attack = (BigMeleeAttack)bMA.GetComponent(typeof(BigMeleeAttack));
            this.attack.init(playerID, STANDARD_ATTACK_FORCE, STANDARD_ATTACK_DAMAGE, ATTACK_DELAY);
            this.attack.setDirection(movingLeft);
            this.attack.attacker = this;

            this.gameObject.playAnimationIfExists("Big Melee Attack");
            this.gameObject.playSoundIfExists(bigMeleeAttackSound, 0.3f);
        }

        /// <summary>
        /// Each character has an unique rangeattack which must be implement by the specific character   
        /// </summary>
        public abstract void rangeAttack();

        /// <summary>
        /// Each character has an unique superattack which must be implement by the specific character   
        /// </summary>
        public abstract void superAttack();

        /// <summary>
        /// Checks, if the collider/rigidbody touches the collider of a wall or 
        /// if the character is thrown
        /// </summary>
        /// <param name="c">
        /// c is the collision on the collider/rigidbody 
        /// </param>
        public void OnCollisionEnter(Collision c)
        {
            // If the character got attacked and bounces off a wall, it should take a maximum of 0.1 additional damage
            if (c.gameObject.tag == "Wall" && (state == PARALYZED || state == THROWN))
            {
                const float MAX_DAMAGE = 0.1f;
                this.weightFactor += Mathf.Min(MAX_DAMAGE, MAX_DAMAGE * Mathf.Abs(transform.rigidbody.velocity.x));
                this.gameObject.playAnimationIfExists("Hitting the Wall");
            }

            // When the character was thrown and hits anything, it should go back to its normal rotation and state
            resetThrowRotation();
        }

        /// <summary>
        ///  Sets weightFactor to use this in GameObserver
        /// </summary>
        /// <param name="weightFactor">New value of the weightFactor</param>
        public void setWeightFactor(float weightFactor)
        {
            this.weightFactor = weightFactor;
        }

        /// <summary>
        /// Checks if the characters was hit by a strong attack that accelerated him really fast.
        /// </summary>
        /// <returns>
        /// Whether the velocity is more than FAST.
        /// </returns>
        protected bool tooFast()
        {
            const float FAST = 3f;
            return Mathf.Abs(this.rigidbody.velocity.x) > FAST;
        }

        /// <summary>
        ///  get weightFactor to use this in GameObserver
        /// </summary>
        /// <returns>The actual weightfactor.</returns>
        public float getWeightFactor()
        {
            return this.weightFactor;
        }

        /// <summary>
        /// Slows down the character for some time e.g. after getting hit by a slime attack.
        /// </summary>
        public void slowDown()
        {
            StartCoroutine(slowDownFor5Seconds());
        }

        /// <summary>
        /// Waits some time till the enemy can move normal
        /// </summary>
        /// <returns>The time which is being waited.</returns>
        protected IEnumerator slowDownFor5Seconds()
        {
            this.speedFactor = 0.3f;
            yield return new WaitForSeconds(5f);
            this.speedFactor = 1f;
        }
    }
}
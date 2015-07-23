using UnityEngine;
using System.Collections;

/// <summary>
/// Name Space for all the Project
/// <summary>
namespace HeroSmash
{
    /// <summary>
    /// This class helps to capture all pressed keys by mapping them to a variable.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// Maps the keyboard key for the vertical axis (jump).
        /// </summary>
        private string jumpKeyMouse;

        /// <summary>
        /// Maps the keyboard keys for the horizontal axis (movement).
        /// </summary>
        private string horizontalKeyMouse;

        /// <summary>
        /// Maps the keyboard key for the standard attack.
        /// </summary>
        private string sAttackKeyMouse;

        /// <summary>
        /// Maps the keyboard key for the range attack.
        /// </summary>
        private string rangeAttackKeyMouse;

        /// <summary>
        /// Maps the keyboard key for defending.
        /// </summary>
        private string defendKeyMouse;

        /// <summary>
        /// Maps the keyboard key for the attack modifier. When pressed, the attack keys perform different attacks.
        /// </summary>
        private string modifierKeyMouse;

        /// <summary>
        /// Maps the keyboard key for the provocation.
        /// </summary>
        private string provocationKeyMouse;

        /// <summary>
        /// Maps the keyboard key for the throw attack.
        /// </summary>
        private string throwKeyMouse;

        /// <summary>
        /// Maps the joystick key for the vertical axis (jump).
        /// </summary>
        private string jumpKeyJoystick;

        /// <summary>
        /// Maps the joystick key for the horizontal axis (movement).
        /// </summary>
        private string horizontalKeyJoystick;

        /// <summary>
        /// Maps the joystick key for the standard attack.
        /// </summary>
        private string sAttackKeyJoystick;

        /// <summary>
        /// Maps the joystick key for the range attack.
        /// </summary>
        private string rangeAttackKeyJoystick;

        /// <summary>
        /// Maps the joystick key for defending.
        /// </summary>
        private string defendKeyJoystick;

        /// <summary>
        /// Maps the joystick key for the attack modifier. When pressed, the attack keys perform different attacks.
        /// </summary>
        private string modifierKeyJoystick;

        /// <summary>
        /// Maps the joystick key for the provocation.
        /// </summary>
        private string provocationKeyJoystick;

        /// <summary>
        /// Maps the joystick key for the throw attack.
        /// </summary>
        private string throwKeyJoystick;

        /// <summary>
        /// Creates the strings which are necessary to use the keys.
        /// </summary>
        /// <param name="playerID">The id of the current player.</param>
        public InputManager(int playerID)
        {
            var playerKeyString = "Player" + playerID;
            jumpKeyMouse = playerKeyString + "_Jump_Mouse";
            horizontalKeyMouse = playerKeyString + "_Horizontal_Mouse";
            sAttackKeyMouse = playerKeyString + "_Attack_Mouse";
            rangeAttackKeyMouse = playerKeyString + "_RangeAttack_Mouse";
            defendKeyMouse = playerKeyString + "_Defend_Mouse";
            modifierKeyMouse = playerKeyString + "_Modifier_Mouse";
            provocationKeyMouse = playerKeyString + "_Provocation_Mouse";
            throwKeyMouse = playerKeyString + "_Throw_Mouse";

            jumpKeyJoystick = playerKeyString + "_Jump_Joystick";
            horizontalKeyJoystick = playerKeyString + "_Horizontal_Joystick";
            sAttackKeyJoystick = playerKeyString + "_Attack_Joystick";
            rangeAttackKeyJoystick = playerKeyString + "_RangeAttack_Joystick";
            defendKeyJoystick = playerKeyString + "_Defend_Joystick";
            modifierKeyJoystick = playerKeyString + "_Modifier_Joystick";
            provocationKeyJoystick = playerKeyString + "_Provocation_Joystick";
            throwKeyJoystick = playerKeyString + "_Throw_Joystick";
        }

        /// <summary>
        /// Checks if the modifier key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        private bool isModifier()
        {
            return Input.GetButton(modifierKeyMouse) || Input.GetButton(modifierKeyJoystick);
        }

        /// <summary>
        /// Checks if the standard attack key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getSAttackKey()
        {
            return Input.GetButtonDown(sAttackKeyMouse) || Input.GetButtonDown(sAttackKeyJoystick);
        }

        /// <summary>
        /// Checks if the modifier key and the standard attack key are pressed for the big melee attack.
        /// </summary>
        /// <returns>True if the keys are pressed.</returns>
        public bool getBMAttackKey()
        {
            return isModifier() && getSAttackKey();
        }

        /// <summary>
        /// Checks if the range attack key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getRangeAttackKey()
        {
            return Input.GetButtonDown(rangeAttackKeyMouse) || Input.GetButtonDown(rangeAttackKeyJoystick);
        }

        /// <summary>
        /// Checks if the jump key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getJumpKey()
        {
            return Input.GetButtonDown(jumpKeyMouse) || Input.GetButtonDown(jumpKeyJoystick);
        }

        /// <summary>
        /// Checks if the throw attack key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getThrowKey()
        {
            return Input.GetButtonDown(throwKeyMouse) || Input.GetButtonDown(throwKeyJoystick);
        }

        /// <summary>
        /// Checks if the movement keys are pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the keys are pressed.</returns>
        public float getHorizontalKey()
        {
            if (Input.GetAxis(horizontalKeyMouse) != 0)
            {
                return Input.GetAxis(horizontalKeyMouse);
            }
            else
            {
                return Input.GetAxis(horizontalKeyJoystick);
            }
        }

        /// <summary>
        /// Checks if the defend key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getDefendKey()
        {
            return Input.GetButtonDown(defendKeyMouse) || Input.GetButtonDown(defendKeyJoystick);
        }

        /// <summary>
        /// Checks if the defend key is released either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getDefendKeyUp()
        {
            return Input.GetButtonUp(defendKeyMouse) || Input.GetButtonUp(defendKeyJoystick);
        }

        /// <summary>
        /// Checks if the modifier key and the range attack key are pressed for performing the super attack.
        /// </summary>
        /// <returns>True if the keys are pressed.</returns>
        public bool getSuperAttackKey()
        {
            return isModifier() && getRangeAttackKey();
        }

        /// <summary>
        /// Checks if the provocation key is pressed either on the keyboard or on the joystick.
        /// </summary>
        /// <returns>True if the key is pressed.</returns>
        public bool getProvocationKey()
        {
            return Input.GetButtonDown(provocationKeyMouse) || Input.GetButtonDown(provocationKeyJoystick);
        }
    }
}
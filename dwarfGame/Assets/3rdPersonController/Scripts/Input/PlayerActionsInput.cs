using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace le0tard.FinalCharacterController
{
    [DefaultExecutionOrder(-2)]
    public class PlayerActionsInput : MonoBehaviour, PlayerControls.IPlayerActionsMapActions
    {
        #region Class Variables
        private PlayerLocomotionInput _playerLocomotionInput;
        private PlayerState _playerState;
        private PlayerController _playerController;
        public bool GatherPressed { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool BlockPressed {  get; private set; }
        #endregion

        [SerializeField] public PlayerCombatState _combatState = PlayerCombatState.Passive;

        #region Startup
        private void Awake()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            _playerState = GetComponent<PlayerState>();
            _playerController = GetComponent<PlayerController>();
        }
        private void OnEnable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("Player controls is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.Enable();
            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("Player controls is not initialized - cannot disable");
                return;
            }

            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.Disable();
            PlayerInputManager.Instance.PlayerControls.PlayerActionsMap.RemoveCallbacks(this);
        }
        #endregion

        #region Update
        private void Update()
        {
            if (_playerLocomotionInput.MovementInput != Vector2.zero ||
                _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping ||
                _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling)
            {
                GatherPressed = false;
            }
        }

        public void SetGatherPressedFalse()
        {
            GatherPressed = false;
        }

        public void SetAttackPressedFalse() 
        { 
            AttackPressed = false;
            if (_combatState != PlayerCombatState.Blocking)
            {
                _combatState = PlayerCombatState.Passive;
            }

        }
        #endregion

        #region Input Callbacks
        public void OnGathering(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            GatherPressed = true;
        }

        public void OnAttacking(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            AttackPressed = true;
            _combatState = PlayerCombatState.Attacking;
        }

        public void OnBlocking(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                BlockPressed = true;   // holding button
                _combatState=PlayerCombatState.Blocking;
                //change to walking here, walking mode?
                _playerLocomotionInput.WalkToggledOn=true;
            }
            else if (context.canceled)
            {
                BlockPressed = false;  // released button
                _combatState = PlayerCombatState.Passive;
                _playerLocomotionInput.WalkToggledOn = false;
            }
        }
        #endregion
    }
}

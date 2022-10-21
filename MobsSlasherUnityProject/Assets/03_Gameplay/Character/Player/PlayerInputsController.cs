using System;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public class PlayerInputsController : MonoBehaviour
    {
        private PlayerInputsActions m_actions = null;

        #region Life Cycle
        void Start()
        {
            m_actions = new PlayerInputsActions();
            m_actions.Enable();

            m_actions.Gameplay.Jump.started += Jump_started;
            m_actions.Gameplay.Jump.canceled += Jump_canceled;
            m_actions.Gameplay.Sprint.started += Sprint_started;
            m_actions.Gameplay.Sprint.canceled += Sprint_canceled;
        }



        private void OnDestroy()
        {
            m_actions.Gameplay.Jump.started -= Jump_started;
            m_actions.Gameplay.Jump.canceled -= Jump_canceled;
            m_actions.Gameplay.Sprint.started -= Sprint_started;
            m_actions.Gameplay.Sprint.canceled -= Sprint_canceled;

            m_actions.Disable();
            m_actions.Dispose();
        }

        void Update()
        {
            ReadMovementInputs();
        }
        #endregion

        #region MovementInputs
        [Header("Movement")]
        [SerializeField]
        private PlayerMovementController m_movementController = null;
        [SerializeField]
        private PlayerCameraController m_cameraController = null;
        private PlayerCharacterMovementInputs m_movementInputs = default;

        private void ReadMovementInputs()
        {
            m_movementInputs.MoveInput = m_actions.Gameplay.Move.ReadValue<Vector2>();
            m_movementInputs.CameraInput = m_actions.Gameplay.LookAround.ReadValue<Vector2>();

            m_movementController.SetInputs(ref m_movementInputs);
            m_cameraController.SetInputs(ref m_movementInputs);
        }
        private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_movementInputs.JumpDown = false;
        }

        private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_movementInputs.JumpDown = true;
        }
        private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_movementInputs.Sprinting = false;
        }

        private void Sprint_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_movementInputs.Sprinting = true;
        }
        #endregion
    }
}
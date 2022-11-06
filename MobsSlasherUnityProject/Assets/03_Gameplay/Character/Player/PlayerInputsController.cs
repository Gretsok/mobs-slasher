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
            m_actions.Gameplay.FirstAttack.started += FirstAttack_started;
            m_actions.Gameplay.FirstAttack.canceled += FirstAttack_canceled;
            m_actions.Gameplay.SecondAttack.started += SecondAttack_started;
            m_actions.Gameplay.SecondAttack.canceled += SecondAttack_canceled;
            m_actions.Gameplay.ThirdAttack.started += ThirdAttack_started;
            m_actions.Gameplay.ThirdAttack.canceled += ThirdAttack_canceled;
        }



        private void OnDestroy()
        {
            m_actions.Gameplay.Jump.started -= Jump_started;
            m_actions.Gameplay.Jump.canceled -= Jump_canceled;
            m_actions.Gameplay.Sprint.started -= Sprint_started;
            m_actions.Gameplay.Sprint.canceled -= Sprint_canceled;
            m_actions.Gameplay.FirstAttack.started -= FirstAttack_started;
            m_actions.Gameplay.FirstAttack.canceled -= FirstAttack_canceled;
            m_actions.Gameplay.SecondAttack.started -= SecondAttack_started;
            m_actions.Gameplay.SecondAttack.canceled -= SecondAttack_canceled;
            m_actions.Gameplay.ThirdAttack.started -= ThirdAttack_started;
            m_actions.Gameplay.ThirdAttack.canceled -= ThirdAttack_canceled;

            m_actions.Disable();
            m_actions.Dispose();
        }

        void Update()
        {
            ReadInputs();
            if(m_inputs.StartFirstAttack)
            {
                m_inputs.StartFirstAttack = false;
            }
            if (m_inputs.StopFirstAttack)
            {
                m_inputs.StopFirstAttack = false;
            }
            if (m_inputs.StartSecondAttack)
            {
                m_inputs.StartSecondAttack = false;
            }
            if (m_inputs.StopSecondAttack)
            {
                m_inputs.StopSecondAttack = false;
            }
            if (m_inputs.StartThirdAttack)
            {
                m_inputs.StartThirdAttack = false;
            }
            if (m_inputs.StopThirdAttack)
            {
                m_inputs.StopThirdAttack = false;
            }
        }
        #endregion

        #region Subsystem
        [Header("Subsystem Refs")]
        [SerializeField]
        private PlayerMovementController m_movementController = null;
        [SerializeField]
        private PlayerCameraController m_cameraController = null;
        [SerializeField]
        private PlayerDamageHandlingController m_combatController = null;
        [SerializeField]
        private PlayerSkillsController m_playerSkillsController = null;
        private PlayerCharacterInputs m_inputs = default;

        private void ReadInputs()
        {
            m_inputs.MoveInput = m_actions.Gameplay.Move.ReadValue<Vector2>();
            m_inputs.CameraInput = m_actions.Gameplay.LookAround.ReadValue<Vector2>();

            m_movementController.SetInputs(ref m_inputs);
            m_cameraController.SetInputs(ref m_inputs);
            m_combatController.SetInputs(ref m_inputs);
            m_playerSkillsController.SetInputs(ref m_inputs);
        }
        #endregion

        #region Movement Callback
        private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.JumpDown = false;
        }

        private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.JumpDown = true;
        }
        private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.Sprinting = false;
        }

        private void Sprint_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.Sprinting = true;
        }
        #endregion

        #region Skills Callback
        private void FirstAttack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.StartFirstAttack = true;
        }

        private void FirstAttack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.StartFirstAttack = true;
        }
        private void SecondAttack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.StopSecondAttack = true;
        }

        private void SecondAttack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.StartSecondAttack = true;
        }
        private void ThirdAttack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.StopThirdAttack = true;
        }

        private void ThirdAttack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_inputs.StartThirdAttack = true;
        }
        #endregion
    }
}
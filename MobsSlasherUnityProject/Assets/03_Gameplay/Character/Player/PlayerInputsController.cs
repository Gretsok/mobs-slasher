using System;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public class PlayerInputsController : MonoBehaviour
    {
        private PlayerInputsActions m_actions = null;

        #region Life Cycle
        // Start is called before the first frame update
        void Start()
        {
            m_actions = new PlayerInputsActions();
            m_actions.Enable();

            m_actions.Gameplay.Jump.performed += Jump_performed;
        }

        private void OnDestroy()
        {
            m_actions.Gameplay.Jump.performed -= Jump_performed;

            m_actions.Disable();
            m_actions.Dispose();
        }

        // Update is called once per frame
        void Update()
        {
            ReadMovementInputs();
        }
        #endregion

        #region MovementInputs
        [Header("Movement")]
        [SerializeField]
        private PlayerMovementController m_movementController = null;
        private PlayerCharacterMovementInputs m_movementInputs = default;

        private void ReadMovementInputs()
        {
            m_movementInputs.MoveInput = m_actions.Gameplay.Move.ReadValue<Vector2>();
            m_movementInputs.CameraInput = m_actions.Gameplay.LookAround.ReadValue<Vector2>();

            m_movementController.SetInputs(ref m_movementInputs);
        }

        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_movementInputs.JumpDown = true;
        }
        #endregion
    }
}
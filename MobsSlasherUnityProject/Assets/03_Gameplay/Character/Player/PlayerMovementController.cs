using KinematicCharacterController;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public struct PlayerCharacterMovementInputs
    {
        public Vector2 MoveInput;
        public Vector2 CameraInput;
        public bool JumpDown;
    }

    public class PlayerMovementController : BaseCharacterMovementController
    {
        PlayerCharacterMovementInputs m_inputs = default;

        [SerializeField]
        private float m_maxSpeed = 5f;

        public void SetInputs(ref PlayerCharacterMovementInputs inputs)
        {
            m_inputs = inputs;
        }

        public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            //if (m_motor.GroundingStatus.IsStableOnGround)
            {
                // Reorient velocity on slope
                currentVelocity = m_motor.GetDirectionTangentToSurface(currentVelocity, m_motor.GroundingStatus.GroundNormal) * currentVelocity.magnitude;

                // Calculate target velocity
                Vector3 inputRight = Vector3.Cross(m_inputs.MoveInput, m_motor.CharacterUp);
                Vector3 reorientedInput = Vector3.Cross(m_motor.GroundingStatus.GroundNormal, inputRight).normalized * m_inputs.MoveInput.magnitude;

                // Applying velocity without smooth
                currentVelocity = reorientedInput * m_maxSpeed;
            }
           // else
            {

            }
        }

    }
}
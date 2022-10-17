using KinematicCharacterController;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    [System.Serializable]
    public struct PlayerCharacterMovementInputs
    {
        public Vector2 MoveInput;
        public Vector2 CameraInput;
        public bool JumpDown;
    }

    public class PlayerMovementController : BaseCharacterMovementController
    {
        PlayerCharacterMovementInputs m_inputs = default;


        [Header("Refs")]
        [SerializeField]
        private Transform m_cameraTarget = null;

        [Header("Movement Params")]
        [SerializeField]
        private float m_maxSpeed = 5f;
        [SerializeField]
        private Vector3 m_gravity = new Vector3(0f, -9.81f, 0f);

        [Header("Camera Params")]
        [SerializeField]
        private float m_cameraSpeed = 50f;
        private float m_cameraRadAngleOnX = 0f;
        [SerializeField]
        private Vector2 m_cameraRadAngleOnXRange = new Vector2(-55f, 75f);

        public void SetInputs(ref PlayerCharacterMovementInputs inputs)
        {
            m_inputs = inputs;
        }

        public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            if (m_motor.GroundingStatus.IsStableOnGround)
            {
                // Reorient velocity on slope
                currentVelocity = m_motor.GetDirectionTangentToSurface(currentVelocity, m_motor.GroundingStatus.GroundNormal) * currentVelocity.magnitude;

                Vector3 move3DInput = transform.TransformDirection(new Vector3(m_inputs.MoveInput.x, 0f, m_inputs.MoveInput.y));
                // Calculate target velocity
                Vector3 inputRight = Vector3.Cross(move3DInput, m_motor.CharacterUp);
                Vector3 reorientedInput = Vector3.Cross(m_motor.GroundingStatus.GroundNormal, inputRight).normalized * move3DInput.magnitude;

                // Applying velocity without smooth
                currentVelocity = reorientedInput * m_maxSpeed;
            }
            else
            {
                // Gravity
                currentVelocity += m_gravity * deltaTime;
            }
        }

        public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            // m_cameraTarget.localEulerAngles += m_inputs.CameraInput.y * m_cameraSpeed * deltaTime * Vector3.right;
            m_cameraRadAngleOnX += m_inputs.CameraInput.y * deltaTime * m_cameraSpeed * Mathf.Deg2Rad;
            m_cameraRadAngleOnX = Mathf.Clamp(m_cameraRadAngleOnX, -m_cameraRadAngleOnXRange.y * Mathf.Deg2Rad, -m_cameraRadAngleOnXRange.x * Mathf.Deg2Rad);
            m_cameraTarget.forward = transform.TransformDirection(new Vector3(0f, Mathf.Sin(m_cameraRadAngleOnX), Mathf.Cos(m_cameraRadAngleOnX)));

            currentRotation.eulerAngles += transform.up * m_cameraSpeed * m_inputs.CameraInput.x * deltaTime;
        }
    }
}
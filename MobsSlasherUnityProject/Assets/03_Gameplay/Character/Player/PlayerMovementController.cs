using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    [System.Serializable]
    public struct PlayerCharacterMovementInputs
    {
        public Vector2 MoveInput;
        public Vector2 CameraInput;
        public bool JumpDown;
        public bool Sprinting;
    }

    public class PlayerMovementController : BaseCharacterMovementController
    {
        PlayerCharacterMovementInputs m_inputs = default;


        [Header("Refs")]
        [SerializeField]
        private Transform m_cameraTarget = null;

        [Header("Movement Params")]
        [SerializeField]
        private float m_maxNormalSpeed = 5f;
        [SerializeField]
        private float m_maxSprintSpeed = 8f;
        [SerializeField]
        private Vector3 m_gravity = new Vector3(0f, -9.81f, 0f);
        [SerializeField]
        private float m_movementSmooth = 8f;
        [SerializeField]
        private float m_orientationSmooth = 15f;
        [SerializeField]
        private float m_jumpVelocity = 5f;
        private const float JUMP_COOLDOWN = 0.2f;
        private float m_timeOfLastJump = float.MinValue;
        [SerializeField]
        private float m_airDeceleration = 2f;



        public void SetInputs(ref PlayerCharacterMovementInputs inputs)
        {
            m_inputs = inputs;
        }

        public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            if (m_motor.GroundingStatus.IsStableOnGround)
            {
                Vector3 move3DInput = m_cameraTarget.TransformDirection(new Vector3(m_inputs.MoveInput.x, 0f, m_inputs.MoveInput.y));
                // Calculate target velocity
                Vector3 inputRight = Vector3.Cross(move3DInput, m_motor.CharacterUp);
                Vector3 reorientedInput = Vector3.Cross(m_motor.GroundingStatus.GroundNormal, inputRight).normalized * move3DInput.magnitude;

                Vector3 velocityTarget = default;

                if (m_inputs.Sprinting)
                {
                    velocityTarget = reorientedInput.normalized * m_maxSprintSpeed;
                }
                else
                {
                    velocityTarget = reorientedInput * m_maxNormalSpeed;
                }

                // Reorient velocity on slope
                velocityTarget = m_motor.GetDirectionTangentToSurface(velocityTarget, m_motor.GroundingStatus.GroundNormal) * velocityTarget.magnitude;

                currentVelocity = Vector3.Lerp(currentVelocity, velocityTarget, deltaTime * m_movementSmooth);

                if(m_inputs.JumpDown && Time.time - m_timeOfLastJump > JUMP_COOLDOWN)
                {
                    m_motor.ForceUnground(JUMP_COOLDOWN);
                    Debug.Log($"JUMP delay :{Time.time - m_timeOfLastJump}");
                    currentVelocity += Vector3.up * m_jumpVelocity;
                    m_timeOfLastJump = Time.time;
                }

            }
            else
            {
                // In Air movement

                Vector3 nonGravityMovement = Vector3.ProjectOnPlane(currentVelocity, m_gravity);

                /*Vector3 move3DInput = m_cameraTarget.TransformDirection(new Vector3(m_inputs.MoveInput.x, 0f, m_inputs.MoveInput.y));
                Vector3 flattenMove3Dinput = new Vector3(move3DInput.x, 0f, move3DInput.z).normalized;*/


                currentVelocity +=
                    // Gravity
                    m_gravity * deltaTime
                    // Air deceleration
                    - nonGravityMovement * deltaTime * m_airDeceleration;

                
            }
        }

        public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            if(m_motor.GroundingStatus.IsStableOnGround)
            {
                if (m_motor.BaseVelocity.sqrMagnitude > 0.001f)
                {
                    currentRotation = Quaternion.Lerp(currentRotation,
                        Quaternion.LookRotation(m_motor.BaseVelocity, m_motor.GroundingStatus.GroundNormal),
                        deltaTime * m_orientationSmooth);
                }
                else
                {
                    currentRotation = Quaternion.Lerp(currentRotation,
                        Quaternion.LookRotation(transform.forward, m_motor.GroundingStatus.GroundNormal),
                        deltaTime * m_orientationSmooth);
                }
            }
            else
            {
                Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z);
                flatForward.Normalize();

                currentRotation = Quaternion.Lerp(currentRotation,
                        Quaternion.LookRotation(flatForward, Vector3.up),
                        deltaTime * m_orientationSmooth);
            }
            
        }
        

    }
}
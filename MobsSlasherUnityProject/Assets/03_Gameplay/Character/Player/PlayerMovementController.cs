using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    [System.Serializable]
    public struct PlayerCharacterInputs
    {
        public Vector2 MoveInput;
        public Vector2 CameraInput;
        public bool JumpDown;
        public bool Sprinting;
        public bool StartFirstAttack;
        public bool StopFirstAttack;
        public bool StartSecondAttack;
        public bool StopSecondAttack;
        public bool StartThirdAttack;
        public bool StopThirdAttack;
    }

    public class PlayerMovementController : BaseCharacterMovementController
    {
        PlayerCharacterInputs m_inputs = default;


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
        [SerializeField]
        private float m_inAirMovementSmooth = 5f;
        private Vector3 m_inAirFlattenMove3DInput = default;




        public void SetInputs(ref PlayerCharacterInputs inputs)
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
                m_inAirFlattenMove3DInput = default;
            }
            else
            {
                // In Air movement
                Vector3 nonGravityMovement = Vector3.ProjectOnPlane(currentVelocity, m_gravity);


                if(m_inputs.MoveInput.sqrMagnitude > 0.001f)
                {
                    Vector3 move3DInput = m_cameraTarget.TransformDirection(
                        m_inputs.MoveInput.x, 
                        0f,
                        m_inputs.MoveInput.y);
                    m_inAirFlattenMove3DInput = // Vector3.Lerp(m_inAirFlattenMove3DInput, 
                        new Vector3(move3DInput.x, 0f, move3DInput.z).normalized;//,
                        // deltaTime * m_inAirMovementSmooth);


                    currentVelocity +=
                        // Gravity
                        m_gravity * deltaTime
                        // Cancel movement
                        -nonGravityMovement
                        // Air control
                        + m_inAirFlattenMove3DInput * nonGravityMovement.magnitude * (1 - deltaTime * m_airDeceleration);
                }
                else
                {

                    currentVelocity +=
                        // Gravity
                        m_gravity * deltaTime
                        // Air deceleration
                        - nonGravityMovement * deltaTime * m_airDeceleration;
                }
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
                currentRotation = Quaternion.Lerp(currentRotation,
                        Quaternion.LookRotation(m_inAirFlattenMove3DInput, Vector3.up),
                        deltaTime * m_orientationSmooth);
            }
            
        }
        

    }
}
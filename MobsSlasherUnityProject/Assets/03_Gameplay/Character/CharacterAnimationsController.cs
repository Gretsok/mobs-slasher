using UnityEngine;

namespace Mobs.Gameplay.Character
{
    public class CharacterAnimationsController : MonoBehaviour
    {
        [SerializeField]
        private Animator m_animator = null;
        [SerializeField]
        private KinematicCharacterController.KinematicCharacterMotor m_movementMotor = null;

        #region Life Cycle
        private void LateUpdate()
        {
            SetSpeed(m_movementMotor.BaseVelocity.magnitude);
            SetIsGrounded(m_movementMotor.GroundingStatus.IsStableOnGround);
        }

        #endregion


        #region Movement
        private readonly int m_speedAnimationHash = Animator.StringToHash("Speed");

        private void SetSpeed(float m_speed)
        {
            m_animator.SetFloat(m_speedAnimationHash, m_speed);
        }

        private readonly int m_isGroundedHash = Animator.StringToHash("IsGrounded");
        private void SetIsGrounded(bool a_isGrounded)
        {
            m_animator.SetBool(m_isGroundedHash, a_isGrounded);
        }

        #endregion
    }
}
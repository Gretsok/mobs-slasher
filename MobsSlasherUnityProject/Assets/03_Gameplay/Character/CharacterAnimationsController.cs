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
        }

        #endregion


        #region Movement
        private readonly int m_speedAnimationHash = Animator.StringToHash("Speed");

        private void SetSpeed(float m_speed)
        {
            m_animator.SetFloat(m_speedAnimationHash, m_speed);
        }

        #endregion
    }
}
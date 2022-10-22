using System.Collections;
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

        #region Attack
        #region Sword Hit
        private readonly int m_swordHitAnimationHash = Animator.StringToHash("SwordHit");

        public void PlaySwordHitAnimation()
        {
            m_animator.SetTrigger(m_swordHitAnimationHash);
            ActivateSwordHitLayer();
        }

        public void ActivateSwordHitLayer()
        {
            StartCoroutine(ActivatingSwordHitLayerRoutine());
        }
        private IEnumerator ActivatingSwordHitLayerRoutine()
        {
            float weight = 0f;
            int layerIndex = m_animator.GetLayerIndex("SwordHitLayer");
            m_animator.SetLayerWeight(layerIndex, weight);
            float lastTime = Time.time;
            while(weight < 1f)
            {
                float currentTime = Time.time;
                weight += (1f / 0.2f) * (currentTime - lastTime);
                m_animator.SetLayerWeight(layerIndex, weight);
                lastTime = currentTime;
                yield return null;
            }
            weight = 1;
            m_animator.SetLayerWeight(layerIndex, weight);
        }

        public void DeactivateSwordHitLayer()
        {
            StartCoroutine(DeactivatingSwordHitLayerRoutine());
        }

        private IEnumerator DeactivatingSwordHitLayerRoutine()
        {
            float weight = 1f;
            int layerIndex = m_animator.GetLayerIndex("SwordHitLayer");
            m_animator.SetLayerWeight(layerIndex, weight);
            float lastTime = Time.time;
            while (weight > 0f)
            {
                float currentTime = Time.time;
                weight -= (1f / 0.2f) * (currentTime - lastTime);
                m_animator.SetLayerWeight(layerIndex, weight);
                lastTime = currentTime;
                yield return null;
            }
            weight = 0;
            m_animator.SetLayerWeight(layerIndex, weight);
        }
        #endregion
        #endregion
    }
}
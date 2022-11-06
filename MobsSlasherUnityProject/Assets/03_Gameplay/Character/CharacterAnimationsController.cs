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

        private AnimatorOverrideController m_overrideController = null;
        #region Life Cycle
        private void Start()
        {
            m_overrideController = new AnimatorOverrideController(m_animator.runtimeAnimatorController);
            m_animator.runtimeAnimatorController = m_overrideController;
        }

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
        private readonly int m_upperBodyPartActionAnimationHash = Animator.StringToHash("UpperBodyPartAction");

        public void PlayUpperBodyPartActionAnimation(AnimationClip a_actionClip)
        {
            if(a_actionClip == null)
            {
                Debug.LogError("Action clip is null");
                return;
            }


            m_overrideController["UpperBodyPartAction"] = a_actionClip;

            m_animator.SetTrigger(m_upperBodyPartActionAnimationHash);
            ActivateSwordHitLayer();
        }

        public void ActivateSwordHitLayer()
        {
            StartCoroutine(ActivatingUpperBodyPartLayerRoutine());
        }
        private IEnumerator ActivatingUpperBodyPartLayerRoutine()
        {
            float weight = 0f;
            int layerIndex = m_animator.GetLayerIndex("UpperBodyPart");
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

        public void DeactivateUpperBodyPartLayer()
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
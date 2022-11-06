using Mobs.Gameplay.Character;
using UnityEngine;

namespace Mobs.Gameplay.Combat.Skills
{
    public class SwordAttackSkill : BaseSkill<Character.CharacterSkillsController>
    {
        [SerializeField]
        private DamageDealer m_swordPrefab = null;
        [SerializeField]
        private Vector3 m_swordLocalPositionInHand = default;
        [SerializeField]
        private Vector3 m_swordLocalEulerAnglesInHand = default;
        [SerializeField]
        private AnimationClip m_attackAnimation = null;
        private DamageDealer m_sword = null;
        protected override void OnSkillStarted()
        {
            base.OnSkillStarted();
            m_sword = Instantiate(m_swordPrefab, CastedController.RightHand);
            m_sword.transform.localPosition = m_swordLocalPositionInHand;
            m_sword.transform.localEulerAngles = m_swordLocalEulerAnglesInHand;
            m_sword.SetOwner(CastedController.DamageHandlingController);
            CastedController.AnimationsController.PlayUpperBodyPartActionAnimation(m_attackAnimation);
            CastedController.AnimationEventsHandler.OnAnimationEventThrown += HandleAnimationEventThrown;
        }

        private void HandleAnimationEventThrown(CharacterAnimationEventsHandler a_animationEventsHandler, string a_eventKey)
        {

            switch(a_eventKey)
            {
                case "SwordHit_StartDealingDamage":
                    StartDealingDamage();
                    break;
                case "SwordHit_StopDealingDamage":
                    StopDealingDamage();
                    break;
                case "SwordHit_AnimationEnd":
                    HandleAnimationEnded();
                    break;
            }

        }

        private void HandleAnimationEnded()
        {
            CastedController.AnimationsController.DeactivateUpperBodyPartLayer();
            Destroy(m_sword.gameObject);
            m_isExecuting = false;
            CastedController.AnimationEventsHandler.OnAnimationEventThrown -= HandleAnimationEventThrown;
        }

        private void StopDealingDamage()
        {
            m_sword.SetCanDealDamage(false);
        }

        private void StartDealingDamage()
        {
            m_sword.ResetTargetsHitData();
            m_sword.SetCanDealDamage(true);
        }

        protected override void OnSkillStopped()
        {

        }
    }
}
using UnityEngine;

namespace Mobs.Gameplay.Character
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {
        [SerializeField]
        private CharacterAnimationsController m_animationsController = null;

        [SerializeField]
        private Combat.DamageDealer m_sword = null;

        #region SwordHit
        public void SwordHit_AnimationEnd()
        {
            m_animationsController.DeactivateSwordHitLayer();
        }

        public void SwordHit_StartDealingDamage()
        {
            m_sword.ResetTargetsHitData();
            m_sword.SetCanDealDamage(true);
        }

        public void SwordHit_StopDealingDamage()
        {
            m_sword.SetCanDealDamage(false);
        }

        #endregion
    }
}
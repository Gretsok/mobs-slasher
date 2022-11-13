using Mobs.Gameplay.Combat.Skills;
using UnityEngine;

namespace Mobs.Gameplay.Character
{
    public class CharacterSkillsController : BaseSkillsController
    {
        [SerializeField]
        private CharacterAnimationsController m_animationsController = null;
        public CharacterAnimationsController AnimationsController => m_animationsController;
        [SerializeField]
        private CharacterAnimationEventsHandler m_animationEventsHandler = null;
        public CharacterAnimationEventsHandler AnimationEventsHandler => m_animationEventsHandler;

        [SerializeField]
        private Transform m_rightHand = null;
        public Transform RightHand => m_rightHand;



        public virtual Ray GetSight()
        {
            return new Ray(transform.position + Vector3.up, transform.forward);
        }
    }
}
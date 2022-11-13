
using Mobs.Gameplay.Character;
using System;
using UnityEngine;

namespace Mobs.Gameplay.Combat.Skills
{
    public class FireBallSkill : BaseSkill<Character.CharacterSkillsController>
    {
        [SerializeField]
        private float m_cooldown = 2.5f;
        [SerializeField]
        private FireBall m_fireBallPrefab = null;
        [SerializeField]
        private Vector3 m_fireballLocalOffset = Vector3.forward * 0.2f;
        [SerializeField]
        private AnimationClip m_throw = null;

        private float m_timeOfLastFireThrown = float.MinValue;

        protected override void OnSkillStarted()
        {
            if (Time.time - m_timeOfLastFireThrown < m_cooldown) return;

            base.OnSkillStarted();
            
            CastedController.AnimationsController.PlayUpperBodyPartActionAnimation(m_throw);
            CastedController.AnimationEventsHandler.OnAnimationEventThrown += HandleAnimationEventThrown;


            m_timeOfLastFireThrown = Time.time;
        }

        protected override void OnSkillStopped()
        {
            
        }

        private void HandleAnimationEventThrown(CharacterAnimationEventsHandler arg1, string a_eventKey)
        {
            switch (a_eventKey)
            {
                case "Throw_EndAnimation":
                    HandleEndOfAnimation();
                    break;
                case "Throw_FireBall":
                    ThrowFireBall();
                    break;
            }
        }

        private void ThrowFireBall()
        {
            var newFireBall = Instantiate(m_fireBallPrefab);
            newFireBall.transform.position = CastedController.RightHand.TransformPoint(m_fireballLocalOffset);
            newFireBall.SetOwner(CastedController.DamageHandlingController);
            Ray sight = CastedController.GetSight();
            newFireBall.transform.forward = sight.direction;

            if (Physics.Raycast(sight, out RaycastHit hitInfo, 150f))
            {
                newFireBall.transform.forward = (hitInfo.point - newFireBall.transform.position).normalized;
            }
        }

        private void HandleEndOfAnimation()
        {
            CastedController.AnimationsController.DeactivateUpperBodyPartLayer();
            CastedController.AnimationEventsHandler.OnAnimationEventThrown -= HandleAnimationEventThrown;
            m_isExecuting = false;
        }
    }
}
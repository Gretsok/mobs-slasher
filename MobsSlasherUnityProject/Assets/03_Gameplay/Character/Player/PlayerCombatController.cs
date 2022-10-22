using Mobs.Gameplay.Combat;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public class PlayerCombatController : CombatController
    {
        [SerializeField]
        private CharacterAnimationsController m_animationsController = null;

        private PlayerCharacterMovementInputs m_inputs;

        public void SetInputs(ref PlayerCharacterMovementInputs inputs)
        {
            m_inputs = inputs;
        }

        [SerializeField]
        private float m_attackCooldown = 2f;
        private float m_lastTimeOfAttack = 0f;


        protected override void Update()
        {
            base.Update();
            if(m_inputs.Attacking)
            {
                if(Time.time - m_lastTimeOfAttack > m_attackCooldown)
                {
                    Attack();
                    m_lastTimeOfAttack = Time.time;
                }
            }
        }

        private void Attack()
        {
            m_animationsController.PlaySwordHitAnimation();
        }
    }
}
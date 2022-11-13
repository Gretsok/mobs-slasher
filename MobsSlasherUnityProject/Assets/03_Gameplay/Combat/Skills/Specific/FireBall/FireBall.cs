using UnityEngine;

namespace Mobs.Gameplay.Combat.Skills
{
    public class FireBall : DamageDealer
    {
        [SerializeField]
        private float m_lifeDuration = 10f;
        private float m_timeOfStart = 0f;

        private void Start()
        {
            m_timeOfStart = Time.time;
        }

        private void Update()
        {
            if(Time.time - m_timeOfStart > m_lifeDuration)
            {
                DestroyFireBall();      
            }
        }

        protected override void OnEnnemyDamageHandlingControllerCollisionRelayHit(DamageHandlingControllerCollisionRelay a_combatControllerCollisionRelay)
        {
            if(Owner == null || a_combatControllerCollisionRelay.Owner.TeamIndex != Owner.TeamIndex)
            {
                DestroyFireBall();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            DestroyFireBall();
        }

        private void DestroyFireBall()
        {
            if (gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
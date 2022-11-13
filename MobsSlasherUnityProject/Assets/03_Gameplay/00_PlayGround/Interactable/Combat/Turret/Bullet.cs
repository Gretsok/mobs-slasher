using Mobs.Gameplay.Combat;
using UnityEngine;

namespace Mobs.Gameplay.Playground.Combat
{
    public class Bullet : DamageDealer
    {
        [SerializeField]
        private float m_travelSpeed = 15f;

        private void FixedUpdate()
        {
            transform.position += transform.forward * m_travelSpeed * Time.fixedDeltaTime;
        }

        protected override void OnEnnemyDamageHandlingControllerCollisionRelayHit(DamageHandlingControllerCollisionRelay a_combatControllerCollisionRelay)
        {
            base.OnEnnemyDamageHandlingControllerCollisionRelayHit(a_combatControllerCollisionRelay);
            Destroy(gameObject);
        }
    }
}
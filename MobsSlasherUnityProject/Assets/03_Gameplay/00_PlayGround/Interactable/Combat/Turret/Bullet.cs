using Mobs.Gameplay.Combat;
using UnityEngine;

namespace Mobs.Gameplay.Interactable.Combat
{
    public class Bullet : DamageDealer
    {
        [SerializeField]
        private float m_travelSpeed = 15f;

        private void FixedUpdate()
        {
            transform.position += transform.forward * m_travelSpeed * Time.fixedDeltaTime;
        }

        protected override void OnCombatControllerCollisionRelayHit(DamageHandlingControllerCollisionRelay a_combatControllerCollisionRelay)
        {
            base.OnCombatControllerCollisionRelayHit(a_combatControllerCollisionRelay);
            Destroy(gameObject);
        }
    }
}
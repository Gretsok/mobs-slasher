using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Gameplay.Combat
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField]
        private CombatController m_owner = null;
        public CombatController Owner => m_owner;

        public void SetOwner(CombatController a_owner)
        {
            m_owner = a_owner;
        }


        [SerializeField]
        private int m_damageToDeal = 5;

        [SerializeField]
        private float m_damageDealingToATargetCooldown = 1f;
        [System.Serializable]
        public class TargetHitData
        {
            public CombatController CombatController = null;
            public float LastTimeHit = float.MinValue;
        }
        private List<TargetHitData> m_targetsHit = new List<TargetHitData>();

        public void ResetTargetsHitData()
        {
            m_targetsHit.Clear();
        }

        [SerializeField]
        private bool m_canDealDamage = true;
        public bool CanDealDamage => m_canDealDamage;

        public void SetCanDealDamage(bool a_canDealDamage)
        {
            m_canDealDamage = a_canDealDamage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CombatControllerCollisionRelay combatControllerCollisionRelay) && m_canDealDamage)
            {
                var registeredTarget = m_targetsHit.Find(t => t.CombatController == combatControllerCollisionRelay.Owner);
                if (registeredTarget != null)
                {
                    if(Time.time - registeredTarget.LastTimeHit < m_damageDealingToATargetCooldown)
                    {
                        return;
                    }
                }
                else
                {
                    registeredTarget = new TargetHitData();
                    registeredTarget.CombatController = combatControllerCollisionRelay.Owner;
                    m_targetsHit.Add(registeredTarget);
                }

                Damage newDamage = new Damage();
                newDamage.DamageToDeal = m_damageToDeal;
                newDamage.Source = this;
                registeredTarget.LastTimeHit = Time.time;
                combatControllerCollisionRelay.Owner.TakeDamage(newDamage);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Gameplay.Combat
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField]
        private DamageHandlingController m_owner = null;
        public DamageHandlingController Owner => m_owner;

        public void SetOwner(DamageHandlingController a_owner)
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
            public DamageHandlingController CombatController = null;
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

        protected virtual void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out DamageHandlingControllerCollisionRelay combatControllerCollisionRelay))
            {
                if (m_canDealDamage
                && (Owner == null || combatControllerCollisionRelay.Owner == null || Owner.TeamIndex != combatControllerCollisionRelay.Owner.TeamIndex))
                {
                    var registeredTarget = m_targetsHit.Find(t => t.CombatController == combatControllerCollisionRelay.Owner);
                    if (registeredTarget != null)
                    {
                        if (Time.time - registeredTarget.LastTimeHit < m_damageDealingToATargetCooldown)
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

                    OnEnnemyDamageHandlingControllerCollisionRelayHit(combatControllerCollisionRelay);
                }
            }
            else
            {
                OnHitWithNoDamageHandlingController();
            }
        }

        protected virtual void OnHitWithNoDamageHandlingController()
        {
            
        }

        protected virtual void OnEnnemyDamageHandlingControllerCollisionRelayHit(DamageHandlingControllerCollisionRelay a_combatControllerCollisionRelay)
        {

        }
    }
}
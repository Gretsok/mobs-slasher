using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mobs.Gameplay.Combat
{
    public class CombatController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        private CombatControllerParameters m_params = default;

        public int TeamIndex => m_params.TeamIndex;
        public int MaxLifePoints => m_params.BaseMaxLifePoints;

        private int m_lifePoints = 0;
        public int LifePoints => m_lifePoints;

        [Header("Events")]
        [SerializeField]
        private UnityEvent m_onDamageTakenForEditor = null;
        [SerializeField]
        private UnityEvent m_onDiedForEditor = null;
        public Action<CombatController, int, int> OnDamageTaken = null;
        public Action<CombatController> OnDied = null;


        public void SetCombatControllerParameters(CombatControllerParameters a_parameters)
        {
            m_params = a_parameters;
            Initialize();
        }

        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        { }

        private void Initialize()
        {
            m_lifePoints = MaxLifePoints;
        }

        protected virtual void Update()
        {

        }

        public void TakeDamage(Damage a_damage)
        {
            if(a_damage == null)
            {
                Debug.LogError($"Damage to deal to {gameObject.name} is null");
                return;
            }

            if(a_damage.Source == null 
                || a_damage.Source.Owner.TeamIndex != TeamIndex)
            {
                var lifePointsBeforeHit = m_lifePoints;
                m_lifePoints -= a_damage.DamageToDeal;
                HandleDamageTaken(m_lifePoints, lifePointsBeforeHit);
            }
        }

        private void HandleDamageTaken(int a_newLifePoints, int a_oldLifePoints)
        {
            m_onDamageTakenForEditor?.Invoke();
            OnDamageTaken?.Invoke(this, a_newLifePoints, a_oldLifePoints);
            if(a_newLifePoints <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            m_onDiedForEditor?.Invoke();
            OnDied?.Invoke(this);
        }
    }
}
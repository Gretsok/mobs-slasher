using Mobs.Gameplay.Combat;
using Mobs.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Gameplay.Interactable.Combat
{
    public class TurretCombatController : CombatController
    {
        private List<CombatControllerCollisionRelay> m_closeTargets = new List<CombatControllerCollisionRelay>();
        private CombatControllerCollisionRelay m_currentTarget = null;
        [SerializeField]
        private Transform m_canon = null;
        [SerializeField]
        private float m_aimSmoothness = 11f;
        private Vector3 m_aimPos = default;

        [SerializeField]
        private CollisionsEventsRelay m_detectionZone = null;

        [Header("Shooting")]
        [SerializeField]
        private DamageDealer m_bullet = null;
        [SerializeField]
        private Transform m_bulletSpawnPoint = null;
        [SerializeField]
        private float m_shootingCooldown = 2f;
        private float m_lastTimeOfShoot = 0f;

        public void ShootBullet()
        {
            if (Time.time - m_lastTimeOfShoot < m_shootingCooldown) return;
            var newBullet = Instantiate(m_bullet, m_bulletSpawnPoint.position, m_bulletSpawnPoint.rotation);
            Debug.Log($"new bullet : {newBullet.name} created");
            newBullet.SetOwner(this);
            m_lastTimeOfShoot = Time.time;
        }


        protected override void Start()
        {
            base.Start();
            m_detectionZone.OnTriggerEnterEvent += HandleDetectionZoneTriggerEntered;
            m_detectionZone.OnTriggerExitEvent += HandleDetectionZoneTriggerExit;
            m_aimPos = m_canon.position + m_canon.forward;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_detectionZone.OnTriggerEnterEvent -= HandleDetectionZoneTriggerEntered;
            m_detectionZone.OnTriggerExitEvent -= HandleDetectionZoneTriggerExit;
        }

        protected override void Update()
        {
            base.Update();
            ManageTargets();

        }

        private void FixedUpdate()
        {
            if(m_currentTarget)
            {
                m_aimPos = Vector3.Lerp(m_aimPos, m_currentTarget.transform.position, m_aimSmoothness * Time.fixedDeltaTime);
                m_canon.LookAt(m_aimPos);
                ShootBullet();
            }
        }

        private void ManageTargets()
        {
            if (!m_currentTarget && m_closeTargets.Count > 0)
            {
                m_currentTarget = m_closeTargets[0];
            }


            if (m_currentTarget)
            {
                m_closeTargets?.ForEach(t =>
                {
                    if (Vector3.Distance(m_canon.position, t.transform.position)
                    < Vector3.Distance(m_canon.position, m_currentTarget.transform.position))
                    {
                        m_currentTarget = t;
                    }

                });

                
            }
        }


        private void HandleDetectionZoneTriggerEntered(Collider other)
        {
            if(other.TryGetComponent(out CombatControllerCollisionRelay a_combatCollisionRelay) 
                && (!a_combatCollisionRelay.Owner || a_combatCollisionRelay.Owner.TeamIndex != TeamIndex))
            {
                if(!m_closeTargets.Contains(a_combatCollisionRelay))
                {
                    m_closeTargets.Add(a_combatCollisionRelay);
                }
            }
        }

        private void HandleDetectionZoneTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CombatControllerCollisionRelay a_combatCollisionRelay))
            {
                if (m_closeTargets.Contains(a_combatCollisionRelay))
                {
                    m_closeTargets.Remove(a_combatCollisionRelay);
                }
                if (a_combatCollisionRelay == m_currentTarget) 
                    m_currentTarget = null;
            }
        }
    }
}
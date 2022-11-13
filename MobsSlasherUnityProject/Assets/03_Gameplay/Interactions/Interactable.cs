using Mobs.Tools;
using UnityEngine;

namespace Mobs.Gameplay.Interactions
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private AInteractionEffect m_activationEffect = null;
        [SerializeField]
        private AInteractionEffect m_enterSightEffect = null;
        [SerializeField]
        private AInteractionEffect m_exitSightEffect = null;
        [SerializeField]
        private AInteractionEffect m_enterProximityEffect = null;
        [SerializeField]
        private AInteractionEffect m_exitProximityEffect = null;
        [SerializeField]
        private CollisionsEventsRelay m_detectionZoneForProximity = null;

        private void Start()
        {
            if(m_detectionZoneForProximity)
            {
                m_detectionZoneForProximity.OnTriggerEnterEvent += HandleDetectionZoneTriggerEnter;
                m_detectionZoneForProximity.OnTriggerExitEvent += HandleDetectionZoneTriggerExit;
                m_detectionZoneForProximity.gameObject.layer = LayerMask.NameToLayer("DetectionZone");
            }
            
        }

        private void OnDestroy()
        {
            if(m_detectionZoneForProximity)
            {
                m_detectionZoneForProximity.OnTriggerEnterEvent -= HandleDetectionZoneTriggerEnter;
                m_detectionZoneForProximity.OnTriggerExitEvent -= HandleDetectionZoneTriggerExit;
            }  
        }

        public void DoActivationEffect(Interactor a_interactor)
        {
            if(m_activationEffect)
            {
                m_activationEffect.DoEffect(a_interactor);
            }
        }
        public void DoEnterSightEffect(Interactor a_interactor)
        {
            if (m_enterSightEffect)
            {
                m_enterSightEffect.DoEffect(a_interactor);
            }
        }

        public void DoExitSightEffect(Interactor a_interactor)
        {
            if (m_exitSightEffect)
            {
                m_exitSightEffect.DoEffect(a_interactor);
            }
        }

        private void DoEnterProximityEffect(Interactor a_interactor)
        {
            if (m_enterProximityEffect)
            {
                m_enterProximityEffect.DoEffect(a_interactor);
            }
        }

        private void DoExitProximityEffect(Interactor a_interactor)
        {
            if (m_exitProximityEffect)
            {
                m_exitProximityEffect.DoEffect(a_interactor);
            }
        }

        #region Detection Zone Callbacks
        private void HandleDetectionZoneTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactor interactor))
            {
                DoExitProximityEffect(interactor);
            }
        }

        private void HandleDetectionZoneTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Interactor interactor))
            {
                DoEnterProximityEffect(interactor);
            }
        }
        #endregion
    }
}

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

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Interactor interactor))
            {
                DoEnterProximityEffect(interactor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactor interactor))
            {
                DoExitProximityEffect(interactor);
            }
        }
    }
}

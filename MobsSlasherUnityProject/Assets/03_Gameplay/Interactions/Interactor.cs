using UnityEngine;

namespace Mobs.Gameplay.Interactions
{
    public class Interactor : MonoBehaviour
    {
        private Interactable m_currentInteractable = null;

        [SerializeField]
        private LayerMask m_interactionMask = default;
        [SerializeField]
        private float m_maxInteractionDistance = 2f;

        private void FixedUpdate()
        {
            Interactable interactableDetectedThisFrame = null;
            var sight = GetSight();
            if(Physics.Raycast(sight, out RaycastHit hitInfo, m_maxInteractionDistance, m_interactionMask))
            {
                if (Vector3.Distance(transform.position, hitInfo.collider.transform.position) < m_maxInteractionDistance)
                {
                    interactableDetectedThisFrame = hitInfo.collider.GetComponent<Interactable>();
                    Debug.DrawLine(sight.origin, hitInfo.point, Color.green);
                }
                else
                {
                    Debug.DrawLine(sight.origin, hitInfo.point, Color.yellow);
                }
            }
            else
            {
                Debug.DrawLine(sight.origin, sight.origin + sight.direction * m_maxInteractionDistance, Color.red);
            }

            if(interactableDetectedThisFrame != m_currentInteractable)
            {
                if(m_currentInteractable)
                {
                    m_currentInteractable.DoExitSightEffect(this);
                }
                m_currentInteractable = interactableDetectedThisFrame;
                if(m_currentInteractable)
                {
                    m_currentInteractable.DoEnterSightEffect(this);
                }
            }
        }

        public void Interact()
        {
            if(m_currentInteractable)
            {
                m_currentInteractable.DoActivationEffect(this);
            }
        }

        protected virtual Ray GetSight()
        {
            return new Ray(transform.position + Vector3.up, transform.forward);
        }


    }
}
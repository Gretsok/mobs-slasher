using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Gameplay.Interactions
{
    public class Interactor : MonoBehaviour
    {
        private List<Interactable> m_overlappingInteractables = new List<Interactable>();

        private Interactable m_currentInteractable = null;

        [SerializeField]
        private LayerMask m_interactionMask = default;
        [SerializeField]
        private float m_maxInteractionDistance = 2f;

        private void FixedUpdate()
        {
            AvoidNullRefsInOverlappingInteractables();

            HandleSightInteraction();
        }

        private void AvoidNullRefsInOverlappingInteractables()
        {
            for (int i = m_overlappingInteractables.Count - 1; i >= 0; --i)
            {
                if (!m_overlappingInteractables[i])
                {
                    m_overlappingInteractables.RemoveAt(i);
                }
            }
        }

        private void HandleSightInteraction()
        {
            Interactable interactableDetectedThisFrame = null;
            if(m_overlappingInteractables.Count > 0)
            {
                interactableDetectedThisFrame = m_overlappingInteractables[0];
            }
            else
            {
                var sight = GetSight();
                if (Physics.Raycast(sight, out RaycastHit hitInfo, m_maxInteractionDistance, m_interactionMask))
                {
                    interactableDetectedThisFrame = hitInfo.collider.GetComponent<Interactable>();

                    if (interactableDetectedThisFrame)
                    {
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
            }

            

            if (interactableDetectedThisFrame != m_currentInteractable)
            {
                if (m_currentInteractable)
                {
                    m_currentInteractable.DoExitSightEffect(this);
                }
                m_currentInteractable = interactableDetectedThisFrame;
                if (m_currentInteractable)
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

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Interactable interactable))
            {
                if (!m_overlappingInteractables.Contains(interactable))
                {
                    m_overlappingInteractables.Add(interactable);
                }
            } 
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Interactable interactable))
            {
                m_overlappingInteractables.Remove(interactable);
            }
        }
    }
}
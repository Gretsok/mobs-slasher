using System;
using UnityEngine;

namespace Mobs.Tools
{
    public class CollisionsEventsRelay : MonoBehaviour
    {
        public Action<Collider> OnTriggerEnterEvent = null;
        public Action<Collider> OnTriggerExitEvent = null;
        public Action<Collision> OnCollisionEnterEvent = null;
        public Action<Collision> OnCollisionExitEvent = null;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExitEvent?.Invoke(collision);
        }
    }
}
using UnityEngine;

namespace Mobs.Tools
{
    public class StickToObject : MonoBehaviour
    {
        [SerializeField]
        private Transform m_target = null;
        [SerializeField]
        private Vector3 m_offset = default;

        protected virtual void LateUpdate()
        {
            transform.position = m_target.position + m_target.TransformDirection(m_offset);
        }
    }
}
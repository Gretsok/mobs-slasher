using UnityEngine;

namespace Mobs.Gameplay.Combat
{
    public class DamageHandlingControllerCollisionRelay : MonoBehaviour
    {
        [SerializeField]
        private DamageHandlingController m_owner = null;
        public DamageHandlingController Owner => m_owner;

    }
}
using UnityEngine;

namespace Mobs.Gameplay.Combat
{
    public class CombatControllerCollisionRelay : MonoBehaviour
    {
        [SerializeField]
        private CombatController m_owner = null;
        public CombatController Owner => m_owner;

    }
}
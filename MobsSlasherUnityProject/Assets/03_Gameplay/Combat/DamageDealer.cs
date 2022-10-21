using UnityEngine;

namespace Mobs.Gameplay.Combat
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField]
        private CombatController m_owner = null;
        public CombatController Owner => m_owner;

        public void SetOwner(CombatController a_owner)
        {
            m_owner = a_owner;
        }
    }
}
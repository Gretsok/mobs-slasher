using Mobs.Gameplay.Combat;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public class PlayerDamageHandlingController : DamageHandlingController
    {
        private PlayerCharacterInputs m_inputs;

        public void SetInputs(ref PlayerCharacterInputs inputs)
        {
            m_inputs = inputs;
        }

    }
}
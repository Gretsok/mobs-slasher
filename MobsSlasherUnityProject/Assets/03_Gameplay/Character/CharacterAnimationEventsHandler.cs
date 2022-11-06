using System;
using UnityEngine;

namespace Mobs.Gameplay.Character
{
    public class CharacterAnimationEventsHandler : MonoBehaviour
    {
        [SerializeField]
        private CharacterAnimationsController m_animationsController = null;

        public Action<CharacterAnimationEventsHandler, string> OnAnimationEventThrown = null;

        public void HandleAnimationEvent(string a_eventKey)
        {
            OnAnimationEventThrown?.Invoke(this, a_eventKey);
        }
    }
}
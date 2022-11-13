using UnityEngine;

namespace Mobs.Gameplay.Interactions
{
    public abstract class AInteractionEffect : MonoBehaviour
    {
        public abstract void DoEffect(Interactor a_interactor);
    }

}

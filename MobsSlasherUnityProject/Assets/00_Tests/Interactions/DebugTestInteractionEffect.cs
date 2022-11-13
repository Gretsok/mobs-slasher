using Mobs.Gameplay.Interactions;
using UnityEngine;

public class DebugTestInteractionEffect : AInteractionEffect
{
    [SerializeField]
    private string m_trucToDebug = "truc";

    public override void DoEffect(Interactor a_interactor)
    {
        Debug.Log(m_trucToDebug);
    }
}

using Mobs.Gameplay.Interactions;
using UnityEngine;

public class TestInteractor : Interactor
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    protected override Ray GetSight()
    {
        return new Ray(transform.position + Vector3.up * 0.1f, transform.forward);
    }
}

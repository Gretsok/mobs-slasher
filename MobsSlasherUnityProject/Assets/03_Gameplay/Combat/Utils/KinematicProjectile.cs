using UnityEngine;

namespace Mobs.Gameplay.Combat.Utils
{
    public class KinematicProjectile : MonoBehaviour
    {
        public float speed = 20f;

        private void FixedUpdate()
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
    }
}
using UnityEngine;

namespace Mobs.Tools
{
    public class LockCursorToScreen : MonoBehaviour
    {
        private void Start()
        {
            ConfineCursor();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                if(Cursor.lockState != CursorLockMode.Locked)
                {
                    ConfineCursor();
                }
                else if(Cursor.lockState != CursorLockMode.None)
                {
                    FreeCursor();
                }
            }
        }

        private static void FreeCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private static void ConfineCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
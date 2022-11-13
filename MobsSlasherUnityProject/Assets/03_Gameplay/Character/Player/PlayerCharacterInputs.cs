using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    [System.Serializable]
    public struct PlayerCharacterInputs
    {
        public Vector2 MoveInput;
        public Vector2 CameraInput;
        public bool JumpDown;
        public bool Sprinting;
        public bool StartFirstAttack;
        public bool StopFirstAttack;
        public bool StartSecondAttack;
        public bool StopSecondAttack;
        public bool StartThirdAttack;
        public bool StopThirdAttack;
        public bool Interact;
    }
}
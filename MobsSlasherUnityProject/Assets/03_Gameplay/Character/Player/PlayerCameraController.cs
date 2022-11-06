using Mobs.Tools;
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public class PlayerCameraController : StickToObject
    {
        private float m_cameraRadAngleOnX = 0f;
        [SerializeField]
        private float m_cameraSpeed = 50f;
        [SerializeField]
        private Vector2 m_cameraRadAngleOnXRange = new Vector2(-55f, 75f);
        [SerializeField]
        private Transform m_horizontalRotator = null;
        [SerializeField]
        private Transform m_verticalRotator = null;

        private PlayerCharacterInputs m_inputs;

        public void SetInputs(ref PlayerCharacterInputs inputs)
        {
            m_inputs = inputs;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            m_cameraRadAngleOnX += m_inputs.CameraInput.y * Time.deltaTime * m_cameraSpeed * Mathf.Deg2Rad;
            m_cameraRadAngleOnX = Mathf.Clamp(m_cameraRadAngleOnX, -m_cameraRadAngleOnXRange.y * Mathf.Deg2Rad, -m_cameraRadAngleOnXRange.x * Mathf.Deg2Rad);
            m_verticalRotator.forward = m_horizontalRotator.TransformDirection(new Vector3(0f, Mathf.Sin(m_cameraRadAngleOnX), Mathf.Cos(m_cameraRadAngleOnX)));

            m_horizontalRotator.eulerAngles += transform.up * m_cameraSpeed * m_inputs.CameraInput.x * Time.deltaTime;
        }
    }
}
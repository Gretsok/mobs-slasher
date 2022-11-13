
using UnityEngine;

namespace Mobs.Gameplay.Character.Player
{
    public class PlayerSkillsController : CharacterSkillsController
    {
        private PlayerCharacterInputs m_inputs;

        private void Start()
        {
            EquipSkill(0, 0);
            EquipSkill(1, 1);
        }

        public void SetInputs(ref PlayerCharacterInputs inputs)
        {
            m_inputs = inputs;
        }

        private void Update()
        {
            if(m_inputs.StartFirstAttack)
            {
                StartFirstSkill();
            }
            if(m_inputs.StopFirstAttack)
            {
                StopFirstSkill();
            }
            if(m_inputs.StartSecondAttack)
            {
                StartSecondSkill();
            }
            if(m_inputs.StopSecondAttack)
            {
                StopSecondSkill();
            }
            if(m_inputs.StartThirdAttack)
            {
                StartThirdSkill();
            }
            if(m_inputs.StopThirdAttack)
            {
                StopThirdSkill();
            }
        }

        public override Ray GetSight()
        {
            return new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        }

    }
}
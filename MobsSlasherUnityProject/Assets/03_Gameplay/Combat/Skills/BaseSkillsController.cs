using System.Collections.Generic;
using UnityEngine;

namespace Mobs.Gameplay.Combat.Skills
{
    public class BaseSkillsController : MonoBehaviour
    {
        [SerializeField]
        protected DamageHandlingController m_damageHandlingController = null;
        public DamageHandlingController DamageHandlingController => m_damageHandlingController;

        [SerializeField]
        private List<SkillData> m_skillsData = null;

        private BaseSkill m_firstEquippedSkill = null;
        private BaseSkill m_secondEquippedSkill = null;
        private BaseSkill m_thirdEquippedSkill = null;

        public void EquipSkill(int a_skillIndex, int a_slotIndex)
        {
            SkillData skillData = m_skillsData[a_skillIndex % m_skillsData.Count];

            if(skillData == null
                || !skillData.BaseSkillPrefab.IsCompatible(this))
            {
                Debug.LogError("Skill and Skill Controller are not compatible");
                return;
            }

            switch(a_slotIndex)
            {
                case 0:
                    if(m_firstEquippedSkill)
                        Destroy(m_firstEquippedSkill.gameObject);
                    m_firstEquippedSkill = Instantiate(skillData.BaseSkillPrefab, transform);
                    m_firstEquippedSkill.SetUp(this);
                    break;
                case 1:
                    if(m_secondEquippedSkill)
                        Destroy(m_secondEquippedSkill.gameObject);
                    m_secondEquippedSkill = Instantiate(skillData.BaseSkillPrefab, transform);
                    m_secondEquippedSkill.SetUp(this);
                    break;
                case 2:
                    if(m_thirdEquippedSkill)
                        Destroy(m_thirdEquippedSkill.gameObject);
                    m_thirdEquippedSkill = Instantiate(skillData.BaseSkillPrefab, transform);
                    m_thirdEquippedSkill.SetUp(this);
                    break;
            } 
        }

        public void StartFirstSkill()
        {
            if(!m_firstEquippedSkill)
            {
                return;
            }
            if(m_firstEquippedSkill.IsExecuting)
            {
                return;
            }

            m_firstEquippedSkill.StartSkill();
        }

        public void StopFirstSkill()
        {
            if (!m_firstEquippedSkill)
            {
                return;
            }
            if (!m_firstEquippedSkill.IsExecuting)
            {
                return;
            }

            m_firstEquippedSkill.StopSkill();
        }

        public void StartSecondSkill()
        {
            if (!m_secondEquippedSkill)
            {
                return;
            }
            if (m_secondEquippedSkill.IsExecuting)
            {
                return;
            }

            m_secondEquippedSkill.StartSkill();
        }

        public void StopSecondSkill()
        {
            if (!m_secondEquippedSkill)
            {
                return;
            }
            if (!m_secondEquippedSkill.IsExecuting)
            {
                return;
            }

            m_secondEquippedSkill.StopSkill();
        }

        public void StartThirdSkill()
        {
            if (!m_thirdEquippedSkill)
            {
                return;
            }
            if (m_thirdEquippedSkill.IsExecuting)
            {
                return;
            }

            m_thirdEquippedSkill.StartSkill();
        }

        public void StopThirdSkill()
        {
            if (!m_thirdEquippedSkill)
            {
                return;
            }
            if (!m_thirdEquippedSkill.IsExecuting)
            {
                return;
            }

            m_thirdEquippedSkill.StopSkill();
        }

    }
}
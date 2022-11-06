using UnityEngine;

namespace Mobs.Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Mobs/Gameplay/Combat/Skills/SkillData")]
    public class SkillData : ScriptableObject
    {
        [SerializeField]
        private string m_skillNameKey = "DEFAULT_SKILL_NAME";
        [SerializeField]
        private string m_skillDescriptionKey = "DEFAULT_SKILL_DESCRIPTION";
        [SerializeField]
        private BaseSkill m_baseSkillPrefab = null;

        public string SkillNameKey => m_skillNameKey;
        public string SkillDescriptionKey => m_skillDescriptionKey;
        public BaseSkill BaseSkillPrefab => m_baseSkillPrefab;
    }
}
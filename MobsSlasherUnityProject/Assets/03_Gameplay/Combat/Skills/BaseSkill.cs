using UnityEngine;

namespace Mobs.Gameplay.Combat.Skills
{
    public class BaseSkill: MonoBehaviour
    {
        private BaseSkillsController m_controller = null;
        public BaseSkillsController Controller => m_controller;

        public virtual void SetUp(BaseSkillsController a_controller)
        {
            m_controller = a_controller;
        }

        #region Life Cycle
        protected bool m_isExecuting = false;
        public bool IsExecuting => m_isExecuting;
        public void StartSkill()
        {
            OnSkillStarted();
        }

        public void StopSkill()
        {
            OnSkillStopped(); 
        }

        protected virtual void OnSkillStarted()
        {
            m_isExecuting = true;
        }

        protected virtual void OnSkillStopped()
        {
            m_isExecuting = false;
        }
        #endregion

        #region Utils
        public virtual bool IsCompatible(BaseSkillsController a_controller)
        {
            return true;
        }
        #endregion
    }

    public class BaseSkill<T> : BaseSkill where T : BaseSkillsController
    {
        public T CastedController => Controller as T;

        public override bool IsCompatible(BaseSkillsController a_controller)
        {
            return a_controller is T;
        }
    }
}
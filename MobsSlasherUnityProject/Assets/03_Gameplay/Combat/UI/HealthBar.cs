using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mobs.Gameplay.Combat.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private CombatController m_combatController = null;

        [SerializeField]
        private Image m_midSlider = null;
        [SerializeField]
        private Image m_frontSlider = null;

        [SerializeField]
        private float m_midSliderSmoothness = 3f;


        private void Start()
        {
            m_combatController.OnDamageTaken += HandleDamageTaken;
            if(m_combatController.LifePoints != 0)
            {
                SetInstantLife((float)m_combatController.LifePoints / (float)m_combatController.MaxLifePoints);
            }
            else
            {
                SetInstantLife(1f);
            }
        }

        private void HandleDamageTaken(CombatController a_combatController, int a_newLife, int a_olfLife)
        {
            SetLife((float)m_combatController.LifePoints / (float)m_combatController.MaxLifePoints);
        }

        private void OnDestroy()
        {
            m_combatController.OnDamageTaken -= HandleDamageTaken;
        }

        public void SetInstantLife(float a_lifeRatio)
        {
            m_midSlider.fillAmount = a_lifeRatio;
            m_frontSlider.fillAmount = a_lifeRatio;
        }

        public void SetLife(float a_lifeRatio)
        {
            m_frontSlider.fillAmount = a_lifeRatio;
        }

        private void Update()
        {
            m_midSlider.fillAmount = Mathf.Lerp(m_midSlider.fillAmount, m_frontSlider.fillAmount, Time.deltaTime * m_midSliderSmoothness);
        }
    }
}
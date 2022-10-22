using UnityEditor;
using UnityEngine;

namespace Mobs.Gameplay.Combat.Editor
{
    [CustomEditor(typeof(CombatController), true)]
    public class CombatControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(30f);

            GUILayout.Label("Combat Controller Infos :", EditorStyles.boldLabel);
            GUILayout.Label($"Life points : {(target as CombatController).LifePoints}/{(target as CombatController).MaxLifePoints}");
            
        }
    }
}
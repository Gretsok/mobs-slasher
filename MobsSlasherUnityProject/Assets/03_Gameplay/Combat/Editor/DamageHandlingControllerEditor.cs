using UnityEditor;
using UnityEngine;

namespace Mobs.Gameplay.Combat.Editor
{
    [CustomEditor(typeof(DamageHandlingController), true)]
    public class DamageHandlingControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(30f);

            GUILayout.Label("Combat Controller Infos :", EditorStyles.boldLabel);
            GUILayout.Label($"Life points : {(target as DamageHandlingController).LifePoints}/{(target as DamageHandlingController).MaxLifePoints}");
            
        }
    }
}
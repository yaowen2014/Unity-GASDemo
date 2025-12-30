using UnityEditor;
using UnityEngine;
using WYGAS;

namespace Editor
{
    [CustomPropertyDrawer(typeof(GameplayEffectMagnitude))]
    public class GameplayEffectMagnitudeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var typeProp = property.FindPropertyRelative("type");
            var constantProp = property.FindPropertyRelative("constantValue");
            var keyProp = property.FindPropertyRelative("setByCallerKey");
            var attributeProp = property.FindPropertyRelative("attributeName");
            var attributeCoefficientProp = property.FindPropertyRelative("attributeCoefficient");

            var line = position;
            line.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(line, typeProp);
            line.y += line.height + 2;
            
            switch ((MagnitudeType)typeProp.enumValueIndex)
            {
                case MagnitudeType.Constant:
                    EditorGUI.PropertyField(line, constantProp, new GUIContent("Value"));
                    break;

                case MagnitudeType.SetByCaller:
                    EditorGUI.PropertyField(line, keyProp, new GUIContent("Key"));
                    break;

                case MagnitudeType.AttributeBased:
                    EditorGUI.PropertyField(line, attributeProp, new GUIContent("Attribute"));
                    line.y += line.height + 2;
                    EditorGUI.PropertyField(line, attributeCoefficientProp, new GUIContent("AttributeCoefficient"));
                    break;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var typeProp = property.FindPropertyRelative("type");

            int lines = 1; // type 本身

            switch ((MagnitudeType)typeProp.enumValueIndex)
            {
                case MagnitudeType.Constant:
                case MagnitudeType.SetByCaller:
                    lines += 1;
                    break;

                case MagnitudeType.AttributeBased:
                    lines += 2;
                    break;
            }

            return lines * (EditorGUIUtility.singleLineHeight + 2);
        }
    }
}
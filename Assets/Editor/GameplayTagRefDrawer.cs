using Editor;
using UnityEditor;
using UnityEngine;
using WYGAS.SO;

[CustomPropertyDrawer(typeof(GameplayTagRef))]
public class GameplayTagRefDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var tagProp = property.FindPropertyRelative("tagPath");

        Rect fieldRect = EditorGUI.PrefixLabel(position, label);
        string display = string.IsNullOrEmpty(tagProp.stringValue)
            ? "<None>"
            : tagProp.stringValue;

        if (GUI.Button(fieldRect, display, EditorStyles.popup))
        {
            GameplayTagPickerWindow.Open(
                FindTagTable(),
                selected =>
                {
                    tagProp.stringValue = selected;
                    property.serializedObject.ApplyModifiedProperties();
                }
            );
        }

        EditorGUI.EndProperty();
    }

    private GameplayTagTable FindTagTable()
    {
        // 推荐：全工程唯一 TagTable
        var guids = AssetDatabase.FindAssets("t:GameplayTagTable");
        if (guids.Length == 0)
        {
            Debug.LogError("No GameplayTagTable found.");
            return null;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<GameplayTagTable>(path);
    }
}
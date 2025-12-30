using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using WYGAS.SO;

namespace Editor
{
    public class GameplayTagPickerWindow : EditorWindow
    {
        private static GameplayTagTable _tagTable;
        private static Action<string> _onTagSelected;

        private string _search;
        private Vector2 _scroll;

        public static void Open(GameplayTagTable table, Action<string> onSelected)
        {
            _tagTable = table;
            _onTagSelected = onSelected;

            var window = CreateInstance<GameplayTagPickerWindow>();
            window.titleContent = new GUIContent("Select Gameplay Tag");
            window.ShowUtility();
        }

        private void OnGUI()
        {
            if (_tagTable == null)
            {
                EditorGUILayout.HelpBox("GameplayTagTable not found.", MessageType.Error);
                return;
            }

            DrawSearchBar();
            DrawTagList();
        }

        private void DrawSearchBar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            _search = EditorGUILayout.TextField(_search);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTagList()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            IEnumerable<string> tags = _tagTable.tags;

            if (!string.IsNullOrEmpty(_search))
            {
                tags = tags.Where(t => t.IndexOf(_search, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            foreach (var tag in tags.OrderBy(t => t))
            {
                if (GUILayout.Button(tag, EditorStyles.label))
                {
                    _onTagSelected?.Invoke(tag);
                    Close();
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }
}
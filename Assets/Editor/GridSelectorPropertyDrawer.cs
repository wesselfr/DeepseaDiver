using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GridSelector))]
public class GridSelectorPropertyDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty grid = property.FindPropertyRelative("grid");
        
        base.OnGUI(position, grid, label);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GridSelectorAttribute))]
public class GridSelectorPropertyDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // First get the attribute since it contains the range for the slider
        GridSelectorAttribute grid = attribute as GridSelectorAttribute;

        EditorGUI.BeginProperty(position, label, property);

        bool[,] boolGrid = new bool[grid.widht, grid.height];
        for(int x = 0; x < grid.widht; x++)
        {
            for(int y = 0; y < grid.height; y++)
            {
                boolGrid[x, y] = EditorGUILayout.Toggle(boolGrid[x,y]);
            }
        }

        EditorGUI.PropertyField(position, property);

        EditorGUI.EndProperty();
        
    }
}

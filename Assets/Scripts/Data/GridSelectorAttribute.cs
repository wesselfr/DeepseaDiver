using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic grid selector. For use in obstacles to set size and possible positions.

[AttributeUsage(AttributeTargets.Field)]
public class GridSelectorAttribute : PropertyAttribute
{
    public int widht = 3;
    public int height = 3;

    public GridSelectorAttribute(int width, int height)
    {
        this.widht = width;
        this.height = height;
    }
}

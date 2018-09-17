using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic grid selector. For use in obstacles to set size and possible positions.

[System.Serializable]
public struct GridSelector
{
    bool[,] m_Grid;
    public GridSelector(int width, int height)
    {
        m_Grid = new bool[width, height];
    }
    public GridSelector(bool[,] grid)
    {
        m_Grid = grid;
    }

    public bool[,] grid { get { return m_Grid; } set { m_Grid = value; } }
}

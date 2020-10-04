using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSquare 
{
    public int col;
    public int row;
    public Square.SQUARE_TYPE squareType;

    public SpecialSquare(int c, int r, Square.SQUARE_TYPE st)
    {
        col = c;
        row = r;
        squareType = st;
    }
}

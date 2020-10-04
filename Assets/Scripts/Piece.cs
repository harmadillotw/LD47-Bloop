using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    // Start is called before the first frame update
    public Square Square{ get; set; }

    public enum MOVEMENT_TYPE {  ALL, ADJACENT, VANDT, VERTICAL, HORIZONTAL, DIAGONAL}

    public MOVEMENT_TYPE moveType;
    public int moveDist;
    public int startCol;
    public int startRow;

    public bool escaped;
    public bool completedMove;

    public Piece()
    { 
        moveType = MOVEMENT_TYPE.ADJACENT;
        completedMove = false;
        escaped = false;
    }
    public Piece(MOVEMENT_TYPE mtype)
    {
        moveType = mtype;
        completedMove = false;
        escaped = false;
    }

    public void SetSquare(Square newSquare)
    {
        if (Square != null)
        {
            Square.RemovePiece(this);
        }
        Square oldSquare = Square;
        Square = newSquare;

        newSquare.AddPiece(this);
    }
    public void DoMove(Square newSquare, bool playerMove)
    {
        SetSquare(newSquare);
        completedMove = playerMove;
    }
    public void SetStart(int col, int row)
    {
        startCol = col;
        startRow = row;
    }
}

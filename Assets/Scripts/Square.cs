using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square 
{
    public enum SQUARE_TYPE { NORMAL, RESET_TURN_COUNT, IMPASSABLE }

    public readonly int Q;  // Column
    public readonly int R;  // Row
    public string Name;
    public SQUARE_TYPE squareType = SQUARE_TYPE.NORMAL;

    private Piece piece;

    public Square( int q, int r, string name)
    {
        Q = q;
        R = r;
        Name = name;
    }
    public string GetName()
    {
        return Name;
    }

    public Vector3 Position()
    {
        return new Vector3(

            0.04f * this.Q ,
            0,
            0.04f * this.R
            );
    }

    public Vector3 EndPosition()
    {
        return new Vector3(

            0.04f * (this.Q -2),
            0,
            0.04f * this.R
            );
    }

    public void RemovePiece( Piece piece)
    {
        this.piece = null;
    }
    

    public void AddPiece( Piece piece)
    {
        this.piece = piece;
    }

    public Piece GetPiece()
    {
        return piece;
    }

    public bool IsEmpty()
    {
        if (piece == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

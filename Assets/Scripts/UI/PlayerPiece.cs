using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece 
{
    public int col;
    public int row;
    public Piece.MOVEMENT_TYPE movementTye;

    public PlayerPiece(int c, int r, Piece.MOVEMENT_TYPE mt)
    {
        col = c;
        row = r;
        movementTye = mt;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public int columns;
    public int rows;
    public int maxTurns;
    public List<PlayerPiece> playerPieces;
    public List<SpecialSquare> specialSquares;
    

    public Level()
    {
        columns = 8;
        rows = 8;
        maxTurns = 6;
        playerPieces = new List<PlayerPiece>();
        specialSquares = new List<SpecialSquare>();
    }

    public void AddPlayerPiece(int col, int row, Piece.MOVEMENT_TYPE moveType )
    {
        PlayerPiece pp = new PlayerPiece(col, row, moveType);
        playerPieces.Add(pp);
    }

    public void AddSpecialSquare(int col, int row, Square.SQUARE_TYPE squareType)
    {
        SpecialSquare sq = new SpecialSquare(col, row, squareType);
        specialSquares.Add(sq);
    }
}

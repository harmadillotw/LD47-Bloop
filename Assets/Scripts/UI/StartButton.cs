using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneData.currentLevel.Add(0);

        //first level
        Level newLevel = new Level();


        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(4, 4, Piece.MOVEMENT_TYPE.ADJACENT);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 6;
        newLevel.AddPlayerPiece(2, 4, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(6, 4, Piece.MOVEMENT_TYPE.ADJACENT);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(4, 0, Piece.MOVEMENT_TYPE.DIAGONAL);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(4, 0, Piece.MOVEMENT_TYPE.VANDT);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(4, 0, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddSpecialSquare(4, 4, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(4, 3, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(3, 2, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddSpecialSquare(4, 2, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(4, 3, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(2, 2, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(6, 2, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddSpecialSquare(4, 2, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(3, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 1, Square.SQUARE_TYPE.IMPASSABLE);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(0, 2, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(1, 1, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(5, 1, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddSpecialSquare(3, 0, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(6, 4, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(7, 4, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(7, 3, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(0, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(6, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 5, Square.SQUARE_TYPE.IMPASSABLE);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(6, 4, Piece.MOVEMENT_TYPE.DIAGONAL);
        newLevel.AddSpecialSquare(0, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(4, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(6, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 5, Square.SQUARE_TYPE.IMPASSABLE);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(6, 4, Piece.MOVEMENT_TYPE.DIAGONAL);
        newLevel.AddPlayerPiece(0, 0, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddSpecialSquare(3, 1, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(1, 1, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(2, 4, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(7, 3, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(0, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(6, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 5, Square.SQUARE_TYPE.IMPASSABLE);
        SceneData.levels.Add(newLevel);

        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(6, 4, Piece.MOVEMENT_TYPE.DIAGONAL);
        newLevel.AddPlayerPiece(6, 2, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(3, 3, Piece.MOVEMENT_TYPE.ADJACENT);
        newLevel.AddPlayerPiece(0, 0, Piece.MOVEMENT_TYPE.VANDT);
        newLevel.AddSpecialSquare(3, 1, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(1, 1, Square.SQUARE_TYPE.RESET_TURN_COUNT);
        newLevel.AddSpecialSquare(0, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(4, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(6, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(0, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 6, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(4, 6, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 1, Square.SQUARE_TYPE.IMPASSABLE);
        SceneData.levels.Add(newLevel);








        newLevel = new Level();
        newLevel.maxTurns = 4;
        newLevel.AddPlayerPiece(6, 4, Piece.MOVEMENT_TYPE.ALL);


        newLevel.AddSpecialSquare(0, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(4, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(6, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(5, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(6, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 5, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 4, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(7, 3, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(1, 1, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(2, 2, Square.SQUARE_TYPE.IMPASSABLE);
        newLevel.AddSpecialSquare(3, 3, Square.SQUARE_TYPE.IMPASSABLE);
        SceneData.levels.Add(newLevel);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SceneMain");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindObjectOfType<Board>();
        maxTurns = board.MaxTurns;
        int turn = board.turn;
        int turnsRem = maxTurns - turn + 1;
        if (TurnPanel != null)
        {
            TurnPanel.text = " Turns Remaining: " + turnsRem;
            LevelPanel.text = " Level: " + (board.currentlevel + 1);
        }
    }

    
    public int maxTurns;

    public Text TurnPanel;
    public Text LevelPanel;
    Board board;

    // Update is called once per frame
    void Update()
    {
        bool endTurn = board.endTurn;
        //foreach (Piece p in board.playerPieces)
        //{
        //    if (!p.completedMove)
        //    {
        //        endTurn = false;
        //    }
        //}
        bool levelComplete = false;
        foreach (Piece p in board.playerPieces)
        {
            if (p.escaped)
            {
                levelComplete = true;
            }
        }
        if (levelComplete)
        {
            NextLevel();
        }
        else
        {
            if (endTurn)
            {
                DoEndTurn();

            }
        }
        
    }

    public void EndTurn()
    {
        DoEndTurn();
    }

    private void DoEndTurn()
    {
        int turn = board.turn;

        if (board.turnReset)
        {
            board.turn = 1;
            turn = 1;
            board.turnReset = false;
        }
        if (turn > maxTurns)
        {
            board.turn = 1;
            turn = 1 ;
            board.Reset();
        }
        int turnsRem = maxTurns - turn + 1;
        if (TurnPanel != null)
        {
            TurnPanel.text = "Turns Remaining: " + turnsRem;
        }
        board.endTurn = false;
    }

    private void NextLevel()
    {

        SceneManager.LoadScene("Transition");
    }
    public void UpdateTurnCount(int t)
    {
        //this.turn = t;
    }
   
}

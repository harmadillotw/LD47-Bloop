using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set the current mode for interaction action
        Update_CurrentFunc = Update_DetectModeStart;
        board = GameObject.FindObjectOfType<Board>();
    }

    Square squareUnderMouse;
    // The current board in play.
    Board board;

    // Current intractivity mode
    delegate void UpdateFunc();
    UpdateFunc Update_CurrentFunc;

    public LayerMask LayerIDForSquareTiles;
    public LayerMask LayerIDForMeeps;

    Piece selectedPiece;
    Square selectedSquare;
    bool endzoneSelected;

    // Update is called once per frame
    void Update()
    {
        GameObject hitGo = MouseToSquare();
        if (hitGo != null)
        {
            
            if (board.HasSquareFromGameObject(hitGo))
            {
                squareUnderMouse = board.GetSquareFromGameObject(hitGo);
                endzoneSelected = false;
            }
            if (board.HasPieceFromGameObject(hitGo))
            {
                Piece pieceUnderMouse = board.GetPieceFromGameObject(hitGo);
                squareUnderMouse = pieceUnderMouse.Square;
                endzoneSelected = false;
            }
            else if (hitGo.Equals(board.endzoneGo))
            {
                endzoneSelected = true;
            }
            else
            {
                endzoneSelected = false;
            }
        }
        

        //squareUnderMouse = board.GetSquareFromGameObject(hitGo);
        // reset detection mode and unselect any piece.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedPiece = null;
            selectedSquare = null;
            CancelUpdateFunc();
        }

        Update_CurrentFunc();
    }

    GameObject MouseToSquare()
    {
        //Debug.Log("MouseToSquare");
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Debug.Log("LayerIDForSquareTiles " + LayerIDForSquareTiles);
        int layerMask = 0;
        if (selectedPiece == null)
        {
            layerMask = LayerIDForSquareTiles.value | LayerIDForMeeps.value;
        }
        else
        {
            layerMask = LayerIDForSquareTiles.value;
        }


        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, layerMask))
        {
            //Something got hit
            //Debug.Log(hitInfo.collider.name);
            GameObject hitGO = hitInfo.collider.gameObject.transform.parent.gameObject;
            return hitGO;
            //return board.GetSquareFromGameObject(squareGO);

            // The collider is a child of the "Correct" game object that we want.
            //GameObject hexGO = hitInfo.rigidbody.gameObject;
            //return hexMap.GetHexFromGameObject(hexGO);

        }
        //Debug.Log("Hit nothing");
        return null;

    }

    void CancelUpdateFunc()
    {
        selectedPiece = null;
        selectedSquare = null;
        Update_CurrentFunc = Update_DetectModeStart;
        board.UpdateHexVisuals();

    }

    void Update_DetectModeStart()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if ((selectedPiece != null) &&(Input.GetMouseButtonUp(0)))
        {
            //Debug.Log(" Left click mouse down");
            Update_CurrentFunc = Update_PieceMovement;
        }
        else if ((selectedPiece != null) && (endzoneSelected) && (Input.GetMouseButtonUp(0)))
        {
            Update_CurrentFunc = Update_PieceMovement;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(" right client mouse up");

            Piece piece = squareUnderMouse.GetPiece();
            
            if (piece != null)
            {
               // Debug.Log(" Piece selected");
                selectedPiece = piece;
                selectedSquare = squareUnderMouse;
                board.HighlightDestinations(piece, squareUnderMouse);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log(" Right click mouse down");
        }
        else if (Input.GetMouseButtonUp(1))
        {
           // Debug.Log(" Left client mouse up");
        }
    }

    void Update_PieceMovement()
    {
        if (Input.GetMouseButtonUp(0) || selectedPiece != null)
        {
            //Debug.Log("move the piece");
            // Can I move there??
            if (board.ValidDestination(selectedPiece, selectedSquare, squareUnderMouse))
            {
                StartCoroutine(board.DoMove(selectedPiece, squareUnderMouse, true));
                CancelUpdateFunc();
            }
            else
            {
                Update_CurrentFunc = Update_DetectModeStart;
            }
            return;
        }
    }
}

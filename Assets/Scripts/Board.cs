using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject SquarePrefab;
    public GameObject MeepPrefab;
    public GameObject BorderPrefab;
    public GameObject BorderSmallPrefab;

    public GameObject Meep3Prefab;

    public Material BoardWhite;
    public Material BoardBrown;
    public Material BoardGreen;
    public Material BoardDarkGreen;
    public Material BoardRed;
    public Material BoardDarkRed;
    public Material BoardBlue;
    public Material BoardDarkBlue;
    public Material BoardBlack;
    public Material BoardTan;
    public Material BoardEndGreen;

    public AudioClip specialSquareClip;
    public AudioClip endZoneClip;
    public AudioClip resetClip;

    private int NumColumns = 8;
    public int NumRows = 9;
    public int MaxTurns = 6;
    private Square[,] squares;
    private Square[,] endZone;
    private Dictionary<Square, GameObject> squareToGameObjectMap;
    private Dictionary<GameObject, Square> gameObjectToSquareMap;

    public List<Piece> playerPieces;
    private Dictionary<Piece, GameObject> pieceToGameObjectMap;
    private Dictionary<GameObject, Piece> gameObjectToPieceMap;

    public GameObject endzoneGo;

    public Level[] levels = new Level[5];

    public int currentlevel;
    private TurnController  turnController;

    public bool turnReset = false;
    public int turn;

    public bool endTurn = false;



    // Start is called before the first frame update
    void Start()
    {
        //Level newLevel = new Level();
        //newLevel.AddPlayerPiece(4, 2, Piece.MOVEMENT_TYPE.ADJACENT);
        //levels[0] = newLevel;
        //MaxTurns = SceneData.levels[0].maxTurns;
        playerPieces = new List<Piece>();
        GenerateBoard();
        turn = 1;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public Square GetSquareFromGameObject(GameObject go)
    {
        return gameObjectToSquareMap[go];
    }

    public bool HasSquareFromGameObject(GameObject go)
    {
        return gameObjectToSquareMap.ContainsKey(go);
    }

    public bool HasPieceFromGameObject(GameObject go)
    {
        return gameObjectToPieceMap.ContainsKey(go);
    }
    public Piece GetPieceFromGameObject(GameObject go)
    {
        return gameObjectToPieceMap[go];
    }
    public void GenerateBoard()
    {
        GenerateBoard(NumColumns, NumRows);
    }
    public void GenerateBoard(int cols, int rows)
        {
            squares = new Square[cols, rows];
        endZone = new Square[cols + 2, 1];

        squareToGameObjectMap = new Dictionary<Square, GameObject>();
        gameObjectToSquareMap = new Dictionary<GameObject, Square>();

        pieceToGameObjectMap = new Dictionary<Piece, GameObject>();
        gameObjectToPieceMap = new Dictionary<GameObject, Piece>();
        
        // Spawn the game area
        for (int column = 0; column < cols; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                string name = "" + column + "," + row;
                Square s = new Square(column, row, name);

                squares[column, row] = s;

                GameObject squareGO = (GameObject)Instantiate(
                    SquarePrefab,
                    s.Position(),
                    Quaternion.identity,
                    this.transform);

                squareToGameObjectMap[s] = squareGO;   //Alternative format for add
                gameObjectToSquareMap[squareGO] = s;
            }
        }


        
        //UpdateHexVisuals();

        

        // right border
        Vector3 bPos = new Vector3(
            0.36f,
            0f,
            -0.04f
            );
        GameObject RightBorderGO = (GameObject)Instantiate(
                    BorderPrefab,
                    bPos,
                    Quaternion.identity,
                    this.transform);
        MeshRenderer mr = RightBorderGO.GetComponentInChildren<MeshRenderer>();
        mr.material = BoardTan;
        //UpdateHexVisuals();
        //left border
        bPos = new Vector3(
            -0.04f,
            0f,
            -0.04f
            );
        GameObject leftBorderGO = (GameObject)Instantiate(
                    BorderPrefab,
                    bPos,
                    Quaternion.identity,
                    this.transform);
        mr = leftBorderGO.GetComponentInChildren<MeshRenderer>();
        mr.material = BoardTan;

        //lower border
        bPos = new Vector3(
            0.28f,
            0f,
            -0.04f
            );
        
        
        GameObject lowerBorderGO = (GameObject)Instantiate(
                    BorderSmallPrefab,
                    bPos,
                    Quaternion.identity,
                    this.transform);
        mr = lowerBorderGO.GetComponentInChildren<MeshRenderer>();
        mr.material = BoardTan;
        UpdateHexVisuals();
        if (SceneData.currentLevel.Count > 0)
        {
            currentlevel = SceneData.currentLevel[0];
            LoadLevel(SceneData.levels[SceneData.currentLevel[0]]);

        }
    }

    public void LoadLevel(Level level)
    {
        MaxTurns = level.maxTurns;
        if (level.specialSquares.Count > 0)
        {
            foreach (SpecialSquare sSquare in level.specialSquares)
            {
                squares[sSquare.col, sSquare.row].squareType = sSquare.squareType;
                GameObject squareGO = squareToGameObjectMap[squares[sSquare.col, sSquare.row]];
                MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                UpdateSpecialSquareVisuals(mr, sSquare.squareType);
            }
        }
        if (level.playerPieces.Count > 0)
        {
            foreach (PlayerPiece piece in level.playerPieces)
            {


                Piece newPiece = new Piece(piece.movementTye);

                int pCol = piece.col;
                int pRow = piece.row;
                newPiece.startCol = piece.col;
                newPiece.startRow = piece.row;
                newPiece.SetSquare(squares[pCol, pRow]);
                playerPieces.Add(newPiece);

                Vector3 pos = new Vector3(
                    0.04f * pCol,
                    0.02f,
                    0.04f * pRow
                    );
                GameObject meepGO = (GameObject)Instantiate(
                            Meep3Prefab,
                            pos,
                            Quaternion.identity,
                            this.transform);
                MeshRenderer mr = meepGO.GetComponentInChildren<MeshRenderer>();
                if (newPiece.moveType == Piece.MOVEMENT_TYPE.ADJACENT)
                {
                    mr.material = BoardDarkBlue;
                }
                else if (newPiece.moveType == Piece.MOVEMENT_TYPE.DIAGONAL)
                {
                    mr.material = BoardDarkGreen;
                }
                if (newPiece.moveType == Piece.MOVEMENT_TYPE.ADJACENT)
                {
                    mr.material = BoardDarkBlue;
                }
                if (newPiece.moveType == Piece.MOVEMENT_TYPE.VANDT)
                {
                    mr.material = BoardDarkRed;
                }             
                if (newPiece.moveType == Piece.MOVEMENT_TYPE.ALL)
                {
                    mr.material = BoardBlack;
                }

                pieceToGameObjectMap[newPiece] = meepGO;
                gameObjectToPieceMap[meepGO] = newPiece;
            }
        }
        else
        {

            Piece newPiece = new Piece(Piece.MOVEMENT_TYPE.ADJACENT);

            int pCol = 0;
            int pRow = 0;
            newPiece.startCol = 0;
            newPiece.startRow = 0;
            newPiece.SetSquare(squares[pCol, pRow]);
            playerPieces.Add(newPiece);

            Vector3 pos = new Vector3(
                0.04f * pCol,
                0.02f,
                0.04f * pRow
                );
            GameObject meepGO = (GameObject)Instantiate(
                        MeepPrefab,
                        pos,
                        Quaternion.identity,
                        this.transform);
            MeshRenderer mr = meepGO.GetComponentInChildren<MeshRenderer>();
            mr.material = BoardDarkBlue;
            pieceToGameObjectMap[newPiece] = meepGO;
            gameObjectToPieceMap[meepGO] = newPiece;
        }
    }

    public void UpdateHexVisuals()
    {
        int count = 0;
        for (int column = 0; column < NumColumns; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {

                Square s = squares[column, row];

                GameObject squareGO = squareToGameObjectMap[s];

                //HexComponent hexComp = hexGO.GetComponentInChildren<HexComponent>();
                MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                if (s.squareType != Square.SQUARE_TYPE.NORMAL)
                {
                    UpdateSpecialSquareVisuals(mr, s.squareType);
                }
                else
                {

                    if (row == (NumRows - 1))
                    {
                        mr.material = BoardEndGreen;
                    }
                    else if ((column % 2) == (row % 2))
                    {
                        mr.material = BoardWhite;

                    }
                    else
                    {
                        mr.material = BoardBrown;

                    }
                }
                count++;
                //MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();
            }
        }
        //if (SceneData.levels.Count > 0)
        //{
        //    Level level = SceneData.levels[currentlevel];
        //    if (level.specialSquares.Count > 0)
        //    {
        //        foreach (SpecialSquare sSquare in level.specialSquares)
        //        {
        //            GameObject squareGO = squareToGameObjectMap[squares[sSquare.col, sSquare.row]];
        //            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
        //            UpdateSpecialSquareVisuals(mr, sSquare.squareType);
        //        }
        //    }
        //}
    }

    public void UpdateSpecialSquareVisuals(MeshRenderer mr, Square.SQUARE_TYPE squareType)
    {
        switch (squareType)
        {
            case Square.SQUARE_TYPE.RESET_TURN_COUNT:
                mr.material = BoardBlue;
                break;
            case Square.SQUARE_TYPE.IMPASSABLE:
                mr.material = BoardRed;
                break;
        }
    }
    public IEnumerator DoMove(Piece piece, Square newSquare, bool playerMove)
    {
        if (playerMove)
        {
            turn++;
        }

        piece.DoMove(newSquare, playerMove);
        
        GameObject go = pieceToGameObjectMap[piece];

        Vector3 newPosition = newSquare.Position();
        newPosition.y = 0.02f;
        go.transform.position = newPosition;

        if (newSquare.R == (NumRows -1))
        {
            piece.escaped = true;
        }
        if (newSquare.squareType != Square.SQUARE_TYPE.NORMAL)
        {
            processSpecialSquare(newSquare);
        }
        endTurn = true;
        yield return null;
    }

    private void processSpecialSquare(Square square)
    {
        switch (square.squareType)
        {
            case Square.SQUARE_TYPE.RESET_TURN_COUNT:

                this.turnReset = true;
                AudioSource.PlayClipAtPoint(specialSquareClip, transform.position, 1f);
                break;
        }
        square.squareType = Square.SQUARE_TYPE.NORMAL;
    }

    public void HighlightDestinations(Piece piece, Square square)
    {
        int cCol = square.Q;
        int cRow = square.R;
        int col;
        int row;

        switch (piece.moveType)
        {           
            case Piece.MOVEMENT_TYPE.ALL:
                for (int column = 0; column < NumColumns; column++)
                {
                    for (int lrow = 0; lrow < NumRows; lrow++)
                    {
                        if (!((column == square.Q) && (lrow == square.R)))
                        {
                        
                            Square s = squares[column, lrow];
                            if ((s.squareType != Square.SQUARE_TYPE.IMPASSABLE) && (s.IsEmpty()))
                            {
                                GameObject squareGO = squareToGameObjectMap[s];
                                MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                                mr.material = BoardGreen;
                            }
                        }
                    }
                }
                break;
            case Piece.MOVEMENT_TYPE.ADJACENT:
                if (cCol != 0)
                {
                    Square s = squares[(cCol - 1), cRow];
                    if ((s.squareType != Square.SQUARE_TYPE.IMPASSABLE) && (s.IsEmpty()))
                    {
                        GameObject squareGO = squareToGameObjectMap[s];
                        MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                        mr.material = BoardGreen;
                    }
                }

                if (cCol != (NumColumns - 1))
                {
                    Square s = squares[(cCol + 1), cRow];
                    if ((s.squareType != Square.SQUARE_TYPE.IMPASSABLE) && (s.IsEmpty()))
                    {
                        GameObject squareGO = squareToGameObjectMap[s];
                        MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                        mr.material = BoardGreen;
                    }
                }

                if (cRow != 0)
                {
                    Square s = squares[cCol, (cRow - 1)];
                    if ((s.squareType != Square.SQUARE_TYPE.IMPASSABLE) && (s.IsEmpty()))
                    {
                        GameObject squareGO = squareToGameObjectMap[s];
                        MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                        mr.material = BoardGreen;
                    }
                }

                if (cRow != (NumRows - 1))
                {
                    Square s = squares[cCol, (cRow + 1)];
                    if ((s.squareType != Square.SQUARE_TYPE.IMPASSABLE) && (s.IsEmpty()))
                    {
                        GameObject squareGO = squareToGameObjectMap[s];
                        MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                        mr.material = BoardGreen;
                    }
                }
                break;
            case Piece.MOVEMENT_TYPE.DIAGONAL:
                col = cCol - 1;
                row = cRow - 1;
                while (!(( col < 0 ) || ( row < 0)))
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                
                    col--;
                    row--;
                }
                col = cCol + 1;
                row = cRow + 1;
                while ((col < NumColumns) && (row < NumRows))
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col++;
                    row++;
                }
                col = cCol - 1;
                row = cRow + 1;
                while ((col >= 0) && (row < NumRows))
                {
                    Square s = squares[col, (row)];

                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col--;
                    row++;
                }
                col = cCol + 1;
                row = cRow - 1;
                while ((col < NumColumns) && (row >= 0))
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col++;
                    row--;
                }
                break;
            case Piece.MOVEMENT_TYPE.VANDT:
                col = cCol - 1;
                row = cRow;
                while (col >= 0) 
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col--;
                }
                col = cCol + 1;
                row = cRow;
                while (col < NumColumns)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col++;
                }
                col = cCol;
                row = cRow - 1;
                while (row >= 0)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    row--;
                }
                col = cCol;
                row = cRow + 1;
                while (row < NumRows)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    row++;
                }
                break;
            case Piece.MOVEMENT_TYPE.HORIZONTAL:
                col = cCol - 1;
                row = cRow;
                while (col >= 0)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col--;
                }
                col = cCol + 1;
                row = cRow;
                while (col < NumColumns)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    col++;
                }
                break;
            case Piece.MOVEMENT_TYPE.VERTICAL:
                col = cCol;
                row = cRow - 1;
                while (row >= 0)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    row--;
                }
                col = cCol;
                row = cRow + 1;
                while (row < NumRows)
                {
                    Square s = squares[col, (row)];
                    if (s.squareType != Square.SQUARE_TYPE.IMPASSABLE) 
                    {
                        if (s.IsEmpty())
                        {
                            GameObject squareGO = squareToGameObjectMap[s];
                            MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                            mr.material = BoardGreen;
                        }
                    }
                    else
                    {
                        break;
                    }
                    row++;
                }
                break;

        }
    }
    public bool ValidDestination(Piece piece, Square selectedSquare, Square newSquare)
    {
        int nCol = newSquare.Q;
        int nRow = newSquare.R;
        int oCol = selectedSquare.Q;
        int oRow = selectedSquare.R;
        int col;
        int row;
        bool valid = false;
        if (newSquare.squareType == Square.SQUARE_TYPE.IMPASSABLE)
        {
            return false;
        }
        if (!newSquare.IsEmpty())
        {
            return false;
        }
        switch (piece.moveType)
        {
            case Piece.MOVEMENT_TYPE.ALL:
                return true;
                break;
            case Piece.MOVEMENT_TYPE.ADJACENT:
                if ((nCol == (oCol-1)) && (nRow == oRow))
                {
                    return true;
                }
                else if((nCol == (oCol + 1)) && (nRow == oRow))
                {
                    return true;
                }
                else if((nCol == oCol) && (nRow == (oRow - 1)))
                {
                    return true;
                }
                else if((nCol == oCol) && (nRow == (oRow + 1)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                break;
                case Piece.MOVEMENT_TYPE.DIAGONAL:
                col = oCol - 1;
                row = oRow - 1;
                while (!((col < 0) || (row < 0)))
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }

                    col--;
                    row--;
                }
                col = oCol + 1;
                row = oRow + 1;
                while ((col < NumColumns) && (row < NumRows))
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    col++;
                    row++;
                }
                col = oCol - 1;
                row = oRow + 1;
                while ((col >= 0) && (row < NumRows))
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }

                    col--;
                    row++;
                }
                col = oCol + 1;
                row = oRow - 1;
                while ((col < NumColumns) && (row >= 0))
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }

                    col++;
                    row--;
                }
                return false;
                break;
            case Piece.MOVEMENT_TYPE.VANDT:
                col = oCol - 1;
                row = oRow;
                while (col >= 0)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    col--;
                }
                col = oCol + 1;
                row = oRow;
                while (col < NumColumns)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    col++;
                }
                col = oCol;
                row = oRow - 1;
                while (row >= 0)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    row--;
                }
                col = oCol;
                row = oRow + 1;
                while (row < NumRows)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    row++;
                }
                break;
            case Piece.MOVEMENT_TYPE.HORIZONTAL:
                col = oCol - 1;
                row = oRow;
                while (col >= 0)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    col--;
                }
                col = oCol + 1;
                row = oRow;
                while (col < NumColumns)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    col++;
                }
                break;
            case Piece.MOVEMENT_TYPE.VERTICAL:
                col = oCol;
                row = oRow - 1;
                while (row >= 0)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    row--;
                }
                col = oCol;
                row = oRow + 1;
                while (row < NumRows)
                {
                    if (squares[col, row].squareType == Square.SQUARE_TYPE.IMPASSABLE)
                    {
                        break;
                    }
                    if ((col == nCol) && (row == nRow))
                    {
                        return true;
                    }
                    row++;
                }
                break;
        }
        return false;
    }

    public void Reset()
    {
        AudioSource.PlayClipAtPoint(resetClip, transform.position, 1f);

        // Reset pieces
        foreach (Piece p in playerPieces)
        {           
            Square resetSquare = squares[p.startCol, p.startRow];
            StartCoroutine(DoMove(p, resetSquare, false));
        }

        // Refresh special squares.
        Level level = SceneData.levels[SceneData.currentLevel[0]];
        if (level.specialSquares.Count > 0)
        {
            foreach (SpecialSquare sSquare in level.specialSquares)
            {
                squares[sSquare.col, sSquare.row].squareType = sSquare.squareType;
                GameObject squareGO = squareToGameObjectMap[squares[sSquare.col, sSquare.row]];
                MeshRenderer mr = squareGO.GetComponentInChildren<MeshRenderer>();
                UpdateSpecialSquareVisuals(mr, sSquare.squareType);
            }
        }
    }
}

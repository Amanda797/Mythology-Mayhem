using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaflMechanics : MonoBehaviour
{
    public Side player1Side;
    public Side player2Side;
    public Turn currentTurn;
    public List<string> exposedArray;
    public TaflPieceScript selectedPiece;
    public TaflTileScript selectedTile;

    public TaflTileScript hoveredTile;

    public Color highlightColorGreen;
    public Color highlightColorBlue;

    public float pieceRotateSpeed;
    public float pieceMoveSpeed;

    public List<TaflPosition> startPositions;

    TaflPosition.TaflPieces[,] placedPieces;

    public float offset;

    public Transform centerPosition;
    public GameObject kingStandardPrefab;
    public GameObject kingAltPrefab;
    public GameObject pawnStandardPrefab;
    public GameObject pawnAltPrefab;

    public GameObject pawnGhostPrefab;
    public GameObject pawnAltGhostPrefab;
    public GameObject kingGhostPrefab;
    public GameObject kingAltGhostPrefab;

    public GameObject tileSelectionPrefab;
    public float tileYOffset;

    public List<TaflTileScript> tiles;

    public List<TaflPosition.TaflDirection> Column1;
    public List<TaflPosition.TaflDirection> Column2;
    public List<TaflPosition.TaflDirection> Column3;
    public List<TaflPosition.TaflDirection> Column4;
    public List<TaflPosition.TaflDirection> Column5;
    public List<TaflPosition.TaflDirection> Column6;
    public List<TaflPosition.TaflDirection> Column7;
    public List<TaflPosition.TaflDirection> Column8;
    public List<TaflPosition.TaflDirection> Column9;
    public List<TaflPosition.TaflDirection> Column10;
    public List<TaflPosition.TaflDirection> Column11;

    [Header("Load System")]
    public bool tilesGenerated;
    public bool boardDataCleared;
    public bool startPiecesGenerated;
    public int tileX;
    public int tileY;
    public int dataX;
    public int dataY;
    public int currentStartPiece;

    [Header("Hotseat Game")]
    public bool hotseatVersion;
    public bool hotseatReady;
    public GameObject seatSwapMenu;
    public Text seatSwapText;
    public Text currentPlayerDisplayText;
    public Button setSwapConfirm;
    public bool delaySwapScreen;

    public enum Turn
    {
        Loading,
        Player1Turn,
        Player1Turning,
        Player1Moving,
        Player1CheckCapture,
        Player1CheckWin,
        Player2Turn,
        Player2Turning,
        Player2Moving,
        Player2CheckCapture,
        Player2CheckWin,
        Player1Win,
        Player2Win,
        DelayEndTurnPlayer1,
        DelayEndTurnPlayer2
    }

    public enum Side
    {
        Attacking,
        Defending
    }
    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<TaflTileScript>();
        exposedArray = new List<string>();
        placedPieces = new TaflPosition.TaflPieces[11, 11];

        delaySwapScreen = false;

        tilesGenerated = false;
        boardDataCleared = false;
        startPiecesGenerated = false;
        tileX = 1;
        tileY = 1;
        dataX = 0;
        dataY = 0;
        currentStartPiece = 0;
        SetHotSeat(0, true);
        currentTurn = Turn.Loading;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentTurn)
        {
            case Turn.Loading:
                if (!tilesGenerated)
                {
                    if (tileX < 12)
                    {
                        if (tileY < 12)
                        {
                            GameObject tileObj = Instantiate(tileSelectionPrefab, centerPosition.position + FindPosition(tileX, tileY, true), SetTileDirection(tileX, tileY), centerPosition);
                            tileObj.name = ((tileX) + ":" + (tileY + 1) + ": Tile");
                            TaflTileScript tileScript = tileObj.GetComponent<TaflTileScript>();
                            tileScript.x = tileX;
                            tileScript.y = tileY;
                            tileScript.occupying = null;
                            tiles.Add(tileScript);
                            tileScript.rend.material.color = highlightColorGreen;
                            tileScript.gameObject.SetActive(false);
                            tileY++;
                        }
                        else
                        {
                            tileY = 1;
                            tileX++;
                        }
                    }
                    else
                    {
                        tilesGenerated = true;
                    }
                }
                else if (!boardDataCleared)
                {
                    if (dataX < placedPieces.GetLength(0))
                    {
                        if (dataY < placedPieces.GetLength(1))
                        {
                            placedPieces[dataX, dataY] = TaflPosition.TaflPieces.None;
                            dataY++;
                        }
                        else
                        {
                            dataY = 0;
                            dataX++;
                        }
                    }
                    else
                    {
                        boardDataCleared = true;
                    }
                }
                else if (!startPiecesGenerated)
                {
                    if (currentStartPiece < startPositions.Count)
                    {
                        int x = startPositions[currentStartPiece].x - 1;
                        int y = startPositions[currentStartPiece].y - 1;

                        placedPieces[x, y] = startPositions[currentStartPiece].piece;
                        exposedArray.Add(x + ":" + y + "/" + startPositions[currentStartPiece].piece);

                        GameObject obj = Instantiate(FindPrefabPiece(startPositions[currentStartPiece].piece), centerPosition.position + FindPosition(x + 1, y + 1, false), Quaternion.identity, centerPosition);
                        TaflPieceScript pieceScript = obj.GetComponent<TaflPieceScript>();
                        pieceScript.x = x + 1;
                        pieceScript.y = y + 1;

                        for (int j = 0; j < tiles.Count; j++)
                        {
                            if (tiles[j].x == startPositions[currentStartPiece].x && tiles[j].y == startPositions[currentStartPiece].y)
                            {
                                if (pieceScript != null)
                                {
                                    tiles[j].occupying = pieceScript;
                                    pieceScript.occupying = tiles[j];
                                    pieceScript.rend.enabled = false;
                                }
                                obj.transform.forward = tiles[j].transform.forward;
                                break;
                            }
                        }

                        currentStartPiece++;
                    }
                    else
                    {
                        startPiecesGenerated = true;
                    }
                }
                else
                {
                    player1Side = Side.Defending;
                    player2Side = Side.Attacking;
                    currentTurn = Turn.Player1Turn;
                    if (currentTurn == Turn.Player1Turn)
                    {
                        SetHotSeat(1, false);
                    }
                    else if (currentTurn == Turn.Player2Turn)
                    {
                        SetHotSeat(2, false);
                    }
                    if (player1Side == Side.Defending)
                    {
                        SetSelectablePieces(1);
                    }
                    else
                    {
                        SetSelectablePieces(2);
                    }
                }
                break;

            case Turn.Player1Turning:
                Vector3 targetDirection1 = selectedTile.transform.position - selectedPiece.transform.position;
                targetDirection1.y = 0;
                float rotStep1 = pieceRotateSpeed * Time.deltaTime;
                selectedPiece.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(selectedPiece.transform.forward, targetDirection1, rotStep1, 0.0f));

                Vector3 pieceForward1 = selectedPiece.transform.forward;
                float angle1 = Vector3.SignedAngle(pieceForward1, targetDirection1, Vector3.up);
                int angleOfAccuracy1 = 2;

                if (angle1 <= angleOfAccuracy1 && angle1 >= -angleOfAccuracy1)
                {
                    currentTurn = Turn.Player1Moving;
                }
                break;
            case Turn.Player1Moving:
                float moveStep1 = pieceMoveSpeed * Time.deltaTime;
                Vector3 targetMove1 = selectedTile.transform.position;
                targetMove1.y = selectedPiece.transform.position.y;
                selectedPiece.transform.position = Vector3.MoveTowards(selectedPiece.transform.position, targetMove1, moveStep1);

                if (Vector3.Distance(selectedPiece.transform.position, targetMove1) < 0.001f)
                {
                    selectedPiece.occupying.occupying = null;
                    selectedPiece.occupying = selectedTile;
                    selectedTile.occupying = selectedPiece;
                    placedPieces[selectedPiece.x - 1, selectedPiece.y - 1] = TaflPosition.TaflPieces.None;
                    selectedPiece.x = selectedTile.x;
                    selectedPiece.y = selectedTile.y;
                    placedPieces[selectedPiece.x - 1, selectedPiece.y - 1] = selectedPiece.type;
                    currentTurn = Turn.Player1CheckCapture;
                    CalculateCaptures();
                }
                break;
            case Turn.Player2Turning:
                Vector3 targetDirection2 = selectedTile.transform.position - selectedPiece.transform.position;
                targetDirection2.y = 0;
                float rotStep2 = pieceRotateSpeed * Time.deltaTime;
                selectedPiece.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(selectedPiece.transform.forward, targetDirection2, rotStep2, 0.0f));

                Vector3 pieceForward2 = selectedPiece.transform.forward;
                float angle2 = Vector3.SignedAngle(pieceForward2, targetDirection2, Vector3.up);
                int angleOfAccuracy2 = 2;

                if (angle2 <= angleOfAccuracy2 && angle2 >= -angleOfAccuracy2)
                {
                    currentTurn = Turn.Player2Moving;
                }
                break;
            case Turn.Player2Moving:
                float moveStep2 = pieceMoveSpeed * Time.deltaTime;
                Vector3 targetMove2 = selectedTile.transform.position;
                targetMove2.y = selectedPiece.transform.position.y;
                selectedPiece.transform.position = Vector3.MoveTowards(selectedPiece.transform.position, targetMove2, moveStep2);

                if (Vector3.Distance(selectedPiece.transform.position, targetMove2) < 0.001f)
                {
                    selectedPiece.occupying.occupying = null;
                    selectedPiece.occupying = selectedTile;
                    selectedTile.occupying = selectedPiece;
                    placedPieces[selectedPiece.x - 1, selectedPiece.y - 1] = TaflPosition.TaflPieces.None;
                    selectedPiece.x = selectedTile.x;
                    selectedPiece.y = selectedTile.y;
                    placedPieces[selectedPiece.x - 1, selectedPiece.y - 1] = selectedPiece.type;
                    currentTurn = Turn.Player2CheckCapture;
                    CalculateCaptures();
                }
                break;
        }
    }

    GameObject FindPrefabPiece(TaflPosition.TaflPieces pieceType)
    {

        GameObject tempObj = null;
        switch (pieceType)
        {

            case TaflPosition.TaflPieces.King:
                tempObj = kingStandardPrefab;
                return tempObj;

            case TaflPosition.TaflPieces.AltKing:
                tempObj = kingAltPrefab;
                return tempObj;

            case TaflPosition.TaflPieces.Pawn:
                tempObj = pawnStandardPrefab;
                return tempObj;

            case TaflPosition.TaflPieces.AltPawn:
                tempObj = pawnAltPrefab;
                return tempObj;

        }

        return tempObj;
    }

    Quaternion SetTileDirection(int x, int y)
    {
        Vector3 forward = Vector3.forward;
        Vector3 dataPull = FindTileDirection(x, y);

        switch (dataPull.z)
        {
            case 0:
                forward = centerPosition.forward;
                break;
            case 1:
                forward = centerPosition.forward * -1;
                break;
            case 2:
                forward = centerPosition.right;
                break;
            case 3:
                forward = centerPosition.right * -1;
                break;
        }

        Quaternion rotation = Quaternion.LookRotation(forward);
        return rotation;
    }

    Vector3 FindPosition(int gridX, int gridY, bool tile)
    {
        Vector3 tempPos = Vector3.zero;
        float tempFloatX = (gridX - 6) * offset;
        float tempFloatY = (gridY - 6) * offset;
        tempPos.x = tempFloatX;
        if (tile)
        {
            tempPos.y = tileYOffset;
        }
        tempPos.z = tempFloatY;
        return tempPos;
    }

    Vector3 FindTileDirection(int x, int y)
    {
        Vector3 tempVect = Vector3.zero;

        switch (x)
        {
            case 1:
                tempVect = new Vector3(x, y, (int)Column1[y - 1]);
                return tempVect;
            case 2:
                tempVect = new Vector3(x, y, (int)Column2[y - 1]);
                return tempVect;
            case 3:
                tempVect = new Vector3(x, y, (int)Column3[y - 1]);
                return tempVect;
            case 4:
                tempVect = new Vector3(x, y, (int)Column4[y - 1]);
                return tempVect;
            case 5:
                tempVect = new Vector3(x, y, (int)Column5[y - 1]);
                return tempVect;
            case 6:
                tempVect = new Vector3(x, y, (int)Column6[y - 1]);
                return tempVect;
            case 7:
                tempVect = new Vector3(x, y, (int)Column7[y - 1]);
                return tempVect;
            case 8:
                tempVect = new Vector3(x, y, (int)Column8[y - 1]);
                return tempVect;
            case 9:
                tempVect = new Vector3(x, y, (int)Column9[y - 1]);
                return tempVect;
            case 10:
                tempVect = new Vector3(x, y, (int)Column10[y - 1]);
                return tempVect;
            case 11:
                tempVect = new Vector3(x, y, (int)Column11[y - 1]);
                return tempVect;
        }

        return new Vector3(1, 1, 2);
    }
    public void SelectPiece(TaflPieceScript taflPiece)
    {
        if (selectedPiece != null)
        {
            SetSelectionPieceColor(selectedPiece, false);
        }
        selectedPiece = taflPiece;
        SetSelectionPieceColor(taflPiece, true);
        LegalMove(selectedPiece.x, selectedPiece.y);
    }
    public void LegalMove(int pieceX, int pieceY)
    {
        foreach (TaflTileScript tile in tiles)
        {
            tile.gameObject.SetActive(false);
        }

        //North
        for (int y = pieceY + 1; y <= 11; y++)
        {
            if (placedPieces[pieceX - 1, y - 1] != TaflPosition.TaflPieces.None)
            {
                break;
            }
            else
            {
                if (selectedPiece.type != TaflPosition.TaflPieces.King && selectedPiece.type != TaflPosition.TaflPieces.AltKing)
                {
                    if (pieceX == 1 && y == 1)
                        break;
                    if (pieceX == 1 && y == 11)
                        break;
                    if (pieceX == 11 && y == 1)
                        break;
                    if (pieceX == 11 && y == 11)
                        break;
                }
                SetPreviewTiles(pieceX, y);
            }
        }
        //South
        for (int y = pieceY - 1; y >= 1; y--)
        {
            if (placedPieces[pieceX - 1, y - 1] != TaflPosition.TaflPieces.None)
            {
                break;
            }
            else
            {
                if (selectedPiece.type != TaflPosition.TaflPieces.King && selectedPiece.type != TaflPosition.TaflPieces.AltKing)
                {
                    if (pieceX == 1 && y == 1)
                        break;
                    if (pieceX == 1 && y == 11)
                        break;
                    if (pieceX == 11 && y == 1)
                        break;
                    if (pieceX == 11 && y == 11)
                        break;
                }
                SetPreviewTiles(pieceX, y);
            }
        }
        //East
        for (int x = pieceX + 1; x <= 11; x++)
        {
            if (placedPieces[x - 1, pieceY - 1] != TaflPosition.TaflPieces.None)
            {
                break;
            }
            else
            {
                if (selectedPiece.type != TaflPosition.TaflPieces.King && selectedPiece.type != TaflPosition.TaflPieces.AltKing)
                {
                    if (x == 1 && pieceY == 1)
                        break;
                    if (x == 1 && pieceY == 11)
                        break;
                    if (x == 11 && pieceY == 1)
                        break;
                    if (x == 11 && pieceY == 11)
                        break;
                }
                SetPreviewTiles(x, pieceY);
            }
        }
        //West
        for (int x = pieceX - 1; x >= 1; x--)
        {
            if (placedPieces[x - 1, pieceY - 1] != TaflPosition.TaflPieces.None)
            {
                break;
            }
            else
            {
                if (selectedPiece.type != TaflPosition.TaflPieces.King && selectedPiece.type != TaflPosition.TaflPieces.AltKing)
                {
                    if (x == 1 && pieceY == 1)
                        break;
                    if (x == 1 && pieceY == 11)
                        break;
                    if (x == 11 && pieceY == 1)
                        break;
                    if (x == 11 && pieceY == 11)
                        break;
                }
                SetPreviewTiles(x, pieceY);
            }
        }
    }
    public void SetSelectionPieceColor(TaflPieceScript pieceScript, bool selected)
    {
        if (selected)
        {
            pieceScript.rend.enabled = true;
        }
        else
        {
            pieceScript.rend.enabled = false;
        }
    }
    public void SetPreviewTiles(int tileX, int tileY)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].x == tileX && tiles[i].y == tileY)
            {
                tiles[i].gameObject.SetActive(true);
            }
        }
    }

    public void HighlightHoverTile()
    {
        if (hoveredTile != null)
        {
            hoveredTile.rend.material.color = highlightColorBlue;
        }

    }

    public void SelectTile(TaflTileScript tileScript)
    {
        if (selectedPiece != null)
        {
            selectedTile = tileScript;
            RunTurn();
        }
    }
    void SetSelectablePieces(int which)
    {
        foreach (TaflTileScript tile in tiles)
        {
            if (tile.occupying != null)
            {
                tile.occupying.rend.enabled = false;
            }
        }
        switch (which)
        {
            case 1:
                foreach (TaflTileScript tile in tiles)
                {
                    if (tile.occupying != null)
                    {
                        if (tile.occupying.type == TaflPosition.TaflPieces.King || tile.occupying.type == TaflPosition.TaflPieces.Pawn)
                        {
                            tile.occupying.rend.gameObject.transform.parent.GetComponent<Collider>().enabled = true;
                        }
                        else
                        {
                            tile.occupying.rend.gameObject.transform.parent.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
                break;
            case 2:
                foreach (TaflTileScript tile in tiles)
                {
                    if (tile.occupying != null)
                    {
                        if (tile.occupying.type == TaflPosition.TaflPieces.King || tile.occupying.type == TaflPosition.TaflPieces.Pawn)
                        {
                            tile.occupying.rend.gameObject.transform.parent.GetComponent<Collider>().enabled = false;
                        }
                        else
                        {
                            tile.occupying.rend.gameObject.transform.parent.GetComponent<Collider>().enabled = true;
                        }
                    }
                }
                break;
            case 3:
                foreach (TaflTileScript tile in tiles)
                {
                    if (tile.occupying != null)
                    {
                        tile.occupying.rend.gameObject.transform.parent.GetComponent<Collider>().enabled = false;
                    }
                }
                break;
        }
    }
    public void RayHitTile(bool hitTile, TaflTileScript tileScript)
    {
        if (hitTile)
        {
            if (hoveredTile != tileScript)
            {
                if (hoveredTile != null)
                {
                    ResetTileColor(hoveredTile);
                }
                hoveredTile = tileScript;
                HighlightHoverTile();
            }
        }
        else
        {
            if (hoveredTile != null)
            {
                ResetTileColor(hoveredTile);
                hoveredTile = null;
            }
        }
    }
    public void ResetTileColor(TaflTileScript tileScript)
    {
        if (tileScript != null)
        {
            tileScript.rend.material.color = highlightColorGreen;
        }
    }

    public void RunTurn()
    {
        foreach (TaflTileScript tile in tiles)
        {
            tile.gameObject.SetActive(false);
        }
        SetSelectablePieces(3);
        if (currentTurn == Turn.Player1Turn)
        {
            currentTurn = Turn.Player1Turning;
        }
        if (currentTurn == Turn.Player2Turn)
        {
            currentTurn = Turn.Player2Turning;
        }
    }

    void CalculateCaptures()
    {
        if (selectedPiece.type != TaflPosition.TaflPieces.King && selectedPiece.type != TaflPosition.TaflPieces.AltKing)
        {
            int x = selectedPiece.x;
            int y = selectedPiece.y;


            int y1a = y + 1;


            int y2a = y - 1;

            int x3a = x + 1;


            int x4a = x - 1;

            if (y1a < 11)
            {
                if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Defending && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int y1b = y1a + 1;
                    print(y1b);
                    if (y1b < 12)
                    {
                        if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 1 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 11 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                    }

                }
                else if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Attacking && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int y1b = y1a + 1;
                    if (y1b < 12)
                    {
                        if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 1 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 11 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Defending && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int y1b = y1a + 1;
                    if (y1b < 12)
                    {
                        if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 1 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 11 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Attacking && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int y1b = y1a + 1;
                    if (y1b < 12)
                    {
                        if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 1 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                        else if (x == 11 && y1b == 11)
                        {
                            CapturePiece(x, y1a);
                        }
                    }
                }
            }
            if (y2a > 1)
            {
                if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Defending && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int y2b = y2a - 1;
                    if (y2b > 0)
                    {
                        if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 1 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 11 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                    }

                }
                else if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Attacking && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int y2b = y2a - 1;
                    if (y2b > 0)
                    {
                        if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 1 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 11 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Defending && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int y2b = y2a - 1;
                    if (y2b > 0)
                    {
                        if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 1 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 11 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Attacking && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int y2b = y2a - 1;
                    if (y2b > 0)
                    {
                        if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 1 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                        else if (x == 11 && y2b == 1)
                        {
                            CapturePiece(x, y2a);
                        }
                    }
                }
            }
            if (x3a < 11)
            {
                if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Defending && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int x3b = x3a + 1;
                    if (x3b < 12)
                    {
                        if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 1)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 11)
                        {
                            CapturePiece(x3a, y);
                        }
                    }
                }
                else if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Attacking && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int x3b = x3a + 1;
                    if (x3b < 12)
                    {
                        if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 1)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 11)
                        {
                            CapturePiece(x3a, y);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Defending && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int x3b = x3a + 1;
                    if (x3b < 12)
                    {
                        if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 1)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 11)
                        {
                            CapturePiece(x3a, y);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Attacking && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int x3b = x3a + 1;
                    if (x3b < 12)
                    {
                        if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 1)
                        {
                            CapturePiece(x3a, y);
                        }
                        else if (x3b == 11 && y == 11)
                        {
                            CapturePiece(x3a, y);
                        }
                    }
                }
            }
            if (x4a > 1)
            {
                if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Defending && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int x4b = x4a - 1;
                    if (x4b > 0)
                    {
                        if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 1)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 11)
                        {
                            CapturePiece(x4a, y);
                        }
                    }
                }
                else if (currentTurn == Turn.Player1CheckCapture && player1Side == Side.Attacking && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int x4b = x4a - 1;
                    if (x4b > 0)
                    {
                        if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 1)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 11)
                        {
                            CapturePiece(x4a, y);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Defending && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                {
                    int x4b = x4a - 1;
                    if (x4b > 0)
                    {
                        if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 1)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 11)
                        {
                            CapturePiece(x4a, y);
                        }
                    }
                }
                else if (currentTurn == Turn.Player2CheckCapture && player2Side == Side.Attacking && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.Pawn)
                {
                    int x4b = x4a - 1;
                    if (x4b > 0)
                    {
                        if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 1)
                        {
                            CapturePiece(x4a, y);
                        }
                        else if (x4b == 1 && y == 11)
                        {
                            CapturePiece(x4a, y);
                        }
                    }
                }
            }
        }
        if (currentTurn == Turn.Player1CheckCapture)
        {
            currentTurn = Turn.Player1CheckWin;
        }
        if (currentTurn == Turn.Player2CheckCapture)
        {
            currentTurn = Turn.Player2CheckWin;
        }

        CalculateWin();
    }

    void CapturePiece(int x, int y)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].x == x && tiles[i].y == y)
            {
                TaflPieceScript pieceScript = tiles[i].occupying;
                if (pieceScript != null)
                {
                    pieceScript.occupying = null;
                    tiles[i].occupying = null;
                    placedPieces[x - 1, y - 1] = TaflPosition.TaflPieces.None;
                    if (pieceScript.type == TaflPosition.TaflPieces.Pawn)
                    {
                        pieceScript.CapturePiece(pawnGhostPrefab);
                        delaySwapScreen = true;
                    }
                    else
                    {
                        pieceScript.CapturePiece(pawnAltGhostPrefab);
                        delaySwapScreen = true;
                    }
                }
            }
        }
    }

    void CalculateWin()
    {
        int x = selectedPiece.x;
        int y = selectedPiece.y;


        int y1a = y + 1;


        int y2a = y - 1;

        int x3a = x + 1;


        int x4a = x - 1;
        if (currentTurn == Turn.Player1CheckWin)
        {
            if (selectedPiece.type == TaflPosition.TaflPieces.King && player1Side == Side.Defending)
            {
                if (x == 1 && y == 1)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                    }
                    currentTurn = Turn.Player1Win;
                    Win(player1Side);
                    return;
                }
                if (x == 1 && y == 11)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                    }
                    currentTurn = Turn.Player1Win;
                    Win(player1Side);
                    return;
                }
                if (x == 11 && y == 1)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                    }
                    currentTurn = Turn.Player1Win;
                    Win(player1Side);
                    return;
                }
                if (x == 11 && y == 11)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                    }
                    currentTurn = Turn.Player1Win;
                    Win(player1Side);
                    return;
                }
            }
        }
        if (currentTurn == Turn.Player2CheckWin)
        {
            if (selectedPiece.type == TaflPosition.TaflPieces.King && player2Side == Side.Defending)
            {
                if (x == 1 && y == 1)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                    }
                    currentTurn = Turn.Player2Win;
                    Win(player2Side);
                    return;
                }
                if (x == 1 && y == 11)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                    }
                    currentTurn = Turn.Player2Win;
                    Win(player2Side);
                    return;
                }
                if (x == 11 && y == 1)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                    }
                    currentTurn = Turn.Player2Win;
                    Win(player2Side);
                    return;
                }
                if (x == 11 && y == 11)
                {
                    if (hotseatVersion)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                    }
                    currentTurn = Turn.Player2Win;
                    Win(player2Side);
                    return;
                }
            }
        }

        if (y1a < 11)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.King)
            {
                int y1b = y1a + 1;
                int x1a = x + 1;
                int x1b = x - 1;
                if (y1b < 12 && x1a < 12 && x1b > 0)
                {
                    if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (y1b == 12 && x1a < 12 && x1b > 0)
                {
                    if (placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (y1b < 12 && x1a == 12 && x1b > 0)
                {
                    if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (y1b < 12 && x1a < 12 && x1b == 0)
                {
                    if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.King)
            {
                int y1b = y1a + 1;
                int x1a = x + 1;
                int x1b = x - 1;
                if (y1b < 12 && x1a < 12 && x1b > 0)
                {
                    if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (y1b == 12 && x1a < 12 && x1b > 0)
                {
                    if (placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (y1b < 12 && x1a == 12 && x1b > 0)
                {
                    if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (y1b < 12 && x1a < 12 && x1b == 0)
                {
                    if (placedPieces[x - 1, y1b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
        }
        else if (y1a == 11)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.King)
            {
                int x1a = x + 1;
                int x1b = x - 1;
                if (x1a < 12 && x1b > 0)
                {
                    if (placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x - 1, y1a - 1] == TaflPosition.TaflPieces.King)
            {
                int x1a = x + 1;
                int x1b = x - 1;
                if (x1a < 12 && x1b > 0)
                {
                    if (placedPieces[x1a - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x1b - 1, y1a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
        }
        if (y2a > 1)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.King)
            {
                int y2b = y2a - 1;
                int x2a = x + 1;
                int x2b = x - 1;
                if (y2b > 0 && x2a < 12 && x2b > 0)
                {
                    if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (y2b == 0 && x2a < 12 && x2b > 0)
                {
                    if (placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (y2b > 0 && x2a == 12 && x2b > 0)
                {
                    if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (y2b > 0 && x2a < 12 && x2b == 0)
                {
                    if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.King)
            {
                int y2b = y2a - 1;
                int x2a = x + 1;
                int x2b = x - 1;
                if (y2b > 0 && x2a < 12 && x2b > 0)
                {
                    if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (y2b == 0 && x2a < 12 && x2b > 0)
                {
                    if (placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (y2b > 0 && x2a == 12 && x2b > 0)
                {
                    if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (y2b > 0 && x2a < 12 && x2b == 0)
                {
                    if (placedPieces[x - 1, y2b - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
        }
        else if (y2a == 1)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.King)
            {
                int x2a = x + 1;
                int x2b = x - 1;
                if (x2a < 12 && x2b > 0)
                {
                    if (placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x - 1, y2a - 1] == TaflPosition.TaflPieces.King)
            {
                int x2a = x + 1;
                int x2b = x - 1;
                if (x2a < 12 && x2b > 0)
                {
                    if (placedPieces[x2a - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x2b - 1, y2a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
        }
        if (x3a < 11)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.King)
            {
                int x3b = x3a + 1;
                int y3a = y + 1;
                int y3b = y - 1;
                if (x3b < 12 && y3a < 12 && y3b > 0)
                {
                    if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (x3b == 12 && y3a < 12 && y3b > 0)
                {
                    if (placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (x3b < 12 && y3a == 12 && y3b > 0)
                {
                    if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (x3b < 12 && y3a < 12 && y3b == 0)
                {
                    if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.King)
            {
                int x3b = x3a + 1;
                int y3a = y + 1;
                int y3b = y - 1;
                if (x3b < 12 && y3a < 12 && y3b > 0)
                {
                    if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (x3b == 12 && y3a < 12 && y3b > 0)
                {
                    if (placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (x3b < 12 && y3a == 12 && y3b > 0)
                {
                    if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (x3b < 12 && y3a < 12 && y3b == 0)
                {
                    if (placedPieces[x3b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
        }
        else if (x3a == 11)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.King)
            {
                int y3a = y + 1;
                int y3b = y - 1;
                if (y3a < 12 && y3b > 0)
                {
                    if (placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x3a - 1, y - 1] == TaflPosition.TaflPieces.King)
            {
                int y3a = y + 1;
                int y3b = y - 1;
                if (y3a < 12 && y3b > 0)
                {
                    if (placedPieces[x3a - 1, y3a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x3a - 1, y3b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
        }
        if (x4a > 1)
        {
            if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.King)
            {
                int x4b = x4a + 1;
                int y4a = y + 1;
                int y4b = y - 1;
                if (x4b < 12 && y4a < 12 && y4b > 0)
                {
                    if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (x4b == 12 && y4a < 12 && y4b > 0)
                {
                    if (placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (x4b < 12 && y4a == 12 && y4b > 0)
                {
                    if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
                if (x4b < 12 && y4a < 12 && y4b == 0)
                {
                    if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 1 Wins!";
                        currentTurn = Turn.Player1Win;
                        Win(player1Side);
                        return;
                    }
                }
            }
            if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.King)
            {
                int x4b = x4a + 1;
                int y4a = y + 1;
                int y4b = y - 1;
                if (x4b < 12 && y4a < 12 && y4b > 0)
                {
                    if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (x4b == 12 && y4a < 12 && y4b > 0)
                {
                    if (placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (x4b < 12 && y4a == 12 && y4b > 0)
                {
                    if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
                if (x4b < 12 && y4a < 12 && y4b == 0)
                {
                    if (placedPieces[x4b - 1, y - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn)
                    {
                        currentPlayerDisplayText.text = "Player 2 Wins!";
                        currentTurn = Turn.Player2Win;
                        Win(player2Side);
                        return;
                    }
                }
            }
            else if (x4a == 1)
            {
                if (currentTurn == Turn.Player1CheckWin && player1Side == Side.Attacking && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.King)
                {
                    int y4a = y + 1;
                    int y4b = y - 1;
                    if (y4a < 12 && y4b > 0)
                    {
                        if (placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            currentPlayerDisplayText.text = "Player 1 Wins!";
                            currentTurn = Turn.Player1Win;
                            Win(player1Side);
                            return;
                        }
                    }
                }
                if (currentTurn == Turn.Player2CheckWin && player2Side == Side.Attacking && placedPieces[x4a - 1, y - 1] == TaflPosition.TaflPieces.King)
                {
                    int y4a = y + 1;
                    int y4b = y - 1;
                    if (y4a < 12 && y4b > 0)
                    {
                        if (placedPieces[x4a - 1, y4a - 1] == TaflPosition.TaflPieces.AltPawn && placedPieces[x4a - 1, y4b - 1] == TaflPosition.TaflPieces.AltPawn)
                        {
                            currentPlayerDisplayText.text = "Player 2 Wins!";
                            currentTurn = Turn.Player2Win;
                            Win(player2Side);
                            return;
                        }
                    }
                }
            }
        }

        if (currentTurn == Turn.Player1CheckWin)
        {
            selectedPiece = null;
            selectedTile = null;
            if (hotseatVersion && delaySwapScreen)
            {
                currentTurn = Turn.DelayEndTurnPlayer1;
                StartCoroutine(DelayHotseat1());
                return;
            }
            else
            {
                currentTurn = Turn.Player2Turn;
                SetHotSeat(2, false);
                if (player2Side == Side.Defending)
                {
                    SetSelectablePieces(1);
                }
                else
                {
                    SetSelectablePieces(2);
                }
                return;
            }
        }
        if (currentTurn == Turn.Player2CheckWin)
        {
            selectedPiece = null;
            selectedTile = null;
            if (hotseatVersion && delaySwapScreen)
            {
                currentTurn = Turn.DelayEndTurnPlayer2;
                StartCoroutine(DelayHotseat2());
                return;
            }
            else
            {
                currentTurn = Turn.Player1Turn;
                SetHotSeat(1, false);
                if (player1Side == Side.Defending)
                {
                    SetSelectablePieces(1);
                }
                else
                {
                    SetSelectablePieces(2);
                }
            }
        }
    }

    void Win(Side side)
    {
        if (side == Side.Attacking)
        {
            foreach (TaflTileScript tile in tiles)
            {
                if (tile.occupying != null)
                {
                    if (tile.occupying.type == TaflPosition.TaflPieces.King)
                    {
                        tile.occupying.CapturePiece(kingGhostPrefab);
                    }
                    else if (tile.occupying.type == TaflPosition.TaflPieces.Pawn)
                    {
                        tile.occupying.CapturePiece(pawnGhostPrefab);
                    }
                }
            }
        }
        else if (side == Side.Defending)
        {
            foreach (TaflTileScript tile in tiles)
            {
                if (tile.occupying != null)
                {
                    if (tile.occupying.type == TaflPosition.TaflPieces.AltKing)
                    {
                        tile.occupying.CapturePiece(kingAltGhostPrefab);
                    }
                    else if (tile.occupying.type == TaflPosition.TaflPieces.AltPawn)
                    {
                        tile.occupying.CapturePiece(pawnAltGhostPrefab);
                    }
                }
            }
        }
    }

    public void Player1SelectPiece(TaflPieceScript taflPiece)
    {
        if (currentTurn == Turn.Player1Turn)
        {
            SelectPiece(taflPiece);
        }
    }
    public void Player1SelectTile(TaflTileScript taflTile)
    {
        if (currentTurn == Turn.Player1Turn)
        {
            SelectTile(taflTile);
        }
    }
    public void Player2SelectPiece(TaflPieceScript taflPiece)
    {
        if (currentTurn == Turn.Player2Turn)
        {
            SelectPiece(taflPiece);
        }
    }
    public void Player2SelectTile(TaflTileScript taflTile)
    {
        if (currentTurn == Turn.Player2Turn)
        {
            SelectTile(taflTile);
        }
    }
    void SetHotSeat(int player, bool needsToLoad)
    {
        if (hotseatVersion)
        {
            hotseatReady = false;
            if (player == 0)
            {
                seatSwapText.text = ("Preparing Board");
            }
            if (player == 1)
            {
                seatSwapText.text = ("Player 1's Turn");
                currentPlayerDisplayText.text = ("Player 1");
            }
            else if (player == 2)
            {
                seatSwapText.text = ("Player 2's Turn");
                currentPlayerDisplayText.text = ("Player 2");
            }
            if (needsToLoad)
            {
                setSwapConfirm.interactable = false;
            }
            else
            {
                setSwapConfirm.interactable = true;
            }
            seatSwapMenu.SetActive(true);
            hotseatReady = false;
        }
    }
    public void ReadyUp()
    {
        hotseatReady = true;
    }

    IEnumerator DelayHotseat1()
    {
        yield return new WaitForSeconds(2f);
        delaySwapScreen = false;
        currentTurn = Turn.Player2Turn;
        SetHotSeat(2, false);
        if (player2Side == Side.Defending)
        {
            SetSelectablePieces(1);
        }
        else
        {
            SetSelectablePieces(2);
        }
        yield return null;
    }

    IEnumerator DelayHotseat2()
    {
        yield return new WaitForSeconds(2f);
        delaySwapScreen = false;
        currentTurn = Turn.Player1Turn;
        SetHotSeat(1, false);
        if (player1Side == Side.Defending)
        {
            SetSelectablePieces(1);
        }
        else
        {
            SetSelectablePieces(2);
        }
        yield return null;
    }

    public void RestartGame() 
    {
        foreach (TaflTileScript tile in tiles) 
        {
            if (tile.occupying != null)
            {
                Destroy(tile.occupying.gameObject);
            }
            tile.occupying = null;
        }
        exposedArray = new List<string>();
        placedPieces = new TaflPosition.TaflPieces[11, 11];

        delaySwapScreen = false;

        boardDataCleared = false;
        startPiecesGenerated = false;

        dataX = 0;
        dataY = 0;
        currentStartPiece = 0;
        SetHotSeat(0, true);
        currentTurn = Turn.Loading;
    }
}

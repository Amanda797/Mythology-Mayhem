using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NineMMMechanics : MonoBehaviour
{
    public Side player1Side;
    public bool canFlyP1;
    public Side player2Side;
    public bool canFlyP2;
    public Turn currentTurn;

    public List<Mill> millsP1;
    public List<Mill> millsP2;

    public int marblesToPlaceP1;
    public int marblesToPlaceP2;
    public List<string> exposedArray;
    public NineMMMarbleScript selectedMarble;
    public NineMMSlotScript selectedSlot;

    public NineMMSlotScript hoveredSlot;

    public NineMMSlotScript[] leftStartSlots;
    public NineMMSlotScript[] rightStartSlots;

    public int capturedMarblesP1;
    public int capturedMarblesP2;

    public Color highlightColorGreen;
    public Color highlightColorBlue;

    public float pieceRotateSpeed;
    public float pieceMoveSpeed;

    public List<TaflPosition> startPositions;

    NineMMPosition.NineMMMarble[,] placedMarbles;

    public Transform centerPosition;
    public GameObject marbleStandardPrefab;
    public GameObject marbleAltPrefab;

    public bool waitForSlots;

    /*
    public GameObject pawnGhostPrefab;
    public GameObject pawnAltGhostPrefab;
    public GameObject kingGhostPrefab;
    public GameObject kingAltGhostPrefab;
    */


    public List<NineMMSlotScript> slots;
    public List<NineMMMarbleScript> marblesOnBoard;

    [Header("Load System")]
    public bool boardDataCleared;
    public bool startPiecesGenerated;
    public int dataX;
    public int dataY;
    public int currentStartSlotLeft;
    public int currentStartSlotRight;

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
        DelayHotseat,
        RaiseSlots,
        Player1Place,
        Player1CheckMillStart,
        Player1SelectCaptureStart,
        RaiseSlotsP1Start,
        Player2Place,
        Player2CheckMillStart,
        Player2SelectCaptureStart,
        RaiseSlotsP2Start,
        Player1Turn,
        Player1Turning,
        Player1Moving,
        Player1CheckMillMain,
        Player1SelectCaptureMain,
        Player1CheckWin,
        RaiseSlotsP1Main,
        Player2Turn,
        Player2Turning,
        Player2Moving,
        Player2CheckMillMain,
        Player2SelectCaptureMain,
        Player2CheckWin,
        RaiseSlotsP2Main,
        Player1Win,
        Player2Win,
        DelayEndTurnPlayer1,
        DelayEndTurnPlayer2
    }

    public enum Side
    {
        First,
        Second
    }
    // Start is called before the first frame update
    void Start()
    {
        exposedArray = new List<string>();
        placedMarbles = new NineMMPosition.NineMMMarble[7, 7];
        marblesToPlaceP1 = 9;
        marblesToPlaceP2 = 9;

        hotseatReady = true;
        delaySwapScreen = false;
        waitForSlots = false;

        boardDataCleared = false;
        startPiecesGenerated = false;
        dataX = 0;
        dataY = 0;
        currentStartSlotLeft = 0;
        currentStartSlotRight = 0;
        capturedMarblesP1 = 9;
        capturedMarblesP2 = 9;
        canFlyP1 = false;
        canFlyP2 = false;
        if (hotseatVersion)
        {
            SetHotSeat(0, true);
        }
        currentTurn = Turn.Loading;
    }

    // Update is called once per frame
    void Update()
    {

        exposedArray = new List<string>();
        for (int x = 0; x < placedMarbles.GetLength(0); x++) 
        {
            for (int y = 0; y < placedMarbles.GetLength(1); y++) 
            {
                if (placedMarbles[x, y] != NineMMPosition.NineMMMarble.Invalid)
                {
                    exposedArray.Add(((x + 1) + ":" + (y + 1) + "/" + placedMarbles[x, y].ToString()));
                }
            }
        }

        switch (currentTurn)
        {
            case Turn.Loading:

                if (!boardDataCleared)
                {
                    if (dataX < placedMarbles.GetLength(0))
                    {
                        if (dataY < placedMarbles.GetLength(1))
                        {
                            if (dataX == 0)
                            {
                                if (dataY == 0 || dataY == 3 || dataY == 76)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else if (dataX == 1)
                            {
                                if (dataY == 1 || dataY == 3 || dataY == 5)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else if (dataX == 2)
                            {
                                if (dataY == 2 || dataY == 3 || dataY == 4)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else if (dataX == 3)
                            {
                                if (dataY == 0 || dataY == 1 || dataY == 2 || dataY == 4 || dataY == 5 || dataY == 6)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else if (dataX == 4)
                            {
                                if (dataY == 2 || dataY == 3 || dataY == 4)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else if (dataX == 5)
                            {
                                if (dataY == 1 || dataY == 3 || dataY == 5)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else if (dataX == 6)
                            {
                                if (dataY == 0 || dataY == 3 || dataY == 6)
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.None;
                                    dataY++;
                                }
                                else
                                {
                                    placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                    dataY++;
                                }
                            }
                            else
                            {
                                placedMarbles[dataX, dataY] = NineMMPosition.NineMMMarble.Invalid;
                                dataY++;
                            }
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
                    if (currentStartSlotLeft < leftStartSlots.Length)
                    {
                        GameObject obj = Instantiate(marbleStandardPrefab, leftStartSlots[currentStartSlotLeft].transform.position, Quaternion.identity, centerPosition);
                        NineMMMarbleScript marbleScript = obj.GetComponent<NineMMMarbleScript>();
                        leftStartSlots[currentStartSlotLeft].occupying = marbleScript;
                        marbleScript.rend.enabled = false;
                        marbleScript.rend.gameObject.GetComponent<Collider>().enabled = false;
                        /*
                         Code for filling tile on player select


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
                        */
                        currentStartSlotLeft++;
                    }
                    else if (currentStartSlotRight < rightStartSlots.Length)
                    {
                        GameObject obj = Instantiate(marbleAltPrefab, rightStartSlots[currentStartSlotRight].transform.position, Quaternion.identity, centerPosition);
                        NineMMMarbleScript marbleScript = obj.GetComponent<NineMMMarbleScript>();
                        rightStartSlots[currentStartSlotRight].occupying = marbleScript;
                        marbleScript.rend.enabled = false;
                        marbleScript.rend.gameObject.GetComponent<Collider>().enabled = false;

                        currentStartSlotRight++;
                    }
                    else
                    {
                        startPiecesGenerated = true;
                    }
                }
                else
                {
                    player1Side = Side.First;
                    player2Side = Side.Second;
                    waitForSlots = true;
                    currentTurn = Turn.RaiseSlots;

                    if (hotseatVersion)
                    {
                        SetHotSeat(1, false);
                    }
                    else
                    {
                        SetSelectableSlots(true);
                    }

                }
                break;
            case Turn.RaiseSlots:
                if (!waitForSlots)
                {
                    LockSlots(false);
                    currentTurn = Turn.Player1Place;
                }
                else
                {
                    if (hotseatVersion)
                    {
                        if (hotseatReady)
                        {
                            if (CheckSlotShifts())
                            {
                                waitForSlots = false;
                            }

                        }
                    }
                    else
                    {
                        if (CheckSlotShifts())
                        {
                            waitForSlots = false;
                        }
                    }
                }
                break;
            case Turn.Player1Turning:
                /*
                Vector3 targetDirection1 = selectedTile.transform.position - selectedPiece.transform.position;
                targetDirection1.y = selectedPiece.transform.position.y;
                float rotStep1 = pieceRotateSpeed * Time.deltaTime;
                selectedPiece.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(selectedPiece.transform.forward, targetDirection1, rotStep1, 0.0f));

                Vector3 pieceForward1 = selectedPiece.transform.forward;
                float angle1 = Vector3.SignedAngle(pieceForward1, targetDirection1, Vector3.up);
                int angleOfAccuracy1 = 2;

                if (angle1 <= angleOfAccuracy1 && angle1 >= -angleOfAccuracy1)
                {
                    currentTurn = Turn.Player1Moving;
                }
                */
                break;
            case Turn.Player1Moving:
                /*
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
                */
                break;
            case Turn.Player2Turning:
                /*
                Vector3 targetDirection2 = selectedTile.transform.position - selectedPiece.transform.position;
                targetDirection2.y = selectedPiece.transform.position.y;
                float rotStep2 = pieceRotateSpeed * Time.deltaTime;
                selectedPiece.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(selectedPiece.transform.forward, targetDirection2, rotStep2, 0.0f));

                Vector3 pieceForward2 = selectedPiece.transform.forward;
                float angle2 = Vector3.SignedAngle(pieceForward2, targetDirection2, Vector3.up);
                int angleOfAccuracy2 = 2;

                if (angle2 <= angleOfAccuracy2 && angle2 >= -angleOfAccuracy2)
                {
                    currentTurn = Turn.Player2Moving;
                }
                */
                break;
            case Turn.Player2Moving:
                /*
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
                */
                break;
        }
    }

    void SetSelectableMarbles(int which, bool includeMills)
    {
        bool reallyIncludeMills = includeMills;
        if (!includeMills)
        {
            reallyIncludeMills = !CheckForNonMills(which);
        }

        foreach (NineMMSlotScript slot in slots)
        {
            if (slot.occupying != null)
            {
                slot.occupying.rend.enabled = false;
                slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
        switch (which)
        {
            case 1:
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying != null)
                    {
                        if (slot.occupying.type == NineMMPosition.NineMMMarble.FirstMarble)
                        {
                            if (!reallyIncludeMills)
                            {
                                bool isInMill = CheckMillContains(slot.occupying, 1);

                                if (!isInMill)
                                {
                                    slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = true;
                                }
                            }
                            else
                            {
                                slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = true;
                            }
                        }
                    }
                }
                break;
            case 2:
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying != null)
                    {
                        if (slot.occupying.type == NineMMPosition.NineMMMarble.SecondMarble)
                        {
                            if (!reallyIncludeMills)
                            {
                                bool isInMill = CheckMillContains(slot.occupying, 2);
                                if (!isInMill)
                                {
                                    slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = true;
                                }
                            }
                            else
                            {
                                slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = true;
                            }
                        }
                    }
                }
                break;
            case 3:
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying != null)
                    {
                        slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = false;
                    }
                }
                break;
        }
    }
    bool CheckForNonMills(int player)
    {
        bool isNonMills = false;
        NineMMPosition.NineMMMarble type = NineMMPosition.NineMMMarble.None;
        if (player == 1)
        {
            type = NineMMPosition.NineMMMarble.FirstMarble;
            foreach (NineMMSlotScript slot in slots)
            {
                if (slot.occupying != null)
                {
                    if (slot.occupying.type == type)
                    {
                        bool isinMill = false;

                        if (CheckMillContains(slot.occupying, 1))
                        {
                            isinMill = true;
                        }                   

                        if (!isinMill)
                        {
                            isNonMills = true;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            type = NineMMPosition.NineMMMarble.SecondMarble;
            foreach (NineMMSlotScript slot in slots)
            {
                if (slot.occupying != null)
                {
                    if (slot.occupying.type == type)
                    {
                        bool isinMill = false;

                        if (CheckMillContains(slot.occupying, 2))
                        {
                            isinMill = true;
                        }                      

                        if (!isinMill)
                        {
                            isNonMills = true;
                            break;
                        }
                    }
                }
            }
        }
        return isNonMills;
    }
    void SetSelectableSlots(bool allAvailable)
    {
        if (allAvailable)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].occupying == null)
                {
                    slots[i].Activate(false);
                }
                else
                {
                    slots[i].Deactivate();
                }
            }
        }
        else
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Deactivate();

            }
        }
        waitForSlots = true;
    }
    void LockSlots(bool on)
    {
        if (on)
        {
            foreach (NineMMSlotScript slot in slots)
            {
                slot.col.enabled = false;
            }
        }
        else
        {
            foreach (NineMMSlotScript slot in slots)
            {
                slot.col.enabled = true; ;
            }
        }
    }
    bool CheckSlotShifts()
    {
        bool slotsReady = true;

        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].finishedShifting)
            {
                slotsReady = false;
                break;
            }
        }

        return slotsReady;
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
        LockSlots(false);
        if (currentTurn != Turn.Player1Turn && currentTurn != Turn.Player2Turn) {
            SetSelectableSlots(true);
        }
        
    }
    IEnumerator DelayHotseat(int player, int which)
    {
        print(player + ":" + which);
        yield return new WaitForSeconds(2f);

        delaySwapScreen = false;
        if (player == 1)
        {
            switch (which)
            {
                case 1:
                    SetHotSeat(1, false);
                    SetSelectableSlots(true);
                    LockSlots(false);
                    currentTurn = Turn.Player1Place;
                    break;
                case 2:
                    SetHotSeat(1, false);
                    SetSelectableSlots(false);
                    SetSelectableMarbles(1, true);
                    currentTurn = Turn.Player1Turn;
                    break;
            }
        }
        else if (player == 2)
        {
            switch (which)
            {
                case 1:
                    SetHotSeat(2, false);
                    SetSelectableSlots(true);
                    LockSlots(false);
                    currentTurn = Turn.Player2Place;
                    break;
                case 2:
                    SetHotSeat(2, false);
                    SetSelectableSlots(false);
                    SetSelectableMarbles(2, true);
                    currentTurn = Turn.Player2Turn;
                    break;
            }
        }

        yield return null;
    }
    IEnumerator LetSlotsShift()
    {
        yield return new WaitForSeconds(2f);
        waitForSlots = false;
        yield return null;
    }
    public void RestartGame()
    {
        foreach (NineMMSlotScript slot in slots)
        {
            if (slot.occupying != null)
            {
                Destroy(slot.occupying.gameObject);
            }
            slot.occupying = null;
        }
        foreach (NineMMSlotScript slot in leftStartSlots)
        {
            if (slot.occupying != null)
            {
                Destroy(slot.occupying.gameObject);
            }
            slot.occupying = null;
        }
        foreach (NineMMSlotScript slot in rightStartSlots)
        {
            if (slot.occupying != null)
            {
                Destroy(slot.occupying.gameObject);
            }
            slot.occupying = null;
        }
        exposedArray = new List<string>();
        placedMarbles = new NineMMPosition.NineMMMarble[7, 7];

        delaySwapScreen = false;

        boardDataCleared = false;
        startPiecesGenerated = false;

        dataX = 0;
        dataY = 0;
        currentStartSlotLeft = 0;
        currentStartSlotRight = 0;
        SetHotSeat(0, true);
        currentTurn = Turn.Loading;
    }
    public void PlaceMarble(NineMMSlotScript slot, int player)
    {
        if (player == 1)
        {
            NineMMMarbleScript marbleScript = leftStartSlots[marblesToPlaceP1 - 1].occupying;

            leftStartSlots[marblesToPlaceP1 - 1].occupying = null;

            slot.occupying = marbleScript;
            marbleScript.x = slot.x;
            marbleScript.y = slot.y;
            placedMarbles[slot.x - 1, slot.y - 1] = NineMMPosition.NineMMMarble.FirstMarble;
            slot.Deactivate();
            marbleScript.transform.position = slot.marblePos.position;
            marblesOnBoard.Add(marbleScript);
            marblesToPlaceP1--;
            if (CheckForMill(slot, 1, false))
            {
                SetSelectableSlots(false);
                SetSelectableMarbles(2, false);
                currentTurn = Turn.Player1SelectCaptureStart;
            }
            else
            {
                if (marblesToPlaceP2 > 0)
                {
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        LockSlots(true);
                        currentTurn = Turn.DelayHotseat;
                        StartCoroutine(DelayHotseat(2, 1));
                        return;
                    }
                    else
                    {
                        SetSelectableSlots(true);
                        currentTurn = Turn.Player2Place;
                        return;
                    }
                }
                else if (marblesToPlaceP1 > 0)
                {
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        LockSlots(true);
                        currentTurn = Turn.DelayHotseat;
                        StartCoroutine(DelayHotseat(1, 1));
                        return;
                    }
                    else
                    {
                        SetSelectableSlots(true);
                        currentTurn = Turn.Player1Place;
                        return;
                    }
                }
                else
                {
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        LockSlots(true);
                        currentTurn = Turn.DelayHotseat;
                        StartCoroutine(DelayHotseat(1, 2));
                        return;
                    }
                    SetSelectableSlots(false);
                    SetSelectableMarbles(1, true);
                    currentTurn = Turn.Player1Turn;
                    return;
                }
            }
            return;
        }
        if (player == 2)
        {
            NineMMMarbleScript marbleScript = rightStartSlots[marblesToPlaceP2 - 1].occupying;

            rightStartSlots[marblesToPlaceP2 - 1].occupying = null;

            slot.occupying = marbleScript;
            marbleScript.x = slot.x;
            marbleScript.y = slot.y;
            placedMarbles[slot.x - 1, slot.y - 1] = NineMMPosition.NineMMMarble.SecondMarble;
            slot.Deactivate();
            marbleScript.transform.position = slot.marblePos.position;
            marblesOnBoard.Add(marbleScript);
            marblesToPlaceP2--;
            if (CheckForMill(slot, 2, false))
            {
                SetSelectableSlots(false);
                SetSelectableMarbles(1, false);
                currentTurn = Turn.Player2SelectCaptureStart;
            }
            else
            {
                if (marblesToPlaceP1 > 0)
                {
                    SetSelectableSlots(true);
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        LockSlots(true);
                        currentTurn = Turn.DelayHotseat;
                        StartCoroutine(DelayHotseat(1, 1));
                        return;
                    }
                    else
                    {
                        currentTurn = Turn.Player1Place;
                        return;
                    }
                }
                else if (marblesToPlaceP2 > 0)
                {
                    SetSelectableSlots(true);
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        LockSlots(true);
                        currentTurn = Turn.DelayHotseat;
                        StartCoroutine(DelayHotseat(2, 1));
                        return;
                    }
                    else
                    {
                        currentTurn = Turn.Player2Place;
                        return;
                    }
                }
                else
                {
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        LockSlots(true);
                        currentTurn = Turn.DelayHotseat;
                        StartCoroutine(DelayHotseat(1, 2));
                        return;
                    }
                    SetSelectableSlots(false);
                    SetSelectableMarbles(1, true);
                    currentTurn = Turn.Player1Turn;
                    return;
                }
            }
            return;
        }
    }
    public void MoveMarble(NineMMSlotScript slot, int player)
    {
        if (player == 1)
        {
            NineMMMarbleScript marbleScript = selectedMarble;

            slot.occupying = marbleScript;
            marbleScript.x = slot.x;
            marbleScript.y = slot.y;
            placedMarbles[slot.x - 1, slot.y - 1] = NineMMPosition.NineMMMarble.FirstMarble;
            slot.Deactivate();
            marbleScript.transform.position = slot.marblePos.position;
            marblesToPlaceP1--;
            if (CheckForMill(slot, 1, true))
            {
                SetSelectableSlots(false);
                SetSelectableMarbles(2, false);
                selectedMarble = null;
                selectedSlot = null;
                currentTurn = Turn.Player1SelectCaptureMain;
            }
            else
            {
                selectedMarble = null;
                selectedSlot = null;
                if (hotseatVersion)
                {
                    delaySwapScreen = true;
                    LockSlots(true);
                    currentTurn = Turn.DelayHotseat;
                    StartCoroutine(DelayHotseat(2, 2));
                    return;
                }
                SetSelectableSlots(false);
                SetSelectableMarbles(2, true);
                currentTurn = Turn.Player2Turn;
                return;

            }
            return;
        }
        if (player == 2)
        {
            NineMMMarbleScript marbleScript = selectedMarble;

            slot.occupying = marbleScript;
            marbleScript.x = slot.x;
            marbleScript.y = slot.y;
            placedMarbles[slot.x - 1, slot.y - 1] = NineMMPosition.NineMMMarble.SecondMarble;
            slot.Deactivate();
            marbleScript.transform.position = slot.marblePos.position;
            marblesToPlaceP2--;
            if (CheckForMill(slot, 2, true))
            {
                SetSelectableSlots(false);
                SetSelectableMarbles(1, false);
                selectedMarble = null;
                selectedSlot = null;
                currentTurn = Turn.Player2SelectCaptureMain;
            }
            else
            {
                selectedMarble = null;
                selectedSlot = null;
                if (hotseatVersion)
                {
                    delaySwapScreen = true;
                    LockSlots(true);
                    currentTurn = Turn.DelayHotseat;
                    StartCoroutine(DelayHotseat(1, 2));
                    return;
                }
                SetSelectableSlots(false);
                SetSelectableMarbles(1, true);
                currentTurn = Turn.Player1Turn;
                return;
            }
            return;
        }
    }
    bool CheckForMill(NineMMSlotScript slot, int player, bool main)
    {
        ScanForMills();
        bool marbleInMill = CheckMillContains(slot.occupying, player);

        /*
        bool foundMill = false;
        NineMMPosition.NineMMMarble freindlyMarble;
        Mill tempMill = new Mill(null, null, null);
        if (player == 1)
        {
            freindlyMarble = NineMMPosition.NineMMMarble.FirstMarble;
        }
        else
        {
            freindlyMarble = NineMMPosition.NineMMMarble.SecondMarble;
        }
        MillCheck horizontalSlots = FindHorizontalSlots(slot.x, slot.y);

        MillCheck verticalSlots = FindVerticalSlots(slot.x, slot.y);
        
        if (placedMarbles[horizontalSlots.x1 - 1, horizontalSlots.y1 - 1] == freindlyMarble && placedMarbles[horizontalSlots.x2 - 1, horizontalSlots.y2 - 1] == freindlyMarble)
        {
            if (player == 1)
            {
                AddToMill(1, slot.occupying);
            }
            else
            {
                AddToMill(2, slot.occupying);
            }
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].x == horizontalSlots.x1 && slots[i].y == horizontalSlots.y1)
                {
                    if (player == 1)
                    {
                        AddToMill(1, slots[i].occupying);
                    }
                    else
                    {
                        AddToMill(2, slots[i].occupying);
                    }
                }
                if (slots[i].x == horizontalSlots.x2 && slots[i].y == horizontalSlots.y2)
                {
                    if (player == 1)
                    {
                        AddToMill(1, slots[i].occupying);
                    }
                    else
                    {
                        AddToMill(2, slots[i].occupying);
                    }
                }
            }
            print("Mills present = " + foundMill.ToString());
            foundMill = true;
        }
        if (placedMarbles[verticalSlots.x1 - 1, verticalSlots.y1 - 1] == freindlyMarble && placedMarbles[verticalSlots.x2 - 1, verticalSlots.y2 - 1] == freindlyMarble)
        {
            if (player == 1)
            {
                AddToMill(1, slot.occupying);
            }
            else
            {
                AddToMill(2, slot.occupying);
            }
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].x == verticalSlots.x1 && slots[i].y == verticalSlots.y1)
                {
                    if (player == 1)
                    {
                        AddToMill(1, slots[i].occupying);
                    }
                    else
                    {
                        AddToMill(2, slots[i].occupying);
                    }
                }
                if (slots[i].x == verticalSlots.x2 && slots[i].y == verticalSlots.y2)
                {
                    if (player == 1)
                    {
                        AddToMill(1, slots[i].occupying);
                    }
                    else
                    {
                        AddToMill(2, slots[i].occupying);
                    }
                }
            }
            foundMill = true;
        }
        */
        return marbleInMill;

    }
    MillCheck FindHorizontalSlots(int x, int y)
    {
        MillCheck tempQuat = new MillCheck(0, 0, 0, 0);
        if (y == 1)
        {
            if (x == 1)
            {
                tempQuat = new MillCheck(4, y, 7, y);
            }
            else if (x == 4)
            {
                tempQuat = new MillCheck(1, y, 7, y);
            }
            else if (x == 7)
            {
                tempQuat = new MillCheck(1, y, 4, y);
            }
        }
        else if (y == 2)
        {
            if (x == 2)
            {
                tempQuat = new MillCheck(4, y, 6, y);
            }
            else if (x == 4)
            {
                tempQuat = new MillCheck(2, y, 6, y);
            }
            else if (x == 6)
            {
                tempQuat = new MillCheck(2, y, 4, y);
            }
        }
        else if (y == 3)
        {
            if (x == 3)
            {
                tempQuat = new MillCheck(4, y, 5, y);
            }
            else if (x == 4)
            {
                tempQuat = new MillCheck(3, y, 5, y);
            }
            else if (x == 5)
            {
                tempQuat = new MillCheck(3, y, 4, y);
            }
        }
        else if (y == 4)
        {
            if (x == 1)
            {
                tempQuat = new MillCheck(2, y, 3, y);
            }
            else if (x == 2)
            {
                tempQuat = new MillCheck(1, y, 3, y);
            }
            else if (x == 3)
            {
                tempQuat = new MillCheck(1, y, 2, y);
            }
            else if (x == 5)
            {
                tempQuat = new MillCheck(6, y, 7, y);
            }
            else if (x == 6)
            {
                tempQuat = new MillCheck(5, y, 7, y);
            }
            else if (x == 7)
            {
                tempQuat = new MillCheck(5, y, 6, y);
            }
        }
        else if (y == 5)
        {
            if (x == 3)
            {
                tempQuat = new MillCheck(4, y, 5, y);
            }
            else if (x == 4)
            {
                tempQuat = new MillCheck(3, y, 5, y);
            }
            else if (x == 5)
            {
                tempQuat = new MillCheck(3, y, 4, y);
            }
        }
        else if (y == 6)
        {
            if (x == 2)
            {
                tempQuat = new MillCheck(4, y, 6, y);
            }
            else if (x == 4)
            {
                tempQuat = new MillCheck(2, y, 6, y);
            }
            else if (x == 6)
            {
                tempQuat = new MillCheck(2, y, 4, y);
            }
        }
        else if (y == 7)
        {
            if (x == 1)
            {
                tempQuat = new MillCheck(4, y, 7, y);
            }
            else if (x == 4)
            {
                tempQuat = new MillCheck(1, y, 7, y);
            }
            else if (x == 7)
            {
                tempQuat = new MillCheck(1, y, 4, y);
            }
        }
        return tempQuat;
    }
    MillCheck FindVerticalSlots(int x, int y)
    {
        MillCheck tempQuat = new MillCheck(0, 0, 0, 0);
        if (x == 1)
        {
            if (y == 1)
            {
                tempQuat = new MillCheck(x, 4, x, 7);
            }
            else if (y == 4)
            {
                tempQuat = new MillCheck(x, 1, x, 7);
            }
            else if (y == 7)
            {
                tempQuat = new MillCheck(x, 1, x, 4);
            }
        }
        else if (x == 2)
        {
            if (y == 2)
            {
                tempQuat = new MillCheck(x, 4, x, 6);
            }
            else if (y == 4)
            {
                tempQuat = new MillCheck(x, 2, x, 6);
            }
            else if (y == 6)
            {
                tempQuat = new MillCheck(x, 2, x, 4);
            }
        }
        else if (x == 3)
        {
            if (y == 3)
            {
                tempQuat = new MillCheck(x, 4, x, 5);
            }
            else if (y == 4)
            {
                tempQuat = new MillCheck(x, 3, x, 5);
            }
            else if (y == 5)
            {
                tempQuat = new MillCheck(x, 3, x, 4);
            }
        }
        else if (x == 4)
        {
            if (y == 1)
            {
                tempQuat = new MillCheck(x, 2, x, 3);
            }
            else if (y == 2)
            {
                tempQuat = new MillCheck(x, 1, x, 3);
            }
            else if (y == 3)
            {
                tempQuat = new MillCheck(x, 1, x, 2);
            }
            else if (y == 5)
            {
                tempQuat = new MillCheck(x, 6, x, 7);
            }
            else if (y == 6)
            {
                tempQuat = new MillCheck(x, 5, x, 7);
            }
            else if (y == 7)
            {
                tempQuat = new MillCheck(x, 5, x, 6);
            }
        }
        else if (x == 5)
        {
            if (y == 3)
            {
                tempQuat = new MillCheck(x, 4, x, 5);
            }
            else if (y == 4)
            {
                tempQuat = new MillCheck(x, 3, x, 5);
            }
            else if (y == 5)
            {
                tempQuat = new MillCheck(x, 3, x, 4);
            }
        }
        else if (x == 6)
        {
            if (y == 2)
            {
                tempQuat = new MillCheck(x, 4, x, 6);
            }
            else if (y == 4)
            {
                tempQuat = new MillCheck(x, 2, x, 6);
            }
            else if (y == 6)
            {
                tempQuat = new MillCheck(x, 2, x, 4);
            }
        }
        else if (x == 7)
        {
            if (y == 1)
            {
                tempQuat = new MillCheck(x, 3, x, 7);
            }
            else if (y == 4)
            {
                tempQuat = new MillCheck(x, 1, x, 7);
            }
            else if (y == 7)
            {
                tempQuat = new MillCheck(x, 1, x, 3);
            }
        }
        return tempQuat;
    }
    public void SelectCapture(NineMMMarbleScript marble, int player, bool main)
    {
        SetSelectableMarbles(3, true);
        if (player == 1)
        {
            if (marble.type == NineMMPosition.NineMMMarble.SecondMarble)
            {
                marble.transform.position = leftStartSlots[capturedMarblesP1 - 1].transform.position;
                leftStartSlots[capturedMarblesP1 - 1].occupying = marble;
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].occupying == marble)
                    {
                        slots[i].occupying = null;
                    }
                }
                for (int i = 0; i < marblesOnBoard.Count; i++) 
                {
                    if (marblesOnBoard[i] == marble) 
                    {
                        marblesOnBoard.RemoveAt(i);
                        break;
                    }
                }
                placedMarbles[marble.x - 1, marble.y - 1] = NineMMPosition.NineMMMarble.None;
                capturedMarblesP1--;
                ScanForMills();
                if (capturedMarblesP1 == 2)
                {

                    foreach (NineMMSlotScript slot in slots) 
                    {
                        if (slot.occupying != null) 
                        {
                            slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = false;
                            slot.occupying.rend.enabled = false;
                        }
                        slot.Deactivate();
                    }
                    currentPlayerDisplayText.text = "Player 1 Wins!";
                    currentTurn = Turn.Player1Win;

                    return;
                }
                else if (capturedMarblesP1 == 3)
                {
                    canFlyP2 = true;
                }
                SetSelectableSlots(false);
                if (main)
                {
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        currentTurn = Turn.DelayHotseat;
                        LockSlots(true);
                        StartCoroutine(DelayHotseat(2, 2));
                        return;
                    }
                    else
                    {
                        SetSelectableMarbles(2, true);
                        currentTurn = Turn.Player2Turn;
                        return;
                    }
                }
                else
                {
                    if (marblesToPlaceP2 > 0)
                    {
                        if (hotseatVersion)
                        {
                            delaySwapScreen = true;
                            LockSlots(true);
                            currentTurn = Turn.DelayHotseat;
                            StartCoroutine(DelayHotseat(2, 1));
                            return;
                        }
                        else
                        {
                            SetSelectableSlots(true);
                            currentTurn = Turn.Player2Place;
                            return;
                        }
                    }
                    else if (marblesToPlaceP1 > 0)
                    {
                        if (hotseatVersion)
                        {
                            delaySwapScreen = true;
                            LockSlots(true);
                            currentTurn = Turn.DelayHotseat;
                            StartCoroutine(DelayHotseat(1, 1));
                            return;
                        }
                        else
                        {
                            SetSelectableSlots(true);
                            currentTurn = Turn.Player1Place;
                            return;
                        }
                    }
                    else
                    {
                        if (hotseatVersion)
                        {
                            delaySwapScreen = true;
                            LockSlots(true);
                            currentTurn = Turn.DelayHotseat;
                            StartCoroutine(DelayHotseat(1, 2));
                            return;
                        }
                        SetSelectableSlots(false);
                        SetSelectableMarbles(1, true);
                        currentTurn = Turn.Player1Turn;
                        return;
                    }
                }
            }
        }
        if (player == 2)
        {
            if (marble.type == NineMMPosition.NineMMMarble.FirstMarble)
            {
                marble.transform.position = rightStartSlots[capturedMarblesP2 - 1].transform.position;
                rightStartSlots[capturedMarblesP2 - 1].occupying = marble;
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].occupying == marble)
                    {
                        slots[i].occupying = null;
                    }
                }
                for (int i = 0; i < marblesOnBoard.Count; i++)
                {
                    if (marblesOnBoard[i] == marble)
                    {
                        marblesOnBoard.RemoveAt(i);
                        break;
                    }
                }
                placedMarbles[marble.x - 1, marble.y - 1] = NineMMPosition.NineMMMarble.None;
                capturedMarblesP2--;
                ScanForMills();
                if (capturedMarblesP2 == 2)
                {

                    foreach (NineMMSlotScript slot in slots)
                    {
                        if (slot.occupying != null)
                        {
                            slot.occupying.rend.gameObject.GetComponent<Collider>().enabled = false;
                            slot.occupying.rend.enabled = false;
                        }
                        slot.Deactivate();
                    }
                    currentPlayerDisplayText.text = "Player 2 Wins!";
                    currentTurn = Turn.Player2Win;

                    return;
                }
                else if (capturedMarblesP2 == 3)
                {
                    canFlyP1 = true;
                }
                if (main)
                {
                    if (hotseatVersion)
                    {
                        delaySwapScreen = true;
                        currentTurn = Turn.DelayHotseat;
                        LockSlots(true);
                        StartCoroutine(DelayHotseat(1, 2));
                        return;
                    }
                    else
                    {
                        SetSelectableMarbles(1, true);
                        currentTurn = Turn.Player1Turn;
                        return;
                    }
                }
                else
                {
                    if (marblesToPlaceP1 > 0)
                    {
                        SetSelectableSlots(true);
                        if (hotseatVersion)
                        {
                            delaySwapScreen = true;
                            LockSlots(true);
                            currentTurn = Turn.DelayHotseat;
                            StartCoroutine(DelayHotseat(1, 1));
                            return;
                        }
                        else
                        {
                            currentTurn = Turn.Player1Place;
                            return;
                        }
                    }
                    else if (marblesToPlaceP2 > 0)
                    {
                        SetSelectableSlots(true);
                        if (hotseatVersion)
                        {
                            delaySwapScreen = true;
                            LockSlots(true);
                            currentTurn = Turn.DelayHotseat;
                            StartCoroutine(DelayHotseat(2, 1));
                            return;
                        }
                        else
                        {
                            currentTurn = Turn.Player2Place;
                            return;
                        }
                    }
                    else
                    {
                        if (hotseatVersion)
                        {
                            delaySwapScreen = true;
                            LockSlots(true);
                            currentTurn = Turn.DelayHotseat;
                            StartCoroutine(DelayHotseat(1, 2));
                            return;
                        }
                        SetSelectableSlots(false);
                        SetSelectableMarbles(1, true);
                        currentTurn = Turn.Player1Turn;
                        return;
                    }
                }
            }
        }
    }
    /*
    void AddToMill(int player, NineMMMarbleScript marble)
    {
        if (player == 1)
        {
            for (int i = 0; i < millsP1.Count; i++)
            {
                if (millsP1[i] == marble)
                {
                    return;
                }
            }
            millsP1.Add(marble);
            return;
        }
        if (player == 2)
        {
            for (int i = 0; i < millsP2.Count; i++)
            {
                if (millsP2[i] == marble)
                {
                    return;
                }
            }
            millsP2.Add(marble);
            return;
        }
    }
    */
    public void Player1SelectMarble(NineMMMarbleScript marble)
    {
        if (marble != null)
        {
            SelectMarble(marble, 1);
        }
    }
    public void Player2SelectMarble(NineMMMarbleScript marble)
    {
        if (marble != null)
        {
            SelectMarble(marble, 2);
        }
    }
    public void Player1SelectSlot(NineMMSlotScript slot)
    {
        if (slot != null)
        {
            SelectSlot(slot, 1);
        }
    }
    public void Player2SelectSlot(NineMMSlotScript slot)
    {
        if (slot != null)
        {
            SelectSlot(slot, 2);
        }
    }
    public void SelectMarble(NineMMMarbleScript marble, int player)
    {
        if (selectedMarble != null)
        {
            SetSelectionPieceColor(selectedMarble, false);
        }
        selectedMarble = marble;
        SetSelectionPieceColor(marble, true);
        if (player == 1 && capturedMarblesP2 <= 3)
        {
            SetSelectableSlots(true);
        }
        else if (player == 2 && capturedMarblesP1 <= 3)
        {
            SetSelectableSlots(true);
        }
        else
        {
            LegalMove(marble.x, marble.y);
        }
    }
    public void SelectSlot(NineMMSlotScript slot, int player) 
    {
        if (selectedMarble != null)
        {
            selectedSlot = slot;

            placedMarbles[selectedMarble.x - 1, selectedMarble.y - 1] = NineMMPosition.NineMMMarble.None;

            foreach (NineMMSlotScript slotScript in slots) 
            {
                if (slotScript.occupying == selectedMarble) 
                {
                    slotScript.occupying = null;
                    break;
                }
            }
            MoveMarble(selectedSlot, player);
        }
    }
    public void SetSelectionPieceColor(NineMMMarbleScript marble, bool selected)
    {
        if (selected)
        {
            marble.rend.enabled = true;
        }
        else
        {
            marble.rend.enabled = false;
        }
    }
    public void LegalMove(int x, int y)
    {
        SetSelectableSlots(false);
        if (x == 1)
        {
            if (y == 1)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 1 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 4)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 1 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 1 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 2 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 7)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 1 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
        else if (x == 2)
        {
            if (y == 2)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 2 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 4)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 1 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 2 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 2 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 3 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 6)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 2 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
        else if (x == 3)
        {
            if (y == 3)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 3 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 4)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 2 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 3 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 3 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 5)
            {

                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 3 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
        else if (x == 4)
        {
            if (y == 1)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 1 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 2)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 2 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 3)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 3 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 5 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 5)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 3 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 5 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 6)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 2 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 7)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 1 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 4 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
        else if (x == 5)
        {
            if (y == 3)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 4 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 5 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 4)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 5 && slot.y == 3)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 5 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 5)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 4 && slot.y == 5)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 5 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
        else if (x == 6)
        {
            if (y == 2)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 4 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 4)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 5 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 2)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 6)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 4 && slot.y == 6)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 6 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
        else if (x == 7)
        {
            if (y == 1)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 4 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 4)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 6 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 1)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
            else if (y == 7)
            {
                foreach (NineMMSlotScript slot in slots)
                {
                    if (slot.occupying == null)
                    {
                        if (slot.x == 4 && slot.y == 7)
                        {
                            slot.Activate(true);
                        }
                        if (slot.x == 7 && slot.y == 4)
                        {
                            slot.Activate(true);
                        }
                    }
                }
            }
        }
    }
    void ScanForMills() 
    {
        millsP1.Clear();
        millsP2.Clear();

        CreateSingleMill(0, 1, 2);
        CreateSingleMill(3, 4, 5);
        CreateSingleMill(6, 7, 8);
        CreateSingleMill(9, 10, 11);
        CreateSingleMill(12, 13, 14);
        CreateSingleMill(15, 16, 17);
        CreateSingleMill(18, 19, 20);
        CreateSingleMill(21, 22, 23);

        CreateSingleMill(0, 9, 21);
        CreateSingleMill(3, 10, 18);
        CreateSingleMill(6, 11, 15);
        CreateSingleMill(1, 4, 7);
        CreateSingleMill(16, 19, 22);
        CreateSingleMill(8, 12, 17);
        CreateSingleMill(5, 13, 20);
        CreateSingleMill(2, 14, 23);
    }
    void CreateSingleMill(int x, int y, int z) 
    {
        if (slots[x].occupying != null && slots[y].occupying != null && slots[z].occupying != null)
        {
            if (slots[x].occupying.type == NineMMPosition.NineMMMarble.FirstMarble && slots[y].occupying.type == NineMMPosition.NineMMMarble.FirstMarble && slots[z].occupying.type == NineMMPosition.NineMMMarble.FirstMarble)
            {
                millsP1.Add(new Mill(slots[x].occupying, slots[y].occupying, slots[z].occupying));
            }
            if (slots[x].occupying.type == NineMMPosition.NineMMMarble.SecondMarble && slots[y].occupying.type == NineMMPosition.NineMMMarble.SecondMarble && slots[z].occupying.type == NineMMPosition.NineMMMarble.SecondMarble)
            {
                millsP2.Add(new Mill(slots[x].occupying, slots[y].occupying, slots[z].occupying));
            }
        }
    }

    bool CheckMillContains(NineMMMarbleScript marble, int player) 
    {
        bool marbleInMill = false;

        if (player == 1)
        {
            for (int i = 0; i < millsP1.Count; i++)
            {
                if (millsP1[i].marble1 == marble || millsP1[i].marble2 == marble || millsP1[i].marble3 == marble)
                {
                    marbleInMill = true;
                    break;
                }
            }
        }
        if (player == 2) 
        {
            for (int i = 0; i < millsP2.Count; i++)
            {
                if (millsP2[i].marble1 == marble || millsP2[i].marble2 == marble || millsP2[i].marble3 == marble)
                {
                    marbleInMill = true;
                    break;
                }
            }
        }

        return marbleInMill;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerAttach : MythologyMayhem
{
    public LocalGameManager localGameManager;
    public Dimension type;
    public Level inScene;
    public CinemachineVirtualCamera vCam;

    public Camera cam;
    public LayerMask taflLayer;
    public LayerMask nmmLayer;
    public TaflMechanics tafl;
    public NineMMMechanics nmm;

    public Transform pinpoint;

    public bool tPressed;

    private void Start()
    {
        tafl = FindObjectOfType<TaflMechanics>();
        nmm = FindObjectOfType<NineMMMechanics>();
        if (tafl != null)
        {
            if (tafl.gameObject.scene != this.gameObject.scene)
            {
                tafl = null;
            }
        }
        if (nmm != null)
        {
            if (nmm.gameObject.scene != this.gameObject.scene)
            {
                nmm = null;
            }
        }
        if (tafl != null || nmm != null) 
        {
            pinpoint.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (tafl != null || nmm != null)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                tPressed = true;
                StartCoroutine(ButtonPress());
            }
        }
    }
    private void FixedUpdate()
    {
        if (tafl != null || nmm != null)
        {
            pinpoint.position = transform.position;
            pinpoint.gameObject.SetActive(false);
        }
        if (tafl != null)
        {
            bool hitTile = false;
            TaflTileScript tileScript = null;
            RaycastHit hit;

            Debug.DrawRay(cam.transform.position, cam.transform.forward * 10, Color.blue, 0.1f);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 10, taflLayer))
            {
                pinpoint.position = hit.point;
                pinpoint.gameObject.SetActive(true);
                if (hit.collider.tag == "TaflPiece")
                {
                    if (tafl != null)
                    {
                        if (tafl.currentTurn == TaflMechanics.Turn.Player1Turn)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                TaflPieceScript taflPiece = hit.collider.gameObject.GetComponent<TaflPieceScript>();
                                if (taflPiece != null)
                                {
                                    tafl.Player1SelectPiece(taflPiece);
                                }
                            }
                        }
                        if (tafl.currentTurn == TaflMechanics.Turn.Player2Turn)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                TaflPieceScript taflPiece = hit.collider.gameObject.GetComponent<TaflPieceScript>();
                                if (taflPiece != null)
                                {
                                    tafl.Player2SelectPiece(taflPiece);
                                }
                            }
                        }
                    }
                }
                if (hit.collider.tag == "TaflTile")
                {
                    hitTile = true;

                    if (tafl != null)
                    {
                        if (tafl.currentTurn == TaflMechanics.Turn.Player1Turn)
                        {
                            tileScript = hit.collider.gameObject.GetComponent<TaflTileScript>();
                            if (tileScript != null && tafl.selectedPiece != null)
                            {
                                if (tPressed)
                                {
                                    tPressed = false;
                                    tafl.Player1SelectTile(tileScript);
                                    hitTile = false;
                                }
                            }
                        }
                        if (tafl.currentTurn == TaflMechanics.Turn.Player2Turn)
                        {
                            tileScript = hit.collider.gameObject.GetComponent<TaflTileScript>();
                            if (tileScript != null && tafl.selectedPiece != null)
                            {
                                if (tPressed)
                                {
                                    tPressed = false;
                                    tafl.Player2SelectTile(tileScript);
                                    hitTile = false;
                                }
                            }
                        }
                    }
                }
            }

            tafl.RayHitTile(hitTile, tileScript);
        }
        if (nmm != null)
        {
            NineMMSlotScript slotScript = null;
            RaycastHit hit;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10, nmmLayer))
            {
                pinpoint.position = hit.point;
                pinpoint.gameObject.SetActive(true);
                if (hit.collider.tag == "9MMMarble")
                {
                    if (nmm != null)
                    {
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player1SelectCaptureStart)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                                if (marbleScript != null)
                                {
                                    print("Selected " + marbleScript.gameObject.name + " to capture.");
                                    nmm.SelectCapture(marbleScript, 1, false);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player2SelectCaptureStart)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                                if (marbleScript != null)
                                {
                                    nmm.SelectCapture(marbleScript, 2, false);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player1Turn)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                                if (marbleScript != null)
                                {
                                    nmm.Player1SelectMarble(marbleScript);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player2Turn)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                                if (marbleScript != null)
                                {
                                    nmm.Player2SelectMarble(marbleScript);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player1SelectCaptureMain)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                                if (marbleScript != null)
                                {
                                    nmm.SelectCapture(marbleScript, 1, true);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player2SelectCaptureMain)
                        {
                            if (tPressed)
                            {
                                tPressed = false;
                                NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                                if (marbleScript != null)
                                {
                                    nmm.SelectCapture(marbleScript, 2, true);
                                }
                            }
                        }
                    }
                }
                if (hit.collider.tag == "9MMSlot")
                {

                    if (nmm != null)
                    {
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player1Place)
                        {
                            slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                            if (slotScript != null)
                            {
                                if (tPressed)
                                {
                                    tPressed = false;
                                    nmm.PlaceMarble(slotScript, 1);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player2Place)
                        {
                            slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                            if (slotScript != null)
                            {
                                if (tPressed)
                                {
                                    tPressed = false;
                                    nmm.PlaceMarble(slotScript, 2);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player1Turn)
                        {
                            slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                            if (slotScript != null && nmm.selectedMarble != null)
                            {
                                if (tPressed)
                                {
                                    tPressed = false;
                                    nmm.Player1SelectSlot(slotScript);
                                }
                            }
                        }
                        if (nmm.currentTurn == NineMMMechanics.Turn.Player2Turn)
                        {
                            slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                            if (slotScript != null && nmm.selectedMarble != null)
                            {
                                if (tPressed)
                                {
                                    tPressed = false;
                                    nmm.Player2SelectSlot(slotScript);
                                }
                            }
                        }
                    }

                }
            }
        }
    }


    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(0.25f);
        tPressed = false;
        yield return null;
    }

}

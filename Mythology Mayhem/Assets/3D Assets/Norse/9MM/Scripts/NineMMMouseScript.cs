using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineMMMouseScript : MonoBehaviour
{
    public Camera cam;
    public LayerMask NineMMLayer;
    public NineMMMechanics nmm;

    public Transform pinpoint;

    public Transform camAnchor;
    public float anchorSpeed;
    public Transform[] anchorPositions;
    public bool mouseClicked;

    // Start is called before the first frame update
    void Start()
    {
        nmm = FindObjectOfType<NineMMMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        bool canControl = false;

        if (nmm != null)
        {
            if (nmm.hotseatVersion && nmm.hotseatReady)
            {
                canControl = true;
            }
            else if (!nmm.hotseatVersion)
            {
                canControl = true;
            }
        }
        if (canControl)
        {
            float step = anchorSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[4].transform.rotation, step);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[5].transform.rotation, step);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[7].transform.rotation, step);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[8].transform.rotation, step);
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[3].transform.rotation, step);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[1].transform.rotation, step);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[2].transform.rotation, step);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[6].transform.rotation, step);
                }
                else
                {
                    camAnchor.transform.rotation = Quaternion.RotateTowards(camAnchor.transform.rotation, anchorPositions[0].transform.rotation, step);
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                mouseClicked = true;
                StartCoroutine(ButtonPress());
            }
        }
    }

    private void FixedUpdate()
    {

        bool hitTile = false;
        NineMMSlotScript slotScript = null;
        bool hitPiece = false;
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        pinpoint.position = transform.position;
        if (Physics.Raycast(ray, out hit, 10, NineMMLayer))
        {
            pinpoint.position = hit.point;
            if (hit.collider.tag == "9MMMarble")
            {

                hitPiece = true;
                if (nmm != null)
                {
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player1SelectCaptureStart)
                    {
                        if (mouseClicked)
                        {
                            mouseClicked = false;
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
                        if (mouseClicked)
                        {
                            mouseClicked = false;
                            NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                            if (marbleScript != null)
                            {
                                nmm.SelectCapture(marbleScript, 2, false);
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player1Turn)
                    {
                        if (mouseClicked)
                        {
                            mouseClicked = false;
                            NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                            if (marbleScript != null)
                            {
                                nmm.Player1SelectMarble(marbleScript);
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player2Turn)
                    {
                        if (mouseClicked)
                        {
                            mouseClicked = false;
                            NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                            if (marbleScript != null)
                            {
                                nmm.Player2SelectMarble(marbleScript);
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player1SelectCaptureMain)
                    {
                        if (mouseClicked)
                        {
                            mouseClicked = false;
                            NineMMMarbleScript marbleScript = hit.collider.transform.parent.gameObject.GetComponent<NineMMMarbleScript>();
                            if (marbleScript != null)
                            {
                                nmm.SelectCapture(marbleScript, 1, true);
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player2SelectCaptureMain)
                    {
                        if (mouseClicked)
                        {
                            mouseClicked = false;
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
                hitTile = true;

                if (nmm != null)
                {
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player1Place)
                    {
                        slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                        if (slotScript != null)
                        {
                            if (mouseClicked)
                            {
                                mouseClicked = false;
                                nmm.PlaceMarble(slotScript, 1);
                                hitTile = false;
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player2Place)
                    {
                        slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                        if (slotScript != null)
                        {
                            if (mouseClicked)
                            {
                                mouseClicked = false;
                                nmm.PlaceMarble(slotScript, 2);
                                hitTile = false;
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player1Turn)
                    {
                        slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                        if (slotScript != null && nmm.selectedMarble != null)
                        {
                            if (mouseClicked)
                            {
                                mouseClicked = false;
                                nmm.Player1SelectSlot(slotScript);
                                hitTile = false;
                            }
                        }
                    }
                    if (nmm.currentTurn == NineMMMechanics.Turn.Player2Turn)
                    {
                        slotScript = hit.collider.gameObject.GetComponent<NineMMSlotScript>();
                        if (slotScript != null && nmm.selectedMarble != null)
                        {
                            if (mouseClicked)
                            {
                                mouseClicked = false;
                                nmm.Player2SelectSlot(slotScript);
                                hitTile = false;
                            }
                        }
                    }
                }

            }

           // nmm.RayHitTile(hitTile, slotScript);
        }
    }


    IEnumerator ButtonPress()
    {
        yield return new WaitForSeconds(0.1f);
        mouseClicked = false;
        yield return null;
    }

    public void ClearMousePress()
    {
        mouseClicked = false;
    }
}

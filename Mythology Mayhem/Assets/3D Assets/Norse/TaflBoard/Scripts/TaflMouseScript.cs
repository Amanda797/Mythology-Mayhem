using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflMouseScript : MonoBehaviour
{
    public Camera cam;
    public LayerMask taflLayer;
    public TaflMechanics tafl;

    public Transform pinpoint;

    public Transform camAnchor;
    public float anchorSpeed;
    public Transform[] anchorPositions;
    public bool mouseClicked;

    // Start is called before the first frame update
    void Start()
    {
        tafl = FindObjectOfType<TaflMechanics>();
    }

    // Update is called once per frame
    void Update()
    {
        bool canControl = false;

        if (tafl != null)
        {
            if (tafl.hotseatVersion && tafl.hotseatReady)
            {
                canControl = true;
            }
            else if (!tafl.hotseatVersion)
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
            TaflTileScript tileScript = null;
            RaycastHit hit;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            pinpoint.position = transform.position;
            if (Physics.Raycast(ray, out hit, 10, taflLayer))
            {
                pinpoint.position = hit.point;
                if (hit.collider.tag == "TaflPiece")
                {
                    if (tafl != null)
                    {
                        if (tafl.currentTurn == TaflMechanics.Turn.Player1Turn)
                        {
                            if (mouseClicked)
                            {
                                mouseClicked = false;
                                TaflPieceScript taflPiece = hit.collider.gameObject.GetComponent<TaflPieceScript>();
                                if (taflPiece != null)
                                {
                                    tafl.Player1SelectPiece(taflPiece);
                                }
                            }
                        }
                        if (tafl.currentTurn == TaflMechanics.Turn.Player2Turn)
                        {
                            if (mouseClicked)
                            {
                                mouseClicked = false;
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
                            if (mouseClicked)
                            {
                                mouseClicked = false;
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
                            if (mouseClicked)
                            {
                                mouseClicked = false;
                                tafl.Player2SelectTile(tileScript);
                                hitTile = false;
                            }
                        }
                    }
                }

            }

            tafl.RayHitTile(hitTile, tileScript);
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

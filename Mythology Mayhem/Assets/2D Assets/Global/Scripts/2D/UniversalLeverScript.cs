using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalLeverScript : MonoBehaviour
{

    public Animator leverAnim;
    public LeverPosition currentPosition;
    public bool canActivate;
    public bool inRange;
    public enum LeverPosition 
    { 
        Up,
        Down
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = LeverPosition.Up;
        leverAnim.SetBool("Up", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchState();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
        }

    }

    void SwitchState() 
    {
        if (currentPosition == LeverPosition.Up)
        {
            currentPosition = LeverPosition.Down;
            leverAnim.SetBool("Up", false);
        }
        else 
        {
            currentPosition = LeverPosition.Up;
            leverAnim.SetBool("Up", true);
        }
    }
}

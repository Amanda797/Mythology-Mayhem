using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{

    public Animator pillarAnim;
    public PillarPosition currentPosition;
    public bool canActivate;
    public bool inRange;
    public enum PillarPosition
    {
        Up,
        Down
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = PillarPosition.Up;
        pillarAnim.SetBool("Up", true);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SwitchState()
    {
        if (currentPosition == PillarPosition.Up)
        {
            currentPosition = PillarPosition.Down;
            pillarAnim.SetBool("Up", false);
        }
        else
        {
            currentPosition = PillarPosition.Up;
            pillarAnim.SetBool("Up", true);
        }
    }
}

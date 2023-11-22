using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineMMAnimScript : MonoBehaviour
{
    public int currentMove;
    public Animator anim;
    public Animator vikingAnim;

    public GameObject[] marblePickups;
    // Start is called before the first frame update
    void Start()
    {
        currentMove = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMove(int move)
    {
        currentMove = move;
    }

    public void RunMove()
    {
        if (anim != null)
        {
            anim.SetInteger("Move", currentMove);
        }
        switch (currentMove)
        {
            case 0:
                vikingAnim.SetBool("Playing", true);
                break;
            case 1:
                vikingAnim.SetTrigger("LowerHead");
                break;
            case 2:
                vikingAnim.SetTrigger("Cheer");
                break;
            case 3:
                vikingAnim.SetTrigger("LowerHead");
                break;
            case 4:
                vikingAnim.SetTrigger("Move");
                break;
            case 5:
                vikingAnim.SetTrigger("Loss");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RunMove();
            ActivateNextPickup(currentMove + 1);
        }
    }

    void ActivateNextPickup(int next)
    {
        if (next <= marblePickups.Length)
        {
            for (int i = 0; i < marblePickups.Length; i++)
            {
                if (marblePickups[i] != null)
                {
                    if (i == next - 1)
                    {
                        marblePickups[i].SetActive(true);
                    }
                    else
                    {
                        marblePickups[i].SetActive(false);
                    }
                }
            }
        }
    }
}

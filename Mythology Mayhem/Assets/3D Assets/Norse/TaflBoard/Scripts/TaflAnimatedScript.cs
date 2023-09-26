using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflAnimatedScript : MonoBehaviour
{

    public int currentMove;
    public Animator anim;
    public Animator vikingAnim;

    public GameObject[] taflPickups;
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
        anim.SetInteger("Move", currentMove);
        switch (currentMove) 
        {
            case 0:
                vikingAnim.SetBool("Playing", true);
                break;
            case 1:
                vikingAnim.SetTrigger("Shocked");
                break;
            case 2:
                vikingAnim.SetTrigger("Cheer");
                break;
            case 3:
                vikingAnim.SetTrigger("Shocked");
                break;
            case 4:
                vikingAnim.SetTrigger("Move");
                break;
            case 5:
                vikingAnim.SetTrigger("Upset2");
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
        if (next <= taflPickups.Length)
        {
            for (int i = 0; i < taflPickups.Length; i++)
            {
                if (taflPickups[i] != null)
                {
                    if (i == next - 1)
                    {
                        taflPickups[i].SetActive(true);
                    }
                    else
                    {
                        taflPickups[i].SetActive(false);
                    }
                }
            }
        }
    }
}

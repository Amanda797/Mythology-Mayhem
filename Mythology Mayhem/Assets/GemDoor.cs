using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemDoor : MonoBehaviour
{
    Animator anim;
    public string doorGem;
    public float gemScale;
    public float distance;
    superliminal superliminal;
    public Transform gemPlacement;
    Collider col;

    GameObject gem;

    bool doorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        superliminal = Camera.main.GetComponent<superliminal>();
        col = GetComponent<Collider>();
    }

    bool ObjClose(GameObject obj, float distance)
    {
        if(Vector3.Distance(obj.transform.position, transform.position) < distance)
        {
            return true;
        }
        return false;
    }

    bool ObjScale(GameObject obj, float scale)
    {
        if(obj.transform.localScale.x >= scale + 1.5f)
        {
            return false;
        }
        if(obj.transform.localScale.x <= scale - 1.5f)
        {
            return false;
        }
        return true;
    }

    void Update()
    {
        if(gem != null)
        {
            gem.transform.position = gemPlacement.position;
            gem.transform.rotation = gemPlacement.rotation;
        }
        List<GameObject> gems = new List<GameObject>(GameObject.FindGameObjectsWithTag("Gem"));

        foreach(GameObject gem in gems)
        {
            //print("Right Disatnce: " + ObjClose(gem, distance));
            //print("Right Scale: "+ObjScale(gem, gemScale));
            if(ObjScale(gem,gemScale) && ObjClose(gem, distance))
            {
                //print("Gem is close enough");
                //gem.transform.localScale = new Vector3(gemScale, gemScale, gemScale);
                if(!doorOpen)
                    OpenDoor(gem.name, gem.transform);
            }
        }
        
    }

    public void OpenDoor(string gem, Transform gemT)
    {
        if(gem == doorGem)
        {
            anim.SetBool("Open", true);
            superliminal.Reset();
            this.gem = gemT.gameObject;
            gemT.gameObject.layer = 0;
            gemT.GetComponent<Rigidbody>().isKinematic = true;
            col.enabled = false;
            doorOpen = true;
        }
    }
}

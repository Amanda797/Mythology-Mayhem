using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzle : MonoBehaviour
{
    [System.Serializable]
    public class Statue 
    {
        public SpriteRenderer statueHeadRenderer;
        public int currentHead = 0;
    }

    public List<Statue> statues = new List<Statue>();

    public List<Sprite> heads = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        ChangeHeads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeHeads()
    {
        foreach (Statue statue in statues)
        {
            if(statue.currentHead >= heads.Count || statue.currentHead < 0)
            {
                statue.statueHeadRenderer.sprite = null;
            }
            else
            {
                statue.statueHeadRenderer.sprite = heads[statue.currentHead];
            }
            
        }
    }

}

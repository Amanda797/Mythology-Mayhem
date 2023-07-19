using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzle3D : MonoBehaviour
{
    [System.Serializable]
    public class Statue
    {
        public MeshFilter statueHeadRenderer;
        public int currentHead = 0;
    }

    public List<Statue> statues = new List<Statue>();

    public List<Mesh> heads = new List<Mesh>();


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
                statue.statueHeadRenderer.mesh = null;
            }
            else
            {
                statue.statueHeadRenderer.mesh = heads[statue.currentHead];
            }
            
        }
    }

}

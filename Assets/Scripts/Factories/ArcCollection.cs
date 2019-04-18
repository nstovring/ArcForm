using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcCollection : Fragment
{
    // Start is called before the first frame update
    void Start()
    {
        if (myArcs == null)
        {
            myArcs = new List<Arc>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

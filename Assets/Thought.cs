using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Unitoken> mySources;
    public List<Thought> myThoughts;
    public Arc myArc;


    public ArcMapManager.ThoughtType myType;

    public void Initialize(ArcMapManager.ThoughtType myType){
        this.myType = myType;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

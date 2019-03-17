using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    // Start is called before the first frame update
    Thought source;
    Thought target;

    public bool isPlaced = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Thought source, Thought target){
        isPlaced = false;
        
    }

    public void Move(){
        //Unitoken follows mouse
        //Arc follows personal source and target
    }
}

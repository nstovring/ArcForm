using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnimation : MonoBehaviour
{
    public Unitoken parent;
    public Vector3 animPulsValue;
    public float AnimSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animPulsValue = new Vector3(1.1f,1.1f,1);
        AnimSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.isHoveredOver == true){
            iTween.ScaleBy(gameObject,iTween.Hash("amount", animPulsValue, "easeType", "easeInOutExpo", "loopType", "pingpong"));
            

        }   
    }
}

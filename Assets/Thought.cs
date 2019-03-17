using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<Unitoken> mySources;
    //public List<Thought> myThoughts;
    public Thought source;
    public Thought target;
    public Arc myArc;

    public enum ThoughtType {Unitoken, Arc, Thought, Fragment};
    public enum ThoughtState {Folded, Unfolded, Thought};
    public ThoughtType myType;

    public bool isLabelled;
    public string Label   { get => GetThoughtLabel();}


    public string GetThoughtLabel(){
        return "";
    }
    public virtual void Initialize(){
        myType = ThoughtType.Arc;
    }
    public void SetType(){

    }
  

    void Start()
    {



        //if(typeof(Arc).Equals(source)){
        //    myType = ThoughtType.Fragment;
        //}
        //if()
    }

    public void CreateUnitokenThought(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using UnityEngine.UI;
using ConceptNetJsonHolder;

public class PredicateToggle : MonoBehaviour
{
    public predicate myPredicate;

    public Xml2CSharp.Class myClass;
    public Xml2CSharp.Category myCategory;

    public Edge myEdge;

    public Toggle myToggle;
    public Text myText;

    public string Label;
    public void SetPredicate(predicate x){
        myPredicate = x;
        myText.text = x.property;
        Label = x.property;
    }

    public void SetPredicate(Xml2CSharp.Class x){
        myClass = x;
        myText.text = x.Label;
        Label = x.Label;
    }
    public void SetPredicate(Xml2CSharp.Category x){
        myCategory = x;
        myText.text = x.Label;
        Label = x.Label;
    }


     public void SetPredicate(Edge x){
        myEdge = x;
        myText.text =!string.IsNullOrWhiteSpace(x.SurfaceText) ? x.SurfaceText :  (x.Rel.Label +" " + x.End.Label).ToLower();
        Label = myText.text;
    }
    
    
}

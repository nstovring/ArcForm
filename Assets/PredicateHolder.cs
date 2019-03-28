using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using UnityEngine.UI;

public class PredicateHolder : MonoBehaviour
{
    public predicate myPredicate;
    public Toggle myToggle;
    public Text myText;
    public void SetPredicate(predicate x){
        myPredicate = x;
        myText.text = x.property;
        
    }

    
    
}

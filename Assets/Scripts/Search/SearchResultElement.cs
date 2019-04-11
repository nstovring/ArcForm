using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using StructureContainer;
using Xml2CSharp;
using ConceptNetJsonHolder;
using System;

public class SearchResultElement : MonoBehaviour
{
    // Start is called before the first frame update
    public Button elementButton;
    public Text elementText;

    public ToggleBox toggleBox;
    
    [SerializeField]
    public Result XMLResult;
    [SerializeField]
    public Concept Concept;
    public string URI;

    public List<predicate> myPredicates;
    void Start()
    {
        if(elementButton == null)
            throw new UnassignedReferenceException();
            
        if(elementText == null)
            throw new UnassignedReferenceException();

    }

    public void SetOnClickDelegate(Action myMethodName){
         elementButton.onClick.AddListener(delegate {myMethodName();});
    }

    public void Initialize(){
        if(XMLResult == null)
            throw new UnassignedReferenceException();

        elementText.text = XMLResult.Label;
        URI = XMLResult.URI;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using StructureContainer;
using Xml2CSharp;

public class SearchResultElement : MonoBehaviour
{
    // Start is called before the first frame update
    public Button elementButton;
    public Text elementText;

    public PredicateContainer predicateContainer;
    
    public Classes classes;
    public Categories categories;
    public string URI;

    public List<predicate> myPredicates;
    void Start()
    {
        if(elementButton == null)
            throw new UnassignedReferenceException();
            
        if(elementText == null)
            throw new UnassignedReferenceException();

        elementButton.onClick.AddListener(delegate {
            GetPredicates(this);
        });
        predicateContainer = SearchBox.Instance.predicateContainer;
        
    }

    void GetPredicates(){
        Debug.Log(URI);
        myPredicates = SparqlInterface.Instance.GetPredicates(URI);
        predicateContainer.CreatePredicates(myPredicates);
        predicateContainer.selectedSearchElement = this;
        //foreach(Stru)
    }

    void GetPredicates(SearchResultElement element){
        Debug.Log(URI);
        myPredicates = SparqlInterface.Instance.GetPredicates(URI);
        predicateContainer.CreatePredicates(element);
        predicateContainer.selectedSearchElement = this;
        //foreach(Stru)
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

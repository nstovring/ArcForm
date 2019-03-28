using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using StructureContainer;
public class SearchResultElement : MonoBehaviour
{
    // Start is called before the first frame update
    public Button elementButton;
    public Text elementText;

    public PredicateContainer predicateContainer;
    

    public string URI;

    public List<predicate> myPredicates;
    void Start()
    {
        if(elementButton == null)
            throw new UnassignedReferenceException();
            
        if(elementText == null)
            throw new UnassignedReferenceException();

        elementButton.onClick.AddListener(delegate {
            GetPredicates();
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

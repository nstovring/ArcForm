using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using StructureContainer;
using Xml2CSharp;
using ConceptNetJsonHolder;

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

        elementButton.onClick.AddListener(delegate {
            Unitoken token = TokenFactory.Instance.AddNewToken(elementText.text, Vector3.zero);

            Debug.Log("Finding Predicates for this search element");
            SearchEngine.Instance.GetRelationsForSearchElement(this, token);
            

            SearchEngine.Instance.ClearFuzzyResults();
            //SearchEngine.Instance.GetPredicates(this);
        });
        //toggleBox = SearchBox.Instance.toggleBox;
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

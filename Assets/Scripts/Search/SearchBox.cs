using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xml2CSharp;
using ConceptNetJsonHolder;

public class SearchBox : MonoBehaviour
{
    public static SearchBox Instance;
     [Header("Prefabs")]
    public Transform searchElementPrefab;
    public Transform togglePrefab;
    [Header("Direct References")]

    public InputField searchField;

    public ToggleBox toggleBox;
    public RectTransform myRectTransform;

    [Header("Search Variables")]
    public List<SearchResultElement> searchResults;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        searchResults = new List<SearchResultElement>();

        if(myRectTransform == null){
            myRectTransform = GetComponent<RectTransform>();
        }
        if(searchElementPrefab == null)
            throw new UnassignedReferenceException();

        if(searchField == null)
            throw new UnassignedReferenceException();

        if(togglePrefab == null)
            throw new MissingReferenceException();

        if(toggleBox == null)
            throw new MissingReferenceException();

        searchField.onEndEdit.RemoveAllListeners();

        searchField.onEndEdit.AddListener(delegate{
            ClearResults();
            Search(searchField.text,myCurrentSearchType);
            //inputField.transform.gameObject.SetActive(false);
        });
    }

    public enum SearchType {dbPediaXML,dbPediaQuery, conceptNet}
    public SearchType myCurrentSearchType = SearchType.dbPediaXML;
    public void Search(string searchTerm, SearchType searchType){
        myCurrentSearchType = searchType;

        if(searchType == SearchType.dbPediaXML){
            List<Result> results =  Xml2CSharp.XMLParser.Instance.ReadLink(searchTerm);
            ReceiveResults(results);
        }

        if(searchType == SearchType.conceptNet){
            //ConceptNetInterface.Instance.GetRelations(searchTerm, this, 10);
        }

       
    }
    public void ReceiveResults(List<Result> edges){
        Debug.Log("Receiving Results" + edges.Count);
         foreach(Result x in edges){
            AddSearchResult(x);
		}
    }


    public void ReceiveResults(List<SearchResultElement> edges){
        Debug.Log("Receiving Results" + edges.Count);
         foreach(SearchResultElement x in edges){
            AddSearchResult(x);
		}
    }

    public void ReceiveResults(Edge[] edges){
        Debug.Log("Receiving Results" + edges.Length);
         foreach(Edge x in edges){
            searchResults.Add(AddSearchResult(x));
		}
    }

    public void ClearResults(){
        if(searchResults.Count > 0){
            foreach(SearchResultElement x in searchResults){
				Destroy(x.gameObject);
		    }
        }
        searchResults.Clear();
    }

    public SearchResultElement AddSearchResult(SearchResultElement x)
    {
        SearchResultElement element = Instantiate(searchElementPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<SearchResultElement>();
        element = x;
        //element.elementText.text = x.Label;
        //element.URI = x.URI;
        //element.XMLResult =x;
        element.Initialize();
        return element;
    }
    public SearchResultElement AddSearchResult(Result x)
    {
        SearchResultElement element = Instantiate(searchElementPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<SearchResultElement>();
        element.elementText.text = x.Label;
        element.URI = x.URI;
        element.XMLResult =x;
        return element;
    }

     public SearchResultElement AddSearchResult(Edge x)
    {
        SearchResultElement element = Instantiate(searchElementPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<SearchResultElement>();
        element.elementText.text = x.SurfaceText;
        //element.Concept = x.;
        //element.classes =x.Classes;
        //element.categories = x.Categories;
        //searchResults.Add(element);
        return element;
    }
    public void AddSearchResult(string label, string description, string uri)
    {
        SearchResultElement element = Instantiate(searchElementPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<SearchResultElement>();
        element.elementText.text = label;
        element.URI = uri;
        searchResults.Add(element);
    }

    //Gets predicates from DBPEdia
    public void GetPredicates(SearchResultElement element){
        element.myPredicates = SparqlInterface.Instance.GetPredicates(element.URI);
        toggleBox.CreatePredicates(element);
        toggleBox.selectedSearchElement = element;
        throw new UnassignedReferenceException();
        //foreach(Stru)
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

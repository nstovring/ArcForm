using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xml2CSharp;
public class SearchBox : MonoBehaviour
{
    public static SearchBox Instance;
    public Transform searchElementPrefab;
    public Transform togglePrefab;
    public InputField inputField;

    public PredicateContainer predicateContainer;

    public RectTransform myRectTransform;
    public List<SearchResultElement> searchResults;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        searchResults = new List<SearchResultElement>();

        if(myRectTransform == null)
            throw new UnassignedReferenceException();
        if(searchElementPrefab == null)
            throw new UnassignedReferenceException();

        if(inputField == null)
            throw new UnassignedReferenceException();

        if(togglePrefab == null)
            throw new MissingReferenceException();

        if(predicateContainer == null)
            throw new MissingReferenceException();

        inputField.onEndEdit.RemoveAllListeners();

        inputField.onEndEdit.AddListener(delegate{
            ClearResults();
            Search(inputField.text);
            //inputField.transform.gameObject.SetActive(false);
        });
    }

    public void Search(string searchTerm){
        List<Result> results =  Xml2CSharp.XMLParser.Instance.ReadLink(searchTerm);
        foreach(Result x in results){
				AddSearchResult(x.Label, x.Description, x.URI);
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

    public void AddSearchResult(string label, string description, string uri)
    {
        SearchResultElement element = Instantiate(searchElementPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<SearchResultElement>();
        element.elementText.text = label;
        element.URI = uri;
        searchResults.Add(element);

        //element.elementButton.onClick.AddListener(delegate{
        //    //Create new token with text and uri
        //    Unitoken token = ArcMapManager.Instance.tokenFactory.AddNewToken(label);
        //    //token.
        //    //Search for predicates
        //    ClearResults();
        //});
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

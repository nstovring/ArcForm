using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Xml2CSharp;
public class SearchBox : MonoBehaviour
{
    public static SearchBox Instance;
    public Transform searchElementPrefab;
    public InputField inputField;
    public List<SearchResultElement> searchResults;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        searchResults = new List<SearchResultElement>();

        if(searchElementPrefab == null)
            throw new UnassignedReferenceException();

        if(inputField == null)
            throw new UnassignedReferenceException();

        inputField.onEndEdit.RemoveAllListeners();


        //Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        //RectTransform rectTransform =  inputField.GetComponent<RectTransform>();
        //rectTransform.position = screenPos;

        inputField.onEndEdit.AddListener(delegate{
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
        foreach(SearchResultElement x in searchResults){
				Destroy(x);
		}
        searchResults.Clear();
    }

    public void AddSearchResult(string label, string description, string uri)
    {
        SearchResultElement element = Instantiate(searchElementPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<SearchResultElement>();
        element.elementText.text = label;
        searchResults.Add(element);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

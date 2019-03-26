using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBox : MonoBehaviour
{
    public static SearchBox Instance;
    public Transform searchElementPrefab;
    public List<SearchResultElement> searchResults;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        searchResults = new List<SearchResultElement>();

        if(searchElementPrefab == null)
            throw new UnassignedReferenceException();
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

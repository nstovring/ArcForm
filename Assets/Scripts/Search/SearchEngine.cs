﻿using System;
using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using Xml2CSharp;

//This class serves as an interface for searching and retreiving data from DBPedia and ConceptNet
public class SearchEngine : MonoBehaviour
{
    [Header("Test Settings")]
    public string testString = "Einstein";
    public int searchLimit = 10;

    public Unitoken focusedUnitoken;

    [Header("Search Refs")]
    public Transform searchEngineCanvasTransform;
    public SearchBox searchBox;
    public List<ToggleBox> ToggleBoxes;


    [Header("Prefabs")]
    public Transform fuzzySearchResultPrefab;

    [Header("Results")]
    public List<SearchResultElement> InstantiatedResults;
    public SearchResultElement selectedSearchResultElement;

    //Static References
    public static SearchEngine Instance;
    
    public void Start(){
        Instance = this;
        FocusedTokenRelations = new List<Relation>();
    }
    public void TEST1(){
        GetFuzzySearchResults(testString);
    }

    public void TEST2(){
        string searchableLabel = InstantiatedResults[0].XMLResult.Label;
        Debug.Log(searchableLabel);
        GetConceptRelations(searchableLabel, searchLimit);
    }

    internal void GetRelationsForSearchElement(SearchResultElement searchResultElement, Unitoken source)
    {
        focusedUnitoken = source;
        
        if(searchResultElement.Concept != null){
            ReceiveConceptAndFillToggle(searchResultElement.Concept);
        }else{
            GetConceptRelations(searchResultElement.elementText.text, searchLimit);
        }

        if(searchResultElement.XMLResult != null){
            ReceiveDBPediaXMLResultsAndFillToggle(searchResultElement.XMLResult);
        }

        //Create preview from results
        Debug.Log("Received relations for " + searchResultElement.elementText.text);
    }


    //Get user input from search input field
    public void GetFuzzySearchResults(string search){
        List<Result> xmlResults = FuzzySearcher.FuzzySearch(search, fuzzySearchResultPrefab, transform);
        InstantiatedResults = FuzzySearcher.ConvertResults(xmlResults, fuzzySearchResultPrefab, searchEngineCanvasTransform);
    }

    public void ClearFuzzyResults(){
        FuzzySearcher.ClearFuzzyResults();
    }

    public void GetConceptRelations(string subject, int limit){
        ConceptNetInterface.GetConceptRelations(subject, this, limit);
    }

    public void GetConceptRelations(Unitoken subject, int limit){
        focusedUnitoken = subject;
        ConceptNetInterface.GetConceptRelations(subject.myLabel.text, this, limit);
    }

    
    
    List<Relation> FocusedTokenRelations;
     public struct Relation{
        public string source;
        public string relations;
        public string target;
    }


    public void CreatePreview(Concept concept){
        ArcCollectionFactory.Instance.GeneratePreviewFromConcept(focusedUnitoken, concept);
    }

    public void ReceiveConceptAndFillToggle(Concept concept){
        ArcCollectionFactory.Instance.GeneratePreviewFromConcept(focusedUnitoken, concept);

        Debug.Log("Received Relations for "+ concept.Edges.Length);
        FillToggleBox(concept, ToggleBoxes[0]);
    }

    public void ReceiveDBPediaXMLResultsAndFillToggle(Result result){
        ArcCollectionFactory.Instance.GeneratePreviewFromXML(focusedUnitoken, result);

        Debug.Log("Received XML Relations for "+ result.Label);
        //FillToggleBox(result.Classes, ToggleBoxes[1]);
        //FillToggleBox(result.Categories, ToggleBoxes[2]);
    }

    public void FillToggleBox(Concept concept, ToggleBox toggleBox){
        toggleBox.CreatePredicates(concept);
    }

    public void FillToggleBox(Classes classes, ToggleBox toggleBox){
        toggleBox.CreatePredicates(classes);
    }

    public void FillToggleBox(Categories category, ToggleBox toggleBox){
        toggleBox.CreatePredicates(category);
    }


    public void ClearToggleBox(ToggleBox toggleBox){
        toggleBox.ClearBox();
    }

    

internal class FuzzySearcher
{
    static List<Result> results;
    static List<SearchResultElement> fuzzySearchResults;
     public static List<Result> FuzzySearch(string testString, Transform fuzzySearchResultPrefab, Transform parent){
        //Send fuzzy search request to DBpedia for Token
        //Return list of tokens
        results = DBPediaXmlSearch(testString, fuzzySearchResultPrefab);
        //fuzzySearchResults = ConvertResults(results,  fuzzySearchResultPrefab, parent);
        return results;
    }
    public static List<Result> DBPediaXmlSearch(string x, Transform searchElementPrefab){
        List<Result> results =  Xml2CSharp.XMLParser.Instance.ReadLink(x);
        return results;
    }

    public static List<SearchResultElement> ConvertResults (List<Result> edges, Transform searchElementPrefab, Transform parent){

        List<SearchResultElement> convertedResults = new List<SearchResultElement>();
        fuzzySearchResults = ClearFuzzyResults();

        Debug.Log("Receiving Results" + edges.Count);
         foreach(Result x in edges){
            SearchResultElement result = ConvertResult(x, searchElementPrefab, parent);
            convertedResults.Add(result);
            fuzzySearchResults.Add(result);
		}
        return convertedResults;
    }
    public static SearchResultElement ConvertResult(Result x, Transform prefab, Transform parent)
    {
        SearchResultElement element = Instantiate(prefab, Vector3.zero, Quaternion.identity, parent).GetComponent<SearchResultElement>();
        element.name = x.Label;
        element.XMLResult =x;

        Action del = delegate {
            Unitoken token = TokenFactory.Instance.AddNewToken(x.Label, Vector3.zero);
            Debug.Log("Created Token from Fuzz");
            token.isSoft = false;

            Debug.Log("Finding Predicates for this search element");
            SearchEngine.Instance.GetRelationsForSearchElement(element, token);
            Debug.Log("Got Relations");
            SearchEngine.Instance.ClearFuzzyResults();
        };

        element.SetOnClickDelegate(del);
        element.Initialize();
        return element;
    }

     public static List<SearchResultElement> ClearFuzzyResults(){
        if(fuzzySearchResults == null){
            fuzzySearchResults = new List<SearchResultElement>();
        }
        if(fuzzySearchResults.Count > 0){
            foreach(SearchResultElement x in fuzzySearchResults){
				Destroy(x.gameObject);
		    }
            fuzzySearchResults.Clear();
        }
        return fuzzySearchResults;
    }


    
}

internal class ConceptNETFinder{

}


}

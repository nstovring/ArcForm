using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcCollectionToggleMenu : MonoBehaviour
{
    public static ArcCollectionToggleMenu Instance;

    public Transform canvasTransform;

    public ArcCollectionItem propertyMenuButton;

    public static List<ArcCollectionItem> ConceptNetPropertyList;
    public static Dictionary<string, ArcCollectionItem> PropertyDictionary;

    public static string[] relationURIs = {"/r/RelatedTo", "/r/ExternalURL", "/r/FormOf", "/r/IsA", "/r/PartOf", "/r/HasA" , "/r/UsedFor", "/r/CapableOf", "/r/AtLocation", "/r/Causes", "/r/HasSubevent", "/r/HasFirstSubevent", "/r/HasLastSubevent", "/r/HasPrerequisite", "/r/HasProperty", "/r/MotivatedByGoal", "/r/ObstructedBy", "/r/Desires", "/r/CreatedBy", "/r/Synonym", "/r/Antonym", "/r/DistinctFrom", "/r/DerivedFrom", "/r/SymbolOf", "/r/DefinedAs", "/r/Entails", "/r/MannerOf", "/r/LocatedNear", "/r/HasContext", "/r/SimilarTo", "/r/EtymologicallyRelatedTo", "/r/EtymologicallyDerivedFrom", "/r/CausesDesire", "/r/MadeOf", "/r/ReceivesAction", "/r/InstanceOf"};
    public static string[] relationsNaming = {"is related to ","External URL ","form of ","is a ","part of ","has a ","used for ","capable of ","at location ","causes ","has subevent ","has first subevent ","has last subevent ","has prerequisite ","has property ","motivated by goal ","obstructed by ","desires ","created by ","synonym ","antonym ","distinct from ","derived from ","symbol of ","defined as ","entails ","manner of ","located near ","has context ","similar to ","etymologically related to ","etymologically derived from ","causes desire ","made of ","receives action ","instance of "};
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ConceptNetPropertyList = new List<ArcCollectionItem>();
        PropertyDictionary = new Dictionary<string, ArcCollectionItem>();

        int count = 0;

        foreach(string x in relationsNaming){
            ArcCollectionItem y = Instantiate(propertyMenuButton, Vector3.zero, Quaternion.identity, canvasTransform);
            y.SetProperty(x);
            y.isActive = false;
            y.index = count;
            ConceptNetPropertyList.Add(y);
            PropertyDictionary.Add(x, y);

            count++;
        }
    }

    public bool[] Filter = new bool[ConceptNetInterface.relationURIs.Length];
    public bool[] SetFilter(bool state, int index){
        Filter[index] = state;
        return Filter;
    }

     public bool[] SetFilter(int index, bool isActive){
        //ConceptNetProperty c = GetProperty(key);
        bool state = isActive;

        Filter[index] = state;
//        Debug.Log("Toggled State for: " + key + " : " + isActive + " In PropertyMenu");

        return Filter;
    }

    //public bool GetFilter(string key){

    //}


    public void SelectProperty(string property, bool isSelected){
        GetProperty(property).isActive = isSelected;
    }


    public ArcCollectionItem GetProperty(string property){
        ArcCollectionItem p;
        PropertyDictionary.TryGetValue(property, out p);
        return p;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

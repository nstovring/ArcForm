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

        for(int i = 0; i < relationURIs.Length; i++){
            ArcCollectionItem arcCollectionItem = Instantiate(propertyMenuButton, Vector3.zero, Quaternion.identity, canvasTransform);
            string label = relationsNaming[i];
            string key = relationURIs[i];

            arcCollectionItem.SetProperty(label);
            arcCollectionItem.isActive = false;
            arcCollectionItem.index = i;
            ConceptNetPropertyList.Add(arcCollectionItem);
            PropertyDictionary.Add(key, arcCollectionItem);
        }
    }

   

    public bool[] Filter = new bool[ConceptNetInterface.relationURIs.Length];
    public bool[] SetFilter(bool state, int index){
        Filter[index] = state;
        return Filter;
    }

     public bool[] SetFilter(int index, bool isActive){
        bool state = isActive;

        Filter[index] = state;

        return Filter;
    }



    public void SelectProperty(string property, bool isSelected){
        GetArcCollectionItem(property).isActive = isSelected;
    }

    public static ArcCollectionItem GetArcCollectionItem(string key){
        ArcCollectionItem val;
        if(!PropertyDictionary.TryGetValue(key, out val)){
            throw new KeyNotFoundException();
        }
        return val;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

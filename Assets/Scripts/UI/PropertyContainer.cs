using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyContainer : MonoBehaviour
{
    public static string[] relationURIs = {"/r/RelatedTo", "/r/ExternalURL", "/r/FormOf", "/r/IsA", "/r/PartOf", "/r/HasA" , "/r/UsedFor", "/r/CapableOf", "/r/AtLocation", "/r/Causes", "/r/HasSubevent", "/r/HasFirstSubevent", "/r/HasLastSubevent", "/r/HasPrerequisite", "/r/HasProperty", "/r/MotivatedByGoal", "/r/ObstructedBy", "/r/Desires", "/r/CreatedBy", "/r/Synonym", "/r/Antonym", "/r/DistinctFrom", "/r/DerivedFrom", "/r/SymbolOf", "/r/DefinedAs", "/r/Entails", "/r/MannerOf", "/r/LocatedNear", "/r/HasContext", "/r/SimilarTo", "/r/EtymologicallyRelatedTo", "/r/EtymologicallyDerivedFrom", "/r/CausesDesire", "/r/MadeOf", "/r/ReceivesAction", "/r/InstanceOf"};
    public static Dictionary<string, ConceptNetProperty> propertyDictionary;
    public ConceptNetProperty propertyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        propertyDictionary = new Dictionary<string, ConceptNetProperty>();

        foreach(string x in relationURIs){
            ConceptNetProperty y = Instantiate(propertyPrefab, Vector3.zero, Quaternion.identity, transform);
            y.SetProperty(x);
            y.isActive = false;
            propertyDictionary.Add(x, y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectProperty(string relationURI){

    }
}

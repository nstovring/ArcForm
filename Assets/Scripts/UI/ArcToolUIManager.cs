using ArcToolConstants;
using ConceptNetJsonHolder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcToolUIManager : MonoBehaviour
{
    //Static Refs
    public static string[] propertyURIs = { "/r/RelatedTo", "/r/ExternalURL", "/r/FormOf", "/r/IsA", "/r/PartOf", "/r/HasA", "/r/UsedFor", "/r/CapableOf", "/r/AtLocation", "/r/Causes", "/r/HasSubevent", "/r/HasFirstSubevent", "/r/HasLastSubevent", "/r/HasPrerequisite", "/r/HasProperty", "/r/MotivatedByGoal", "/r/ObstructedBy", "/r/Desires", "/r/CreatedBy", "/r/Synonym", "/r/Antonym", "/r/DistinctFrom", "/r/DerivedFrom", "/r/SymbolOf", "/r/DefinedAs", "/r/Entails", "/r/MannerOf", "/r/LocatedNear", "/r/HasContext", "/r/SimilarTo", "/r/EtymologicallyRelatedTo", "/r/EtymologicallyDerivedFrom", "/r/CausesDesire", "/r/MadeOf", "/r/ReceivesAction", "/r/InstanceOf" };
    public static ArcToolUIManager Instance;

    //Structs
    public struct Relation
    {
        public string Label;
        public bool isActive;
        public Unitoken token;
    }

    public struct Property
    {
        public string Label;
        public bool isActive;
        public List<Relation> Relations;
    }

   

    [Header("Scene References")]
    public Text PropertyMenuText;
    public Transform PropertyMenu;
    public Transform PropertySubMenu;


    [Header("Canvas Prefabs")]
    public Transform PropertyMenuButton;
    public Transform PropertySubMenuButton;

    [Header("Data Containers")]
    public Dictionary<string, Property> Properties;
    public Dictionary<string, ArcCollectionItem> PropertyMenuItems;

    void Start()
    {
        Instance = this;

        Properties = new Dictionary<string,Property>();
        PropertyMenuItems = new Dictionary<string, ArcCollectionItem>();

        foreach (string x in StaticConstants.RelationURIs)
        {
            Property p = new Property {
                Label = x,
                isActive = false,
                Relations = new List<Relation>()
            };

            Properties.Add(x,p);
        }

        InitializeToggleMenu();
    }

    public void InitializeToggleMenu()
    {
        for (int i = 0; i < StaticConstants.RelationURIs.Length; i++)
        {
            ArcCollectionItem arcCollectionItem = Instantiate(PropertyMenuButton, Vector3.zero, Quaternion.identity, PropertyMenu).GetComponent<ArcCollectionItem>();
            string label = StaticConstants.relationsNaming[i];
            string key = StaticConstants.RelationURIs[i];

            arcCollectionItem.SetProperty(key, label);
            arcCollectionItem.isActive = false;
            arcCollectionItem.index = i;
            arcCollectionItem.transform.name = key;

            PropertyMenuItems.Add(key, arcCollectionItem);
        }
    }

    public void UpdatePropertyMenuFromConcept(Unitoken unitoken, Concept concept)
    {

        //Update title in menu to label of unitoken
        PropertyMenuText.text = unitoken.myLabel.text;

        //Update every Collection item count
        Properties = GeneratePropertiesContainerFromConcept(concept);

        foreach(string x in StaticConstants.RelationURIs)
        {
            string key = x;
            Property property;
            ArcCollectionItem collectionItem;

            Properties.TryGetValue(key,out property);
            PropertyMenuItems.TryGetValue(key, out collectionItem);

            int relationCount = property.Relations.Count;
            collectionItem.subItemCount.text = relationCount.ToString();
            //Update Menu Item from property
            
        }

        //Store Current Properties in unitoken
        unitoken.StoreProperties(Properties);
    }

    public void ToggleProperty(ArcCollectionItem key)
    {

    }

    public void ToggleSubProperty(ArcCollectionSubItem item)
    {

    }

    public void UpdateToggleMenuWithProperties(List<Property> properties)
    {

    }

    public void UpdateUnitokenWithProperties(Unitoken u, List<Property> properties)
    {

    }


    public Dictionary<string, Property> GeneratePropertiesContainerFromConcept(Concept concept)
    {
        int[] count = new int[StaticConstants.RelationURIs.Length];
        string[] relations = StaticConstants.RelationURIs;
        Dictionary<string, Property> properties = new Dictionary<string, Property>();

        foreach (string x in relations)
        {
            Property p = new Property
            {
                Label = x,
                isActive = false,
                Relations = new List<Relation>()
            };

            properties.Add(x,p);
        }


        foreach (Edge edge in concept.Edges)
        {
            //Check if edge contains any relation uris
            for (int i = 0; i < count.Length; i++)
            {
                string type = relations[i];
                if (edge.Id.Contains(type))
                {
                    //Get Relation
                    Relation r = GenerateRelationFromEdge(edge);

                    //Get Property
                    Property p;
                    properties.TryGetValue(type, out p);

                    //Add Relation To Property
                    p.Relations.Add(r);
                }
            }
        }

        return properties;
    }

    Relation GenerateRelationFromEdge(Edge edge)
    {
        return new Relation
        {
            Label = edge.End.Label,
            isActive = false,
            token = null
        };
    }

}

using ArcToolConstants;
using ConceptNetJsonHolder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StructureContainer;
using System;

public class ArcToolUIManager : MonoBehaviour
{
    //Static Refs
    public static ArcToolUIManager Instance;

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

 
       
    internal void ToggleMenuItem(ArcCollectionItem arcCollectionItem)
    {
        Unitoken unitoken = ArcMapManager.Instance.GetFocusedToken();
        //Check if property is active
        Property property = unitoken.GetProperty(arcCollectionItem.key);
        List<Relation> relations = property.Relations;
        //if active instantiate arc collection
        //else remove arc collection
        if (arcCollectionItem.isActive == true)
        {
            property.isActive = true;
            //Create sub buttons
            List<ArcCollectionSubItem> subItems = CreateSubMenuButtons(property);
            //Add collection to map
            ArcCollection cd = ArcCollectionFactory.Instance.AddNewCollection(unitoken, StaticConstants.RelationURIs[arcCollectionItem.index], subItems);
            //Set Property to active
        }
        else
        {
            property.isActive = false;
            //Remove Sub Buttons
            RemoveSubMenuButtons();
            ArcCollection cd;

            unitoken.myArcCollection.TryGetValue(arcCollectionItem.key, out cd);
            unitoken.myArcCollection.Remove(arcCollectionItem.key);

            ArcCollectionFactory.Instance.DestroyArcCollection(cd);

        }
    }

    internal void ToggleSubMenu(ArcCollectionItem arcCollectionItem)
    {
        bool toggleSubMenu = arcCollectionItem.subMenuIsActive;
        Unitoken unitoken = ArcMapManager.Instance.GetFocusedToken();
        Property property = unitoken.GetProperty(arcCollectionItem.key);
        if (toggleSubMenu)
        {
            List<ArcCollectionSubItem> subItems = CreateSubMenuButtons(property);
        }
        else
        {
            RemoveSubMenuButtons();
        }
    }


    internal List<ArcCollectionSubItem> CreateSubMenuButtons(Property property)
    {
        List<ArcCollectionSubItem> subItems = new List<ArcCollectionSubItem>();
        foreach (Relation rel in property.Relations)
        {
            ArcCollectionSubItem item = Instantiate(PropertySubMenuButton, PropertySubMenu).GetComponent<ArcCollectionSubItem>();
            item.Refresh(rel, property.Label);
            item.isActive = rel.isActive;
            //item.buttonToggle.toggled = rel.isActive;
            item.buttonToggle.TaskOnClick();
            subItems.Add(item);
        }
        return subItems;
    }

    public Relation GetRelation(ArcCollectionSubItem acsi)
    {
        Unitoken unitoken = ArcMapManager.Instance.GetFocusedToken();
        Property property = unitoken.GetProperty(acsi.key);
        string label = acsi.text.text;

        Relation rel = new Relation();
        //Find relation
        foreach(Relation r in property.Relations)
        {
            if(r.Label == label)
            {
                rel = r;
            }
        }

        return rel;
    }

   // internal List<ArcCollectionSubItem> CreateSubMenuButtons(Property property, bool active)
   // {
   //     List<ArcCollectionSubItem> subItems = new List<ArcCollectionSubItem>();
   //     foreach (Relation rel in property.Relations)
   //     {
   //         rel.SetActive(active);
   //         ArcCollectionSubItem item = Instantiate(PropertySubMenuButton, PropertySubMenu).GetComponent<ArcCollectionSubItem>();
   //         item.Refresh(rel, property.Label);
   //         item.isActive = rel.isActive;
   //         item.buttonToggle.TaskOnClick();
   //         subItems.Add(item);
   //     }
   //     return subItems;
   // }

    internal void RemoveSubMenuButtons()
    {
        foreach (Transform child in PropertySubMenu)
        {
            Destroy(child.gameObject);
        }
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

    public void UpdatePropertyMenuFromUnitoken(Unitoken unitoken)
    {
        if (unitoken.myProperties == null)
        {
            //Get Properties
            SearchEngine.Instance.GetConceptRelations(unitoken);
        }
        else
        {
            UpdatePropertyMenuFromProperties(unitoken.myProperties);
        }
    }


    public void UpdatePropertyMenuFromConcept(Unitoken unitoken, Concept concept)
    {

        //Update title in menu to label of unitoken
        PropertyMenuText.text = unitoken.myLabel.text;

        //Update every Collection item count
        Properties = GeneratePropertiesContainerFromConcept(concept);

        //Update property menu
        UpdatePropertyMenuFromProperties(Properties);

        //Store Current Properties in unitoken
        unitoken.StoreProperties(Properties);
    }

    public void UpdatePropertyMenuFromProperties(Dictionary<string, Property> properties)
    {
        foreach (string x in StaticConstants.RelationURIs)
        {
            string key = x;
            Property property;
            ArcCollectionItem collectionItem;

            properties.TryGetValue(key, out property);
            PropertyMenuItems.TryGetValue(key, out collectionItem);

            int relationCount = property.Relations.Count;
            collectionItem.subItemCount.text = relationCount.ToString();
            //Update Menu Item from property

        }

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

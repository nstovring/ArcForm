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
    public Text PropertySubMenuText;
    public Transform PropertySubMenu;

    [Header("Canvas Prefabs")]
    public Transform PropertyMenuButton;
    public Transform PropertySubMenuButton;

    [Header("Booleans")]
    public bool subMenuIsActive = false;


    void Start()
    {
        Instance = this;

        ArcUIUtility.InitializeSceneRefs(PropertyMenuText, PropertyMenu, PropertySubMenuText, PropertySubMenu);
        ArcUIUtility.InitializePrefabs(PropertyMenuButton, PropertySubMenuButton);

        ArcDataUtility.Initialize();
        ArcDataUtility.PropertyMenuItems = ArcUIUtility.CreateMenuButtons();

        ArcUIUtility.ClearMenu();
    }

    internal void ToggleMenuItem(ArcMenuItem arcCollectionItem, bool numToggle, bool numToggleIsActive)
    {
        ArcUIUtility.RemoveSubMenuButtons();

        foreach(string item in StaticConstants.RelationURIs)
        {
            ArcMenuItem aci = ArcDataUtility.PropertyMenuItems[item];
            if(aci != arcCollectionItem)
                aci.myButtonToggle.TogglePressed(false);
        }

        //Getproperties
        Dictionary<string, Property> focusedProperties = ArcMapManager.Instance.GetFocusedToken().myPropertiesFromConceptNet;
        Property property = focusedProperties[arcCollectionItem.key];
        List<ArcMenuSubItem> subItems = new List<ArcMenuSubItem>();

        if (numToggle)
        {
            foreach (Relation rel in property.Relations)
            {
                ArcMenuSubItem item = Instantiate(ArcUIUtility.PropertySubMenuButtonPrefab, PropertySubMenu).GetComponent<ArcMenuSubItem>();
                item.Refresh(rel, property.Label);
                item.arcCollectionItem = arcCollectionItem;
                item.SetActive(rel.isActive);
                item.buttonToggle.TogglePressed(rel.isActive);
                subItems.Add(item);

                
            }

            SubItemController.Instance.arcSubItemMenu.SetActive(true);
        }
        else
        {
            ArcUIUtility.RemoveSubMenuButtons();
            SubItemController.Instance.arcSubItemMenu.SetActive(false);
        }
        //SetRelationsToState(property.Relations, true);
      
        //Foreach relation in property
  
        //List<ArcMenuSubItem> arcMenuSubItems = ToggleSubMenu(arcCollectionItem, numToggle);
        //
        Unitoken focusedToken = ArcMapManager.Instance.GetFocusedToken();
        string key = arcCollectionItem.key;
        Property selectedProperty = focusedToken.GetProperty(key);
        
        PropertySubMenuText.text = selectedProperty.Label;
        
        
        ArcCollection ac;
        bool isCollectionActive = focusedToken.myArcCollections.TryGetValue(key, out ac);
        //Get relation activeness
        if (numToggle && isCollectionActive == false)
        {
            ac = ArcCollectionFactory.Instance.AddNewCollection(focusedToken, key, subItems);
            focusedToken.myArcCollections.Add(key, ac);
        }
        else
        {
            focusedToken.myArcCollections.TryGetValue(key, out ac);
            focusedToken.myArcCollections.Remove(key);
            ArcCollectionFactory.Instance.DestroyArcCollection(ac);
        }
    }

    void DestroyCollection(Unitoken unitoken, string key)
    {
        ArcCollection ac;
        unitoken.myArcCollections.TryGetValue(key, out ac);
        unitoken.myArcCollections.Remove(key);
        ArcCollectionFactory.Instance.DestroyArcCollection(ac);
    }

    internal void ToggleSubMenuItem(ArcMenuSubItem arcMenuSubitem)
    {
        //Get specific relation and set it to toggle state
        Unitoken focusedToken = ArcMapManager.Instance.GetFocusedToken();
        string key = arcMenuSubitem.key;
        //bool active = arcMenuSubitem.isActive;
        Property selectedProperty = focusedToken.GetProperty(key);
        
        ArcCollection ac;
        bool arcCollectionExists = focusedToken.myArcCollections.TryGetValue(key, out ac);
        //focusedToken.myArcCollection.Remove(key);

        Relation r = ArcDataUtility.GetRelation(arcMenuSubitem);

        //r.SetActive(active, true);
        bool active = r.isActive;
        if (active)
        {
            if(ac == null)
            {
                ac = ArcCollectionFactory.Instance.AddNewCollection(focusedToken, key, arcMenuSubitem);
                focusedToken.myArcCollections.Add(key, ac);
            }
            else
            {
                ac.AddToCollection(arcMenuSubitem);
            }

            r.SetActive(active,true);
        }
        else
        {
            r.SetActive(active, true);
            ac.RemoveFromCollection(arcMenuSubitem);

            if (ac.myArcs.Count < 1) {
                focusedToken.myArcCollections.Remove(key);
                ArcCollectionFactory.Instance.DestroyArcCollection(ac);
            }
        }

       
    }

    public ArcMenuItem ActiveArcMenuItem;

    public void SetActiveArcMenuItem(ArcMenuItem item)
    {
        ActiveArcMenuItem = item;
    }
    public ArcMenuItem GetActiveArcMenuItem()
    {
        return ActiveArcMenuItem;
    }

    internal List<ArcMenuSubItem> ToggleSubMenu(ArcMenuItem arcCollectionItem, bool numToggle)
    {
        //ArcMenuItem amI = GetActiveArcMenuItem();
        //if(amI != null)
        //{
        //    amI.myButtonToggle.TogglePressed(false);
        //    SetActiveArcMenuItem(arcCollectionItem);
        //}
        //else
        //{
        //    SetActiveArcMenuItem(arcCollectionItem);
        //}

        Unitoken focusedToken = ArcMapManager.Instance.GetFocusedToken();
        string key = arcCollectionItem.key;
        bool buttonActive = numToggle;
        subMenuIsActive = !subMenuIsActive;
        Property selectedProperty = focusedToken.GetProperty(key);

        //ArcUIUtility.UpdatePropertyMenuFromProperties(focusedToken.myPropertiesFromConceptNet);
        if (buttonActive)
        {
        
            foreach (Relation rel in selectedProperty.Relations)
            {
                if(!rel.isEdited)
                  ArcToolUIManager.ArcDataUtility.SetRelation(ArcMapManager.Instance.GetFocusedToken(), key, rel.Label, buttonActive);
            }
            selectedProperty = focusedToken.GetProperty(key);
            return ArcUIUtility.CreateSubMenuButtons(arcCollectionItem, selectedProperty);
        }
        else
        {
            foreach (Relation rel in selectedProperty.Relations)
            {
                ArcToolUIManager.ArcDataUtility.SetRelation(ArcMapManager.Instance.GetFocusedToken(), key, rel.Label, buttonActive);
            }
            return ArcUIUtility.RemoveSubMenuButtons();
        }
    }

    internal void UpdatePropertyMenuFromConcept(Unitoken focusedToken, Concept concept)
    {
        ArcUIUtility.UpdatePropertyMenuFromConcept(focusedToken, concept);
    }

   
    public static class ArcDataUtility
    {
        [Header("Data Containers")]
        public static Dictionary<string, Property> Properties;
        public static Dictionary<string, ArcMenuItem> PropertyMenuItems;
        public static Dictionary<string, ArcMenuSubItem> PropertyMenuSubItems;

        public static void Initialize()
        {
            Properties = new Dictionary<string, Property>();
            PropertyMenuItems = new Dictionary<string, ArcMenuItem>();
            PropertyMenuSubItems = new Dictionary<string, ArcMenuSubItem>();
        }

        public static void SetRelationsToState(List<Relation> relations, bool isActive)
        {
            foreach (Relation rel in relations)
            {
                rel.SetActive(isActive, false);
            }
        }

        public static Dictionary<string, Property> GeneratePropertiesContainerFromConcept(Concept concept)
        {
            int[] count = new int[StaticConstants.RelationURIs.Length];
            string[] relations = StaticConstants.RelationURIs;
            Dictionary<string, Property> properties = new Dictionary<string, Property>();

            int c = 0;
            foreach (string x in relations)
            {
                Property p = new Property
                {
                    Label = x,
                    isActive = false,
                    Relations = new List<Relation>()
                };
                c++;
                properties.Add(x, p);
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

        public static Relation GetRelation(ArcMenuSubItem acsi)
        {
            Unitoken unitoken = ArcMapManager.Instance.GetFocusedToken();
            Property property = unitoken.GetProperty(acsi.key);
            string label = acsi.text.text;

            Relation rel = new Relation();
            //Find relation
            foreach (Relation r in property.Relations)
            {
                if (r.Label == label)
                {
                    rel = r;
                }
            }

            return rel;
        }

        public static void SetRelation(Unitoken u, string key, string label, bool isActiveIn)
        {
            Property property = u.GetProperty(key);
            //string label = acsi.text.text;
            int count = 0;

            List<Relation> relations = new List<Relation>();

            if(property.Relations == null)
            {
                throw new Exception("ERROR : " + property.Label + " : " + "Has No Relations");
            }
            //Find relation
            foreach (Relation r in property.Relations)
            {
                Relation rel = new Relation { isEdited = r.isEdited, isActive = r.isActive, Label = r.Label, token = u };
                if (rel.Label == label)
                {
                    rel.SetActive(isActiveIn, true);// = rel;
                }
                relations.Add(rel);
            }

            property.Relations = relations;

            u.myPropertiesFromConceptNet[key] = property;

        }

        public static Relation GenerateRelationFromEdge(Edge edge)
        {
            return new Relation
            {
                Label = edge.End.Label,
                isActive = true,
                token = null
            };
        }
    }
    public static class ArcUIUtility
    {
        [Header("Scene References")]
        public static Text PropertyMenuText;
        public static Transform PropertyMenu;
        public static Text PropertySubMenuText;
        public static Transform PropertySubMenu;

        [Header("Canvas Prefabs")]
        public static Transform PropertyMenuButtonPrefab;
        public static Transform PropertySubMenuButtonPrefab;

        public static void InitializeSceneRefs(Text propertyMenuText, Transform propertyMenu, Text propertySubMenuText, Transform propertySubMenu)
        {
            PropertyMenuText = propertyMenuText;
            PropertyMenu = propertyMenu;
            PropertySubMenuText = propertySubMenuText;
            PropertySubMenu = propertySubMenu;
        }

        public static void InitializePrefabs(Transform propertyMenuButtonPrefab, Transform propertySubMenuButtonPrefab)
        {
            PropertyMenuButtonPrefab = propertyMenuButtonPrefab;
            PropertySubMenuButtonPrefab = propertySubMenuButtonPrefab;
        }

        public static void ClearMenu()
        {
            for (int i = 0; i < StaticConstants.RelationURIs.Length; i++)
            {
                string key = StaticConstants.RelationURIs[i];
                ArcMenuItem Ami;
                ArcDataUtility.PropertyMenuItems.TryGetValue(key,out Ami);
                //ArcDataUtility.PropertyMenuItems.Remove(key);
                Ami.transform.gameObject.SetActive(false);
            }
        }

        public static Dictionary<string, ArcMenuItem> CreateMenuButtons()
        {
            Dictionary<string, ArcMenuItem> PropertyMenuItems = new Dictionary<string, ArcMenuItem>();

            for (int i = 0; i < StaticConstants.RelationURIs.Length; i++)
            {
                ArcMenuItem arcCollectionItem = Instantiate(PropertyMenuButtonPrefab, Vector3.zero, Quaternion.identity, PropertyMenu).GetComponent<ArcMenuItem>();
                string label = StaticConstants.relationsNaming[i];
                string key = StaticConstants.RelationURIs[i];

                arcCollectionItem.SetProperty(key, label);
                arcCollectionItem.textToggleIsActive = false;
                arcCollectionItem.index = i;
                arcCollectionItem.transform.name = key;
                arcCollectionItem.key = key;

                PropertyMenuItems.Add(key, arcCollectionItem);
            }
            return PropertyMenuItems;
        }


        public static List<ArcMenuSubItem> CreateSubMenuButtons(ArcMenuItem arcCollectionItem, Property property)
        {
            List<ArcMenuSubItem> subItems = new List<ArcMenuSubItem>();
            //SetRelationsToState(property.Relations, true);
            foreach (Relation rel in property.Relations)
            {
                ArcMenuSubItem item = Instantiate(PropertySubMenuButtonPrefab, PropertySubMenu).GetComponent<ArcMenuSubItem>();
                item.Refresh(rel, property.Label);
                item.arcCollectionItem = arcCollectionItem;
                item.SetActive(rel.isActive);


                subItems.Add(item);

                SubItemController.toggleCounter += 1;
            }
            return subItems;
        }

        internal static List<ArcMenuSubItem> RemoveSubMenuButtons()
        {
            foreach (Transform child in PropertySubMenu)
            {
                Destroy(child.gameObject);
                SubItemController.toggleCounter -= 1;
            }

            return new List<ArcMenuSubItem>();
        }

        public static void UpdatePropertyMenuFromUnitoken(Unitoken unitoken)
        {
            if (unitoken.myPropertiesFromConceptNet == null)
            {
                //Get Properties
                SearchEngine.Instance.GetConceptRelations(unitoken);
            }
            else
            {
                UpdatePropertyMenuFromProperties(unitoken.myPropertiesFromConceptNet);
            }
        }


        public static void UpdatePropertyMenuFromConcept(Unitoken unitoken, Concept concept)
        {

            //Update title in menu to label of unitoken
            PropertyMenuText.text = unitoken.myLabel.text;

            //Update every Collection item count
            Dictionary<string, Property> Properties = ArcDataUtility.GeneratePropertiesContainerFromConcept(concept);

            //Update property menu
            UpdatePropertyMenuFromProperties(Properties);

            //Store Current Properties in unitoken
            unitoken.StoreProperties(Properties);
        }

        public static void UpdatePropertyMenuFromProperties( Dictionary<string, Property> properties)
        {
            foreach (string x in StaticConstants.RelationURIs)
            {
                string key = x;
                Property property;
                ArcMenuItem collectionItem;

                properties.TryGetValue(key, out property);
                bool propertyIsPresent = ArcDataUtility.PropertyMenuItems.TryGetValue(key, out collectionItem);

                int relationCount = property.Relations.Count;
                if(relationCount < 1)
                {
                    collectionItem.transform.gameObject.SetActive(false);
                }
                else
                {
                    collectionItem.transform.gameObject.SetActive(true);
                }
                collectionItem.subItemCount.text = relationCount.ToString();
            }

        }


    }







}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcToolConstants;



public class ArcCollectionToggleMenu : MonoBehaviour
{
    public static ArcCollectionToggleMenu Instance;

    public Transform canvasTransform;

    public ArcCollectionItem propertyMenuButton;

    public static List<ArcCollectionItem> ConceptNetPropertyList;
    public static Dictionary<string, ArcCollectionItem> PropertyDictionary;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ConceptNetPropertyList = new List<ArcCollectionItem>();
        PropertyDictionary = new Dictionary<string, ArcCollectionItem>();

        for(int i = 0; i < StaticConstants.RelationURIs.Length; i++){
            ArcCollectionItem arcCollectionItem = Instantiate(propertyMenuButton, Vector3.zero, Quaternion.identity, canvasTransform);
            string label = StaticConstants.relationsNaming[i];
            string key = StaticConstants.RelationURIs[i];

            arcCollectionItem.SetProperty(key, label);
            arcCollectionItem.isActive = false;
            arcCollectionItem.index = i;
            arcCollectionItem.transform.name = label + " Toggle";

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

    public ArcCollectionItem GetArcCollectionItem(string key){
        ArcCollectionItem val;
        if(!PropertyDictionary.TryGetValue(key, out val)){
            throw new KeyNotFoundException();
        }
        return val;
    }

    public ArcCollectionItem GetArcCollectionItem(ArcCollectionSubItem subItem)
    {
        ArcCollectionItem val;
        if (!PropertyDictionary.TryGetValue(subItem.key, out val))
        {
            throw new KeyNotFoundException();
        }
        return val;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

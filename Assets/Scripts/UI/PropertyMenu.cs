using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyMenu : MonoBehaviour
{
    public static PropertyMenu Instance;
    public PropertyContainer PropertyContainer;

    public ConceptNetProperty propertyMenuButton;

    public static List<ConceptNetProperty> ConceptNetPropertyList;
    public static Dictionary<string, ConceptNetProperty> PropertyDictionary;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ConceptNetPropertyList = new List<ConceptNetProperty>();
        PropertyDictionary = new Dictionary<string, ConceptNetProperty>();

        int count = 0;

        foreach(string x in PropertyContainer.relationURIs){
            ConceptNetProperty y = Instantiate(propertyMenuButton, Vector3.zero, Quaternion.identity, transform);
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


    public ConceptNetProperty GetProperty(string property){
        ConceptNetProperty p;
        PropertyContainer.propertyDictionary.TryGetValue(property, out p);
        return p;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

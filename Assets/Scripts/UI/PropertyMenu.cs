using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyMenu : MonoBehaviour
{
    public static string[] relationsNaming = {"is related to ","External URL ","form of ","is a ","part of ","has a ","used for ","capable of ","at location ","causes ","has subevent ","has first subevent ","has last subevent ","has prerequisite ","has property ","motivated by goal ","obstructed by ","desires ","created by ","synonym ","antonym ","distinct from ","derived from ","symbol of ","defined as ","entails ","manner of ","located near ","has context ","similar to ","etymologically related to ","etymologically derived from ","causes desire ","made of ","receives action ","instance of "};

    public PropertyContainer PropertyContainer;

    public ConceptNetProperty propertyMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string x in relationsNaming){
            ConceptNetProperty y = Instantiate(propertyMenuButton, Vector3.zero, Quaternion.identity, transform);
            y.SetProperty(x);
            y.isActive = false;
            //properties.Add(x, y);
        }
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyMenu : MonoBehaviour
{
    public PropertyContainer PropertyContainer;

    public ConceptNetProperty propertyMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string x in PropertyContainer.relationURIs){
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

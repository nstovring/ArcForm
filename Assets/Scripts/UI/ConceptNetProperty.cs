using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConceptNetProperty : MonoBehaviour
{
    public string propertyType;

    public bool isActive;
    public Text textField;
    public Button button;
    public void SetProperty(string p){
        propertyType = p;
        textField.text = p;
    }

}

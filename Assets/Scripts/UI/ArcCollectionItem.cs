using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcCollectionItem : MonoBehaviour
{
    public string propertyType;

    public bool isActive;
    public Text textField;
    public Button button;
    public int index;
    

    public void SetProperty(string p){
        propertyType = p;
        textField.text = p;
    }

    void Start(){
        button.onClick.AddListener(delegate{
            isActive = !isActive;
            ArcCollectionToggleMenu.Instance.SetFilter(index, isActive);
            Debug.Log("Toggled State for: " + propertyType + " : " + isActive + " In Button");
        });
    }

}

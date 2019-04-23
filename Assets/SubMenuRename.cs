using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuRename : MonoBehaviour
{
    public string propertyType;
    public Text textField;
    // Start is called before the first frame update
    
    public void SetProperty(string p){
        propertyType = p;
        textField.text = p;
    }
    void Start()
    {
        SetProperty("Some Name");
        
    }

    // Update is called once per frame
   

    
}

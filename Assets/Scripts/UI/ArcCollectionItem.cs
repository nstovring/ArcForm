using System;
using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using UnityEngine.UI;

public class ArcCollectionItem : MonoBehaviour
{
    public string propertyType;

    public bool isActive;
    public bool subMenuIsActive = false;

    public Text textField;
    public Button togglebutton;
    public Button toggleSubMenuButton;
    public int index;

    public Text toggleSubMenuTextField;

    public List<ArcCollectionSubItem> subItems;

    public void SetProperty(string p){
        propertyType = p;
        textField.text = p;
    }

    void Start(){
        toggleSubMenuTextField.text = 0.ToString();

        togglebutton.onClick.AddListener(delegate{
            isActive = !isActive;
            ArcCollectionToggleMenu.Instance.SetFilter(index, isActive);

            //UIFactory.Instance.AddSubItem(subItems);

            Debug.Log("Toggled State for: " + propertyType + " : " + isActive + " In Button");
        });

         toggleSubMenuButton.onClick.AddListener(delegate{
            subMenuIsActive = !subMenuIsActive;
            //ArcCollectionToggleMenu.Instance.SetFilter(index, isActive);
            if(subMenuIsActive){
                UIFactory.Instance.AddSubItem(subItems);
            }else{
                UIFactory.Instance.Clear();
            }

            Debug.Log("Toggled SubMenu for: " + propertyType + " : " + isActive + " In Button");
        });
    }

    internal void Fill(List<Edge> edgelist)
    {
        if(subItems == null){
            subItems = new List<ArcCollectionSubItem>();
        }
        int count = 0;
        foreach(Edge x in edgelist){
            ArcCollectionSubItem y = new ArcCollectionSubItem(x);
            subItems.Add(y);
            toggleSubMenuTextField.text = count++.ToString();
            //UIFactory.Instance.AddSubItem(y);
            Debug.Log("SubItem: " + x + " Added");
        }
    }
}

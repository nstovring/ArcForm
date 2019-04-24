using System;
using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using UnityEngine.UI;
using ArcToolConstants;
using StructureContainer;
public class ArcMenuItem : MonoBehaviour
{
    public string key;

    public bool textToggleIsActive;
    public bool numToggleIsActive;

    public Text textField;
    public Button togglebutton;
    public Button toggleSubMenuButton;
    public int index;

    public Text subItemCount;
    public ArcCollection myArcCollection;

    public List<ArcMenuSubItem> subItems;

    public void SetProperty(string p, string label){
        key = p;
        textField.text = label;
    }

    void Start(){
        subItems = new List<ArcMenuSubItem>();
        subItemCount.text = 0.ToString();

        togglebutton.onClick.AddListener(delegate{
            textToggleIsActive = !textToggleIsActive;

            ArcToolUIManager.Instance.ToggleMenuItem(this, textToggleIsActive, numToggleIsActive);

            Debug.Log("Toggled State for: " + key + " : " + textToggleIsActive + " In Button");
        });

         toggleSubMenuButton.onClick.AddListener(delegate{
             textToggleIsActive = !textToggleIsActive;

             ArcToolUIManager.Instance.ToggleMenuItem(this, textToggleIsActive, numToggleIsActive);

             Debug.Log("Toggled SubMenu for: " + key + " : " + numToggleIsActive + " In Button");
        });
    }


    internal void Fill(List<Edge> edgelist)
    {
        if(subItems == null){
            subItems = new List<ArcMenuSubItem>();
        }
        int count = 0;
        foreach(Edge x in edgelist){
            ArcMenuSubItem y = new ArcMenuSubItem(x);
            subItems.Add(y);
            subItemCount.text = count++.ToString();
            //UIFactory.Instance.AddSubItem(y);
            Debug.Log("SubItem: " + x + " Added");
        }
    }

    public bool hasActiveSubItem()
    {
        bool b;
        foreach(ArcMenuSubItem item in subItems)
        {
            if (item.isActive)
            {
                return true;
            }
        }
        return false;
    }

    internal bool hasOtherActiveSubItem(ArcMenuSubItem arcCollectionSubItem)
    {
        bool b;
        foreach (ArcMenuSubItem item in subItems)
        {
            if (item != arcCollectionSubItem && item.isActive)
            {
                return true;
            }
        }
        return false;
    }
}

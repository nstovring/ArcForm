﻿using System;
using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using UnityEngine.UI;
using ArcToolConstants;
using StructureContainer;
public class ArcCollectionItem : MonoBehaviour
{
    public string key;

    public bool isActive;
    public bool subMenuIsActive = false;

    public Text textField;
    public Button togglebutton;
    public Button toggleSubMenuButton;
    public int index;

    public Text subItemCount;
    public ArcCollection myArcCollection;

    public List<ArcCollectionSubItem> subItems;

    public void SetProperty(string p, string label){
        key = p;
        textField.text = label;
    }

    void Start(){
        subItems = new List<ArcCollectionSubItem>();
        subItemCount.text = 0.ToString();

        togglebutton.onClick.AddListener(delegate{
            isActive = !isActive;

            ArcToolUIManager.Instance.ToggleMenuItem(this);

            Debug.Log("Toggled State for: " + key + " : " + isActive + " In Button");
        });

         toggleSubMenuButton.onClick.AddListener(delegate{
             subMenuIsActive = !subMenuIsActive;

             ArcToolUIManager.Instance.ToggleSubMenu(this);

             Debug.Log("Toggled SubMenu for: " + key + " : " + subMenuIsActive + " In Button");
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
            subItemCount.text = count++.ToString();
            //UIFactory.Instance.AddSubItem(y);
            Debug.Log("SubItem: " + x + " Added");
        }
    }
}

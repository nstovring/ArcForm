﻿using System;
using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using UnityEngine.UI;
using ArcToolConstants;
using StructureContainer;
using System.Linq;

public class ArcMenuItem : MonoBehaviour
{
    [Header("Settings")]
    public string key;
    public int index;
    public bool textToggleIsActive;
    public bool numToggleIsActive;
    public bool submenuEdited;
    public ButtonStateHandler buttonStateHandler;
   

    [Header("UI Refs")]
    public Text textField;
    public Button togglebutton;
    public Button toggleSubMenuButton;
    public Image myButtonImage;
    public Text subItemCount;

    [Header("Misc")]
    public ArcCollection myArcCollection;
    public List<ArcMenuSubItem> subItems;
    public List<Relation> relations;

    public void SetProperty(string p, string label){
        key = p;
        textField.text = label;
    }

    void Start(){
        subItems = new List<ArcMenuSubItem>();
        relations = new List<Relation>();
        buttonStateHandler.SetToDefault();

        togglebutton.onClick.AddListener(delegate{
            textToggleIsActive = !textToggleIsActive;
            ArcToolUIManager.Instance.ToggleMenuItem(this, textToggleIsActive);
            DataLogger.Instance.LogToggle(this);
        });

         toggleSubMenuButton.onClick.AddListener(delegate{
             //textToggleIsActive = !textToggleIsActive;
             ArcToolUIManager.Instance.ToggleSubMenu(this);
         
             DataLogger.Instance.LogToggle(this);
         });
    }

    public void UpdateState()
    {

    }

    void Update()
    {
        //if(relations != null)
        //{
        //    if(relations.Any(x => x.isActive == true))
        //    {
        //        buttonStateHandler.SetToEdited();
        //    }
        //}
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

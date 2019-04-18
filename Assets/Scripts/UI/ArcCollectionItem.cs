using System;
using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using UnityEngine.UI;
using ArcToolConstants;

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
        subItems = new List<ArcCollectionSubItem>();
        toggleSubMenuTextField.text = 0.ToString();

        togglebutton.onClick.AddListener(delegate{
            StartCoroutine(OnToggle());
            //UIFactory.Instance.AddSubItem(subItems);

            Debug.Log("Toggled State for: " + propertyType + " : " + isActive + " In Button");
        });

         toggleSubMenuButton.onClick.AddListener(delegate{
            subMenuIsActive = !subMenuIsActive;
            //ArcCollectionToggleMenu.Instance.SetFilter(index, isActive);
            if(subMenuIsActive){
                UIFactory.Instance.AddItemToSubMenu(subItems, propertyType);
            }else{
                UIFactory.Instance.Clear();
            }

            Debug.Log("Toggled SubMenu for: " + propertyType + " : " + isActive + " In Button");
        });
    }

    IEnumerator OnToggle()
    {
        isActive = !isActive;
        if (isActive)
        {
            ArcCollectionToggleMenu.Instance.SetFilter(index, isActive);
            ArcCollection cd =  ArcCollectionFactory.Instance.AddNewCollection(ArcMapManager.Instance.focusedToken, StaticConstants.RelationURIs[index], subItems);
            ArcMapManager.Instance.unitokens.Add(cd);
            ArcMapManager.Instance.SetFocusedCollection(cd);
            yield return null;
        }
        else
        {
            ArcCollectionFactory.Instance.DestroyArcCollection(ArcMapManager.Instance.GetFocusedCollection());
        }
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

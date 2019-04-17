using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFactory : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIFactory Instance;
    public ArcCollectionToggleMenu ArcCollectionToggleMenu;
    public Transform ArcCollectionSubMenuLayout;

    public ArcCollectionSubItem subItemPrefab;

    public List<ArcCollectionSubItem> subItems;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AddSubItem(List<ArcCollectionSubItem> y)
    {
        if(subItems == null)
        subItems = new List<ArcCollectionSubItem>();


        foreach(ArcCollectionSubItem x in y){
            ArcCollectionSubItem z = Instantiate(subItemPrefab, Vector3.zero, Quaternion.identity, ArcCollectionSubMenuLayout);
            z.Refresh(x);
            subItems.Add(z);
        }
        //throw new NotImplementedException();
    }

    public void SetSubItem(){

    }

    internal void Clear()
    {
        if(subItems != null)
        foreach(ArcCollectionSubItem x in subItems){
            Destroy(x.gameObject);
        }
        subItems = new List<ArcCollectionSubItem>();
    }
}

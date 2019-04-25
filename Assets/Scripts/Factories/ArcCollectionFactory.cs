using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcToolConstants;
using StructureContainer;

public class ArcCollectionFactory : MonoBehaviour
{
    public Transform collectionPrefab;
    public static ArcCollectionFactory Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public ArcCollection AddNewCollection(Unitoken source, string topic, List<ArcMenuSubItem> subItems)
    {
        //Create Collection token
        ArcCollection ac = Instantiate(collectionPrefab, source.transform.position + StaticConstants.rngVector(), Quaternion.identity).GetComponent<ArcCollection>();
        ac.SetLabel(topic);

        //Add items to collection
        foreach(ArcMenuSubItem x in subItems)
        {
            x.SetActive(true);
            ac.AddToCollection(x);
        }

        //Link Collection to source
        Arc a = ArcFactory.Instance.AddNewArc(source, " ", ac);
        a.transform.parent = ac.transform;
        
        ArcMapManager.Instance.unitokens.Add(ac);
        ArcMapManager.Instance.SetFocusedCollection(ac);
        return ac;
    }


    public ArcCollection AddNewCollection(Unitoken source, string topic, ArcMenuSubItem subItem)
    {
        //Create Collection token
        ArcCollection ac = Instantiate(collectionPrefab, source.transform.position + StaticConstants.rngVector(), Quaternion.identity).GetComponent<ArcCollection>();
        ac.SetLabel(topic);

        //Add items to collection
        ac.AddToCollection(subItem);

        //Link Collection to source
        Arc a = ArcFactory.Instance.AddNewArc(source, " ", ac);
        a.transform.parent = ac.transform;

        return ac;
    }

   

    internal IEnumerator PlaceCollectionOnMap(string topic, List<ArcMenuSubItem> subItems)
    {
        throw new NotImplementedException();
    }

    internal void DestroyArcCollection(ArcCollection myArcCollection)
    {
        ArcMapManager.Instance.unitokens.Remove(myArcCollection);
        ArcMapManager.Instance.DestroyCollection(myArcCollection);
        
        //throw new NotImplementedException();
    }
}

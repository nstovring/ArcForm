using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcToolConstants;
using StructureContainer;
using System.Linq;
using static ArcMapGrid;

public class ArcCollectionFactory : Factory
{
    public Transform collectionPrefab;
    public static ArcCollectionFactory Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public ArcCollection AddNewCollection(Unitoken source, string topic, ArcMenuSubItem subItem)
    {
        Vector3 foundPosition;
        List<GridCell> cells = ArcMapGrid.Instance.FindEmptySpot(source.TransientPosition, 3, out foundPosition);
        //Create Collection token
        ArcCollection ac = Instantiate(collectionPrefab, foundPosition, Quaternion.identity).GetComponent<ArcCollection>();
        ac.MyCells = cells;
        ac.Source = source;
        ac.SetLabel(StaticConstants.KeyToLabel[topic]);

        //Add items to collection
        ac.AddToCollection(subItem);
        ArcMapManager.Instance.AddTokenToList(ac);

        //Link Collection to source
        Arc a = ArcFactory.Instance.AddNewArc(source, " ", ac);
        a.transform.parent = ac.transform;

        return ac;
    }

  
    internal void DestroyArcCollection(ArcCollection myArcCollection)
    {
        ArcMapManager.Instance.unitokens.Remove(myArcCollection);
        ArcMapManager.Instance.DestroyCollection(myArcCollection);
    }
}

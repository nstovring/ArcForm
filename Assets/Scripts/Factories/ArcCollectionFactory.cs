using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcToolConstants;
public class ArcCollectionFactory : MonoBehaviour
{
    public Transform collectionPrefab;
    public static ArcCollectionFactory Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public ArcCollection AddNewCollection(Unitoken source, string topic, List<ArcCollectionSubItem> subItems)
    {
        ArcCollection ac = Instantiate(collectionPrefab, source.transform.position + StaticConstants.rngVector(), Quaternion.identity).GetComponent<ArcCollection>();
        ac.SetLabel(topic);

        foreach(ArcCollectionSubItem x in subItems)
        {
            Unitoken target = TokenFactory.Instance.AddNewToken(x.edge.End.Label, ac.transform.position + StaticConstants.rngVector());
            Arc arc = ArcFactory.Instance.AddNewArc(ac, " ", target);
            arc.SetLabel(" ");

            target.transform.parent = arc.transform;
            arc.transform.parent = ac.transform;

            ac.myArcs.Add(arc);
        }

        Arc a = ArcFactory.Instance.AddNewArc(source, " ", ac);
        a.transform.parent = ac.transform;

        return ac;
    }

    internal IEnumerator PlaceCollectionOnMap(string topic, List<ArcCollectionSubItem> subItems)
    {
        throw new NotImplementedException();
    }

    internal void DestroyArcCollection(ArcCollection myArcCollection)
    {
        throw new NotImplementedException();
    }
}

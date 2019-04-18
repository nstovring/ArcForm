using ConceptNetJsonHolder;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xml2CSharp;

public class UIFactory : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIFactory Instance;
    public ArcCollectionToggleMenu ArcCollectionToggleMenu;
    public Transform ArcCollectionSubMenuLayout;

    public ArcCollectionSubItem subItemPrefab;

    public List<ArcCollectionSubItem> subItems;

    public string currentTopic;
    void Start()
    {
        Instance = this;
    }


    public void GeneratePreviewFromConcept(Unitoken core, Concept concept)
    {

        StartCoroutine(GenerateEdgesInMenu(core, concept));

        Debug.Log("Generating Concept Edges");
    }



    public void GeneratePreviewFromXML(Unitoken core, Result result)
    {
        Debug.Log("Generating Class & Category Edges");
        // StartCoroutine(GenerateClasses(core, result));

    }

    public IEnumerator GenerateEdgesInMenu(Unitoken core, Concept concept)
    {
        List<List<Edge>> ConceptEdges = CountEdgePropertyTypes(concept);
        string[] relations = ConceptNetInterface.relationURIs;

        int count = 0;
        foreach (List<Edge> edgelist in ConceptEdges)
        {
            if (edgelist.Count > 0)
            {
                string relation = relations[count];

                //Check if label is within toggled array
                ArcCollectionItem arcCollectionItem = ArcCollectionToggleMenu.GetArcCollectionItem(relation);

                Unitoken.UnitokenState state = !arcCollectionItem.isActive ? Unitoken.UnitokenState.Preview : Unitoken.UnitokenState.Loaded;

                arcCollectionItem.Fill(edgelist);

                yield return new WaitForSeconds(0.1f);
            }

            count++;
            yield return new WaitForSeconds(0.1f);
        }
    }


    internal void AddItemToSubMenu(List<ArcCollectionSubItem> y, string topic)
    {
        currentTopic = topic;

        if (subItems == null)
        subItems = new List<ArcCollectionSubItem>();

        //ArcCollection arcCollection = new ArcCollection();
        //arcCollection.

        foreach (ArcCollectionSubItem x in y){
            ArcCollectionSubItem z = Instantiate(subItemPrefab, Vector3.zero, Quaternion.identity, ArcCollectionSubMenuLayout);
            z.Refresh(x, topic);
            subItems.Add(z);
        }
        //throw new NotImplementedException();
    }


    internal void Clear()
    {
        if(subItems != null)
        foreach(ArcCollectionSubItem x in subItems){
            Destroy(x.gameObject);
        }
        subItems = new List<ArcCollectionSubItem>();
    }



    public List<List<Edge>> CountEdgePropertyTypes(Concept concept)
    {
        int[] count = new int[ConceptNetInterface.relationURIs.Length];
        string[] relations = ConceptNetInterface.relationURIs;
        List<List<Edge>> ConceptEdges = new List<List<Edge>>();

        for (int j = 0; j < relations.Length; j++)
        {
            string type = relations[j];
            ConceptEdges.Add(new List<Edge>());
        }


        foreach (Edge x in concept.Edges)
        {
            for (int i = 0; i < count.Length; i++)
            {
                string type = relations[i];
                if (x.Id.Contains(type))
                {
                    //Debug.Log(x.Id);
                    ConceptEdges[i].Add(x);
                }
            }
        }

        return ConceptEdges;
    }

}

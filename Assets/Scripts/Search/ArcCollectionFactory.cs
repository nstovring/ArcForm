using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using Xml2CSharp;

public class ArcCollectionFactory : MonoBehaviour
{
    public static ArcCollectionFactory Instance;
    public Sprite collectionIconSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void GeneratePreviewFromConcept(Unitoken core, Concept concept){
        
        StartCoroutine(GenerateEdges(core, concept));

        Debug.Log("Generating Concept Edges");
    }



    public void GeneratePreviewFromXML(Unitoken core, Result result){
        Debug.Log("Generating Class & Category Edges");
       // StartCoroutine(GenerateClasses(core, result));
     
    }

   // public IEnumerator GenerateClasses(Unitoken core, Result result){
   //     foreach(Category x in result.Categories.Category){
   //         Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
   //         Unitoken target  = TokenFactory.Instance.AddNewToken(x.Label, core.transform.position + rngVector);
   //         Arc arc = ArcFactory.Instance.AddNewArc(core, "in category", target);
   //         yield return new WaitForSeconds(0.1f);
   //     }
   // }

  


    public IEnumerator GenerateEdges(Unitoken core, Concept concept){
        List<List<Edge>> ConceptEdges = CountEdgePropertyTypes(concept);
        string[] relations = ConceptNetInterface.relationURIs;

        //Give core a branch
        ArcCollection coreCollection = new ArcCollection();
        coreCollection.Initialize();
        core.AddCollection(coreCollection);
        

        int count = 0;
        foreach(List<Edge> edgelist in ConceptEdges){
            if(edgelist.Count > 0){
                string relation = relations[count];

                //Check if label is within toggled array
                ArcCollectionItem arcCollectionItem = ArcCollectionToggleMenu.GetArcCollectionItem(relation);

                Unitoken.UnitokenState state = !arcCollectionItem.isActive ? Unitoken.UnitokenState.Preview : Unitoken.UnitokenState.Loaded;

                arcCollectionItem.Fill(edgelist);


                //Unitoken newCore  = TokenFactory.Instance.AddNewToken(edgeUnitokenLabel, core.transform.position + rngVector());
                //newCore.SetState(state);
                //newCore.SetSprite(collectionIconSprite);
                //ArcMapManager.Instance.SetFocusedToken(newCore);
//
                //Arc arc = ArcFactory.Instance.AddNewArc(core,"", newCore);
//
//
                //ArcCollection subBranch = new ArcCollection();
                //subBranch.SetCore(newCore);
                //foreach(Edge edge in edgelist){
                //    subBranch.AddEdge(edge);
                //}
//
                //coreCollection.AddConnection(subBranch);
                //yield return StartCoroutine(SpawnEdges(edgelist, newCore, edgeUnitokenLabel, state));
                yield return new WaitForSeconds(0.1f);
            }

            count ++;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static Vector3 rngVector(){
        return new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
    }

 

    IEnumerator SpawnEdges(List<Edge> edgelist, Unitoken core, string type, Unitoken.UnitokenState state){
        foreach(Edge x in edgelist){

            Unitoken target  = TokenFactory.Instance.AddNewToken(x.End.Label, core.transform.position + rngVector());
            target.SetState(state);
            target.isSoft = false;
            Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
        }
        yield return new WaitForSeconds(1.5f);
    }

    public List<List<Edge>> ConceptEdges;

    public List<List<Edge>> CountEdgePropertyTypes(Concept concept){
        int[] count = new int[ConceptNetInterface.relationURIs.Length];
        string[] relations = ConceptNetInterface.relationURIs;
        ConceptEdges = new List<List<Edge>>();

        for(int j = 0; j< relations.Length; j++){
                string type = relations[j];
                ConceptEdges.Add(new List<Edge>());
        }

    
        foreach(Edge x in concept.Edges){
            for(int i = 0; i< count.Length; i++){
                string type = relations[i];
                if(x.Id.Contains(type)){
                    //Debug.Log(x.Id);
                    ConceptEdges[i].Add(x);
                }
            }
        }

        return ConceptEdges;
    }

    public string ExtractNaturalLanguage(string x){
        return null;
    }

 
}

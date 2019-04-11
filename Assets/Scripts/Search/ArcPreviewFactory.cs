using System.Collections;
using System.Collections.Generic;
using ConceptNetJsonHolder;
using UnityEngine;
using Xml2CSharp;

public class ArcPreviewFactory : MonoBehaviour
{
    public static ArcPreviewFactory Instance;
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

    public IEnumerator GenerateClasses(Unitoken core, Result result){
        foreach(Category x in result.Categories.Category){
            Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
            Unitoken target  = TokenFactory.Instance.AddNewToken(x.Label, core.transform.position + rngVector);
            Arc arc = ArcFactory.Instance.AddNewArc(core, "in category", target);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator GenerateEdges(Unitoken core, Concept concept){
        List<List<Edge>> ConceptEdges = CountEdgePropertyTypes(concept);
        string[] relations = ConceptNetInterface.relationURIs;
        Debug.Log(ConceptEdges.Count);

        int count = 0;
        foreach(List<Edge> edgelist in ConceptEdges){
            //Debug.Log(edgelist.Count);
            if(edgelist.Count > 0){
                Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
                string edgeUnitokenLabel = relations[count];
                string type = relations[count];
                Unitoken newCore  = TokenFactory.Instance.AddNewToken(edgeUnitokenLabel, core.transform.position + rngVector);
                Arc arc = ArcFactory.Instance.AddNewArc(core,"", newCore);
                yield return new WaitForSeconds(3f);

                yield return StartCoroutine(SpawnEdges(edgelist, newCore, type));

                   

            }

            count ++;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator SpawnEdges(List<Edge> edgelist, Unitoken core, string type){
        foreach(Edge x in edgelist){

            Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
            Unitoken target  = TokenFactory.Instance.AddNewToken(x.End.Label, core.transform.position + rngVector);
            Arc arc = ArcFactory.Instance.AddNewArc(core, "", target);
        }
        yield return new WaitForSeconds(0.5f);
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
                    Debug.Log(x.Id);
                    ConceptEdges[i].Add(x);
                }
            }
        }

        return ConceptEdges;
    }

    public string ExtractNaturalLanguage(string x){
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

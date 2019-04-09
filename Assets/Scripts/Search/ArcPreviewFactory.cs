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
         foreach(Edge x in concept.Edges){
            Vector3 rngVector = new Vector3(Random.Range(-2.0f, 2.0f),Random.Range(-2.0f, 2.0f));
            Unitoken target  = TokenFactory.Instance.AddNewToken(x.End.Label, core.transform.position + rngVector);
            Arc arc = ArcFactory.Instance.AddNewArc(core, x.Rel.Label, target);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

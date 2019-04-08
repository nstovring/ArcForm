using System.Collections;
using System.Collections.Generic;
using JsonLD.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
using ConceptNetJsonHolder;
public class ConceptNetInterface : MonoBehaviour
{
    public static ConceptNetInterface Instance;

    public void Start(){
        Instance = this;
    }

    public static string[] relationURIs = {"/r/RelatedTo", "/r/CapableOf", "/r/HasProperty", "/r/dbpedia/knownFor"};
    public static string ConceptNetRequestBuilder(string subject, int requestLimit){
         string limit = "limit=" + requestLimit;
         string relationUri = relationURIs[3];
         string test = "http://api.conceptnet.io/query?start=/c/en/"+subject.Replace(" ", "_").ToLower()+"&rel="+relationUri+"&"+limit;

         return test;

     }

    public static void GetRelations(string subject, SearchEngine asker, int requestLimit){
        //mySubject = subject;
        string request = ConceptNetRequestBuilder(subject, requestLimit);
        asker.StartCoroutine(ProcessRequest(request, asker));
     }

    public static IEnumerator ProcessRequest(string url, SearchEngine asker)
     {
         UnityWebRequest myWr = UnityWebRequest.Get(url);
         yield return myWr.SendWebRequest();
          if(myWr.isNetworkError || myWr.isHttpError) {
            Debug.Log(myWr.error);
        }
        else {
            Concept concept = DeSerializeJSON(myWr.downloadHandler.text);
            asker.ReceiveConcept(concept);
        }  
     }

     public static Concept DeSerializeJSON(string jsonString)
     {
         
         Debug.Log(jsonString);
         
         var json = JObject.Parse(jsonString);

        // when
        IEntitySerializer serializer = new EntitySerializer(new StaticContextProvider());
        var person = serializer.Deserialize<Concept>(json);
      
         
        return person;
     }
}

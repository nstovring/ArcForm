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

    public static string[] relationURIs = {"/r/RelatedTo", "/r/ExternalURL", "/r/FormOf", "/r/IsA", "/r/PartOf", "/r/HasA" , "/r/UsedFor", "/r/CapableOf", "/r/AtLocation", "/r/Causes", "/r/HasSubevent", "/r/HasFirstSubevent", "/r/HasLastSubevent", "/r/HasPrerequisite", "/r/HasProperty", "/r/MotivatedByGoal", "/r/ObstructedBy", "/r/Desires", "/r/CreatedBy", "/r/Synonym", "/r/Antonym", "/r/DistinctFrom", "/r/DerivedFrom", "/r/SymbolOf", "/r/DefinedAs", "/r/Entails", "/r/MannerOf", "/r/LocatedNear", "/r/HasContext", "/r/SimilarTo", "/r/EtymologicallyRelatedTo", "/r/EtymologicallyDerivedFrom", "/r/CausesDesire", "/r/MadeOf", "/r/ReceivesAction", "/r/InstanceOf"};
    Dictionary<string, string> ConceptNetRelations = new Dictionary<string, string>();

     public static string ConceptNetRequestBuilder(string subject, int requestLimit){
         string limit = "&"+"limit=" + requestLimit;
         string relationUri = "";//"&rel=" + relationURIs[0] + "&rel=" + relationURIs[1] + "&rel=" + relationURIs[3];
         string test = "http://api.conceptnet.io/query?start=/c/en/"+subject.Replace(" ", "_").ToLower()+relationUri+limit;

         return test;

     }

    public static void GetConceptRelations(string subject, SearchEngine asker, int requestLimit){
        string request = ConceptNetRequestBuilder(subject, requestLimit);
        Debug.Log("Concept Net Request : " +request);
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
            asker.ReceiveConceptAndFillToggle(concept);
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

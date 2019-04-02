using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
public class ConceptNetInterface : MonoBehaviour
{
     IEnumerator Start()
     {
         string url = "http://api.conceptnet.io/c/en/example";
         //UnityWebRequest www = new UnityWebRequest(url);
         //DownloadHandler handler = www.downloadHandler;
         UnityWebRequest myWr = UnityWebRequest.Get(url);
         yield return myWr.SendWebRequest();
          if(myWr.isNetworkError || myWr.isHttpError) {
            Debug.Log(myWr.error);
        }
        else {
            // Show results as text
            Processjson(myWr.downloadHandler.text);
 
            // Or retrieve results as binary data
            byte[] results = myWr.downloadHandler.data;
        }  
     }
     //     [System.Ser]
     public JSONOBject myObject;    
     private void Processjson(string jsonString)
     {
         Debug.Log(jsonString);
         JSONOBject newObject = JsonUtility.FromJson<JSONOBject>(jsonString);
         myObject = newObject;
         
         //parseJSON parsejson;
        // parsejson = new parseJSON();
         //parsejson.title = jsonvale["title"].ToString();
         //parsejson.id = jsonvale["ID"].ToString();
         
         //parsejson.but_title = new ArrayList ();
         //parsejson.but_image = new ArrayList ();
         
         //for(int i = 0; i<jsonvale["buttons"].Count; i++)
         //{
         //    parsejson.but_title.Add(jsonvale["buttons"][i]["title"].ToString());
         //    parsejson.but_image.Add(jsonvale["buttons"][i]["image"].ToString());
         //}    
     }
    [System.Serializable]
     public class JSONOBject{
         [SerializeField]
         [JsonProperty("@context")] public string @context, view,  @id, edges;
     }
}

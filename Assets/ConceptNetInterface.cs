using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class ConceptNetInterface : MonoBehaviour
{
     IEnumerator Start()
     {
         string url = " URL of the JSON to be Decode";
         WWW www = new WWW(url);
         yield return www;
         if (www.error == null)
         {
             //Processjson(www.data);
         }
         else
         {
             Debug.Log("ERROR: " + www.error);
         }        
     }    
     private void Processjson(string jsonString)
     {
         //JsonUtility.FromJson<>(jsonString);
         //parseJSON parsejson;
         //parsejson = new parseJSON();
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
}

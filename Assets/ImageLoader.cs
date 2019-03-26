using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public string url = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f2/Nikolaus_Kopernikus.jpg/300px-Nikolaus_Kopernikus.jpg";
    public Renderer thisRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thisRenderer.enabled = false;
        StartCoroutine(GetTexture());
        
        //The following will be called even before the load finished.
        //thisRenderer.material.color = new Color(255,0,0,0);
        
    }

    private IEnumerator GetTexture()
    {
        Debug.Log("Loading...");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);   //create www object pointing to url
        yield return www.SendWebRequest();                              //start load whatever in that url (Delay happens here)
        Texture myTexture;
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            yield return null;
        }
        
        myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

        Debug.Log("Loaded");
        thisRenderer.material.color = Color.white; // set white
        thisRenderer.material.mainTexture = myTexture; // set loaded text
        thisRenderer.enabled = true;

    }
}

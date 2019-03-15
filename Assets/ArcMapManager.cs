using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ArcMapManager : MonoBehaviour
{
    public static ArcMapManager Instance;
    public static List<Unitoken> unitokens;

    public TMP_InputField inputField;

    public float linePadding = 0.2f;
    public float mapScale = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        unitokens = new List<Unitoken>();
    }

    public void ShowInputField(Vector3 worldPos, ILabelable asker){

        inputField.transform.gameObject.SetActive(true);

        inputField.onEndEdit.RemoveAllListeners();


        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        RectTransform rectTransform =  inputField.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        inputField.onEndEdit.AddListener(delegate{
            asker.SetLabel(inputField.text);
            inputField.transform.gameObject.SetActive(false);
        });

    }

    public void GenerateOctree(){

    }

    public static bool AddToken(Unitoken token){
        if(!unitokens.Contains(token)){
            unitokens.Add(token);
            return true;
        }else{
            Debug.Log("Failed to add token");
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

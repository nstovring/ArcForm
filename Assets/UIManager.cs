using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
     public TMP_InputField inputField;

     public Transform searchBox;
     public Transform infoBox;
    void Start()
    {
        Instance = this;
        if(searchBox == null)
            throw new UnassignedReferenceException();
        if(infoBox == null)
            throw new UnassignedReferenceException();
    }
//    private bool searchBoxToggle = false;
    public void ToggleSearchBox(bool state){
        searchBox.transform.gameObject.SetActive(state);
    }

    public void ToggleInfoBox(bool state){
        infoBox.transform.gameObject.SetActive(state);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

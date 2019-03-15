using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts
    public static ArcMapManager Instance;
    public enum ThoughtType {Unitoken, Arc, Thought};
    public static List<Unitoken> unitokens;
    public Transform unitokenPrefab;
    public Transform joinArcPrefab;
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

    public void CreateToken(){

    }

    public Thought CreateThought(ThoughtType type){
        switch(type){
            case ThoughtType.Unitoken:
            //Request info to create new Unitoken
            break;
            case ThoughtType.Arc:
            break;
            case ThoughtType.Thought:
            break;
        }
    }
    public void CreateThought(Thought source, Thought target){

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

    
    public void AddNewToken(Unitoken source){
        source.tokenRotation = (360.0f/(source.myArcs.Count + 1.0f));
        Vector3 rotationVector = new Vector3(Mathf.Sin(source.tokenRotation * Mathf.Deg2Rad), Mathf.Cos(source.tokenRotation * Mathf.Deg2Rad), 0);
        Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        Unitoken newToken = Instantiate(unitokenPrefab, transform.position + offset, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize();

        CreateJoinArc(source, newToken);
    }

   
    void CreateJoinArc(Unitoken source, Unitoken target){
        JoinArc newJoinArc = Instantiate(joinArcPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<JoinArc>();
        newJoinArc.Initialize(source,target);

        AddArc(newJoinArc);
    }

    public void AddArc(Arc arc){
        throw new MissingReferenceException();
        //myArcs.Add(arc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

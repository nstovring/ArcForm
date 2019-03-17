using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ArcMapManager : MonoBehaviour
{
    //This class is responsible for creating Tokens, Arcs and Thoughts
    public static ArcMapManager Instance;
    public static List<Unitoken> unitokens;
    public Transform unitokenPrefab;
    public Transform joinArcPrefab;
    public TMP_InputField inputField;

    public float linePadding = 0.2f;
    public float mapScale = 2.0f;

    Camera mCamera;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        unitokens = new List<Unitoken>();
        mCamera = Camera.main;
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


    public static bool AddToken(Unitoken token){
        if(!unitokens.Contains(token)){
            unitokens.Add(token);
            return true;
        }else{
            Debug.Log("Failed to add token");
            return false;
        }
    }
    public void OnClickAddNewToken(){
        AddNewToken();
    }
    public Unitoken AddNewToken(){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        //transform.position = mouseDelta;


        Unitoken newToken = Instantiate(unitokenPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize();

        return newToken;
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


   public void AddNewArc(Unitoken source){
       Unitoken target = AddNewToken();
       Arc arc = CreateJoinArc(source,target);
       source.transform.parent = arc.transform;
       target.transform.parent = arc.transform;
       selectedUnitoken = target;
       //Fragment frag = new GameObject("Fragment").AddComponent<Fragment>();
       //frag.Initialize(arc);

   }
    Arc CreateJoinArc(Fragment source, Fragment target){
        Arc newJoinArc = Instantiate(joinArcPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<Arc>();
        newJoinArc.Initialize(source,target);

        AddArc(newJoinArc);
        return newJoinArc;
    }

    public Unitoken selectedUnitoken;
    public void SelectUnitoken(Unitoken selected){
        if(selectedUnitoken == null){
            selectedUnitoken = selected;
        }
    }
    public void MoveUnitoken(){
        if(selectedUnitoken != null){
            FollowMouse(selectedUnitoken.transform);
            if(Input.GetMouseButtonUp(0)){
                selectedUnitoken = null;
            }
        }
    }

    public void FollowMouse(Transform trs){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        trs.position = mouseDelta;
    }
    public void AddArc(Arc arc){
        //throw new MissingReferenceException();
        //myArcs.Add(arc);
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnitoken();
    }
}

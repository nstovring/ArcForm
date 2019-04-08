using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using System;

public class TokenFactory : MonoBehaviour
{
    public Transform unitokenPrefab;
    private Camera mCamera;

    public static TokenFactory Instance;
public void Initialize(){
    Instance = this;
    mCamera = Camera.main;
}
 public Unitoken AddNewToken(){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        //transform.position = mouseDelta;


        Unitoken newToken = Instantiate(unitokenPrefab, mouseDelta, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
          //throw new NotImplementedException();
        newToken.Initialize("Label", mouseDelta, "Empty URI!!");

        ArcMapManager.Instance.AddToken(newToken);

        return newToken;
    }


    public Unitoken AddNewToken(unitoken token){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        //transform.position = mouseDelta;
        Unitoken newToken = Instantiate(unitokenPrefab, token.TransientPosition, Quaternion.identity, transform.parent).GetComponent<Unitoken>();

        newToken.transform.position = token.TransientPosition;
        newToken.transform.name = "Unitoken";
        newToken.Initialize(token);

        ArcMapManager.Instance.AddToken(newToken);

        return newToken;
    }


    public Unitoken AddNewToken(predicate p){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mouseDelta = new Vector3(h,v,0);
        //transform.position = mouseDelta;
        Unitoken newToken = Instantiate(unitokenPrefab, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<Unitoken>();

        newToken.transform.position = Vector3.zero;
        newToken.transform.name = "Unitoken";
        newToken.Initialize(p.property, Vector3.zero,  p.URI);

        ArcMapManager.Instance.AddToken(newToken);

        return newToken;
    }
    public Unitoken AddNewToken(Vector3 position){
        //source.tokenRotation = (360.0f/(source.myArcs.Count + 1.0f));
        //Vector3 rotationVector = new Vector3(Mathf.Sin(source.tokenRotation * Mathf.Deg2Rad), Mathf.Cos(source.tokenRotation * Mathf.Deg2Rad), 0);
        ///Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        Unitoken newToken = Instantiate(unitokenPrefab, position, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize("Label", position, "Empty URI!");

        ArcMapManager.Instance.AddToken(newToken);

        return newToken;

        //CreateJoinArc(source, newToken);
    }

     public Unitoken AddNewToken(string Label, Vector3 offset){
        //source.tokenRotation = (360.0f/(source.myArcs.Count + 1.0f));
        //Vector3 rotationVector = new Vector3(Mathf.Sin(source.tokenRotation * Mathf.Deg2Rad), Mathf.Cos(source.tokenRotation * Mathf.Deg2Rad), 0);
        ///Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        Unitoken newToken = Instantiate(unitokenPrefab, Vector3.zero + offset, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = Label;
        newToken.Initialize(Label, Vector3.zero, "Empty URI!");

        ArcMapManager.Instance.AddToken(newToken);

        return newToken;

        //CreateJoinArc(source, newToken);
    }
}

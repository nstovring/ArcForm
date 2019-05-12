using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using System;

namespace FragmentResources{
  public enum MapState {Preview, Locked};
  public enum InteractState{Selected, Unselected};

}
public class TokenFactory : MonoBehaviour
{
  

    public Transform unitokenPrefab;
    private Camera mCamera;

    public static TokenFactory Instance;
public void Initialize(){
    Instance = this;
    mCamera = Camera.main;
}



  
    public Unitoken AddNewToken(Vector3 position){
        //source.tokenRotation = (360.0f/(source.myArcs.Count + 1.0f));
        //Vector3 rotationVector = new Vector3(Mathf.Sin(source.tokenRotation * Mathf.Deg2Rad), Mathf.Cos(source.tokenRotation * Mathf.Deg2Rad), 0);
        ///Vector3 offset = rotationVector * ArcMapManager.Instance.mapScale;

        Unitoken newToken = Instantiate(unitokenPrefab, ArcMapGrid.Instance.FindEmptySpot(position,2 ), Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = "Unitoken";
        newToken.Initialize("Label", position, "Empty URI!");

        ArcMapManager.Instance.AddTokenToList(newToken);

        return newToken;

        //CreateJoinArc(source, newToken);
    }

     public Unitoken AddNewToken(string Label, Vector3 position)
    {

        Unitoken newToken = Instantiate(unitokenPrefab,ArcMapGrid.Instance.FindEmptySpot(position, 3), Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.transform.name = Label;
        newToken.Initialize(Label, Vector3.zero, "Empty URI!");

        ArcMapManager.Instance.AddTokenToList(newToken);

        return newToken;

        //CreateJoinArc(source, newToken);
    }
}

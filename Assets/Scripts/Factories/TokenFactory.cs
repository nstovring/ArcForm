using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructureContainer;
using System;
using static ArcMapGrid;

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
    
     public Unitoken AddNewToken(string Label, Vector3 position)
    {
        Vector3 foundPosition;
        List<GridCell> cells = ArcMapGrid.Instance.FindEmptySpot(position, 3, out foundPosition);

        Unitoken newToken = Instantiate(unitokenPrefab, foundPosition, Quaternion.identity, transform.parent).GetComponent<Unitoken>();
        newToken.MyCells = cells;
        newToken.transform.name = Label;
        newToken.Initialize(Label, Vector3.zero, "Empty URI!");

        ArcMapManager.Instance.AddTokenToList(newToken);

        return newToken;

        //CreateJoinArc(source, newToken);
    }
}

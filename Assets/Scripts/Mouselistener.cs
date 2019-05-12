using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mouselistener : MonoBehaviour
{
    //ArcMapManager.Instance.SelectUnitoken(this);
    Camera mCamera;

    [Header("Settings")]
    public bool editingEnabled = false;
    public bool startVectorStored;
    public bool consoleSingleMessage = false;

    public Vector3 mouseDelta;
    public static Mouselistener Instance;

    public static Vector3 mousePositionInSpace;
    public Vector3 unitokenStartPosVector;

    [Header("Temporary Fragments")]
    public Unitoken hoveredOverToken;
    public ActionGroupMenu hoveredOverActionGroup;
    public Unitoken hoveredStore;
    public Arc hoveredOverArc;
    public Unitoken endPointUnitoken;
    public Unitoken startPointUnitoken;

    public Arc selectedArc;
    [Header("Drag booleans")]
    public bool isDraging;
    public bool draggingFromBackground = false;
    public bool draggingFromToken = false;
    public bool draggingFromArc = false;
    public bool rightClickInstantiate = false;
    [Header("Delay floats")]
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    float holdtime = 0f;

    public bool DoubleClick(){
        if (Input.GetMouseButton(0)){
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
    public bool ClickAndHold(){
        if(Input.GetMouseButton(0)){
            holdtime += Time.deltaTime;
        }
        else{
            holdtime = 0;
            //Debug.Log("Reset the counter");
        
        }
        if(holdtime >= clickdelay){
            //Debug.Log("I Am Working!");
            return true;
        }

        return false;
    }

    public bool tokenSpawn;
    public bool arcSpawn;

    public bool LeftMouseButtonHeld(){
        return Input.GetMouseButton(0);
    }
    public bool RightMouseButtonHeld(){
        return Input.GetMouseButton(1);
    }
    public bool MiddleMouseButtonHeld(){
        return Input.GetMouseButton(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        Instance = this;
        
    }

    public bool isActive = true;
    // Update is called once per frame
    void Update()
    {
        if (!isActive)
            return;
        mousePositionInSpace = new Vector3(mousePositionInSpace.x,mousePositionInSpace.y,0);
        CheckOnClick(); //Checks if clicked on background, Arc or Token

        if(editingEnabled){
            ClickAndHold(); //Checks if mouseIsHeld
            DragFromBackground(); //Checks if dragging from background
            DragFromToken(); //Checks if dragging from Token
            DragFromArc(); //Checks if dragging from Arc
            OnDraggedRelease(); //Determines what is dragged and instatiated
            
        }
      
        
    
        if (endPointUnitoken != null){ // Makes sure the instance is updated until mouseRelease();
            endPointUnitoken.transform.position = mousePositionInSpace;
        }
       
           
        if (Input.GetMouseButtonUp(0)){ // DragReleaseOptions. Returns values and bools to null or false.
            if(isDraging){
                OnDraggedRelease();
            }
        }

        if(Input.GetMouseButtonUp(1) && rightClickInstantiate   ){
        
                ArcMapManager.Instance.selectedUnitoken = TokenFactory.Instance.AddNewToken("Test", mousePositionInSpace);
                ArcMapManager.Instance.SelectUnitoken(ArcMapManager.Instance.selectedUnitoken);
                Debug.Log(Input.mousePosition);
                Debug.Log(mousePositionInSpace);
        }
    }
    
    public void CheckOnClick(){
        bool isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
        if(Input.GetMouseButtonDown(0) && !isMouseOverUI)
        {
            
            if(hoveredOverToken == null && hoveredOverArc == null){//clicked on background
                Debug.Log("Clicked on background");
                draggingFromBackground = true;
                unitokenStartPosVector = new Vector3(mousePositionInSpace.x, mousePositionInSpace.y, 0);
                tokenSpawn = true;
            }
            if(hoveredOverToken != null && hoveredOverArc == null){//clicked on Token
                Debug.Log("Left Clicked on Token");

                DataLogger.Instance.LogSelection(hoveredOverToken);

                ArcMapManager.Instance.SetFocusedToken(hoveredOverToken);
                ExecuteQuery();
                draggingFromToken = true;
                hoveredStore = hoveredOverToken;
                tokenSpawn = true;

            }
            if(hoveredOverToken == null && hoveredOverArc != null){//clicked on Arc
                Debug.Log("Clicked on Arc");
                draggingFromArc = true;
                arcSpawn = true;
            }
        }

         if(Input.GetMouseButtonDown(1)){
              if(hoveredOverToken != null && hoveredOverArc == null){//clicked on Token
                Debug.Log("Right Clicked on Token");

                ExecuteQuery();
                draggingFromToken = true;
                hoveredStore = hoveredOverToken;
                tokenSpawn = true;

            }
         }
    }

    public void ExecuteQuery(){
        ArcToolUIManager.ArcUIUtility.ClearMenu();
        ArcMapManager.Instance.autoMoveInterrupt = false;
        //ArcToolUIManager.ArcUIUtility.CreateSubMenu();
        if (hoveredOverToken.myPropertiesFromConceptNet != null)
        {
            ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromProperties(hoveredOverToken.myPropertiesFromConceptNet);
        }
        else
        {
            SearchEngine.Instance.GetConceptRelations(hoveredOverToken);
        }

        SubItemController.Instance.arcSubItemMenu.SetActive(false);
    }

    public void DragFromBackground(){
        if(ClickAndHold() == true && draggingFromBackground == true){
                Debug.Log("Dragging from background");
                //StoreStart vector from empty space
                if(tokenSpawn==true){
                    
                    endPointUnitoken = TokenFactory.Instance.AddNewToken("Test",mousePositionInSpace);
                    startPointUnitoken = TokenFactory.Instance.AddNewToken("Test", unitokenStartPosVector);
                    selectedArc = ArcFactory.Instance.AddNewArc(startPointUnitoken, "Test", endPointUnitoken);
                    
                    startPointUnitoken.isInactive = false;
                    tokenSpawn = false;
                }
                
                isDraging = true;
        }       
    }
    public void DragFromToken(){
        if(ClickAndHold() == true && draggingFromToken == true){
                Debug.Log("Dragging from Token");
                if(tokenSpawn==true){
                    endPointUnitoken = TokenFactory.Instance.AddNewToken("Test", mousePositionInSpace);
                    selectedArc = ArcFactory.Instance.AddNewArc(hoveredStore, "Test", endPointUnitoken);
                    tokenSpawn = false;
                }
                isDraging = true;              
        }   
    }
    public void DragFromArc(){
        if(ClickAndHold() == true && draggingFromArc == true){
            if(consoleSingleMessage == true){
                Debug.Log("Dragging from Ark");
                consoleSingleMessage = false;
                }           
        }
    }

    public void OnDraggedRelease(){
            if(Input.GetMouseButtonUp(0) == true && draggingFromBackground == true && isDraging == true){
                Debug.Log("Not draging from Background anymore");
                draggingFromBackground = false;
                consoleSingleMessage = true;
                EndToken();
                isDraging = false;    
                draggingFromToken = false;
        
            }

            if(Input.GetMouseButtonUp(0) == true && draggingFromToken == true && isDraging == true){
                Debug.Log("Not draging from Token anymore");
                EndToken();//Checks whether endpoint is another Token or background.
                draggingFromToken = false;
                consoleSingleMessage = true;
                endPointUnitoken = null;
                
                hoveredStore = null;
                isDraging = false;    
                draggingFromToken = false;
            }
            
            //if(startVectorStored == true && hoveredOverToken != null){ // Drag from background New Token to existing Token
            //    
            //    
            //    hoveredOverToken=null;
            //    startVectorStored = false;
            //     
            //}

            //if(hoveredStore != null && startVectorStored==false && hoveredOverToken == null){ // Drag from existing Token to new
            //    endPoint = null;
            //    hoveredStore = null;
            //
            //
            //}
            
            //if(hoveredStore != null && startVectorStored==false && hoveredOverToken != null){ // Drag from existing Token to another existing
            //
            //    ArcFactory.Instance.AddNewArc(hoveredStore, "Test", hoveredOverToken);
            //    hoveredStore = null;
            //
            //}    
    }
    public void EndToken(){
         if(hoveredOverArc != null){
            UnitokenDelete(selectedArc.target);
            selectedArc.target = hoveredOverArc;
            endPointUnitoken.isInactive = false;
            endPointUnitoken = null;
            return;
        }
        if(hoveredOverToken != null){
            UnitokenDelete(selectedArc.target);
            selectedArc.target = hoveredOverToken;
            endPointUnitoken.isInactive = false;
            endPointUnitoken = null;
        }
        if(hoveredOverToken == null){
            endPointUnitoken.isInactive = false;
            endPointUnitoken = null;
        }
    }

    public void FollowMouse(Transform trs){
        Vector3 mouseWorldPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        float h = mouseWorldPos.x;
        float v = mouseWorldPos.y;
        Vector3 mousePositionInSpace = new Vector3(h,v,0);
        trs.position = mousePositionInSpace;
    }

    public void UnitokenDelete(Fragment deleteU){// Deleting the soft Token instantiated.
        ArcMapManager.Instance.DestroyToken((Unitoken)deleteU);
        }

     

}

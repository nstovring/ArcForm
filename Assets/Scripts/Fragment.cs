using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ArcMapGrid;

public abstract class Fragment : MonoBehaviour
{
    public enum Type { Unitoken, Arc, Collection }

    [Header("Settings")]
    public int id;
    public int cellSize;
    public List<Arc> myArcs;
    public Fragment Source;

    [Header("Refs")]
    public Vector3 TransientPosition;
    public Collider2D myCollider;
    public TextMeshPro myLabel;
    private List<GridCell> myCells;
    public List<GridCell> MyCells { get => myCells; set => myCells = value; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TransientPosition = transform.position;
        if(MyCells != null)
        {
            foreach (GridCell g in MyCells)
            {
                g.Filled = true;
                g.DebugCube.Draw();
            }
        }
       
    }
    public bool isSelected;
    public Transform hoverIcon;
    public bool isHoveredOver = false;


    public virtual bool SetHoverActive(bool state)
    {
        if(hoverIcon != null) 
        hoverIcon.gameObject.SetActive(state);
        isHoveredOver = (state);
        return state;
    }

    public virtual void ShowInputField()
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetLabel(string label)
    {
        myLabel.text = label;
        transform.name = label;
    }

}

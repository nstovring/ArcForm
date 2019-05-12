using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Fragment : MonoBehaviour
{
    public int id;
    public int cellSize;
    public List<Arc> myArcs;

    public enum Type {Unitoken, Arc, Collection}
    public Type myType;

    public Vector3 TransientPosition;
    public Collider2D myCollider;

    public TextMeshPro myLabel;


    public List<Transform> spawnLocations;

  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TransientPosition = transform.position;
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

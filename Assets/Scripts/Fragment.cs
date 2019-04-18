using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    public int id;

    public List<Arc> myArcs;

    public enum Type {Unitoken, Arc, Collection}
    public Type myType;

    public Vector3 TransientPosition;

    public TextMeshPro myLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isSelected;
    public Transform hoverIcon;
    public bool isHoveredOver = false;
    public bool SetHoverActive(bool state)
    {
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

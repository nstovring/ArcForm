using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arc : Thought, ILabelable
{
    public Unitoken source;
    public Unitoken target;

    public TextMeshPro myLabel;
    public TextMeshPro MyLabel { get => myLabel;}

    public Vector3 center;

    public virtual void ShowInputField()
    {
        ArcMapManager.Instance.ShowInputField(center, this);
    }
    public virtual void SetLabel(string label)
    {
        if(MyLabel == null){
            throw new System.NotImplementedException();
        }
        MyLabel.text = label;
    }
    // Start is called before the first frame update
    public void SetSource(Unitoken source){
        this.source = source;
    }

    public void SetTarget(Unitoken target){
        this.target = target;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}

using StructureContainer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTokenCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(string label)
    {
        string key = ArcToolUIManager.Instance.currentKey;
        Unitoken focus = ArcMapManager.Instance.GetFocusedToken();
        //Property p = focus.myPropertiesFromConceptNet[key];
        Relation r = new Relation { Label = label, token = focus };
        //p.Relations.Add(r);
        focus.myPropertiesFromConceptNet[key].Relations.Add(r);
        ArcToolUIManager.ArcUIUtility.UpdatePropertyMenuFromProperties(focus.myPropertiesFromConceptNet);
        ArcToolUIManager.Instance.RefreshSubMenu(ArcToolUIManager.ArcDataUtility.PropertyMenuItems[key]);
    }
}

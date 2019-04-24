using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public string pathName;
    public bool moveOut;
    public bool moveIn;
    Color alphaFrom = new Color(1,1,1,0);
    Color alphaTo = new Color(1,1,1,1);
    Vector3 scaleIni = new Vector3(0,0,0);
    Vector3 scaleEnd = new Vector3(0.15f,0.15f,0.15f);
    // Start is called before the first frame update
    void Start(){

    }
    void Update(){
        if(Input.GetKeyUp("space")){
            AnimateOut();
        }
        if(Input.GetKeyUp("backspace")){
            AnimateIn();
        }
    }
    void AnimateOut(){   
        iTween.ColorTo(gameObject, alphaTo, 0.2f);
        iTween.ScaleTo(gameObject, scaleEnd, 0.3f);
        iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName),"time", 1, "easetype", iTween.EaseType.easeOutQuint));
    }
    
    void AnimateIn(){
        iTween.ColorTo(gameObject, alphaFrom, 0.2f);
        iTween.ScaleTo(gameObject, scaleIni, 0.3f);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName),"time", 1, "easetype", iTween.EaseType.easeOutQuint));


    }
}

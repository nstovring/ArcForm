using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGroupAnimate : MonoBehaviour
{
    public string pathName;
    public bool moveOut;
    public bool moveIn;
    Color alphaFrom = new Color(1,1,1,0);
    Color alphaTo = new Color(1,1,1,1);
    Vector3 scaleIni = new Vector3(0,0,0);
    Vector3 scaleEnd = new Vector3(0.15f,0.15f,0.15f);
    CircleCollider2D daCircle;
    private Color emptyColor = new Color(0,0,0,0);
    public Color startColor;
    public SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start(){
        startColor = mySpriteRenderer.color;
        daCircle = gameObject.GetComponent<CircleCollider2D>();
        mySpriteRenderer.color = emptyColor;
    }
    void Update(){
        
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(0.9f);
        daCircle.enabled = true;
    }
    public void AnimateOut(){

        daCircle.enabled = false;
        mySpriteRenderer.color = startColor;
        iTween.ColorTo(gameObject, alphaTo, 0.2f);
        iTween.ScaleTo(gameObject, scaleEnd, 0.3f);
        iTween.MoveFrom(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName),"time", 1, "easetype", iTween.EaseType.easeOutCubic));
        StartCoroutine(wait());
        
    }
    
    public void AnimateIn(){
        
        iTween.ColorTo(gameObject, alphaFrom, 0.2f);
        iTween.ScaleTo(gameObject, scaleIni, 0.3f);
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName),"time", 1, "easetype", iTween.EaseType.easeOutCirc));


    }
}

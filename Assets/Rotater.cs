using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    void Start()
    {
        angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
        transform.Rotate(0,0,angle, Space.World);

    }
    public float angle;
    // Update is called once per frame
    void PointSpriteInDirection(Vector3 direction, Transform obj){
        angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
        obj.Rotate(0,0,angle, Space.World);
    }
    void Update()
    {
        
    }
}

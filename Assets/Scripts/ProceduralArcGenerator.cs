using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralArcGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3[] vertices;
    public int xSize, ySize;
    void Start()
    {
        
    }

	private void Awake () {
		StartCoroutine(Generate());
	}

	private IEnumerator Generate () {
		WaitForSeconds wait = new WaitForSeconds(0.05f);
		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x, y);
				yield return wait;
			}
		}
	}
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos () {
        if(vertices == null)
        return;
		
        Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArcMapGrid;
using static ArcMapGrid.Searcher;

public class SearchPatternShower : MonoBehaviour
{
    // Start is called before the first frame update
    public static SearchPatternShower Instance;
    public Searcher searcher;
    void Start()
    {
        Instance = this;
    }
    public Transform thing;
    public List<GridCell> cells;
    public IEnumerator ShowPattern()
    {
        foreach(Vector3 g in searcher.checkedLocations)
        {
            DebugCube d = new DebugCube { center = g };
            thing.transform.position = g;
            //d.Draw();
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

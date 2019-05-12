using ArcToolConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMapGrid : MonoBehaviour
{
    public static ArcMapGrid Instance;
    public class GridCell
    {
        public int Xindex;
        public int Yindex;
        public bool Placed = false;

        public void Draw()
        {
        }
    }

    public Transform cellPrefab;

    public int X;
    public int Y;

    public Dictionary<Vector2Int, GridCell> Map;

    // Start is called before the first frame update
    void Start()
    {
        Map = new Dictionary<Vector2Int, GridCell>();
        Instance = this;
        //Use a dictionary to store grid
        //look through grid by using positions as index if there is nothing under a specific index then place there
        //the method which fills the dictionary spot is also responsible for spawning a debug element at the location
    }

    public Vector3 FindEmptySpot(Fragment frag, int targetSize)
    {
        Vector3 position = frag.TransientPosition;
        return FindEmptySpot(position, targetSize);
    }

    class Searcher{

        public int size;
        public Vector3 startPos;
        public Vector3 currentPos;
        public Vector3 endPos;
        bool alternate = true;
        int alternateSign = 1;
        int alternateSign2 = -1;
        internal Vector3 startDirection;
        List<Vector2Int> checkedLocations;
        public bool searching = true;


        public void Run()
        {
            Roam();
        }

        public bool Search(Vector3 pos)
        {
            if(size == 1)
            {
                return !IsCellOccupied(pos);
            }
            else
            {
                return AreCellsOccupied(pos, size);
            }
        }

        public bool IsCellOccupied(Vector3 position)
        {
            GridCell g;
            Vector2Int indexPos = new Vector2Int((int)position.x, (int)position.y);
            //Place fragment
            bool isOccupied = ArcMapGrid.Instance.Map.TryGetValue(indexPos, out g);
            return isOccupied;
        }
        public bool AreCellsOccupied(Vector3 position, int range)
        {
            Vector3 center = position;
            currentPos = position - new Vector3(1, 0, 0);
            for (int i = 0; i < range; i++)
            {
                bool isOcuppied = IsCellOccupied(currentPos);
                if (isOcuppied)
                {
                    return false;
                }
                currentPos += new Vector3(1, 0, 0);
            }

            currentPos = center;

            return true;
        }

        public void Place(Vector3 pos)
        {
            if (size == 1)
            {
                PlaceCell(pos);
            }
            else
            {
                PlaceWide(pos);
            }
        }

        public void PlaceCell(Vector3 position)
        {
            Vector2Int indexPos = new Vector2Int((int)position.x, (int)position.y);
            GridCell g = new GridCell();
            g.Placed = true;
            Instance.Map.Add(indexPos, g);
            Instance.AddDebugCube(position);
        }

        public void PlaceWide(Vector3 position)
        {
            Vector3 tempPos = position - new Vector3(1, 0, 0);
            ///startPos = position - new Vector3(1, 0, 0);
            for (int i = 0; i < size; i++)
            {
                PlaceCell(tempPos);
                tempPos += new Vector3(1, 0, 0);
            }
        }

        public Vector3[] searchPattern = {new Vector3(1,0,0), new Vector3(0, 1, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0)};
        public int searchCount = 0;

        public void Roam()
        {
            if (checkedLocations == null)
                checkedLocations = new List<Vector2Int>();

            for (int i = 0; i < 4; i++)
            {
                currentPos = startPos + (searchPattern[i % searchPattern.Length] * searchCount);
                if (Search(currentPos))
                {
                    Place(currentPos);
                    endPos = currentPos;
                    searching = false;
                    break;
                }
                //else
                //{
                //    searchCount++;
                //}
            }

            searchCount++;
        }
    }


    public Vector3 FindEmptySpot(Vector3 position, int targetSize)
    {
        Searcher searcher = new Searcher();
        searcher.size = targetSize;
        searcher.startPos = position;
        searcher.currentPos = searcher.startPos;
        searcher.startDirection = new Vector3(1, 1,0);
        while (searcher.searching && searcher.searchCount < 10)
        {
            searcher.Run();
        }

        return searcher.endPos;
    }

  
    public struct DebugCube
    {
        public Vector3 center;
        public Vector3 a;
        public Vector3 b;
        public Vector3 c;
        public Vector3 d;

        public float scale;

        public void Set()
        {
            scale = 0.5f;
            a = center + new Vector3(-1, -1, 0) * scale;
            b = center + new Vector3(-1, 1, 0) * scale;
            c = center + new Vector3(1, 1, 0) * scale;
            d = center + new Vector3(1, -1, 0) * scale;
        }

        public void Draw()
        {
            Set();

            Debug.DrawLine(a, b);
            Debug.DrawLine(b, c);
            Debug.DrawLine(c, d);
            Debug.DrawLine(d, a);
        }
    }

    public List<DebugCube> debugCubes;

    public void AddDebugCube(Vector3 center)
    {
        if (debugCubes == null)
            debugCubes = new List<DebugCube>();
        DebugCube d = new DebugCube { center = center };
        debugCubes.Add(d);
    }

    // Update is called once per frame
    void Update()
    {
        if(debugCubes != null)
        {
            foreach(DebugCube d in debugCubes)
            {
                d.Draw();
            }
        }
        //Draw grid with debug.draw
    }

    internal void RemoveFromMap(Fragment frag)
    {
        Vector3 position = frag.TransientPosition;

        Vector2Int indexPos = new Vector2Int((int)position.x, (int)position.y);
        GridCell gTemp = new GridCell();
        bool isOcuppied = Map.TryGetValue(indexPos, out gTemp);
        if (isOcuppied)
        {
            Map.Remove(indexPos);
        }
        else
        {
            throw new MissingReferenceException("Cell not inside of map?!");
        }
    }
}

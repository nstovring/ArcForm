using ArcToolConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMapGrid : MonoBehaviour
{
    public static ArcMapGrid Instance;
    [System.Serializable]
    public class GridCell
    {
        public Vector2Int Index;
        public bool Filled = false;
        public Searcher.DebugCube DebugCube;
    }


    public Dictionary<Vector2Int, GridCell> Map;

    // Start is called before the first frame update
    void Start()
    {
        Map = new Dictionary<Vector2Int, GridCell>();
        GridCells = new List<GridCell>();
        Instance = this;
    }

   

    public class Searcher
    {
        public int size;
        public Vector3 startPos;
        public Vector3 currentPos;
        public Vector3 endPos;
        bool alternate = true;
        int alternateSign = 1;
        int alternateSign2 = -1;
        internal Vector3 startDirection;
        public List<Vector3> checkedLocations;
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
            List<Vector3> consideredCells = new List<Vector3>();
            Vector3 center = position;
            currentPos = position - new Vector3(1, 0, 0);
            for (int i = 0; i < range; i++)
            {
                bool isOcuppied = IsCellOccupied(currentPos);
                checkedLocations.Add(position);
                if (isOcuppied)
                {
                    return false;
                }
                else
                {
                    consideredCells.Add(position);
                }

                currentPos += new Vector3(1, 0, 0);
            }

            currentPos = center;

            PlaceWide(position);

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

        public List<GridCell> GridCells;
        public GridCell PlaceCell(Vector3 position)
        {
            if (GridCells == null)
                GridCells = new List<GridCell>();

            Vector2Int indexPos = new Vector2Int((int)position.x, (int)position.y);
            GridCell g ;
            bool doesCellExist = Instance.Map.TryGetValue(indexPos, out g);
            if (doesCellExist)
            {
                return g;
            }

            g = new GridCell();
            g.Index = indexPos;
            GridCells.Add(g);
            //g.Filled = true;
            DebugCube d = new DebugCube { center = position };
            g.DebugCube = d;
            Instance.Map.Add(indexPos, g);
            return g;
        }



        public List<GridCell> PlaceWide(Vector3 position)
        {
            List<GridCell> g = new List<GridCell>();
            Vector3 tempPos = position - new Vector3(1, 0, 0);
            ///startPos = position - new Vector3(1, 0, 0);
            for (int i = 0; i < size; i++)
            {
                g.Add(PlaceCell(tempPos));
                tempPos += new Vector3(1, 0, 0);
            }
            return g;
        }
        const int x = 2;
        //public Vector3[] searchPattern = { new Vector3(0, 0, 0),new Vector3(0,1,0), new Vector3(x, 1, 0), new Vector3(x, 0, 0),  new Vector3(x, -1, 0), new Vector3(0, -1, 0), new Vector3(-x, -1, 0), new Vector3(-x, 0, 0) };
        public Vector3[] searchPattern = { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(x, 0, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(-x, 0, 0), new Vector3(-x, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(x, 0, 0) };

        public int searchCount = 1;

        public void Roam()
        {
            if (checkedLocations == null)
                checkedLocations = new List<Vector3>();

            for (int i = 0; i < searchPattern.Length; i++)
            {
                for (int j = 0; j <= searchCount; j++)
                {
                    currentPos = currentPos + (searchPattern[i % searchPattern.Length]);// + searchPattern[(i + 1) % searchPattern.Length];
                    checkedLocations.Add(currentPos);

                    if (!IsCellOccupied(currentPos))
                    {
                        Vector3 left = currentPos - new Vector3(1, 0, 0);
                        Vector3 right = currentPos + new Vector3(1, 0, 0);

                        if (!IsCellOccupied(left))
                        {
                            if (!IsCellOccupied(right))
                            {
                                Place(currentPos);
                                endPos = currentPos;
                                searching = false;
                                return;
                            }
                        }
                    }
                }
            }

            currentPos = startPos;
            //startPos = currentPos;
            searchCount++;
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

    }


    public List<GridCell> FindEmptySpot(Vector3 position, int targetSize, out Vector3 outPos)
    {
        Searcher searcher = new Searcher();
        //position = Vector3.zero;
        searcher.size = targetSize;
        searcher.startPos = position;
        searcher.currentPos = searcher.startPos;
        searcher.startDirection = new Vector3(1, 1, 0);
        while (searcher.searching && searcher.searchCount < 10)
        {
            searcher.Roam();
        }

        SearchPatternShower.Instance.searcher = searcher;
        StartCoroutine(SearchPatternShower.Instance.ShowPattern());
        outPos = searcher.endPos;
        GridCells.AddRange(searcher.GridCells);
        return searcher.GridCells;
    }



    // Update is called once per frame
    List<GridCell> GridCells;
    void Update()
    {

        if (GridCells != null)
        {
            foreach (GridCell g in GridCells)
            {
                g.Filled = true;
                g.DebugCube.Draw();
            }
        }
    }

    public List<GridCell> FindEmptySpot(Fragment frag, int targetSize)
    {
        Vector3 position = frag.TransientPosition;
        List<GridCell> cells = FindEmptySpot(position, targetSize, out position);
        
        return cells;
    }

    internal void RemoveFromMap(Fragment frag)
    {
        Vector3 position = frag.TransientPosition;
        foreach(GridCell cell in frag.MyCells)
        {
            Vector2Int indexPos = cell.Index;
            GridCell gTemp = new GridCell();
            bool isOcuppied = Map.TryGetValue(indexPos, out gTemp);
            if (isOcuppied)
            {
                Map.Remove(indexPos);
                GridCells.Remove(gTemp);
            }
            else
            {
                throw new MissingReferenceException("Cell not inside of map?!");
            }
        }
        frag.MyCells = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MazeCell
{
    public int X;
    public int Y;

    public bool Left = true;
    public bool Right = true;
    public bool Up = true;
    public bool Bottom = true;

    public bool Visited = false;
}

public class Maze
{
    public MazeCell[,] cells;
    public Vector2Int startCell;
    public int[,] distances;
}

public class Generator
{
    int Width = 10;
    int Height = 10;
    private bool useWilsonAlgorithm;

    public Generator(bool useWilson = false)
    {
        useWilsonAlgorithm = useWilson;
    }

    public Maze GenerateMaze(int Width, int Height)
    {
        this.Width = Width;
        this.Height = Height;

        MazeCell[,] cells = new MazeCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeCell { X = x, Y = y };
            }
        }

        RemoveWallsWilson(cells);
       
        AddCycles(cells);

        Maze maze = new Maze();

        maze.cells = cells;

        maze.startCell = ChooseStartCell(cells);
        CalculateDistances(maze);

        return maze;
    }


    private void RemoveWallsWilson(MazeCell[,] maze)
    {

        List<MazeCell> unvisited = new List<MazeCell>();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                unvisited.Add(maze[x, y]);
            }
        }


        MazeCell first = unvisited[Random.Range(0, unvisited.Count)];
        unvisited.Remove(first);
        first.Visited = true;

        while (unvisited.Count > 0)
        {

            MazeCell current = unvisited[Random.Range(0, unvisited.Count)];
            List<MazeCell> path = new List<MazeCell> { current };


            while (unvisited.Contains(current))
            {
                List<MazeCell> neighbours = GetNeighbours(maze, current);
                current = neighbours[Random.Range(0, neighbours.Count)];

                int index = path.IndexOf(current);
                if (index >= 0)
                {
                    path = path.GetRange(0, index + 1);
                }
                else
                {
                    path.Add(current);
                }
            }


            for (int i = 0; i < path.Count - 1; i++)
            {
                RemoveWall(path[i], path[i + 1]);
                path[i].Visited = true;
                unvisited.Remove(path[i]);
            }
        }
    }

    private Vector2Int ChooseStartCell(MazeCell[,] maze)
    {
        return new Vector2Int(Random.Range(0, Width), Random.Range(0, Height));
    }

    private Vector2Int ChooseEndCell(MazeCell[,] maze)
    {
        return new Vector2Int(Random.Range(0, Width), Random.Range(0, Height));
    }

    private void CalculateDistances(Maze maze)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        maze.distances = new int[Width, Height];


        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                maze.distances[x, y] = -1;
            }
        }


        maze.distances[maze.startCell.x, maze.startCell.y] = 0;
        queue.Enqueue(maze.startCell);

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            int currentDistance = maze.distances[current.x, current.y];


            List<Vector2Int> neighbours = GetReachableNeighbours(maze.cells, current);

            foreach (Vector2Int neighbour in neighbours)
            {
                if (maze.distances[neighbour.x, neighbour.y] == -1)
                {
                    maze.distances[neighbour.x, neighbour.y] = currentDistance + 1;
                    queue.Enqueue(neighbour);
                }
            }
        }


    }

    private void AddCycles(MazeCell[,] maze)
    {

        int additionalPassages = (Width * Height) / 10;

        for (int i = 0; i < additionalPassages; i++)
        {
            int x = Random.Range(0, Width);
            int y = Random.Range(0, Height);


            int direction = Random.Range(0, 4);

            switch (direction)
            {
                case 0: // ¬лево
                    if (x > 0 && maze[x, y].Left)
                    {
                        RemoveWall(maze[x, y], maze[x - 1, y]);
                    }
                    break;
                case 1: // ¬право
                    if (x < Width - 1 && maze[x, y].Right)
                    {
                        RemoveWall(maze[x, y], maze[x + 1, y]);
                    }
                    break;
                case 2: // ¬верх
                    if (y < Height - 1 && maze[x, y].Up)
                    {
                        RemoveWall(maze[x, y], maze[x, y + 1]);
                    }
                    break;
                case 3: // ¬низ
                    if (y > 0 && maze[x, y].Bottom)
                    {
                        RemoveWall(maze[x, y], maze[x, y - 1]);
                    }
                    break;
            }
        }
    }
    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
            {
                a.Bottom = false;
                b.Up = false;
            }
            else
            {
                b.Bottom = false;
                a.Up = false;
            }
        }
        else
        {
            if (a.X > b.X)
            {
                a.Left = false;
                b.Right = false;
            }
            else
            {
                b.Left = false;
                a.Right = false;
            }
        }

    }


    private List<MazeCell> GetNeighbours(MazeCell[,] maze, MazeCell cell)
    {
        List<MazeCell> neighbours = new List<MazeCell>();
        int x = cell.X;
        int y = cell.Y;

        if (x > 0) neighbours.Add(maze[x - 1, y]);
        if (y > 0) neighbours.Add(maze[x, y - 1]);
        if (x < Width - 1) neighbours.Add(maze[x + 1, y]);
        if (y < Height - 1) neighbours.Add(maze[x, y + 1]);

        return neighbours;
    }

    private List<Vector2Int> GetReachableNeighbours(MazeCell[,] maze, Vector2Int cell)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();
        int x = cell.x;
        int y = cell.y;


        if (x > 0 && !maze[x, y].Left) neighbours.Add(new Vector2Int(x - 1, y));
        if (y > 0 && !maze[x, y].Bottom) neighbours.Add(new Vector2Int(x, y - 1));
        if (x < Width - 1 && !maze[x, y].Right) neighbours.Add(new Vector2Int(x + 1, y));
        if (y < Height - 1 && !maze[x, y].Up) neighbours.Add(new Vector2Int(x, y + 1));

        return neighbours;
    }
}



public class Spawner : MonoBehaviour
{
    public Camera cam;
    public GameObject mazeHandler;

    public GameObject exit_portal;
    public GameObject enter_portal;
    public GameObject enter_point;

    public Cell CellPrefab;
    public Vector2 Cellsize = new Vector2(1, 1);

    public int Width = 10;
    public int Height = 10;

    public bool UseWilsonAlgorithm = false;

    public Button switchAlgorithmButton;
    public TextMeshProUGUI algorithmText;

    public GameObject exit_pref;
    public GameObject enter_pref;

    public NavMeshSurface navMeshSurface;

    public void GenerateMaze()
    {
        bool exit = false;
        Destroy(exit_pref);
        Destroy(enter_pref);
        foreach (Transform child in mazeHandler.transform)
            GameObject.Destroy(child.gameObject);


        enter_pref = Instantiate(enter_portal, new Vector3(44.5f, 240.51f, 25.85f), Quaternion.identity);

        Generator generator = new Generator(UseWilsonAlgorithm);
        Maze maze = generator.GenerateMaze(Width, Height);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int z = 0; z < maze.cells.GetLength(1); z++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * Cellsize.x, 0, z * Cellsize.y), Quaternion.identity);

                if (maze.cells[x, z].Left == false)
                    Destroy(c.Left);
                if (maze.cells[x, z].Right == false)
                    Destroy(c.Right);
                if (maze.cells[x, z].Up == false)
                    Destroy(c.Up);
                if (maze.cells[x, z].Bottom == false)
                    Destroy(c.Bottom);

                if (maze.distances[x, z] >= 0)
                {
                    c.Distance.text = maze.distances[x, z].ToString();
                }
                else
                {
                    c.Distance.text = "X";
                }

                if (x == maze.startCell.x && z == maze.startCell.y)
                {
                    c.Distance.text = "0";
                    c.Distance.color = Color.red;
                    enter_point.transform.position = new Vector3(10 + 10 * x, 1, 10 * z);
                }

                if (maze.distances[x, z] == 13 && !exit)
                {
                    c.Distance.color = Color.green;
                    exit = true;
                    exit_pref = Instantiate(exit_portal, new Vector3(10 + 10*x, 0, 10*z), Quaternion.identity);
                }

                c.transform.parent = mazeHandler.transform;
            }
        }

        cam.transform.position = new Vector3((Width * Cellsize.x) / 2, Mathf.Max(Width, Height) * 8, (Height * Cellsize.y) / 2);

        StartCoroutine(buildNavMesh());
    }

    public IEnumerator buildNavMesh()
    {
        yield return new WaitForSeconds(1);
        
        navMeshSurface.BuildNavMesh();
    }
}
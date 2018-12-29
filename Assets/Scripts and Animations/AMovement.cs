using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMovement : MonoBehaviour
{
    private GridScript grid;
    public Transform seeker;
    private Transform target;
    private int floorLevel = 0;
    private bool anotherRun = false;
    private bool donePart2 = false;
    private bool stair = false;
    private int numberOfWaypoints;
    private bool leftStair;
    private List<int> leftRooms = new List<int> { 211, 212, 213, 214, 209, 208, 207, 110, 111, 108, 107, 106, 105, 104, 311, 312, 313, 314, 309, 308, 307, 306 };
    private List<GameObject> rightWaypoints = new List<GameObject>();
    private List<GameObject> leftWaypoints = new List<GameObject>();
    private GameObject doorToChange2;

    void GoStairs()
    {
        numberOfWaypoints = 0;
        leftStair = false;
        if (MainMenuScript.door > 100 && MainMenuScript.door < 200)
        {
            floorLevel = 1;
            if (leftRooms.Contains(MainMenuScript.door))
            {
                numberOfWaypoints = 4;
                leftStair = true;
            }
            else
            {
                numberOfWaypoints = 3;
            }

        }
        else if (MainMenuScript.door > 200 && MainMenuScript.door < 300)
        {
            floorLevel = 2;
            if (leftRooms.Contains(MainMenuScript.door))
            {
                numberOfWaypoints = 8;
                leftStair = true;
            }
            else
            {
                numberOfWaypoints = 7;
            }

        }
        else
        {
            floorLevel = 3;
            if (leftRooms.Contains(MainMenuScript.door))
            {
                numberOfWaypoints = 12;
                leftStair = true;
            }
            else
            {
                numberOfWaypoints = 11;
            }
        }
    }

    void StairTargetSet()
    {
        doorToChange2 = GameObject.Find("door " + MainMenuScript.door);
        target = doorToChange2.GetComponentInChildren<BoxCollider>().transform;

    }
    void TargetSet()
    {

        if (MainMenuScript.door < 100)
        {
            doorToChange2 = GameObject.Find("door " + MainMenuScript.door);
            target = doorToChange2.GetComponentInChildren<BoxCollider>().transform;
        }
        else
        {
            stair = true;
            if (leftRooms.Contains(MainMenuScript.door))
            {
                Debug.Log("left stair");
                doorToChange2 = GameObject.Find("Waypoint PL");
                target = doorToChange2.GetComponentInChildren<BoxCollider>().transform;
            }
            else
            {
                Debug.Log("right stair");
                doorToChange2 = GameObject.Find("Waypoint PR");
                target = doorToChange2.GetComponentInChildren<BoxCollider>().transform;
            }
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        Debug.Log("findpath jes");
        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
    void Awake()
    {
        grid = GetComponent<GridScript>();
        rightWaypoints.AddRange(new GameObject[] { GameObject.Find("Waypoint PR1"), GameObject.Find("Waypoint PR2"), GameObject.Find("Waypoint 1R"), GameObject.Find("Waypoint PR4"), GameObject.Find("Waypoint PR5"), GameObject.Find("Waypoint PR6"), GameObject.Find("Waypoint 2R"), GameObject.Find("Waypoint PR8"), GameObject.Find("Waypoint PR9"), GameObject.Find("Waypoint PR10"), GameObject.Find("Waypoint 3R") });
        leftWaypoints.AddRange(new GameObject[] { GameObject.Find("Waypoint PL1"), GameObject.Find("Waypoint PL2"), GameObject.Find("Waypoint PL3"), GameObject.Find("Waypoint 1L"), GameObject.Find("Waypoint PL5"), GameObject.Find("Waypoint PL6"), GameObject.Find("Waypoint PL7"), GameObject.Find("Waypoint 2L"), GameObject.Find("Waypoint PL9"), GameObject.Find("Waypoint PL10"), GameObject.Find("Waypoint PL11"), GameObject.Find("Waypoint 3L") });

        if (floorLevel == 0)
        {

            TargetSet();
        }
    }

    void Start()
    {
        FindPath(seeker.position, target.position);

        AnimatePath();

    }

    void Update()
    {
        if (donePart2)
        {

            if (floorLevel != 0 && stair)
            {//on floor enable script to go to door

                if (leftStair)
                {
                    GameObject.Find("A*" + floorLevel).GetComponent<AMoveFloor>().seeker = GameObject.Find("Waypoint " + floorLevel + "L").transform;
                    GameObject.Find("A*" + floorLevel).GetComponent<AMoveFloor>().leftStair = true;
                }
                else
                {
                    GameObject.Find("A*" + floorLevel).GetComponent<AMoveFloor>().seeker = GameObject.Find("Waypoint " + floorLevel + "R").transform;

                }
                GameObject.Find("A*" + floorLevel).GetComponent<AMoveFloor>().donePart2 = true;
                floorLevel = 0;
                GameObject.Find("A*").GetComponent<AMovement>().enabled = false;

            }
            // seeker.LookAt(doorToChange2.GetComponentInChildren<SphereCollider>().center);

        }

    }
    void AnimatePath()
    {
        Vector3 currentPos = seeker.position;

        if (grid.path != null)
        { //Debug.Log("ANIMATING PATH");

            StartCoroutine(UpdatePosition(currentPos, grid.path[0], 0, (coroutineBool) =>
            {

                currentPos = seeker.position;
                if (!anotherRun)
                {
                    if (coroutineBool)
                    {
                        GoStairs();

                        Debug.Log("startcour:" + donePart2);
                        if (stair)
                        {
                            if (leftStair)
                            {
                                Debug.Log("started left stair coroutin");
                                StartCoroutine(UpdatePositionStair(currentPos, leftWaypoints[0], 0, (coroutineBool2) =>
                                {
                                    if (coroutineBool2)
                                    { donePart2 = true; }
                                }));

                            }
                            else
                            {
                                Debug.Log("started right stair coroutin");
                                StartCoroutine(UpdatePositionStair(currentPos, rightWaypoints[0], 0, (coroutineBool2) =>
                                {
                                    if (coroutineBool2)
                                    { donePart2 = true; }
                                }));
                            }
                        }
                    }
                }
            }));
        }

    }
    IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index, System.Action<bool> callback)
    {
        //Debug.Log ("Started Coroutine...");

        float t = 0.0f;
        Vector3 correctedPathPos = new Vector3(n.worldPosition.x, 0.8f, n.worldPosition.z);
        while (t < 1.0f)
        {
            t += Time.deltaTime * 8;
            seeker.position = Vector3.Lerp(currentPos, correctedPathPos, t);
            yield return null;
        }

        //Debug.Log ("Finished updating..."); 
        seeker.position = correctedPathPos;
        currentPos = correctedPathPos;
        index++;
        if (index == grid.path.Count)
        {

            Debug.Log("callback true");
            callback(true);
        }
        if (index < grid.path.Count)
            StartCoroutine(UpdatePosition(currentPos, grid.path[index], index, callback));
    }

    IEnumerator UpdatePositionStair(Vector3 currentPos, GameObject g, int index, System.Action<bool> callback)
    {
        //Debug.Log ("Started Coroutine...");

        float t = 0.0f;
        Vector3 correctedPathPos = new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z);

        while (t < 1.0f)
        {
            t += Time.deltaTime / 2;
            seeker.position = Vector3.Lerp(currentPos, correctedPathPos, t);
            yield return null;
        }
        
        //Debug.Log ("Finished updating..."); 
        seeker.position = correctedPathPos;
        currentPos = correctedPathPos;
        index++;
        if (index == numberOfWaypoints)
        {
            if (leftStair)
            {

                Debug.Log("left callback");
                //yield return new WaitForSeconds(1);
                callback(true);

            }
            else
            {
                Debug.Log("right callback");
               // yield return new WaitForSeconds(1);
                callback(true);

            }
        }
        if (index < numberOfWaypoints)
        {
            if (leftStair)
            {
                StartCoroutine(UpdatePositionStair(currentPos, leftWaypoints[index], index, callback));
            }
            else
            {
                StartCoroutine(UpdatePositionStair(currentPos, rightWaypoints[index], index, callback));
            }
        }
    }
}
    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMovement : MonoBehaviour
{
    private GridScript grid;
    private Vector3 cachedSeekerPos;
    private Vector3 cachedTargetPos;
    public Transform seeker;//, target;
    public Transform target;
    private bool stair = false;
    private bool move = false, canStart = true;
    private List<int> leftRooms = new List<int> {211,212,213,214,209,208,207,110,111,108,107,106,105,104,311,312,313,314,309,308,307,306};
    private List<GameObject> rightWaypoints = new List<GameObject>();
    private List<GameObject> leftWaypoints = new List<GameObject>();
    void GoStairs()
    {
        int numberOfWaypoints=0;
        bool leftStair = false;
        if (MainMenuScript.door > 100 && MainMenuScript.door<200)
        {
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
        else if(MainMenuScript.door>200 && MainMenuScript.door < 300)
        {
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
        //
        if (leftStair)
        {
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                seeker.position = Vector3.Lerp(seeker.position, leftWaypoints[i].transform.position, 1);
            }
        }
        else
        {
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                seeker.position = Vector3.Lerp(seeker.position, rightWaypoints[i].transform.position, 1);
            }
        }

    }

    void TargetSet()
    {
        GameObject doorToChange2;//= GameObject.Find("door " + MainMenuScript.door);
        //GameObject doorToChange2 = GameObject.Find("door 20");
       
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
                doorToChange2 = GameObject.Find("Waypoint PL");
                target = doorToChange2.GetComponentInChildren<BoxCollider>().transform;
            }
            else
            {
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
    void Awake() {
        
        
        grid = GetComponent<GridScript>();
        TargetSet();
        //someStringList.AddRange(new string[] { "seven", "eight", "nine" });
        rightWaypoints.AddRange(new GameObject[] { GameObject.Find("Waypoint PR1"), GameObject.Find("Waypoint PR2"), GameObject.Find("Waypoint 1R"), GameObject.Find("Waypoint PR4"), GameObject.Find("Waypoint PR5"), GameObject.Find("Waypoint PR6"), GameObject.Find("Waypoint 2R"), GameObject.Find("Waypoint PR8"), GameObject.Find("Waypoint PR9"), GameObject.Find("Waypoint PR10"), GameObject.Find("Waypoint 3R") });
        leftWaypoints.AddRange(new GameObject[] { GameObject.Find("Waypoint PL1"), GameObject.Find("Waypoint PL2"), GameObject.Find("Waypoint PL3"), GameObject.Find("Waypoint 1L"), GameObject.Find("Waypoint PL5"), GameObject.Find("Waypoint PL6"), GameObject.Find("Waypoint PL7"), GameObject.Find("Waypoint 2L"), GameObject.Find("Waypoint PL9"), GameObject.Find("Waypoint PL10"), GameObject.Find("Waypoint PL11"), GameObject.Find("Waypoint 3L") });

    }

    void Start() {
       
        // TargetSet(doorToChange.GetComponent<Transform>(),"doorTarget");

        cachedSeekerPos = seeker.position;
        cachedTargetPos = target.position;
        FindPath(seeker.position, target.position); }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            move = true;
            Debug.Log("move tru");
                };
        if (!move && canStart) {
            if (cachedSeekerPos != seeker.position) {
                cachedSeekerPos = seeker.position;
                FindPath(seeker.position, target.position); }
            if (cachedTargetPos != target.position) {
                cachedTargetPos = target.position;
                FindPath(seeker.position, target.position); 
            }
        }
        else {
            AnimatePath();
        }
    }
    void AnimatePath()
    {
        move = false;
        canStart = false;
        Vector3 currentPos = seeker.position;
        if (grid.path != null)
        { //Debug.Log("ANIMATING PATH"); 
            StartCoroutine(UpdatePosition(currentPos, grid.path[0], 0));
        }
        if (stair)
        {

            //nie dziala
            GoStairs();
        }

    }
    IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)  {
        //Debug.Log ("Started Coroutine...");
        float t = 0.0f;
        // camera.transform.position=n.worldPosition;
        Vector3 correctedPathPos = new Vector3 (n.worldPosition.x, 0.8f, n.worldPosition.z);
        while (t < 1.0f) { t += Time.deltaTime*8;
            seeker.position = Vector3.Lerp(currentPos, correctedPathPos, t);
            //seeker.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(correctedPathPos), 0.15F);
            yield return null;
        }
        //Debug.Log ("Finished updating..."); 
        seeker.position = correctedPathPos;
        currentPos = correctedPathPos;
        index++;
        if (index < grid.path.Count) StartCoroutine(UpdatePosition(currentPos, grid.path[index], index));
        else canStart = true;
    }
}
    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMoveFloor : MonoBehaviour
{
    private GridScript grid;
    public Transform seeker;
    private Transform target;
    public bool donePart2 = false;
    private GameObject doorToChange2;
    private bool doneFind = false;
    public bool leftStair = false;
    private int xyc = 0;
    
    void StairTargetSet()
    {
        doorToChange2 = GameObject.Find("door " + MainMenuScript.door);
        target = doorToChange2.GetComponentInChildren<BoxCollider>().transform;

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

            doneFind = true;
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
        StairTargetSet();
    }
    void Update()
    {
        if (donePart2)
        {
            
            if (xyc == 0) {
                
                FindPath(seeker.position, target.position);
                if (doneFind)
                {
                    seeker = GameObject.Find("Player").transform;

                    AnimatePath();
                    xyc = 1;
                    Debug.Log("Animating move floor");
                    
                }

            }
        }

    }
    void AnimatePath()
    {
        Vector3 currentPos = seeker.position;

        if (grid.path != null)
        { 

            StartCoroutine(UpdatePosition(currentPos, grid.path[0], 0));
        }

    }


  
    IEnumerator UpdatePosition(Vector3 currentPos, Node n, int index)
    {
    

        float t = 0.0f;
        Vector3 correctedPathPos = new Vector3(n.worldPosition.x, n.worldPosition.y, n.worldPosition.z);
        while (t < 1.0f)
        {
            t += Time.deltaTime * 8;
            seeker.position = Vector3.Lerp(currentPos, correctedPathPos, t);
            yield return null;
        }

        seeker.position = correctedPathPos;
        currentPos = correctedPathPos;
        index++;
        if (index < grid.path.Count)
            StartCoroutine(UpdatePosition(currentPos, grid.path[index], index));
    }
}


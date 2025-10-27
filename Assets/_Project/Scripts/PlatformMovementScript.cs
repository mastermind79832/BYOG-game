using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Transform> nodes;

    public bool IsGizmoView;
    private int index;

    private void Start()
    {
        CheckIfNodesEmpty();
        int side = nodes.Count; // No. of nodes
        int index = 0;
        platform.position = nodes[index].position;
        index++;

   }

    //  If node is empty Create one
    private void CheckIfNodesEmpty()
    {
        if (nodes.Count < 1)
            this.enabled = false;
    }

    public void StartMoving()
    {
        ResetPlatform();
    }

    private void ResetPlatform()
    {
        int side = nodes.Count; // No. of nodes
        int index = 0;
        platform.localPosition = nodes[index].localPosition;
        index++;
    }


    void Update()
    {
        if (nodes.Count < 2) // If there are less than two nodes generate one. 
            return;

        NodeBasedMovement();  
    }

//  Move Platform Based on Node Positions
    public void NodeBasedMovement()
    {

        Vector3 platformPosition = platform.position;
        if(platformPosition == nodes[index].position)
        {
            index++;
            if(index >= nodes.Count)
                index = 0;
        }
        Vector3 endNode = nodes[index].position;

        platformPosition = Vector3.MoveTowards(platformPosition,endNode,moveSpeed *  Time.deltaTime);
        platform.position = platformPosition;
    }

//  Shows Platfrom Path
    void OnDrawGizmos()
    {
        if(IsGizmoView)
        {        
            for (int i = 0; i < nodes.Count; i++)
            {
                if(nodes[i] == null)
                    continue;
                Vector2 startNode = nodes[i].position;
                Vector2 endNode = nodes[(i+1 == nodes.Count)?0:i+1].position;
                Gizmos.color = Color.green;
                Gizmos.DrawLine(startNode,endNode);
            }   
        }
    }

    //  Create node and Add to list
    [ContextMenu("Generate Node")]
    public void GenerateNode()
    {
        GameObject newNode = new GameObject(string.Format("Node {0}", nodes.Count + 1));
        newNode.transform.parent = transform;
        nodes.Add(newNode.transform);
        newNode.gameObject.SetActive(true);
    }

//  Delete the last node
    public void DeleteNode()
    {
        Transform lastNode = nodes[nodes.Count-1];
        nodes.Remove(lastNode);
        DestroyImmediate(lastNode.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeshData //Class to hold mesh data
{
    public List<Vector3> vertices = new List<Vector3>(); //List to hold vertices  
    public List<int> triangles = new List<int>(); //List to hold triangle indices  {0,1,3, ,1,2,3} 3n
    public List<Vector2> uvs = new List<Vector2>(); //List to hold UV coordinates 

    public List<Vector3> colliderVertices = new List<Vector3>(); //List to hold collider vertices
    public List<int> colliderTriangles = new List<int>(); //List to hold collider triangle indices

    public MeshData waterMesh;
    private bool isMainMesh = true;

    /*public MeshData(bool isMainMesh = true)
    {
        
        if (isMainMesh)
        {
            waterMesh = new MeshData(false);
        }
    }*/

    public void AddVertex(Vector3 vertex, bool vertexGeneratesCollider)
    {
        vertices.Add(vertex);
        if (vertexGeneratesCollider)
        {
            colliderVertices.Add(vertex);
        }
    }
    public void AddQuadTriangles(bool quadGeneratesCollider)
    {
        triangles.Add(vertices.Count - 4); // index = 0
        triangles.Add(vertices.Count - 3); // index = 1
        triangles.Add(vertices.Count - 2); // index = 2 

        triangles.Add(vertices.Count - 4); //index = 0
        triangles.Add(vertices.Count - 2); //index = 2
        triangles.Add(vertices.Count - 1); // index = 3

        if (quadGeneratesCollider)
        {
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 3);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 4);
            colliderTriangles.Add(colliderVertices.Count - 2);
            colliderTriangles.Add(colliderVertices.Count - 1);
        }
    }
    

}
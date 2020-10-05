using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static Mesh GeneratePolygonMesh(Vector3[] points) {

        MeshData meshData = new MeshData(points.Length);
        meshData.vertices = points;

        for (int i = 1; i < points.Length - 1; i++) {
            meshData.AddTriangle(0, i, i + 1);
        }

        return meshData.CreateMesh();
    }
}
 
public class MeshData {
    public Vector3[] vertices;
    public int[] triangles;

    int triangleIndex;

    public MeshData(int numVertices) {
        vertices = new Vector3[numVertices];
        triangles = new int[(numVertices - 1) * 3];
    }

    public void AddTriangle(int a, int b, int c) {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        return mesh;
    }
}
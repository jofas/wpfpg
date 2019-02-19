using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Cube : MonoBehaviour
{
  public Vector3 p0 = new Vector3(0,0,0);
  public Vector3 p1 = new Vector3(1,0,0);
  public Vector3 p2 = new Vector3(0,1,0);
  public Vector3 p3 = new Vector3(1,1,0);
  public Vector3 p4 = new Vector3(0,0,1);
  public Vector3 p5 = new Vector3(1,0,1);
  public Vector3 p6 = new Vector3(0,1,1);
  public Vector3 p7 = new Vector3(1,1,1);

  public MeshFilter mesh_f;
  public MeshRenderer mesh_r;
  public Mesh mesh;

  public Material material;

  public void set_points(Vector3 pos, float r) {
    this.p0 = new Vector3(pos.x - r, pos.y - r, pos.z - r);
    this.p1 = new Vector3(pos.x + r, pos.y - r, pos.z - r);
    this.p2 = new Vector3(pos.x - r, pos.y + r, pos.z - r);
    this.p3 = new Vector3(pos.x + r, pos.y + r, pos.z - r);
    this.p4 = new Vector3(pos.x - r, pos.y - r, pos.z + r);
    this.p5 = new Vector3(pos.x + r, pos.y - r, pos.z + r);
    this.p6 = new Vector3(pos.x - r, pos.y + r, pos.z + r);
    this.p7 = new Vector3(pos.x + r, pos.y + r, pos.z + r);
  }

  public void CreateMesh() {
    mesh_f = this.gameObject.GetComponent<MeshFilter>();
    if (mesh_f == null) {
      mesh_f = this.gameObject.AddComponent<MeshFilter>();
    }

    mesh_r = this.gameObject.GetComponent<MeshRenderer>();
    if (mesh_r == null) {
      mesh_r =
        this.gameObject.AddComponent<MeshRenderer>();
    }

    mesh = mesh_f.sharedMesh;
    if (mesh == null) {
      mesh = new Mesh();
      mesh.name = "Cube";
    }

    if (material == null)
      material = new Material(Shader.Find("Standard"));

    mesh_r.sharedMaterial = material;

    Vector3[] vertices = new Vector3[] { p0, p1, p2, p3
                                       , p4, p5, p6, p7

                                       , p4, p0, p6, p2
                                       , p5, p1, p7, p3

                                       , p2, p3, p6, p7
                                       , p0, p1, p4, p5 };

    int[] triangles = new int[] { 0, 2, 1
                                , 1, 2, 3

                                , 4, 5, 6
                                , 5, 7, 6

                                , 8, 10, 9
                                , 9, 10, 11

                                , 12, 13, 14
                                , 13, 15, 14

                                , 16, 18, 17
                                , 17, 18, 19

                                , 20, 21, 22
                                , 21, 23, 22 };

    mesh.Clear();
    mesh.vertices  = vertices;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();
    mesh_f.sharedMesh = mesh;
  }
}

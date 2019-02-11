using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
  public Vector3 p0 = Vector3.zero;
  public Vector3 p1 = Vector3.forward;
  public Vector3 p2 = Vector3.right;

  public MeshFilter mesh_f;
  public MeshRenderer mesh_r;
  public Mesh mesh;

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
      mesh.name = "Triangle";
    }

    Vector3[] vertices = new Vector3[] { p0, p1, p2 };
    int[] triangles = new int[] { 0, 1, 2 };

    mesh.Clear();
    mesh.vertices = vertices;
    mesh.triangles = triangles;

    mesh_f.sharedMesh = mesh;
  }

  // OnDrawGizmos {{{
  void OnDrawGizmos() {
    Vector3 p0_trans = transform.TransformPoint(p0);
    Vector3 p1_trans = transform.TransformPoint(p1);
    Vector3 p2_trans = transform.TransformPoint(p2);

    Gizmos.DrawLine(p0_trans, p1_trans);
    Gizmos.DrawLine(p0_trans, p2_trans);
    Gizmos.DrawLine(p1_trans, p2_trans);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(p0_trans, 0.1f);

    Gizmos.color = Color.green;
    Gizmos.DrawSphere(p1_trans, 0.1f);

    Gizmos.color = Color.blue;
    Gizmos.DrawSphere(p2_trans, 0.1f);

    CreateMesh();
  }
  // }}}
}

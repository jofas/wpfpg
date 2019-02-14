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
  public Texture texture;

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
    if (texture == null)
      texture  = Resources.Load<Texture>("texture");

    material.mainTexture  = texture;

    mesh_r.sharedMaterial = material;

    Vector2[] uvs = new Vector2[] { new Vector2(0f, 0f)
                                  , new Vector2(0f, 1f)
                                  , new Vector2(1f, 0f)
                                  , new Vector2(1f, 1f)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(0f, 1f)
                                  , new Vector2(1f, 0f)
                                  , new Vector2(1f, 1f)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(0f, 1f)
                                  , new Vector2(1f, 0f)
                                  , new Vector2(1f, 1f)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(0f, 1f)
                                  , new Vector2(1f, 0f)
                                  , new Vector2(1f, 1f)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(0f, 1f)
                                  , new Vector2(1f, 0f)
                                  , new Vector2(1f, 1f)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(0f, 1f)
                                  , new Vector2(1f, 0f)
                                  , new Vector2(1f, 1f) };

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

    mesh.uv = uvs;

    mesh_f.sharedMesh = mesh;
  }

  // OnDrawGizmos {{{
  void OnDrawGizmos() {
    Vector3 p0_trans = transform.TransformPoint(p0);
    Vector3 p1_trans = transform.TransformPoint(p1);
    Vector3 p2_trans = transform.TransformPoint(p2);
    Vector3 p3_trans = transform.TransformPoint(p3);
    Vector3 p4_trans = transform.TransformPoint(p4);
    Vector3 p5_trans = transform.TransformPoint(p5);
    Vector3 p6_trans = transform.TransformPoint(p6);
    Vector3 p7_trans = transform.TransformPoint(p7);

    Gizmos.DrawLine(p0_trans, p1_trans);
    Gizmos.DrawLine(p0_trans, p2_trans);
    Gizmos.DrawLine(p0_trans, p4_trans);
    Gizmos.DrawLine(p1_trans, p3_trans);
    Gizmos.DrawLine(p1_trans, p5_trans);
    Gizmos.DrawLine(p2_trans, p3_trans);
    Gizmos.DrawLine(p2_trans, p6_trans);
    Gizmos.DrawLine(p3_trans, p7_trans);
    Gizmos.DrawLine(p4_trans, p5_trans);
    Gizmos.DrawLine(p4_trans, p6_trans);
    Gizmos.DrawLine(p5_trans, p7_trans);
    Gizmos.DrawLine(p6_trans, p7_trans);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(p0_trans, 0.1f);

    Gizmos.color = Color.green;
    Gizmos.DrawSphere(p1_trans, 0.1f);

    Gizmos.color = Color.blue;
    Gizmos.DrawSphere(p2_trans, 0.1f);

    Gizmos.color = Color.black;
    Gizmos.DrawSphere(p3_trans, 0.1f);

    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(p4_trans, 0.1f);

    Gizmos.color = Color.cyan;
    Gizmos.DrawSphere(p5_trans, 0.1f);

    Gizmos.color = Color.magenta;
    Gizmos.DrawSphere(p6_trans, 0.1f);

    Gizmos.color = Color.grey;
    Gizmos.DrawSphere(p7_trans, 0.1f);

    CreateMesh();
  }
  // }}}
}

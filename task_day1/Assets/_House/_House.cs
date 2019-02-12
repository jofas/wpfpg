using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _House : MonoBehaviour
{
  public float width  = 1f;
  public float height = 1f;
  public float depth  = 1f;
  [Range(0f,1f)]
  public float p      = 0.7f;

  public MeshFilter mesh_f;
  public MeshRenderer mesh_r;
  public Mesh mesh;

  public Material material_front;
  public Texture texture_front;

  public Material material_side;
  public Texture texture_side;

  public Material material_roof;
  public Texture texture_roof;

  private Vector3 p0 = new Vector3(0,0,0);
  private Vector3 p1;
  private Vector3 p2;
  private Vector3 p3;
  private Vector3 p4;
  private Vector3 p5;
  private Vector3 p6;
  private Vector3 p7;
  private Vector3 p8;
  private Vector3 p9;

  private void set_vecs() {
    p1 = new Vector3(width,    0,          0);
    p2 = new Vector3(0,        height * p, 0);
    p3 = new Vector3(width,    height * p, 0);
    p4 = new Vector3(0,        0,          depth);
    p5 = new Vector3(width,    0,          depth);
    p6 = new Vector3(0,        height * p, depth);
    p7 = new Vector3(width,    height * p, depth);
    p8 = new Vector3(width/2f, height,     0);
    p9 = new Vector3(width/2f, height,     depth);
  }

  // CreateMesh {{{
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

    if (material_front == null)
      material_front =
        new Material(Shader.Find("Standard"));
    if (texture_front == null)
      texture_front  = Resources.Load<Texture>("texture");

    material_front.mainTexture  = texture_front;

    if (material_side == null)
      material_side =
        new Material(Shader.Find("Standard"));
    if (texture_side == null)
      texture_side  = Resources.Load<Texture>("java");

    material_side.mainTexture  = texture_side;

    if (material_roof == null)
      material_roof =
        new Material(Shader.Find("Standard"));
    if (texture_roof == null)
      texture_roof  = Resources.Load<Texture>("rec");

    material_roof.mainTexture  = texture_roof;

    mesh_r.sharedMaterials = new Material[]
      { material_front, material_side, material_roof };

    Vector2[] uvs = new Vector2[] { new Vector2(0f, 0f)
                                  , new Vector2(width, 0f)
                                  , new Vector2(0f, height * p)
                                  , new Vector2(width, height * p)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(width, 1f)
                                  , new Vector2(0f, height * p)
                                  , new Vector2(width, height * p)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(depth, 0f)
                                  , new Vector2(0f, height * p)
                                  , new Vector2(depth, height * p)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(depth, 0f)
                                  , new Vector2(0f, height * p)
                                  , new Vector2(depth, height * p)

                                  , new Vector2(0f, height * p)
                                  , new Vector2(width / 2, height)
                                  , new Vector2(width, height * p)

                                  , new Vector2(0f, height * p)
                                  , new Vector2(width / 2, height)
                                  , new Vector2(width, height * p)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(depth, 0f)
                                  , new Vector2(0f, height - height * p)
                                  , new Vector2(depth, height - height * p)

                                  , new Vector2(0f, 0f)
                                  , new Vector2(depth, 0f)
                                  , new Vector2(0f, height - height * p)
                                  , new Vector2(depth, height - height * p)
                                  };

    Vector3[] vertices = new Vector3[] { p0, p1, p2, p3
                                       , p4, p5, p6, p7

                                       , p4, p0, p6, p2
                                       , p5, p1, p7, p3

                                       , p2, p8, p3
                                       , p7, p9, p6

                                       , p6, p2, p9, p8
                                       , p3, p7, p8, p9
                                       };

    int[] triangles_front = new int[] {
                                  // front
                                  0, 2, 1
                                , 1, 2, 3

                                  // back
                                , 4, 5, 6
                                , 5, 7, 6

                                  // gibel front
                                , 16, 17, 18

                                  // gibel back
                                , 19, 20, 21
    };


    int[] triangles_side = new int[] {
                                  // left side
                                  8, 10, 9
                                , 9, 10, 11

                                  // right side
                                , 12, 13, 14
                                , 13, 15, 14

    };


    int[] triangles_roof = new int[] {
                                  // left roof
                                  22, 24, 23
                                , 23, 24, 25

                                  // right roof
                                , 26, 28, 27
                                , 27, 28, 29
    };

    int[] triangles = new int[] {
                                  // front
                                  0, 2, 1
                                , 1, 2, 3

                                  // back
                                , 4, 5, 6
                                , 5, 7, 6

                                  // left side
                                , 8, 10, 9
                                , 9, 10, 11

                                  // right side
                                , 12, 13, 14
                                , 13, 15, 14

                                  // gibel front
                                , 16, 17, 18

                                  // gibel back
                                , 19, 20, 21

                                  // left roof
                                , 22, 24, 23
                                , 23, 24, 25

                                  // right roof
                                , 26, 28, 27
                                , 27, 28, 29
                                };

    mesh.Clear();
    mesh.subMeshCount = 3;

    mesh.vertices  = vertices;
    //mesh.triangles = triangles;

    mesh.SetTriangles(triangles_front, 0);
    mesh.SetTriangles(triangles_side, 1);
    mesh.SetTriangles(triangles_roof, 2);

    mesh.uv = uvs;

    mesh_f.sharedMesh = mesh;
  }
  // }}}

  // OnDrawGizmos {{{
  void OnDrawGizmos() {

    set_vecs();

    Vector3 p0_trans = transform.TransformPoint(p0);
    Vector3 p1_trans = transform.TransformPoint(p1);
    Vector3 p2_trans = transform.TransformPoint(p2);
    Vector3 p3_trans = transform.TransformPoint(p3);
    Vector3 p4_trans = transform.TransformPoint(p4);
    Vector3 p5_trans = transform.TransformPoint(p5);
    Vector3 p6_trans = transform.TransformPoint(p6);
    Vector3 p7_trans = transform.TransformPoint(p7);
    Vector3 p8_trans = transform.TransformPoint(p8);
    Vector3 p9_trans = transform.TransformPoint(p9);

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

    // Gibel
    Gizmos.DrawLine(p2_trans, p8_trans);
    Gizmos.DrawLine(p3_trans, p8_trans);
    Gizmos.DrawLine(p6_trans, p9_trans);
    Gizmos.DrawLine(p7_trans, p9_trans);
    Gizmos.DrawLine(p8_trans, p9_trans);

    /*
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
    */
    CreateMesh();
  }
  // }}}
}

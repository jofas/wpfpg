using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public delegate Quaternion del(Quaternion q);

public class JuliaSet : MonoBehaviour
{
  public float x    = -1f;
  public float y    =  0f;
  public float z    =  0f;
  public float w    =  0f;

  // TODO
  public float pow  = 2f;

  public int iter   = 16;

  public int x_min  = -32,
             x_max  =  32,
             y_min  = -32,
             y_max  =  32,
             z_min  = -32,
             z_max  =  32;

  public float r    = 0.5f;

  MeshFilter   mesh_f;
  MeshRenderer mesh_r;
  Mesh         mesh;
  Material     material;

  private del F;

  private List<Quaternion> set = null;
  private List<Vector3> points = null;

  public void init() {
    var q0 =
      new Quaternion(this.x, this.y, this.z, this.w);
    this.F = q => add_q((q * q * q * q * q * q * q), q0);

    if (this.set == null) {
      this.set = inverse_iteration_method();
      this.points = set_to_points();
    } else {
      this.set = null;
      this.set = inverse_iteration_method();
      this.points = null;
      this.points = set_to_points();
    }
    CreateMesh();
  }

  List<Vector3> set_to_points() {
    var res = new List<Vector3>();
    foreach (Quaternion q in this.set) {
      var point = new Vector3(q.x, q.y, q.z);
      res.Add(point);
    }
    return res;
  }

  float normalize(float val, float max, float min) {
    return (val - min) / (max - min);
  }

  Quaternion scale(float x_, float y_, float z_) {
    float x = normalize(x_, (float) x_max, (float) x_min)
            * 6f - 3f;
    float y = normalize(y_, (float) y_max, (float) y_min)
            * 6f - 3f;
    float z = normalize(z_, (float) z_max, (float) z_min)
            * 6f - 3f;
    return new Quaternion(x, y, z, this.w);
  }

  List<Quaternion> inverse_iteration_method() {
    var res = new List<Quaternion>();
    for (int x = x_min; x <= x_max; x++) {
      for (int y = y_min; y <= y_max; y++) {
        for (int z = z_min; z <= z_max; z++) {
          var q_ = new Quaternion(
            (float) x, (float) y, (float) z, this.w
          );
          Quaternion q = scale(x, y, z);
          var in_set = true;
          for (int i = 0; i < iter; i++) {
            q = F(q);
            if (magnitude(q) > 4d) {
              in_set = false;
              break;
            }
          }
          if (in_set)
            res.Add(q_);
        }
      }
    }
    return res;
  }

  float magnitude(Quaternion q) {
    return (float) Math.Sqrt((q.x * q.x) + (q.y * q.y)
                           + (q.z * q.z) + (q.w * q.w));
  }

  Quaternion add_q(Quaternion q1, Quaternion q2) {
    float x = q1.x + q2.x;
    float y = q1.y + q2.y;
    float z = q1.z + q2.z;
    float w = q1.w + q2.w;
    return new Quaternion(x, y, z, w);
  }

  void set_mesh() {
    mesh_f = GetComponent<MeshFilter>();
    if (mesh_f == null)
      mesh_f = gameObject.AddComponent<MeshFilter>();

    mesh_f.sharedMesh = mesh = new Mesh();

    mesh_r = GetComponent<MeshRenderer>();
    if (mesh_r == null)
      mesh_r = gameObject.AddComponent<MeshRenderer> ();

    if (material == null)
      material = new Material(Shader.Find("_Shader"));

    mesh_r.sharedMaterial = material;
  }
  public void CreateMesh() {
    //set_mesh();
    //foreach (Vector3 point in this.points) {
    //  var cube = new _Cube();
    //  cube.set_points(point, 0.1f);
    //  cube.CreateMesh();
    //}
  }

  public void OnDrawGizmos() {
    //Gizmos.matrix = transform.localToWorldMatrix;
    //Gizmos.color = Color.white;
    //foreach (var point in this.points) {
    //  Gizmos.DrawSphere(point, this.r);
    //}
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}

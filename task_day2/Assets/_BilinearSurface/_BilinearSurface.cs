using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BilinearSurface : MonoBehaviour
{
  public Vector3 p0 = new Vector3(0,0,0);
  public Vector3 p1 = new Vector3(1,0,0);
  public Vector3 p2 = new Vector3(0,1,0);
  public Vector3 p3 = new Vector3(1,1,0);

  [Range(0,1)]
  public float u = 0.5f;
  [Range(0,1)]
  public float w = 0.5f;

  [Range(2,10)]
  public int m = 5;
  [Range(2,10)]
  public int n = 3;

  private Vector3 pt;

  Vector3 compute_pt(float u_, float w_) {
    return p0 * (1 - u_) * (1 - w_)
         + p1 * (1 - u_) * w_
         + p2 * u_ * (1 - w_)
         + p3 * u_ * w_;
  }

  void OnDrawGizmos() {
    pt = compute_pt(u, w);

    Vector3 p0_trans = transform.TransformPoint(p0);
    Vector3 p1_trans = transform.TransformPoint(p1);
    Vector3 p2_trans = transform.TransformPoint(p2);
    Vector3 p3_trans = transform.TransformPoint(p3);

    Vector3 pt_trans = transform.TransformPoint(pt);

    Gizmos.DrawLine(p0_trans, p1_trans);
    Gizmos.DrawLine(p0_trans, p2_trans);
    Gizmos.DrawLine(p1_trans, p3_trans);
    Gizmos.DrawLine(p2_trans, p3_trans);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(p0_trans, 0.1f);

    Gizmos.color = Color.green;
    Gizmos.DrawSphere(p1_trans, 0.1f);

    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(p2_trans, 0.1f);

    Gizmos.color = Color.cyan;
    Gizmos.DrawSphere(p3_trans, 0.1f);

    Gizmos.color = Color.black;
    Gizmos.DrawSphere(pt_trans, 0.1f);

    Gizmos.color = Color.grey;

    float step_m = 1f / ( m - 1 );
    float step_n = 1f / ( n - 1 );

    for (int i = 0; i < m; i++) {
      for (int j = 0; j < n; j++) {
        Vector3 p_ = compute_pt(j * step_n, i * step_m);
        Vector3 p__trans = transform.TransformPoint(p_);
        Gizmos.DrawSphere(p__trans, 0.05f);
      }
    }
  }
}

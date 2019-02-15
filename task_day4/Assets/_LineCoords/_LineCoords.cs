using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _LineCoords : MonoBehaviour
{
  public Vector3 p0 = new Vector3(0,0,0);
  public Vector3 p1 = new Vector3(1,1,1);
  [Range(-1,2)]
  public float t = 0.5f;

  private Vector3 pt;

  void compute_pt() {
    pt = (1 - t) * p0 + t * p1;
  }

  void OnDrawGizmos() {
    compute_pt();

    Vector3 p0_trans = transform.TransformPoint(p0);
    Vector3 p1_trans = transform.TransformPoint(p1);
    Vector3 pt_trans = transform.TransformPoint(pt);

    Gizmos.DrawLine(p0_trans, p1_trans);

    Gizmos.color = Color.red;
    Gizmos.DrawSphere(p0_trans, 0.1f);

    Gizmos.color = Color.green;
    Gizmos.DrawSphere(p1_trans, 0.1f);

    Gizmos.color = Color.blue;
    Gizmos.DrawSphere(pt_trans, 0.1f);
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

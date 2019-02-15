using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(_LineCoords))]
public class _LineCoordsEditor : Editor
{
  public _LineCoords lc;

  void Awake() {
    lc = target as _LineCoords;
  }

  [MenuItem("GameObject/3D Object/_LineCoords")]
  public static _LineCoords CreateLineCoords() {
    GameObject go = new GameObject("_LineCoords");
    _LineCoords lc = go.AddComponent<_LineCoords>();
    return lc;
  }

  public void OnSceneGUI() {
    Handles.matrix = lc.transform.localToWorldMatrix;

    lc.p0 = Handles.FreeMoveHandle(
      lc.p0,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    lc.p1 = Handles.FreeMoveHandle(
      lc.p1,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(_BilinearSurface))]
public class _BilinearSurfaceEditor : Editor
{
  public _BilinearSurface bs;

  void Awake() {
    bs = target as _BilinearSurface;
  }

  [MenuItem("GameObject/3D Object/_BilinearSurface")]
  public static _BilinearSurface CreateLineCoords() {
    GameObject go = new GameObject("_BilinearSurface");
    _BilinearSurface bs =
      go.AddComponent<_BilinearSurface>();
    return bs;
  }

  public void OnSceneGUI() {
    EditorGUI.BeginChangeCheck();

    Handles.matrix = bs.transform.localToWorldMatrix;

    bs.p0 = Handles.FreeMoveHandle(
      bs.p0,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    bs.p1 = Handles.FreeMoveHandle(
      bs.p1,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    bs.p2 = Handles.FreeMoveHandle(
      bs.p2,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    bs.p3 = Handles.FreeMoveHandle(
      bs.p3,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    if (EditorGUI.EndChangeCheck())
      bs.CreateMesh();
  }


  public override void OnInspectorGUI() {
    EditorGUI.BeginChangeCheck();
    DrawDefaultInspector();
    if (EditorGUI.EndChangeCheck())
      bs.CreateMesh();
  }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(_Quad))]
public class _QuadEditor : Editor
{
  private static bool draw_default = false;

  public _Quad quad;

  void Awake() {
    quad = target as _Quad;
  }

  [MenuItem("GameObject/3D Object/_Quad")]
  public static _Quad CreateQuad() {
    GameObject go = new GameObject("_Quad");
    _Quad quad = go.AddComponent<_Quad>();
    quad.CreateMesh();
    return quad;
  }

  public void OnSceneGUI() {

    Vector3 p0_trans = quad.transform
      .TransformPoint(quad.p0);

    Vector3 p1_trans = quad.transform
      .TransformPoint(quad.p1);

    Vector3 p2_trans = quad.transform
      .TransformPoint(quad.p2);

    Vector3 p3_trans = quad.transform
      .TransformPoint(quad.p3);

    quad.p0 = Handles.FreeMoveHandle(
      p0_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    quad.p0 = quad.transform
      .InverseTransformPoint(quad.p0);

    quad.p1 = Handles.FreeMoveHandle(
      p1_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    quad.p1 = quad.transform
      .InverseTransformPoint(quad.p1);

    quad.p2 = Handles.FreeMoveHandle(
      p2_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    quad.p2 = quad.transform
      .InverseTransformPoint(quad.p2);

    quad.p3 = Handles.FreeMoveHandle(
      p3_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    quad.p3 = quad.transform
      .InverseTransformPoint(quad.p3);
  }

  public override void OnInspectorGUI() {
    quad.p0 = EditorGUILayout
      .Vector3Field("p0", quad.p0);
    quad.p1 = EditorGUILayout
      .Vector3Field("p1", quad.p1);
    quad.p2 = EditorGUILayout
      .Vector3Field("p2", quad.p2);
    quad.p3 = EditorGUILayout
      .Vector3Field("p3", quad.p3);

    if (draw_default = EditorGUILayout
        .Foldout(draw_default, "Default")) {
      DrawDefaultInspector();
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Triangle))]
public class TriangleEditor : Editor
{
  private static bool draw_default = false;

  public Triangle triangle;

  void Awake() {
    triangle = target as Triangle;
  }

  [MenuItem("GameObject/3D Object/Triangle")]
  public static Triangle CreateTriangle() {
    GameObject go = new GameObject("Triangle");
    Triangle triangle = go.AddComponent<Triangle>();
    triangle.CreateMesh();
    return triangle;
  }

  public void OnSceneGUI() {

    Vector3 p0_trans = triangle.transform
      .TransformPoint(triangle.p0);

    Vector3 p1_trans = triangle.transform
      .TransformPoint(triangle.p1);

    Vector3 p2_trans = triangle.transform
      .TransformPoint(triangle.p2);

    triangle.p0 = Handles.FreeMoveHandle(
      p0_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    triangle.p0 = triangle.transform
      .InverseTransformPoint(triangle.p0);

    triangle.p1 = Handles.FreeMoveHandle(
      p1_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    triangle.p1 = triangle.transform
      .InverseTransformPoint(triangle.p1);

    triangle.p2 = Handles.FreeMoveHandle(
      p2_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    triangle.p2 = triangle.transform
      .InverseTransformPoint(triangle.p2);
  }

  public override void OnInspectorGUI() {
    triangle.p0 = EditorGUILayout
      .Vector3Field("p0", triangle.p0);
    triangle.p1 = EditorGUILayout
      .Vector3Field("p1", triangle.p1);
    triangle.p2 = EditorGUILayout
      .Vector3Field("p2", triangle.p2);

    if (draw_default = EditorGUILayout
        .Foldout(draw_default, "DrawDefaultInspector")) {
      DrawDefaultInspector();
    }
  }
}

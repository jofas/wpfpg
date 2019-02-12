using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(_Cube))]
public class _CubeEditor : Editor
{
  private static bool draw_default = false;

  public _Cube cube;

  void Awake() {
    cube = target as _Cube;
  }

  [MenuItem("GameObject/3D Object/_Cube")]
  public static _Cube CreateCube() {
    GameObject go = new GameObject("_Cube");
    _Cube cube = go.AddComponent<_Cube>();
    cube.CreateMesh();
    return cube;
  }

  public void OnSceneGUI() {

    Vector3 p0_trans = cube.transform
      .TransformPoint(cube.p0);

    Vector3 p1_trans = cube.transform
      .TransformPoint(cube.p1);

    Vector3 p2_trans = cube.transform
      .TransformPoint(cube.p2);

    Vector3 p3_trans = cube.transform
      .TransformPoint(cube.p3);

    Vector3 p4_trans = cube.transform
      .TransformPoint(cube.p4);

    Vector3 p5_trans = cube.transform
      .TransformPoint(cube.p5);

    Vector3 p6_trans = cube.transform
      .TransformPoint(cube.p6);

    Vector3 p7_trans = cube.transform
      .TransformPoint(cube.p7);

    // Handles {{{
    cube.p0 = Handles.FreeMoveHandle(
      p0_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p0 = cube.transform
      .InverseTransformPoint(cube.p0);

    cube.p1 = Handles.FreeMoveHandle(
      p1_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p1 = cube.transform
      .InverseTransformPoint(cube.p1);

    cube.p2 = Handles.FreeMoveHandle(
      p2_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p2 = cube.transform
      .InverseTransformPoint(cube.p2);

    cube.p3 = Handles.FreeMoveHandle(
      p3_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p3 = cube.transform
      .InverseTransformPoint(cube.p3);
    // }}}

    // Handles {{{
    cube.p4 = Handles.FreeMoveHandle(
      p4_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p4 = cube.transform
      .InverseTransformPoint(cube.p4);

    cube.p5 = Handles.FreeMoveHandle(
      p5_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p5 = cube.transform
      .InverseTransformPoint(cube.p5);

    cube.p6 = Handles.FreeMoveHandle(
      p6_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p6 = cube.transform
      .InverseTransformPoint(cube.p6);

    cube.p7 = Handles.FreeMoveHandle(
      p7_trans,
      Quaternion.identity,
      0.1f,
      Vector3.zero,
      Handles.SphereHandleCap
    );

    cube.p7 = cube.transform
      .InverseTransformPoint(cube.p7);
    // }}}
  }

  public override void OnInspectorGUI() {
    cube.p0 = EditorGUILayout
      .Vector3Field("p0", cube.p0);
    cube.p1 = EditorGUILayout
      .Vector3Field("p1", cube.p1);
    cube.p2 = EditorGUILayout
      .Vector3Field("p2", cube.p2);
    cube.p3 = EditorGUILayout
      .Vector3Field("p3", cube.p3);
    cube.p4 = EditorGUILayout
      .Vector3Field("p4", cube.p4);
    cube.p5 = EditorGUILayout
      .Vector3Field("p5", cube.p5);
    cube.p6 = EditorGUILayout
      .Vector3Field("p6", cube.p6);
    cube.p7 = EditorGUILayout
      .Vector3Field("p7", cube.p7);

    if (draw_default = EditorGUILayout
        .Foldout(draw_default, "Default")) {
      DrawDefaultInspector();
    }
  }
}

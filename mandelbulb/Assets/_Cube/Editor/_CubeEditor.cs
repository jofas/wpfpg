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
    EditorGUI.BeginChangeCheck();
    if (EditorGUI.EndChangeCheck())
      cube.CreateMesh();
  }

  public override void OnInspectorGUI() {
    EditorGUI.BeginChangeCheck();

    DrawDefaultInspector();

    if (EditorGUI.EndChangeCheck())
      cube.CreateMesh();
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JuliaSet))]
public class JuliaSetEditor : Editor
{
  public JuliaSet J;

  void Awake() {
    J = target as JuliaSet;
  }

  [MenuItem("GameObject/JuliaSet")]
  public static JuliaSet CreateJS() {
    GameObject go = new GameObject("JuliaSet");
    var J = go.AddComponent<JuliaSet>();
    J.init();
    return J;
  }

  public void OnSceneGUI() {
    EditorGUI.BeginChangeCheck();
    if (EditorGUI.EndChangeCheck())
      J.init();
  }

  public override void OnInspectorGUI() {
    EditorGUI.BeginChangeCheck();

    DrawDefaultInspector();

    if (EditorGUI.EndChangeCheck())
      J.init();
  }
}

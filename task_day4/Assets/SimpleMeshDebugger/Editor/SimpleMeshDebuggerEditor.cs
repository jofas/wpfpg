using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleMeshDebugger))]
public class SimpleMeshDebuggerEditor : Editor
{
  SimpleMeshDebugger dbg;
  MeshFilter mesh_f;
  MeshRenderer mesh_r;
  Mesh mesh;

  // Info-Toggles {{{
  public bool vertexPos;
  public bool vertexInd;
  public bool uvs;
  public bool vertexNorm;
  public bool triangleNum;
  public bool triangleVertexInd;
  public bool triangleNorm;
  // }}}

  void Awake() {
    dbg    = target as SimpleMeshDebugger;
    mesh_f = dbg.GetComponent<MeshFilter>();
    mesh_r = dbg.GetComponent<MeshRenderer>();
    mesh   = mesh_f.sharedMesh;
  }

  void OnSceneGUI() {
    Handles.matrix = dbg.transform.localToWorldMatrix;

    if(vertexPos)         showVertexPos();
    if(vertexInd)         showVertexInd();
    if(uvs)               showUvs();
    if(vertexNorm)        showVertexNorm();
    if(triangleNum)       showTriangleNum();
    if(triangleVertexInd) showTriangleVertexInd();
    if(triangleNorm)      showTriangleNorm();
  }

  private void showVertexPos() {
    for (int i = 0; i < mesh.vertexCount; i++) {
      Handles.Label( mesh.vertices[i]
                   , mesh.vertices[i].ToString());
    }
  }

  private void showVertexInd() {
    for (int i = 0; i < mesh.vertexCount; i++) {
      Handles.Label( mesh.vertices[i]
                   , i.ToString());
    }
  }

  private void showUvs() {
    for (int i = 0; i < mesh.vertexCount; i++) {
      Handles.Label( mesh.vertices[i]
                   , mesh.uv[i].ToString());
    }
  }

  private void showVertexNorm() {
    for (int i = 0; i < mesh.vertexCount; i++) {
      Handles.color = new Color(
        Mathf.Abs(mesh.normals[i].x),
        Mathf.Abs(mesh.normals[i].y),
        Mathf.Abs(mesh.normals[i].z) );

      Handles.DrawLine( mesh.vertices[i]
                      , mesh.vertices[i] + mesh.normals[i]
                      );
    }
  }

  private void showTriangleNum() {
    for (int i = 0; i < mesh.triangles.Length; i+=3) {
      int p0_index = mesh.triangles[i + 0];
      int p1_index = mesh.triangles[i + 1];
      int p2_index = mesh.triangles[i + 2];

      Vector3 p0 = mesh.vertices[p0_index];
      Vector3 p1 = mesh.vertices[p1_index];
      Vector3 p2 = mesh.vertices[p2_index];

      Vector3 centroid = (p0 + p1 + p2) / 3f;

      Handles.Label( centroid, (i/3).ToString() );
    }
  }

  private void showTriangleVertexInd() {
    for (int i = 0; i < mesh.triangles.Length; i+=3) {
      int p0_index = mesh.triangles[i + 0];
      int p1_index = mesh.triangles[i + 1];
      int p2_index = mesh.triangles[i + 2];

      Vector3 p0 = mesh.vertices[p0_index];
      Vector3 p1 = mesh.vertices[p1_index];
      Vector3 p2 = mesh.vertices[p2_index];

      Vector3 centroid = (p0 + p1 + p2) / 3f;

      Vector3 p0Label = p0 + (centroid - p0) * 0.3f;
      Vector3 p1Label = p1 + (centroid - p1) * 0.3f;
      Vector3 p2Label = p2 + (centroid - p2) * 0.3f;

      Handles.Label(p0Label, p0_index.ToString());
      Handles.Label(p1Label, p1_index.ToString());
      Handles.Label(p2Label, p2_index.ToString());
    }
  }

  private void showTriangleNorm() {
    for (int i = 0; i < mesh.triangles.Length; i+=3) {
      int p0_index = mesh.triangles[i + 0];
      int p1_index = mesh.triangles[i + 1];
      int p2_index = mesh.triangles[i + 2];

      Vector3 p0 = mesh.vertices[p0_index];
      Vector3 p1 = mesh.vertices[p1_index];
      Vector3 p2 = mesh.vertices[p2_index];

      Vector3 centroid = (p0 + p1 + p2) / 3f;

      Vector3 p0_normal = mesh.normals[p0_index];
      Vector3 p1_normal = mesh.normals[p1_index];
      Vector3 p2_normal = mesh.normals[p2_index];

      Vector3 tc =
        (p0_normal + p1_normal + p2_normal).normalized;

      Handles.color = new Color(
        Mathf.Abs(tc.x),
        Mathf.Abs(tc.y),
        Mathf.Abs(tc.z) );

      Handles.DrawLine(centroid, centroid + tc);
    }
  }

  public override void OnInspectorGUI() {
    EditorGUI.BeginChangeCheck();

    vertexPos = EditorGUILayout
      .ToggleLeft( "Vertex Positions", vertexPos );
    vertexInd = EditorGUILayout
      .ToggleLeft( "Vertex Indices", vertexInd );
    uvs = EditorGUILayout
      .ToggleLeft( "UVs", uvs );
    vertexNorm = EditorGUILayout
      .ToggleLeft( "Vertex Normals", vertexNorm );
    triangleNum = EditorGUILayout
      .ToggleLeft( "Triangle Numbers", triangleNum );
    triangleVertexInd = EditorGUILayout
      .ToggleLeft( "Triangle Vertex Indices"
                 , triangleVertexInd );
    triangleNorm = EditorGUILayout
      .ToggleLeft( "Triangle Normals", triangleNorm );

    if (EditorGUI.EndChangeCheck())
      SceneView.RepaintAll();
  }
}

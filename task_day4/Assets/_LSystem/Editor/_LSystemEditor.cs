using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(_LSystemBehavior))]
public class _LSystemEditor : Editor
{
  _LSystemBehavior lsb;
  _LSystem         lsys;
  _TurtleRenderer  tr;

  string           nr = "Add new rule";

  void Awake() {
    lsb  = target as _LSystemBehavior;
    lsys = lsb.lsys;
    tr   = lsb.tr;
  }

  public override void OnInspectorGUI() {
    EditorGUI.BeginChangeCheck();

    lsys.axiom =
      EditorGUILayout.TextField("Axiom", lsys.axiom);

    lsys.iter =
      EditorGUILayout.IntSlider("Iter", lsys.iter, 0, 10);

    List<char> ks = lsys.dem_rulz.Keys.ToList();
    foreach (char k in ks) {
      EditorGUILayout.BeginHorizontal();

      EditorGUILayout.LabelField(k + "=");

      string v =
        EditorGUILayout.TextField(lsys.dem_rulz[k]);

      lsys.update_dem_rulz(k, v);

      if(GUILayout.Button("X"))
        lsys.del_da_fckn_rule(k);

      EditorGUILayout.EndHorizontal();
    }

    nr = EditorGUILayout.TextField(nr);

    if (GUILayout.Button("Add")) {
      lsys.update_dem_rulz(nr);
      nr = "Add new rule";
    }

    EditorGUILayout.Separator();

    tr.angle =
      EditorGUILayout.Slider("Angle", tr.angle, 0f, 360f);

    tr.dist =
      EditorGUILayout.Slider("Dist", tr.dist, -10f, 10f);

    if (EditorGUI.EndChangeCheck()) {
      lsb.gen = lsys.generate();
      SceneView.RepaintAll();
    }
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

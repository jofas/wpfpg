using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _LSystemBehavior : MonoBehaviour
{
  public _LSystem lsys = new _LSystem();
  public _TurtleRenderer tr = new _TurtleRenderer();
  public string gen = string.Empty;

  void OnDrawGizmos() {
    //string gs = lsys.generate();
    //Debug.Log("GS: " + gs);
    tr.process(gen);
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

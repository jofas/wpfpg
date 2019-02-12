using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeGrid : MonoBehaviour
{
    public int width  = 10;
    public int height = 10;

    // Start is called before the first frame update
    void Start()
    {
      for ( int x = 0; x < width; x++ ) {
        for ( int z = 0; z < width; z ++ ) {
          // GO + pos + append to parent
          GameObject go =
            GameObject.CreatePrimitive(PrimitiveType.Cube);
          go.transform.position = new Vector3(x, 0, z);
          go.transform.parent = this.gameObject.transform;
        }
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

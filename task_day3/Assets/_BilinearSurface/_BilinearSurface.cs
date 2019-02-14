using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BilinearSurface : MonoBehaviour
{
  public Vector3 p0 = new Vector3(0,0,0);
  public Vector3 p1 = new Vector3(20,0,0);
  public Vector3 p2 = new Vector3(0,0,20);
  public Vector3 p3 = new Vector3(20,0,20);

  [Range(0,1)]
  public float u = 0.5f;
  [Range(0,1)]
  public float w = 0.5f;

  [Range(2,360)]
  public int m = 5;
  [Range(2,180)]
  public int n = 3;

  public float amplitude = 1f;
  public float noise = 1f;

  public Texture2D main_t;
  public Texture2D height_map_t;
  public Texture2D noise_t;

  public Texture2D grass_t;
  public Texture2D rock_t;
  public Texture2D water_t;

  public float water_alt = 0.3f;
  public float grass_alt = 0.5f;

  int texture_dims = 512;

  private Vector3 pt;

  List<Vector3> vertices;
  List<Vector3> sphere_v;
  List<int> triangles;
  List<Vector2> uvs;

  MeshFilter   mesh_f;
  MeshRenderer mesh_r;
  Mesh         mesh;

  Material material;

  Vector3 compute_pt(float u_, float w_) {
    return p0 * (1 - u_) * (1 - w_)
         + p1 * (1 - u_) * w_
         + p2 * u_ * (1 - w_)
         + p3 * u_ * w_;
  }

  void OnDrawGizmos() {
    Gizmos.matrix = transform.localToWorldMatrix;

    pt = compute_pt(u, w);

    /*
    Vector3 p0_trans = transform.TransformPoint(p0);
    Vector3 p1_trans = transform.TransformPoint(p1);
    Vector3 p2_trans = transform.TransformPoint(p2);
    Vector3 p3_trans = transform.TransformPoint(p3);

    Vector3 pt_trans = transform.TransformPoint(pt);
    */
    Gizmos.DrawLine(p0, p1);
    Gizmos.DrawLine(p0, p2);
    Gizmos.DrawLine(p1, p3);
    Gizmos.DrawLine(p2, p3);
    /*
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(p0, 0.1f);

    Gizmos.color = Color.green;
    Gizmos.DrawSphere(p1, 0.1f);

    Gizmos.color = Color.yellow;
    Gizmos.DrawSphere(p2, 0.1f);

    Gizmos.color = Color.cyan;
    Gizmos.DrawSphere(p3, 0.1f);

    Gizmos.color = Color.black;
    Gizmos.DrawSphere(pt, 0.1f);
    */
    //Gizmos.color = Color.grey;

    /*
    float step_m = 1f / ( m - 1 );
    float step_n = 1f / ( n - 1 );

    for (int i = 0; i < m; i++) {
      for (int j = 0; j < n; j++) {
        Vector3 p_ = compute_pt(j * step_n, i * step_m);
        Vector3 p__trans = transform.TransformPoint(p_);
        Gizmos.DrawSphere(p__trans, 0.05f);
      }
    }
    */
    /*
    if (vertices != null) {
      for (int i = 0; i < vertices.Count; i++) {
        Gizmos.DrawSphere(vertices[i], 0.005f);
      }
    }
    */
    //CreateMesh();
  }

  void Update() {
    //if (vertices == null)
    //  StartCoroutine(CreateMesh());
  }

  void set_mesh() {
    mesh_f = GetComponent<MeshFilter>();
    if (mesh_f == null)
      mesh_f = gameObject.AddComponent<MeshFilter>();

    mesh_f.sharedMesh = mesh = new Mesh();

    mesh_r = GetComponent<MeshRenderer>();
    if (mesh_r == null)
      mesh_r = gameObject.AddComponent<MeshRenderer> ();

    if (material == null)
      material = new Material(Shader.Find("Standard"));

    material.mainTexture = main_t;
    mesh_r.sharedMaterial = material;
  }

  Vector3 point_on_sphere(float lon, float lat, float r){
    float lon_rad = lon * Mathf.Deg2Rad;
    float lat_rad = lat * Mathf.Deg2Rad;

    float x = Mathf.Cos(lon_rad) * Mathf.Sin(lat_rad);
    float y = Mathf.Sin(lon_rad) * Mathf.Sin(lat_rad);
    float z = Mathf.Cos(lat_rad) ;

    return new Vector3(x, y, z) * r;
  }

  Texture2D create_main_texture_from_noise() {
    Texture2D mt =
      new Texture2D(texture_dims, texture_dims);
    mt.name = "MainTexture";

    if (grass_t == null || rock_t == null || water_t == null) {
      Debug.LogWarning("Textures not set.");
      return mt;
    }

    for (int i = 0; i < texture_dims; i++) {
      for (int j = 0; j < texture_dims; j++) {
        Color c = height_map_t.GetPixel(i, j);

        if (c.grayscale < water_alt) {
          height_map_t.SetPixel(i, j,
            new Color(water_alt, water_alt, water_alt));
          mt.SetPixel(i, j,
            water_t.GetPixel(i % water_t.width,
                             j % water_t.height));
        } else {
          float lp = 1f - c.grayscale
                   - grass_alt / (1f - grass_alt);

          Color grass_hill = Color.Lerp(
            grass_t.GetPixel(
              i % grass_t.width,
              j % grass_t.height),
            rock_t.GetPixel(
              i % rock_t.width,
              j % rock_t.height),
            lp
          );

          mt.SetPixel(i, j, grass_hill);
        }
      }
    }

    mt.Apply();
    return mt;
  }

  Texture2D create_noise_height_map() {
    noise_t =
      new Texture2D(texture_dims, texture_dims);
    noise_t.name = "NoiseTexture";

    for (int i = 0; i < texture_dims; i++) {
      for (int j = 0; j < texture_dims; j++) {
        float grey_v = Mathf.PerlinNoise
          ( i * noise / (float) texture_dims
          , j * noise / (float) texture_dims );

        noise_t.SetPixel(
          i, j, new Color(grey_v, grey_v, grey_v)
        );
      }
    }

    noise_t.Apply();
    return noise_t;
  }

  public void CreateMesh() {
    set_mesh();

    //WaitForSeconds wait = new WaitForSeconds(0.25f);
    //WaitForSeconds wait_longer = new WaitForSeconds(1f);

    float step_m = 1f / ( m - 1 );
    float step_n = 1f / ( n - 1 );

    float sphere_step_m = 360f / ( m - 1 );
    float sphere_step_n = 180f / ( n - 1 );

    height_map_t = create_noise_height_map();
    main_t       = create_main_texture_from_noise();

    vertices = new List<Vector3>();
    sphere_v = new List<Vector3>();
    uvs      = new List<Vector2>();

    for (int i = 0; i < m; i++) {
      for (int j = 0; j < n; j++) {
        float _i = step_m * i;
        float _j = step_n * j;

        Color h_c = height_map_t.GetPixelBilinear(_i, _j);
        float h   = h_c.grayscale * amplitude;

        Vector3 p = compute_pt(_i, _j);
        //p.y = Mathf.PerlinNoise(p.x * noise, p.z * noise)
        //    * amplitude;

        p.y = h;

        vertices.Add(p);

        float lon = sphere_step_m * i;
        float lat = sphere_step_n * j;

        Vector3 sphere_p =
          point_on_sphere(lon, lat, 10f + h);
        sphere_v.Add(sphere_p);

        uvs.Add(new Vector2(_i, _j));
        //yield return wait;
      }
    }

    mesh.SetVertices(sphere_v);
    mesh.uv = uvs.ToArray();

    triangles = new List<int>();

    for (int i = 0; i < (m - 1); i++) {
      for (int j = 0; j < (n - 1); j++) {

        int i0 = i       * n + j     ;
        int i1 = i       * n + j + 1 ;
        int i2 = (i + 1) * n + j     ;
        int i3 = (i + 1) * n + j + 1 ;

        triangles.Add(i0);
        triangles.Add(i1);
        triangles.Add(i2);

        triangles.Add(i2);
        triangles.Add(i1);
        triangles.Add(i3);

        mesh.triangles = triangles.ToArray();
        //yield return wait_longer;
      }
    }
    mesh.RecalculateNormals();
  }
}

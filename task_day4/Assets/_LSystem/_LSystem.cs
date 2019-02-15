using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class _LSystem
{
  public string axiom = string.Empty;
  public int    iter  = 1;

  public Dictionary<char, string> dem_rulz
    = new Dictionary<char, string>();

  // alpha {{{
  public const char CLOCKKK        = '-' ,
                    COUNTERCLOCKKK = '+' ,
                    PITCHUP        = '&' ,
                    PITCHDOWN      = '^' ,
                    ROLLLEFT       = '\\',
                    ROLLRIGHT      = '/' ,
                    TURNAROUND     = 't' ,
                    PUSH           = '[' ,
                    POP            = ']' ,
                    COLOR          = 'c' ;
  // }}}

  public _LSystem() {
    this.axiom = "ab";
    this.dem_rulz.Add('a', "aab");
    this.dem_rulz.Add('b', "baa");
    this.iter = 3;
  }

  public string generate() {
    string axiom          = this.axiom;
    StringBuilder res     = new StringBuilder(axiom);
    StringBuilder gen_log = new StringBuilder();

    gen_log.Append("Axiom: ").Append(axiom);

    for (int i = 0; i < iter; i++) {
      axiom = res.ToString();
      res = new StringBuilder();

      for (int j = 0; j < axiom.Length; j++) {
        char c = axiom[j];
        string rule;
        if (dem_rulz.TryGetValue(c, out rule))
          res.Append(rule);
        else
          res.Append(c);

        if (res.Length > 100000) {
          Debug.LogError("Error: string too long");
          return "F*CK";
        }
      }
      gen_log.Append("\n")
        .Append((i + 1).ToString())
        .Append(". Iteration: ")
        .Append(res.ToString());
    }

    Debug.Log(gen_log.ToString());
    return res.ToString();
  }

  public bool update_dem_rulz(char k, string v) {
    if (!dem_rulz.ContainsKey(k)) {
      dem_rulz.Add(k, v);
    } else {
      string x;
      if (dem_rulz.TryGetValue(k, out x)) {
        dem_rulz[k] = v;
      }
    }
    return true;
  }

  public bool update_dem_rulz(string v) {
    if (!v.Contains("="))
      return false;

    string[] partz = v.Split('=');
    if (partz.Length == 2) {
      return update_dem_rulz(partz[0][0], partz[1]);
    }
    return false;
  }

  public void del_da_fckn_rule(char k) {
    if (dem_rulz.ContainsKey(k))
      dem_rulz.Remove(k);
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

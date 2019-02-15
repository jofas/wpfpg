using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TurtleRenderer
{
  public float angle = 22.5f;
  public float dist  = 1f;

  Stack<_Location> stack = new Stack<_Location>();

  public _TurtleRenderer() {

  }

  public void process(string s) {
    stack          = new Stack<_Location>();
    _Location last = new _Location();
    _Location curr = new _Location();

    int i = 0;
    foreach (char c in s) {
      switch (c) {
        case _LSystem.CLOCKKK:
          curr.clockkk(angle);
          break;
        case _LSystem.COUNTERCLOCKKK:
          curr.counterclockkk(angle);
          break;
        case _LSystem.PITCHUP:
          curr.pitchup(angle);
          break;
        case _LSystem.PITCHDOWN:
          curr.pitchdown(angle);
          break;
        case _LSystem.ROLLLEFT:
          curr.rollleft(angle);
          break;
        case _LSystem.ROLLRIGHT:
          curr.rollright(angle);
          break;
        case _LSystem.TURNAROUND:
          curr.turnaround();
          break;
        case _LSystem.PUSH:
          stack.Push(new _Location(curr));
          break;
        case _LSystem.POP:
          curr = stack.Pop();
          break;
        case _LSystem.COLOR:
          if (i + 1 < s.Length) {
            int color_num = int.Parse(s.Substring(i+1, 1));
            curr.color = get_color(color_num);
          }
          break;
        default:

          Gizmos.color = curr.color;
          Gizmos.DrawLine(last.pos, curr.pos);

          last.to(curr);
          curr.move(dist);
          break;
      }
      i++;
    }
  }

  Color get_color(int cn) {
    switch (cn) {
      case 0: return Color.black;
      case 1: return Color.red;
      case 2: return Color.blue;
      case 3: return Color.green;
      case 4: return Color.yellow;
      case 5: return Color.magenta;
      default:
        return Color.white;
    }
  }
}

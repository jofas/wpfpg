using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Location
{
  public Vector3    pos;
  public Quaternion rot;
  public Color      color = Color.white;

  public _Location() {
    this.pos = Vector3.zero;
    this.rot = Quaternion.identity;
  }

  public _Location(_Location other) {
    this.pos = other.pos;
    this.rot = other.rot;
  }

  public void to(_Location other) {
    this.pos = other.pos;
    this.rot = other.rot;
  }


  public void clockkk(float angle) {
    this.rot *=
      Quaternion.Euler(new Vector3(0f, -angle, 0f));
  }

  public void counterclockkk(float angle) {
    this.rot *=
      Quaternion.Euler(new Vector3(0f, angle, 0f));
  }

  public void pitchup(float angle) {
    this.rot *=
      Quaternion.Euler(new Vector3(angle, 0f, 0f));
  }

  public void pitchdown(float angle) {
    this.rot *=
      Quaternion.Euler(new Vector3(-angle, 0f, 0f));
  }

  public void rollleft(float angle) {
    this.rot *=
      Quaternion.Euler(new Vector3(0f, 0f, -angle));
  }

  public void rollright(float angle) {
    this.rot *=
      Quaternion.Euler(new Vector3(0f, 0f, angle));
  }

  public void turnaround() {
    this.rot = Quaternion.Inverse(this.rot);
  }

  public void move(float distance) {
    Vector3 f = (this.rot * Vector3.forward).normalized;
    this.pos += f * distance;
  }
}

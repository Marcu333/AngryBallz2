using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionEntity
{
    public float Attack { get; set; } 
    public float Defense { get; set; }

    public bool IsImmutable();

    public void PushOther(Collision collision);
}

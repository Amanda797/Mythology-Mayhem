using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public float ProjectileDamage { get; set; }
    public bool Parryable { get; set; }
    public float DestroyTimer { get; set; }

    public void Parry() { }

}

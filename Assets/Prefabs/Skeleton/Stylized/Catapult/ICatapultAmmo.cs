using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICatapultAmmo
{
    public ParticleSystem BlowEffect { get; }
    public int Damage { get; }

}

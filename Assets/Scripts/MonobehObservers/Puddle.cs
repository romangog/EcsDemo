using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private PuddleParticlesComponent _puddlePaticles;

    public PuddleParticlesComponent PuddleParticles => _puddlePaticles;
}

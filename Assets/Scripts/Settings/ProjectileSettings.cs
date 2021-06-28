using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ProjectileSettings
{
    [Header("Prefabs")]
    public GameObject cube;
    public GameObject sphere;
    [Header("Settings")]
    [Range(0, 50)] public float shotForce;
    [Range(0, 30)] public float explosionRange;
    [Range(0, 100)] public float maxDistanceToExpolison;
}

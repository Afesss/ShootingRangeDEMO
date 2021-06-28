using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ShapeSettings
{
    [Header("Prefabs")]
    public GameObject parent;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    [Header("Pool")]
    [Range(0, 50)]public int maxShapeNumberOnLevel;
    [Range(0, 10)] public int startShapeNumber;
    [Header("Shape")]
    [Range(0,100)] public float impulseForce;
    [Range(0, 50)] public float distanceToPlayer;
    [Range(0, 10)] public float minDistanceBetweenShapes;

}

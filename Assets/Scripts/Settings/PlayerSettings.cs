using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PlayerSettings
{
    [Header("Aim")]
    [Tooltip("Degrees per sec")]
    [Range(0, 80)] public float aimSpeed;
    [Tooltip("Rotation Y limit in Degrees")]
    [Range(0, 45)] public float maxYLimit;
    [Tooltip("Rotation Y limit in Degrees")]
    [Range(0,45)] public float minYLimit;
   
}

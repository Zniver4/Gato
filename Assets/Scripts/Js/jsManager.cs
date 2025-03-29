using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatoDbjs
{
    public int round;
    public int actual;
    public int score1;
    public int score2;
    public int[] board;
    public string p1;
    public string p2;
}
public class jsManager : MonoBehaviour
{
    GatoDbjs gatoDbjs;

    public void Start()
    {
        string jsonString = data
        gatoDbjs = JsonUtility.FromJson<GatoDbjs>();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public bool loose;
    [HideInInspector] public bool won;

    public List<GameObject> cameras = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    internal void Won()
    {
        Debug.Log("Won!!!");
        won = true;
    }

    internal void Loss()
    {
        Debug.Log("Loss");
        loose = true;
    }
}
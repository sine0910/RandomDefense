using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PrefabManager prefabs;

    void Awake()
    {
        instance = this;
    }
}

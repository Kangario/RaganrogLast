using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public string nameWorld = "DefaultWorld";
    public int sizeWorld = 100;
    public bool LoadWorld = false;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

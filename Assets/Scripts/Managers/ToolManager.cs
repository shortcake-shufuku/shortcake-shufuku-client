using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager
{
    public Transform SpawnPoint;
    public GameObject Prefab;

    [HideInInspector] public GameObject Instance;

    public void Setup()
    {
        // TODO
    }

    public void Reset()
    {
        Instance.transform.position = SpawnPoint.position;
        Instance.transform.rotation = SpawnPoint.rotation;

        Instance.SetActive(false);
        Instance.SetActive(true);
    }
}

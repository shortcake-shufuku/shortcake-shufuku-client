using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private GameObject Instance;
    private GameObject CakePrefab;

    public void Create(Transform SpawnPoint)
    {
        CakePrefab = Resources.Load("Prefabs/Cake") as GameObject;
        Instance = Instantiate(Resources.Load<GameObject>("Prefabs/Cake"), SpawnPoint.position, SpawnPoint.rotation);
        Setup();
    }

    public void Setup()
    {
        // TODO
    }

    public void Reset(Transform SpawnPoint)
    {
        Instance.transform.position = SpawnPoint.position;
        Instance.transform.rotation = SpawnPoint.rotation;

        Instance.SetActive(false);
        Instance.SetActive(true);
    }
}

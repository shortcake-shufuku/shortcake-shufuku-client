using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeManager : MonoBehaviour
{
    [HideInInspector] public GameObject Instance;

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
    }}

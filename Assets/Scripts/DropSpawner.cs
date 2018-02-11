using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{

    public Transform drop;

    public void spawn()
    {
        Transform obj = Instantiate(drop);
        obj.position = transform.position;
    }
}

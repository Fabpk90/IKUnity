using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameRunning && Gamepad.current.rightShoulder.wasReleasedThisFrame)
        {
            var spawned = Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}

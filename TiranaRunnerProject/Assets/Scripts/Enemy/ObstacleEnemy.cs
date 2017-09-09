using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ObstacleEnemy : MonoBehaviour
{
    public Transform explosionPrefab;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "FenceObstacle")
        {
            Debug.LogError("");
        }
    }
}

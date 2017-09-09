using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SCRIPT NOT USED IN PROJECT!!
/// </summary>
/// 
public class Spawner : MonoBehaviour 
{
    public GameObject[] obstacles;
    public Vector3 spawnValues;
    public Transform playerTransform;
    public int startWait;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public bool stop;

    float rand_x;
    float rand_z;
    float ySpread, lastYpos;
    int randObstacle;

    void Start()
    {
        //@KEJSI CHECK !!!!!!!!
        rand_x = Random.Range(0,6);
        rand_z = Random.Range(3, 50);
        lastYpos = playerTransform.position.y;
        StartCoroutine(waitSpwaner());
    }

    void Update()
    { 
        spawnWait = Random.Range(spawnLeastWait,spawnMostWait);
    }

    IEnumerator waitSpwaner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            //randomly pick an obstacle type to be spawned 
            randObstacle = Random.Range(0,obstacles.Length);

            float x_coordinate = playerTransform.position.x + rand_x;
            float z_coordinate = playerTransform.position.z + rand_z;

            if (GameObject.FindWithTag("Player")!= null) 
            {
                Debug.Log("Player transform XXXXX: "+playerTransform.position.x);
                Vector3 spawnPosition =
                    new Vector3(Random.Range(playerTransform.position.x-3,playerTransform.position.x+2), 
                        playerTransform.position.y, playerTransform.position.z + 12);


                obstacles[randObstacle].layer = LayerMask.NameToLayer("unwalkableMask");

                Debug.Log("Layer: " + obstacles[randObstacle].layer);

                Instantiate(obstacles[randObstacle],spawnPosition + transform.TransformPoint(0,0,0),
                    obstacles[randObstacle].transform.rotation);

                Debug.Log("Obstacle Name: " + obstacles[randObstacle].name);
                if (playerTransform.position.z > spawnPosition.z)
                {
                    Debug.LogError("Destroying obstacle after runner passed through it");
                }
                else {
                    Debug.Log("Obstacle is ahead runner");
                }

            }

            yield return new WaitForSeconds(spawnWait);
        }
    }
}

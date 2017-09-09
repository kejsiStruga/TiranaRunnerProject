using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PatrollingWaypoints : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int Currentpoint;
    public float moveSpeed;
    public float rotateSpeed;
    public bool rotating;
    int Range = 5;
    public Animator anim;

    public void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        transform.position = patrolPoints[0].position;
        Currentpoint = 0;
    }
    public void Patrol()
    {
        anim.Play("Walk");
        QuaternionRotateTowards(patrolPoints[Currentpoint].position);
        if (Vector3.Distance(transform.position, patrolPoints[Currentpoint].position) < 0.5f) 
        {
          Currentpoint++;
        }

        if (Currentpoint >= patrolPoints.Length)
        {
            Currentpoint = 0;
        }
        transform.position = 
            Vector3.MoveTowards(transform.position, patrolPoints[Currentpoint].position, moveSpeed * Time.deltaTime);
        
    }
    // Enemy rrotullohet vetem sipas Y ne menyre qe te veshtroje ne waypoint-in e rradhes 
    void QuaternionRotateTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        // Get rid of the Y axis variations
        direction.y = 0.0f;
        if (direction.magnitude < 0.1f)
        {
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                rotateSpeed * Time.deltaTime);
       // transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

}


﻿using UnityEngine;
using System.Collections;
using Assets.CoinsScripts;
using UnityEngine.SceneManagement;

public class UnitJill : PatrollingWaypoints
{
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;
    public Transform target;
    public float speed;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 0;
    public float awarnessRadius = 10;
    public float distanceToPlayer;
    public float enemyPlayerDistanceKill;
    public CharacterController enemyController;
    public GameManager gameManager;

    public bool stopFollowing;
    private float timer = 100f;
    public static Unit1 _instance;
    private bool stopp = false;
    private bool showGameOver = false;
    private Rect textArea = new Rect(0, 0, Screen.width, Screen.height);
    private Animator animEnemy, animPlayer;
    private bool caught;
    public bool lost = false;
    public static bool stopFollowing_JILL;

    Path path;
    GameObject player;
    private bool secondLevel;

    void Awake()
    {
        stopFollowing = false;
        stopFollowing_JILL = false;
        caught = false;
        player = GameObject.FindGameObjectWithTag("Player");

        animPlayer = player.GetComponentInChildren<Animator>();
        animEnemy = gameObject.GetComponentInChildren<Animator>();

        if (SceneManager.GetSceneAt(0).name == "SecondLevelTiranaRunner")
        {
            secondLevel = true;
        }
    }

    void Update()
    {
        if (!stopp && !caught && !showGameOver &&!stopFollowing_JILL)
        {
            StartCoroutine(UpdatePath());
            Invoke("TimedFunction", timer);
        }
        if (caught)
        {
            StartCoroutine("faceCameraOnGameOver");
        }
        if (ScoreSystem.onObstacle)
        {
            // speed += 0.001f;
        }
    }
    IEnumerator faceCameraOnGameOver()
    {
        if (caught || stopFollowing)
        {
            //Camera.main.transform.rotation = Quaternion.AngleAxis(-5, Vector3.right);
            //Camera.main.transform.position =  new Vector3 (Camera.main.transform.position.x,5,Camera.main.transform.position.z);
            float RotationSpeed = 0.05f;
            Quaternion newRot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.time * 0.004f);
            yield return new WaitForSeconds(2.0f);
            //After attacking
            if (lost)
            {
                GetComponentInChildren<Animator>().Play("Dead");
            }
            else
            {
                Debug.Log("Otherwise !!");
                animEnemy.Play("Idle");
            }
        }
    }
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful && !caught)
        {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);

            StopCoroutine("FollowPath");
            if (!lost )
                StartCoroutine("FollowPath");
        }
    }
    IEnumerator UpdatePath()
    {
        distanceToPlayer = 
            Vector3.Distance((Vector3)target.transform.position, (Vector3)gameObject.transform.position);

        if ((distanceToPlayer >= awarnessRadius))
        {
            if (!caught)
            {
                Patrol();
            }
        }
        else
        {
            stopp = true;
            if (Time.timeSinceLevelLoad < .3f)
            {
                yield return new WaitForSeconds(.3f);
            }
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPosOld = target.position;

            while (true)
            {
                /*
                * The magnitude of a vector v is calculated as Mathf.Sqrt(Vector3.Dot(v, v)).
                * However, the Sqrt calculation is quite complicated and takes longer to 
                * execute than the normal arithmetic operations. Calculating the squared
                * magnitude instead of using the magnitude property is much faster 
                * /
                 */
                yield return new WaitForSeconds(minPathUpdateTime);
                if ((Vector3.Distance(target.position, gameObject.transform.position) < enemyPlayerDistanceKill) && !caught && !stopFollowing && !lost)
                {
                    Debug.Log("Game Overr");
                    GameOverState();
                }
                if (((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) && !caught)
                {
                    PathRequestManager.RequestPath(new PathRequest(transform.position, target.position,
                        OnPathFound));
                    targetPosOld = target.position;
                }
            }
        }
    }

    void GameOverState()
    {

        Debug.Log("Game Over !!");
        if (secondLevel)
        {
            //Kill all enemies
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                obj.SetActive(false);
            }
        }
        else
        {
            anim.Play("GameOver");
        }
        gameManager.EndGame();
        Assets.CoinsScripts.ScoreSystem.InstanceScore.isDead = true;
        player.GetComponent<Player>().enabled = false;
        animPlayer.Play("Dead");
        //after dead animation , we show screeen 
        showGameOver = true;
        caught = true;
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

            while (followingPath)
            {
                Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
                while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
                {
                    if (pathIndex == path.finishLineIndex)
                    {
                        followingPath = false;
                        break;
                    }
                    else
                    {
                        pathIndex++;
                    }
                }
                if (followingPath)
                {
                    if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                    {
                        speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                        if (speedPercent < 0.01f)
                        {
                            followingPath = false;
                        }
                    }
                    Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                    anim.Play("Run");
                    Vector3 movementAI = new Vector3(0, 0, 1);
                    transform.Translate(movementAI * Time.deltaTime * speed * speedPercent, Space.Self);
                }
                yield return null;
            }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }

    void TimedFunction()
    {
        Destroy(gameObject);
        //---edit---\\
        return;
    }
}
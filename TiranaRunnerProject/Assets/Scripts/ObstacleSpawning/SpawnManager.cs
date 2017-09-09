using Assets.CoinsScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public Transform playerTransform;
    public int poolSize;
    public GameObject[] obstacles;
    /*
     * Ne menyre qe te sigurohemi se nuk behen instantiate afer borders !!
     */
    public Vector3 borderLeft;//=new Vector3(-15.17f, -8.7f, 133.5f);
    public Vector3 borderRight;// = new Vector3(-6.7f, -8.7f, 130.2f);
    public int startWait;
    public float playerZMax;
    public float playerZMin;
    /*Per ne coroutine
     * Vete spawnWait merr vlera random mes spwanMostWait dhe spawnLeastWait !!
     * */
    public float spawnWait;
    //2 vlera mes te cilave do fluctuate dhe do vendosim ne cilat incremente duam qe obstacles te behen spawn 
    public float spawnMostWait;
    public float spawnLeastWait;
    public float spwanYCoordinate;
    public static bool doNotSpawn;
    float lastYpos;
    private int obstaclesLength;
    private float xSpawnMin2;
    private float xSpawnMax2;
    public GameObject coin;
    private List<GameObject> activeCoins;
    private bool secondLevel = false;

    void Start()
    {
        activeCoins = new List<GameObject>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            PoolManager.instance.CreatePool(obstacles[i], poolSize);
        }
        lastYpos = playerTransform.position.y;

        
        StartCoroutine(waitSpwaner());
        
        
        doNotSpawn = false;
        //Check level to modify coordinates
        if (SceneManager.GetSceneAt(0).name == "SecondLevelTiranaRunner")
        {
            //spwanYCoordinate = -0.27f;
         //   secondLevel = true;
            spwanYCoordinate = -3.44f;
        }
        else
        {
            spwanYCoordinate = -0.9f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        if (GameObject.FindGameObjectWithTag("LeftBorder") != null)
        {
            borderLeft = GameObject.FindGameObjectWithTag("LeftBorder").transform.position;
            xSpawnMin2 = borderLeft.x + 2f;
            borderRight = GameObject.FindGameObjectWithTag("RightBorder").transform.position;
            xSpawnMax2 = borderRight.x - 2f;
        }
    }

    IEnumerator waitSpwaner()
    {
        //=> nuk do te filloje shperndarja e obstacles qe ne fillim te lojes por me pak vonese !!
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            if (!ScoreSystem.onObstacle)
            {
                //HARDCODED BOUNDRIES !!!!!!!!!!!!
                float distp = Mathf.Abs(borderLeft.x - playerTransform.position.x);

                float xSpawnMin;
                float xSpawnMax;

                if (SceneManager.GetSceneAt(0).name == "SecondLevelTiranaRunner")
                {
                    #region Second Level configs
                    xSpawnMin = (playerTransform.position.x + 2f);
                    xSpawnMax = (playerTransform.position.x - 2f);
                    #endregion
                }
                else
                {
                    secondLevel = false;
                    #region First Level configs

                    Debug.Log("dist player brod " + distp);
                    if (distp <= 0.9)
                    {
                        Debug.Log("Change Coordinates");
                        xSpawnMin = (playerTransform.position.x);
                        xSpawnMax = (playerTransform.position.x + 0.01f);
                    }
                    else if (distp >= 8)
                    {
                        xSpawnMin = (playerTransform.position.x - 0.01f);
                        xSpawnMax = (playerTransform.position.x);
                    }
                    else
                    {
                        Debug.Log("Coordinates are ok ");
                        xSpawnMin = (playerTransform.position.x - 1.88f);
                        xSpawnMax = (playerTransform.position.x + 1.88f);
                    }
                    #endregion
                }

                //KONTRROLLOJME NQS PLAYERI EKZISTON
                if (GameObject.FindWithTag("Player") != null)
                {// && !Score.Instance.gameOver)
                    // Debug.Log("Spawning Obstacles");
                    /*
                     * Vendosim ne menyre random pos se ku do behet instantiate obstacle 
                     * Secila nga dim zgjidhet ne menyre random pervec Y qe eshte hardcoded
                     * Ndekrohe qe Z eshte ne lidhje me playerin 
                     */
                    float randX;
                    if (secondLevel)
                    {
                        randX = Random.Range(borderLeft.x + 5f, borderRight.x - 5f);
                    }
                    else
                    {
                        randX = Random.Range(playerTransform.position.x - 0.2f, playerTransform.position.x + 0.4f);
                    }
                    Vector3 spawnPosition =
                        new Vector3(
                        //must be spawned between borders !!
                           // Random.Range(playerTransform.position.x - 0.2f, playerTransform.position.x + 0.4f),
                          randX,
                        //playerTransform.position.x+2f,
                            spwanYCoordinate,
                            Random.Range(
                                playerTransform.position.z + playerZMin,
                                playerTransform.position.z + playerZMax)
                        );
                    /*
                     * Pengesat do te behen instantiated vetem nqs loja nuk ka perfunduar
                     */
                    if (!ScoreSystem._instanceScore.gameManager.GameIsOver)
                    {
                        /*
                         * Ne menyre qe te rrisim veshtirsine e lojes => llogarisim distancen e player-it nga 
                         * finish-i (flag) dhe nese dist plotson nje kusht te caktuar => bejme instantiate edhe 
                         * 2 obstacles te tjera / jane me te gjera dhe zejne me shume hapesire ne rruge
                         */
                        //Tani vendosim se cilin obstacle do te bejme instatiate ne menyre random 
                        // duke perdorur Random.Range 
                        int randObstacle;
                        float dist = Vector3.Distance(GameObject.FindGameObjectWithTag("WinningFlag").transform.position,

                        GameObject.FindGameObjectWithTag("Player").transform.position);
                        //INCREASE DIFFICULTY LEVEL IF CLOSE TO WINNING STATE BY SPAWNING 3 OBJECTS 
                        if (ScoreSystem.InstanceScore.difficulty >= 11.7941)
                        {
                            //Debug.Log("Increase Spawn Speed !!");
                            obstaclesLength = obstacles.Length;
                        }
                        else
                        {
                            obstaclesLength = obstacles.Length;
                        }
                        randObstacle = Random.Range(0, obstacles.Length);
                        obstacles[randObstacle].layer = LayerMask.NameToLayer("Unwalkable");
                        /*
                         * Bejme instantiate nje objekt random (pra indexin e array te obstacles e zgjedhim ne menyre random)
                         */
                        float xAwayMin = Random.Range(borderLeft.x, borderLeft.x + 2f);
                        float xAwayMax = Random.Range(borderRight.x, borderLeft.x - 2f);

                        //if (secondLevel)
                        //{
                        //    float[] shittyCoordinates = {Random.Range(borderLeft.x + 5f, borderLeft.x + 6f)
                        //                                    ,Random.Range(borderRight.x - 5f, borderRight.x - 7f)};

                        //    Vector3 otherObstacle = new Vector3(shittyCoordinates[Random.Range(0, shittyCoordinates.Length)],
                        //             spwanYCoordinate,
                        //             Random.Range(playerTransform.position.z + 10, playerTransform.position.z + 8)
                        //         );

                        //    PoolManager.instance.ReuseObject(obstacles[randObstacle], otherObstacle, obstacles[randObstacle].transform.rotation);

                        //    yield return new WaitForSeconds(Random.Range(0.4f, 0.09f));
                        //}
                        //else
                        //{
                        PoolManager.instance.ReuseObject(obstacles[randObstacle], spawnPosition, obstacles[randObstacle].transform.rotation);
                      //  }
                    }
                }
                GenerateCoin();
            }
            /*
             * Brenda ciklit while presim per spawnWait , pra per nje kohe random derisa do te behet insatntiate objekti i rradhes
             */
            //Waitforsecs eshte arsyeja se pse po perdorim coroutine !! nuk e duam cdo frame
            yield return new WaitForSeconds(spawnWait);
        }
    }

    private void DeleteCoin()
    {
        if (activeCoins.Count > 0 && activeCoins[0] != null && activeCoins[0].transform.position.z < playerTransform.position.z)
        {
            Destroy(activeCoins[0]);
            activeCoins.RemoveAt(0);
        }
    }

    private void GenerateCoin()
    {
        var generateCoin = Random.Range(0, 3) == 1;
        float YCoordinateCoin = 0.354f;
        if (secondLevel)
        {
            YCoordinateCoin = -2.43f;
        }

        if (generateCoin)
        {
            int coinsToGenerate = Random.Range(0, 5);
            var x = Random.Range(borderLeft.x + 2, borderRight.x - 2);
            var z = Random.Range(playerTransform.position.z + 20, playerTransform.position.z + 25);
            for (int i = 0; i < coinsToGenerate; i++)
            {
                var newCoin = Instantiate(coin);
                newCoin.transform.position = new Vector3(x, -2.43f, z);
                activeCoins.Add(newCoin);
                z = z + 5;
            }
        }
        DeleteCoin();
    }
}


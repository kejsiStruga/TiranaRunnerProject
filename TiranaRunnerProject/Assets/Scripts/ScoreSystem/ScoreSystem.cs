using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.CoinsScripts
{
    public class ScoreSystem : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject winningFlag;
        public Text scorePoints;
        public Text highScore;
        public CharacterController player;
        public float _score = 0.0f;
        private int difficultyLevel = 1;
        private int maxDifficultyLevel = 10;
        private int scoreToNextLevel = 10;
        public static ScoreSystem _instanceScore;
        public bool gameOver = false;
        public bool isDead = false;
        public static bool onObstacle;
        public float difficulty;
        private bool secondLevel;

        public static ScoreSystem InstanceScore
        {
            get { return _instanceScore; }
        }

        void Awake()
        {
            if (_instanceScore == null)
                _instanceScore = this;
            else
                UnityEngine.Object.Destroy(this);
        }
        void Start()
        {
            gameOver = false;
            if (PlayerPrefs.GetInt("HighScore")!=null)
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
            winningFlag = GameObject.FindGameObjectWithTag("WinningFlag");

            if (SceneManager.GetSceneAt(0).name == "SecondLevelTiranaRunner")
            {
                secondLevel = true;
            }
        }

        void Update()
        {
            difficulty = (Time.time + 300) / 30;
            // Debug.Log(difficulty);

            if (!gameManager.GameIsOver)
            {
                _score += Time.deltaTime * difficultyLevel;
                scorePoints.text = ((int)_score).ToString();
                PlayerPrefs.SetInt("Score", (int)_score);

                if (PlayerPrefs.GetInt("HighScore") <= (int)_score)
                {
                    PlayerPrefs.SetInt("HighScore", (int)_score);
                    highScore.text = ((int)_score).ToString();
                }
            }
            if (Time.timeSinceLevelLoad >= 75f)
            {
                UnitJill.stopFollowing_JILL = true;
            }
            //KEJSI ADDED CODE FOR WIN STATE 
            if (winningFlag != null)
            {
                if (player.transform.position.z > winningFlag.transform.position.z + 34f)
                {
                    //Nqs Eli eshte afruar shume drejt fitores => AI nuk ka me cfare te beje !
                    Unit1._instance.stopFollowing = true;
                    Unit1._instance.lost = true;
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
                        if (GameObject.FindGameObjectWithTag("Enemy") != null)
                        {
                            GameObject.FindGameObjectWithTag("Enemy").SetActive(false);
                        }
                        isDead = true;
                    }
                    //GetComponentInChildren<Animator>().Play("Dead");
                    Debug.Log("Level Won !!");
                    Player.Speed = 0;
                    gameObject.GetComponentInChildren<Animator>().Play("winning_Eli");
                    gameManager.WinLevel();
                    this.enabled = false;
                }
            }
        }

        private void LevelUp()
        {
            if (difficultyLevel == maxDifficultyLevel)
            {
                return;
            }
            scoreToNextLevel *= 2;
            difficultyLevel++;
        }
        /*KEJSI ADDED CODE TO DECREASE SCORE/ENERGY? EACH TIME HE'LL COLLIDE WITH OBSTACLES 
         * SINCE WE WANT TO CHECK IF PLAYER IS COLLIDED => SCRIPT MUST BE ATTACHED TO PLAYER
        */
        public void OnControllerColliderHit(ControllerColliderHit hit)
        {
            
            if (hit.gameObject.layer == 9)
            {
                decreaseScore();
                onObstacle = true;

                if (UnitJill._instance != null)
                {
                    Unit1._instance.speed += 0.02f;
                }
                Unit1._instance.speed += 0.001f;
            }
        }
        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.name == "RoadCone_B")
            {
                onObstacle = true;
                Debug.Log("On trigger");
            }
        }
        void OnTriggerExit(Collider other)
        {

            if (other.gameObject.name != "plane")
            {
                onObstacle = false;
            }
        }
        //KEJSI
        public void decreaseScore()
        {
            this._score -= Time.deltaTime * difficultyLevel;
            scorePoints.text = ((int)_score).ToString();
            PlayerPrefs.SetInt("Score", (int)_score);
        }
    }
}

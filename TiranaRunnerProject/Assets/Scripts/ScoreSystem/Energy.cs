using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.CoinsScripts
{
    public class Energy : MonoBehaviour
    {
        public CharacterController playerController;
        public GameObject player;
        public Animator animPlayer;
        public Text energy;
        public float startOfEnergy;
        public GameObject gameoverpanel;
        public GameManager gm;

        void Start()
        {
            energy.text = startOfEnergy.ToString();
        }

        void Update()
        {
            if (startOfEnergy > 0 && startOfEnergy < 1)
            {
                gm.EndGame();
                //anim.Play("GameOver");
                //New Screen
                Assets.CoinsScripts.ScoreSystem.InstanceScore.isDead = true;
                animPlayer.Play("Dead");
                Player.Speed = 0;
                //playerTransform.gameObject.SetActive(false);
            }
            else
            {
                //@KEJSI: DECREASE ENERGY OF PLAYER IF ONOBSTACLE !
                if (ScoreSystem.onObstacle)
                {
                    startOfEnergy -= Time.deltaTime * 10;
                }
                else
                {
                    startOfEnergy -= Time.deltaTime;
                }
                energy.text = Convert.ToInt32(startOfEnergy).ToString();
            }
        }

        private void GameOver()
        {
            player.GetComponent<Player>().enabled = false;
            Time.timeScale = 0;
        }
    }

}
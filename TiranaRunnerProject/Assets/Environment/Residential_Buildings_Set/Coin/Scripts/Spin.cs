using Assets.CoinsScripts;
using System;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public static CoinsSystem coin;
    public static int value;

    void Start()
    {
        coin = FindObjectOfType<CoinsSystem>();
        value = 1;
    }

    void Update()
    {
        transform.Rotate(0, 5, 0, Space.World);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            coin.coins.text = (Convert.ToInt32(coin.coins.text) + value).ToString();
            PlayerPrefs.SetInt("TempCoins", Convert.ToInt32(coin.coins.text) + value);
        }
        gameObject.SetActive(false);
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    gameObject.SetActive(false);
    //}
}

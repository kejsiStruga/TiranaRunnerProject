using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CoinsScripts
{
    public class CoinsSystem : MonoBehaviour
    {
        public Text coins;
        public int coinsCollected = 0;
        public static CoinsSystem _instanceCoins;
        void Awake()
        {
            if (_instanceCoins == null)
                _instanceCoins = this;
            else
                UnityEngine.Object.Destroy(this);
        }
        public static CoinsSystem InstanceCoins
        {
            get { return _instanceCoins; }
        }
        void Start()
        {
            coinsCollected = 0;
            Debug.Log("Coins Collecteds " + coins.ToString());
            coins.text = coinsCollected.ToString();
        }
    }
}

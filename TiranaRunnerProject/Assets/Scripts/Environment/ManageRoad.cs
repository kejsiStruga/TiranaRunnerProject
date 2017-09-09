using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Assets.CoinsScripts
{
    public class ManageRoad : MonoBehaviour
    {
        #region  Declaration
        
        public GameObject[] bridgePrefabs;
        public GameObject terrain;
        public float spawnZ;
        public float bridgeLength;
        public int nurOfBridgeItems = 10;
        public float safeZone;
        private Transform playerTransform;
        private List<GameObject> activeItems;
        private int lastItemIndex = 0;
        public static ManageRoad _instanceRoad;
        public float lastIndexZ;
        public float YCoordinate;

        public bool secondLevel;
        private float zeroCoordindate;
        #endregion
        public static ManageRoad InstanceRoad
        {
            get { return _instanceRoad; }
        }
        void Awake()
        {
            if (_instanceRoad == null)
                _instanceRoad = this;
            else
                //Debug.LogError("destr");
                UnityEngine.Object.Destroy(this);

            if(SceneManager.GetSceneAt(0).name =="SecondLevelTiranaRunner")
            {
                Debug.Log("SecondLevelTiranaRunner");
                secondLevel = true;
            }
        }

        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            activeItems = new List<GameObject>();
            for (int i = 0; i < nurOfBridgeItems; i++)
            {
                if (i <= 4)
                {
                    GenerateBridgeItem(0);
                }
                else
                {
                    GenerateBridgeItem();
                }
            }
        }

        void Update()
        {
            if (playerTransform.position.z - safeZone > (spawnZ - nurOfBridgeItems * bridgeLength))
            {
                GenerateBridgeItem();
                DeleteItem();
            }
        }

      
        private void GenerateBridgeItem(int index = -1)
        {
            if (index == -1)
            {
                index = GenerateRandomIndex();
            }

            var newBridge = Instantiate(bridgePrefabs[index]) as GameObject;

            newBridge.transform.SetParent(transform);
            if (secondLevel)
            {
              //  YCoordinate = -0.81f;
                //YCoordinate = 0.76f;
                //XCoordindate = -0.02f;
                //zeroCoordindate = -16f;
                Debug.Log("SECOND lEVEL DEBUG");
                YCoordinate = 0.0f;
                XCoordindate = 0.0f;
                zeroCoordindate = 0.0f;
            }
            else {
                YCoordinate = 8.3f;
                XCoordindate = -15.9f;
                zeroCoordindate = 0f;
            }
            ///TODO : WHAT MEANS INDEX ==1 ?? WHY 0 @ELSE ?
            if (index == 1)
            {
                newBridge.transform.position = new Vector3(XCoordindate, YCoordinate, spawnZ);
            }
            else
            {
                newBridge.transform.position = new Vector3(zeroCoordindate, YCoordinate, spawnZ);
            }

            spawnZ += bridgeLength;

            activeItems.Add(newBridge);
            terrain.transform.position = new Vector3(XCoordindate, -9, (spawnZ));
        }

        private int GenerateRandomIndex()
        {
            if (bridgePrefabs.Length <= 1)
            {
                return 0;
            }
            int randomIndex = lastItemIndex;
            //while (randomIndex == lastItemIndex)
            //{
            randomIndex = Random.Range(0, bridgePrefabs.Length);
            //}

            lastItemIndex = randomIndex;
            return randomIndex;
        }

        public float XCoordindate { get; set; }
        
        public void DeleteItem()
        {
            Destroy(activeItems[0]);
            activeItems.RemoveAt(0);
        }

    }
}
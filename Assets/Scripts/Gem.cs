using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class Gem1 : MonoBehaviour
    {
        public GameObject player1;
        public GameObject player2;

        public GameObject collectSound;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player1)
            {
                Destroy();
                GameManager.GemOwner = player1;
            }

            if (other.gameObject == player2)
            {
                Destroy();
                GameManager.GemOwner = player2;
            }
        }

        private void Destroy()
        {
            Instantiate(collectSound, Vector3.zero, Quaternion.identity).SetActive(true);
            Destroy(gameObject);
        }
    }
}
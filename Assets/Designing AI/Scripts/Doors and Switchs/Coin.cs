using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LucasAIP
{
    public class Coin : MonoBehaviour
    {
        private HumanScript myPlayer;
        public Vector3 Position => transform.position;
        void Start()
        {
            myPlayer = FindObjectOfType<HumanScript>();
        }


        private void OnTriggerStay(Collider _collision)
        {
            if (_collision.gameObject.tag == "Player")
            {
                myPlayer.CollectCoin();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
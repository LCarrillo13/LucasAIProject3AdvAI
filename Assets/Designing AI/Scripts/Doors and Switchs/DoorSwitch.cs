using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LucasAIP
{
    public class DoorSwitch : MonoBehaviour
    {
        private HumanScript myPlayer;
        protected BaseDoor accoceatedDoor;
        public Vector3 Position => transform.position;
        void Awake()
        {
            myPlayer = FindObjectOfType<HumanScript>();
        }

        // If you collide with the player, open the door.
        private void OnTriggerStay(Collider _collision)
        {
            if (_collision.gameObject.tag == "Player")
            {
                accoceatedDoor.LiftThisDoor();
                // Basically if you are not looking for a door dont stop, your path will not be changing in this module.
                if (myPlayer.CurrentState == States.FindGateSwitch)
                {
                    myPlayer.OpenDoor();
                }
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
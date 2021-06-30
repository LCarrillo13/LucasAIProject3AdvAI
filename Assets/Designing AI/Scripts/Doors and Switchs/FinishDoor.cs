using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LucasAIP
{
    public class FinishDoor : BaseDoor
    {
        private HumanScript myPlayer;
        void Awake()
        {
            // Get the player.
            myPlayer = FindObjectOfType<HumanScript>();
            StartCoroutine(OpenFinalDoor());
        }

        // Wait to see if we can open the door.
        IEnumerator OpenFinalDoor()
        {
            // Second do while I think I have used.
            // We need it because some things are all being initialised at different times so we want to wait first.
            // God it feels good to do that ^_^.
            do
            {
                yield return new WaitForSeconds(0.5f);
            } while (!myPlayer.HaveAllCoinsBeenCollected);
            LiftThisDoor();
        }
    }
}
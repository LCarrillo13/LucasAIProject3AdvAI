using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LucasAIP
{
    public class BlueDoorSwitch : DoorSwitch
    {
        private void Start()
        {
            accoceatedDoor = FindObjectOfType<BlueDoor>();
        }
    }
}
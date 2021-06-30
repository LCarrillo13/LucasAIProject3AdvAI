using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LucasAIP
{
    public class RedDoorSwitch : DoorSwitch
    {
        private void Start()
        {
            accoceatedDoor = FindObjectOfType<RedDoor>();
        }
    }
}
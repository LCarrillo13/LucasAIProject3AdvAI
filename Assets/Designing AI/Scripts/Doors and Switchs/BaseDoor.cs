using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LucasAIP
{
    // This is the basic door it will have the lifting script.
    [RequireComponent(typeof(NavMeshObstacle))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BaseDoor : MonoBehaviour
    {
        // These Will basically be how the door moves up.
        private float moveSpeed, movingTimer;
        private Vector3 _moveDir;
        protected bool doorClosed;
        public CharacterController _charC;

        void Start()
        {
            _moveDir = Vector3.zero;
            movingTimer = 4;
            moveSpeed = 11;
            doorClosed = true;
        }

        private void Update()
        {
            _charC.Move(_moveDir * Time.deltaTime);
        }

        // 
        public void LiftThisDoor() { if (doorClosed) StartCoroutine(LiftDoor()); }

        // Do you even lift.
        protected IEnumerator LiftDoor()
        {
            float x = 1;
            while (x > 0 || !doorClosed)
            {
                _moveDir = transform.TransformDirection(0, 1, 0) * moveSpeed;
                x -= Time.deltaTime;
                yield return null;
            }
            _moveDir = Vector3.zero;
            doorClosed = false;
        }
    }
}
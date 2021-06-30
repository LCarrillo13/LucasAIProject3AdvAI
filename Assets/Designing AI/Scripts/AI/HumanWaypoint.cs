using UnityEngine;

namespace LucasAIP
{
    public class HumanWaypoint : MonoBehaviour
    {
        private HumanScript myPlayer;
        // Lambda - Inline functions - Functions without actual definitions 
        // like when we use private void - Just points to something // LAMBDAS ROCK
        // In this case points to transform.position
        public Vector3 Position => transform.position;
        private void Start()
        {
            myPlayer = FindObjectOfType<HumanScript>();
        }

        private void OnTriggerStay(Collider _collision)
        {
            if (_collision.gameObject.tag == "Player")
            {
                myPlayer.FinishGame();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LucasAIP
{
    // All AI States.
    public enum States
    {
        CoinCollect,
        FindGateSwitch,
        GotoFinish,
        FinishedGame
    }

    // The delegate that dictates what the fuctions for each state will look like.
    public delegate void StateDelegate();

    // Basically All The State Machine
    public partial class HumanScript
    {
        // Dictionary of all the states.
        private Dictionary<States, StateDelegate> states = new Dictionary<States, StateDelegate>();

        [SerializeField] private Vector3[] CoinWaypoints;
        [SerializeField] private Vector3[] DoorSwitchWaypoints;
        [SerializeField] private Vector3 FinishDestination;

        private bool haveAllCoinsBeenCollected;
        public bool HaveAllCoinsBeenCollected => haveAllCoinsBeenCollected;
        private bool haveIReachedFinish;

        private NavMeshAgent agent;
        private Animator AgentAnimator;


        // Start is called before the first frame update
        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            AgentAnimator = GetComponent<Animator>();

            states.Add(States.CoinCollect, GotoClosestCoin);
            states.Add(States.FindGateSwitch, GotoClosestSwitch);
            states.Add(States.GotoFinish, GotoFinishPoint);
            states.Add(States.FinishedGame, GotoFinishPoint);

            // Starts the state machine.
            FindNextState();
        }

        // Starts the CoRoutine to find the next state.
        private void FindNextState()
        {
            StartCoroutine(FindingNextState());
        }

        // Waits 2 Seconds then Finds the next state, to make sure the path is clear
        private IEnumerator FindingNextState()
        {
            LocationPosition.text = ("Finding Closest Point");
            InitialDistance.text = ("Comparing Distances");
            float x = 2;
            while (x >= 0)
            {
                x -= Time.deltaTime;
                playerState.text = ("Next State in " + x.ToString("F2"));
                yield return null;
            }

            // Refresh all the goal points
            UpdateGoals();

            // In Update Goals it will Update if All coins have been collected.
            if (!haveAllCoinsBeenCollected)
            {
                if (CanIGetToAnyPoint(CoinWaypoints))
                {
                    UpdateState(States.CoinCollect, CoinWaypoints);
                }
                else if (CanIGetToAnyPoint(DoorSwitchWaypoints))
                {
                    UpdateState(States.FindGateSwitch, DoorSwitchWaypoints);
                }
            }
            else if (!haveIReachedFinish)
            {
                if (CanIGetToThisPoint(FinishDestination))
                {
                    // Change this into a vector 3 cause I ain't writing another overload at 2am.
                    Vector3[] finalDestinationArray = new Vector3[1];
                    finalDestinationArray[0] = FinishDestination;
                    UpdateState(States.GotoFinish, finalDestinationArray);
                }
                else if (CanIGetToAnyPoint(DoorSwitchWaypoints))
                {
                    UpdateState(States.FindGateSwitch, DoorSwitchWaypoints);
                }
            }
            else if (haveIReachedFinish)
            {
                UpdateState(States.FinishedGame);
            }
        }

        // Updates with the new state.
        public void UpdateState(States _NextState, Vector3[] _StateWaypoints)
        {
            // Updates the current state with the new state.
            currentState = _NextState;

            // Go to closest point.
            nextDestination = FindClosestGoal(_StateWaypoints);
            agent.SetDestination(nextDestination);

            // Finally Update all the UI.
            UpdateUI();
        }
        public void UpdateState(States _NextState)
        {
            // Updates the current state with the new state.
            currentState = _NextState;
            // Finally Update all the UI.
            UpdateUI();
        }

        private void GotoClosestCoin()
        {
            // If running, play running animation.
            AgentAnimator.SetBool("Running", agent.velocity.magnitude > runSpeed);
        }

        private void GotoClosestSwitch()
        {
            // If running, play running animation.
            AgentAnimator.SetBool("Running", agent.velocity.magnitude > runSpeed);
        }
        private void GotoFinishPoint()
        {
            // If running, play running animation.
            AgentAnimator.SetBool("Running", agent.velocity.magnitude > runSpeed);
        }
        private void Celerbrate()
        {
            // If running, play running animation.
            AgentAnimator.SetBool("FinishedGame", true);
        }

        //private void GotoFinishPoint() => controlled.localScale += Vector3.one * Time.deltaTime * speed;
    }
    //lol
}
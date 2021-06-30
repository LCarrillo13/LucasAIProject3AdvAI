using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LucasAIP
{
    // This will just hold all the fuctions for checking paths and stuff
    public partial class HumanScript
    {
        // Checks if can get to a list of Vector3s in an Array.
        private bool CanIGetToAnyPoint(Vector3[] _ArrayOfPoints)
        {
            for (int i = 0; i < _ArrayOfPoints.Length;)
            {
                if (CanIGetToThisPoint(_ArrayOfPoints[i]))
                    return true;
                i++;
            }
            return false;
        }

        // Checks if we can even get to this point.
        private bool CanIGetToThisPoint(Vector3 _Point)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            if (agent.CalculatePath(_Point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                return true;
            }
            return false;
        }

        // Updates the Array of goals.
        private void UpdateGoals()
        {
            // Get all the Coin Positions in Scene.
            Coin[] waypoints = FindObjectsOfType<Coin>();
                CoinWaypoints = new Vector3[waypoints.Length];
                for (int i = 0; i < waypoints.Length;)
                {
                    CoinWaypoints[i] = waypoints[i].Position;
                    i++;
                }
            if (waypoints.Length <= 0)
                haveAllCoinsBeenCollected = true;
            else
            {
                haveAllCoinsBeenCollected = false;
            }
            // Get all the DoorSwitch Positions in Scene.
            DoorSwitch[] waypoints2 = FindObjectsOfType<DoorSwitch>();
            DoorSwitchWaypoints = new Vector3[waypoints2.Length];
            for (int i = 0; i < waypoints2.Length;)
            {
                DoorSwitchWaypoints[i] = waypoints2[i].Position;
                i++;
            }

            // Find The Finish Door.
            HumanWaypoint finishDoor = FindObjectOfType<HumanWaypoint>();
            if (finishDoor != null)
            {
                FinishDestination = finishDoor.Position;
                haveIReachedFinish = false;
            }
            else
            {
                haveIReachedFinish = true;
            }
        }

        // Gives out the closest avaliable point
        // !!! TO NOTE !!!
        // This ONLY gets called after we know it can get to atleast ONE of the points.
        private Vector3 FindClosestGoal(Vector3[] _ArrayOfPoints)
        {
            print("!!!FINDING NEXT CLOSEST POINT!!!");

            // We are making the path -2 so that we can check if it has been touched, remember you can't have negative distance thus any distance will be larger.
            float fastestPathDistance = -2;

            // We know this will be over ridden so for now we are initialising it as 0,0,0
            Vector3 closestPoint = new Vector3(0f, 0f, 0f);

            // For all points in the array.
            for (int i = 0; i < _ArrayOfPoints.Length;)
            {

                // Check if you can even get to this itterated point. Again we KNOW we will get to atleast one point from the earlier checks
                if (CanIGetToThisPoint(_ArrayOfPoints[i]))
                {

                    // If we already have a point we can get to.
                    if (fastestPathDistance >= -1)
                    {

                        // Make a new distance.
                        float newDistance = 0;

                        // Make a new path.
                        NavMeshPath navMeshPath = new NavMeshPath();

                        // Calculate this new path.
                        NavMesh.CalculatePath(transform.position, _ArrayOfPoints[i], NavMesh.AllAreas, navMeshPath);

                        // Basically we will get each cornor of the path and check the combined length of them. TO NOTE THERE IS NO OTHER WAY TO DO THIS!
                        for (int y = 1; y < navMeshPath.corners.Length; ++y)
                        {
                            newDistance += Vector3.Distance(navMeshPath.corners[y - 1], navMeshPath.corners[y]);
                        }

                        // Checks if this interated point closer on the NavMesh.
                        if (newDistance < fastestPathDistance)
                        {
                            // If so make it the new destination.
                            fastestPathDistance = newDistance;
                            closestPoint = _ArrayOfPoints[i];
                            // I dont really like printing stuff so this should come off.
                            print("NEW fastest path distance = " + fastestPathDistance + " -- From point" + _ArrayOfPoints[i]);
                            distanceToDestination = fastestPathDistance;
                            nextDestination = closestPoint;
                        }
                        else
                        {
                            print("larger distance = " + newDistance + " -- To point" + _ArrayOfPoints[i]);
                        }
                    }

                    // Basically if this is the FIRST closest path make it the .
                    else
                    {
                        // Will check the distace of this path
                        closestPoint = _ArrayOfPoints[i];
                        NavMeshPath navMeshPath = new NavMeshPath();
                        NavMesh.CalculatePath(transform.position, _ArrayOfPoints[i], NavMesh.AllAreas, navMeshPath);

                        // resets this to 0/
                        fastestPathDistance = 0;

                        for (int y = 1; y < navMeshPath.corners.Length; ++y)
                        {
                            fastestPathDistance += Vector3.Distance(navMeshPath.corners[y - 1], navMeshPath.corners[y]);
                        }

                        print("fastest path distance = " + fastestPathDistance + " -- From point" + _ArrayOfPoints[i]);
                        distanceToDestination = fastestPathDistance;
                        nextDestination = closestPoint;
                    }
                }
                i++;
            }
            return closestPoint;
        }
    }
}
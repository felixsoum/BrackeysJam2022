using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingHead : BaseNPC
{
    [SerializeField] Transform[] waypointObjects;
    [SerializeField] NavMeshAgent agent;
    List<Vector3> waypointPositions = new List<Vector3>();
    int waypointIndex;

    protected override void Start()
    {
        waypointPositions.Add(transform.position);

        if (waypointObjects != null && waypointObjects.Length > 0)
        {
            foreach (var waypointObject in waypointObjects)
            {
                waypointPositions.Add(waypointObject.position);
            }
        }


        base.Start();
    }

    protected override void Update()
    {
        var currentPos = transform.position;
        currentPos.y = 0;
        var targetPos = waypointPositions[waypointIndex];
        targetPos.y = 0;

        if (Vector3.Distance(currentPos, targetPos) < 1f)
        {
            waypointIndex = (waypointIndex + 1) % waypointPositions.Count;
            agent.SetDestination(waypointPositions[waypointIndex]);
        }

        base.Update();
    }

    internal override void OnBeerHit()
    {
        gameObject.SetActive(false);
    }
}

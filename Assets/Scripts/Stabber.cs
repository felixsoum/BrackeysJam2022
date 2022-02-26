using UnityEngine;
using UnityEngine.AI;

public class Stabber : BaseNPC
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    bool isStalking;

    protected override void Update()
    {
        var directionToEnemy = transform.position - player.transform.position;
        float distanceToPlayer = directionToEnemy.magnitude;
        if (!isStalking)
        {
            if (distanceToPlayer < 10f && IsPlayerInVision())
            {
                isStalking = true;
            }
        }
        else
        {
            if (Vector3.Angle(directionToEnemy.normalized, mainCamera.transform.forward) < 60f)
            {
                agent.velocity = Vector3.zero; 
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);
            }
        }
        bool isAttacking = distanceToPlayer < 2f;
        animator.SetBool("IsAttacking", isAttacking);
        if (isAttacking)
        {
            player.AddInsanity(0.25f * Time.deltaTime);
        }
        base.Update();
    }
}

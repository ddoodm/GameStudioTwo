using UnityEngine;
using System.Collections;

public class FSMBotController : MonoBehaviour
{
    /// <summary>
    /// The duration for which the bot may ram the player
    /// </summary>
    private float contactTimer, reverseTimer;
    public float maxContactTime = 0.8f, maxReverseTime = 1.0f;

    public Transform playerTransform;

    private BotVehicleController controller;
    private Rigidbody botRigidbody;

    private NavMeshPath path;

    private enum FSMState
    {
        PATROL,
        ARRIVE,
        EVADE,
        REVERSE,
    }
    private FSMState state = FSMState.PATROL;

    void Start()
    {
        controller = GetComponent<BotVehicleController>();
        botRigidbody = GetComponent<Rigidbody>();
        path = new NavMeshPath();

        goToRandomPosition();
    }

    void Update()
    {
        if (!controller.canPathfind)
            goToRandomPosition();

        switch (state)
        {
            case FSMState.PATROL:
                execute_patrol();
                state = transition_patrol();
                return;
            case FSMState.ARRIVE:
                execute_arrive();
                state = transition_arrive();
                return;
            case FSMState.EVADE:
                execute_evade();
                state = transition_evade();
                return;
            case FSMState.REVERSE:
                execute_reverse();
                state = transition_reverse();
                return;
        }
    }

    private void execute_patrol()
    {
        controller.targetSpeed = 1.0f;
        if (controller.atTarget || botRigidbody.velocity.magnitude < 0.1f)
            goToRandomPosition();
    }

    private FSMState transition_patrol()
    {
        float distanceToPlayer =
            (this.transform.position - playerTransform.position).magnitude;

        if (distanceToPlayer < 10.0f)
            return FSMState.ARRIVE;
        return FSMState.PATROL;
    }

    private void execute_arrive()
    {
        controller.targetSpeed = 1.0f;
        controller.setTargetWaypoint(playerTransform.position);
    }

    private FSMState transition_arrive()
    {
        float distanceToPlayer =
            (this.transform.position - playerTransform.position).magnitude;

        if (distanceToPlayer > 10.0f)
            return FSMState.PATROL;

        //if (distanceToPlayer < 2.5f)
        //    return FSMState.EVADE;

        return FSMState.ARRIVE;
    }

    private void execute_evade()
    {
        controller.setTargetWaypoint(
            (transform.position - playerTransform.position).normalized * 2.0f);
    }

    private FSMState transition_evade()
    {
        float distanceToPlayer =
            (this.transform.position - playerTransform.position).magnitude;

        if (distanceToPlayer > 10.0f)
            return FSMState.PATROL;

        return FSMState.EVADE;
    }

    private void execute_reverse()
    {
        
    }

    private FSMState transition_reverse()
    {
        return FSMState.ARRIVE;
    }

    private void goToRandomPosition()
    {
        Vector3 finalTarget = Random.insideUnitSphere * 100.0f;
        finalTarget.y = 0;

        controller.setTargetWaypoint(finalTarget);
    }
}

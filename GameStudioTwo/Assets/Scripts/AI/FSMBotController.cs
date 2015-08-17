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

    private enum FSMState
    {
        PATROL,
        ARRIVE,
        EVADE,
    }
    private FSMState state = FSMState.PATROL;

    private NavMeshPath path;

    void Start()
    {
        path = new NavMeshPath();
        controller = GetComponent<BotVehicleController>();

        goToRandomPosition();
    }

    void Update()
    {
        switch(state)
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
        }
    }

    private void execute_patrol()
    {
        controller.targetSpeed = 0.4f;
        if (controller.atTarget)
            goToRandomPosition();
    }

    private FSMState transition_patrol()
    {
        float distanceToPlayer =
            (this.transform.position - playerTransform.position).magnitude;

        if (distanceToPlayer < 15.0f)
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

        if (distanceToPlayer > 15.0f)
            return FSMState.ARRIVE;

        if (distanceToPlayer < 2.5f)
            return FSMState.EVADE;

        return FSMState.PATROL;
    }

    private void execute_evade()
    {
        controller.setTargetWaypoint(
            (transform.position - playerTransform.position).normalized * 20.0f);
    }

    private FSMState transition_evade()
    {
        float distanceToPlayer =
            (this.transform.position - playerTransform.position).magnitude;

        if (distanceToPlayer > 15.0f)
            return FSMState.PATROL;

        return FSMState.EVADE;
    }

    private void goToRandomPosition()
    {
        Vector3 finalTarget = Random.insideUnitSphere * 30.0f;
        finalTarget.y = 0;
        controller.setTargetWaypoint(finalTarget);
    }
}

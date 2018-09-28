using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
        RotateEye(controller);
    }

    private void Patrol(StateController controller)
    {
        // The first thing to do is to check the wayPoint it's headed toward.
        controller.Agent.destination = controller.WayPointsList[controller.NextWayPoint].position;

        // Start movement of agent.
        controller.Agent.isStopped = false;

        // Check if agent arrive to current destination.
        if (controller.Agent.remainingDistance <= controller.Agent.stoppingDistance &&
            !controller.Agent.pathPending)
        {
            // move agent to the next way point.
            controller.NextWayPoint = (controller.NextWayPoint + 1) % controller.WayPointsList.Count;
        }
    }

    // It will let the enemy to rotate its eye transform between 80 to -80 so it look like its facing right and left
    private void RotateEye(StateController controller)
    {
        if (!controller.ReachedToRight)
        {
            controller.Value += controller.EnemyStats.RotationSpeedOfEye;
            if (controller.Value >= 80)
            {
                controller.ReachedToRight = true;
            }
        }
        else 
        {
            controller.Value -= controller.EnemyStats.RotationSpeedOfEye;
            if (controller.Value <= -80)
            {
                controller.ReachedToRight = false;
            }
        }

        controller.Eyes.localEulerAngles = new Vector3(0, controller.Value, 0);
    }

}

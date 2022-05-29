using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class MovementModule
{
    static WaitForEndOfFrame waitFrame = new WaitForEndOfFrame(); // Reduce garbage collection

    public static bool TryMove(Transform targetUnit, Vector2 direction, float speed, float maxNavRayStepDistance = 1)
    {

        Vector2 step = (Vector2)targetUnit.position + speed * Time.deltaTime * direction;

        Debug.DrawLine(targetUnit.position, step, Color.yellow);

        bool isValidStep = NavMesh.SamplePosition(step, out NavMeshHit hit, maxNavRayStepDistance, NavMesh.AllAreas);

        if (isValidStep)
        {
            if (direction.magnitude >= 1)
            {
                //rb.velocity = moveDir * clampSpeed * baseSpeed;
                if ((targetUnit.position - hit.position).magnitude >= .02f)
                {
                    targetUnit.position = (Vector2)hit.position;

                    return true;
                }
            }

        }

        return false;

    }

    public static void ForceMove(Transform targetUnit, Vector2 direction, float speed)
    {
        Vector2 step = (Vector2)targetUnit.position + speed * Time.deltaTime * direction;

        targetUnit.position = step;

    }

    public static bool GetWarpPosition(Transform targetUnit, Vector2 direction, float speed, LayerMask enemyLayer, float navRayDist,
        out Vector3 finalWarpPos)
    {
        finalWarpPos = Vector3.zero;

        Vector2 step = (Vector2)targetUnit.position + speed * direction;

        Collider2D enemies = Physics2D.OverlapBox(step, new Vector2(0.1f, 0.1f), 0, enemyLayer);

        if (enemies != null)
        {
            step = (Vector2)targetUnit.position + direction * (speed * 0.5f);
        }

        if (NavMesh.SamplePosition(step, out NavMeshHit hit, navRayDist, NavMesh.AllAreas))
        {
            finalWarpPos = (Vector2)hit.position;
            return true;
        }

        return false;
    }



    /// <summary>
    /// Dash the <paramref name="targetUnit"/> by the defined <paramref name="dashDuration"/> ALWAYS SEND THE WORLD POSITION.
    /// </summary>
    /// <param name="targetUnit">The target unit to move</param>
    /// <param name="targetPos">Otherwise custom target position, normally is the pos of the skill</param>
    /// <param name="dashDuration">How long the dash will move the unit</param>
    /// <param name="dashSpeed">Speed bonus in case the normal speed slower than desired </param>
    /// <param name="actionAfterMove">Action that can be called after player stopped moving. E.g. start cooldown </param>
    /// <param name="actionAfterMove">Action that can be called while player is moving. E.g. spawn movement particles </param>
    /// <returns></returns>
    public static IEnumerator DashByTimer(GameObject targetUnit, Vector3 direction, float dashDuration, float dashSpeed, float navRayDist, 
        LayerMask enemies, Action actionAfterMove = null, Action actionWhileMove = null)
    {
        bool collided = false;
        float dashCurrentTimer = 0;

        float timer = 0;
        collided = false;
        Vector2 initialPos = targetUnit.transform.position;

        bool gotWarpPos = GetWarpPosition(targetUnit.transform, direction, dashSpeed, enemies, navRayDist, out Vector3 finalPos);

        while (targetUnit != null && timer <= dashDuration && !collided)
        {
            dashCurrentTimer = timer / dashDuration;

            Debug.Log("Dash Curren Timer: " + dashCurrentTimer);
            if (gotWarpPos)
            {
                targetUnit.transform.position = Vector2.Lerp(initialPos, finalPos, dashCurrentTimer);
            }
            else
            {
                TryMove(targetUnit.transform, direction, dashSpeed, navRayDist);
            }

            actionWhileMove?.Invoke();

            yield return waitFrame;
            timer += Time.deltaTime;
        }

        yield return waitFrame;

        actionAfterMove?.Invoke();
    }
}

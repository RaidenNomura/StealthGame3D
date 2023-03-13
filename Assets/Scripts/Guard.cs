using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    #region Exposed

    [SerializeField] float speed = 5f;
    [SerializeField] float waitTime = 0.3f;
    [SerializeField] float turnSpeed = 90f;

    [SerializeField] Transform pathHolder;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        Vector3[] waypoint = new Vector3[pathHolder.childCount];

        for (int i = 0; i < waypoint.Length; i++)
        {
            waypoint[i] = pathHolder.GetChild(i).position;
            waypoint[i] = new Vector3(waypoint[i].x, transform.position.y, waypoint[i].z);
        }

        StartCoroutine(FollowPath(waypoint));
    }

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

    #endregion

    #region Methods

    IEnumerator FollowPath(Vector3[] waypoint)
    {
        transform.position = waypoint[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoint[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoint.Length;
                targetWaypoint = waypoint[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    #endregion

    #region Private & Protected



    #endregion
}

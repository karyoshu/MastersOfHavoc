using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyMovementController : MonoBehaviour {

    public Transform target;

    private Seeker seeker;
    private CharacterController cController;

    public Path path;

    public float speed = 2f;

    public float nextWayPointDistance = 3;

    private int currentWayPoint = 0;
    public float repathRate = 0.5f;
    private float lastRepath = -9999;

	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        cController = GetComponent<CharacterController>();
	}
	
    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was found. Erro? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void Update()
    {
        if (Time.time - lastRepath > repathRate && seeker.IsDone())
        {
            lastRepath = Time.time + Random.value * repathRate * 0.5f;

            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }

        if (path == null)
            return;
        if (currentWayPoint > path.vectorPath.Count)
            return;

        if (currentWayPoint == path.vectorPath.Count)
        {
            Debug.Log("End of path reached");
            currentWayPoint++;
            return;
        }

        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed;

        cController.SimpleMove(dir);
        if ((transform.position - path.vectorPath[currentWayPoint]).sqrMagnitude < nextWayPointDistance * nextWayPointDistance)
        {
            currentWayPoint++;
            return;
        }
    }

    //private void OnDisable()
    //{
    //    seeker.pathCallback -= OnPathComplete;
    //}
}

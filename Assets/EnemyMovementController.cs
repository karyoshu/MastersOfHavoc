using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyMovementController : MonoBehaviour {

    public Vector3 targetPosition;
    
	// Use this for initialization
	void Start () {
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);	
	}
	
    public void OnPathComplete(Path p)
    {
        Debug.Log("Reached destination");
    }
}

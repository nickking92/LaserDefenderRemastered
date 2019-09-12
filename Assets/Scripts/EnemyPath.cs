
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {
     WaveConfig waveConfig;
    List<Transform> waypoints;
   // [SerializeField] List<Transform> waypoints; //trazimo pozicije
    int waypointIndex = 0;
	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWaypoint();
        transform.position = waypoints[waypointIndex].position;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();

    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = waveConfig.GetMoveSpeed()* Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


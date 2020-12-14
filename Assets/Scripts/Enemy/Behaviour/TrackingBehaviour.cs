using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unbreakable.Enemy.Behaviour;
using UnityEngine;
[CreateAssetMenu]
public class TrackingBehaviour : EnemyBehavior
{
    [SerializeField] private float nextWaypointDistance = 3.0f;
    [SerializeField] [MinMaxSlider(0f, 300f)] private MinMax speedRange;
    
    private Seeker seeker;
    private Transform transform;
    private Path path;
    private int currentWaypoint;
    private Rigidbody2D rbody;
    private float speed;
    
    public override void initialize(MonoBehaviour enemy, Transform target) {
        base.initialize(enemy, target);
        speed = speedRange.RandomValue;
        seeker = enemy.GetComponent<Seeker>();
        rbody = enemy.GetComponent<Rigidbody2D>();
        transform = enemy.transform;
        seeker.StartCoroutine(updatePath());
    }

    public override void update() {
        if(path == null || currentWaypoint >= path.vectorPath.Count) return;
        
        setVelocity();

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }
    
    private void setVelocity() {
        Vector2 currentWaypointPosition = path.vectorPath[currentWaypoint];
        Vector2 direction = (currentWaypointPosition - rbody.position).normalized;
        rbody.velocity =  Time.deltaTime * speed * direction;
    }
    
    private IEnumerator updatePath() {
        while (true) {
            path = seeker.StartPath(transform.position, target.position, onPathComplete);
            yield return new WaitForSeconds(.1f);
        }
    }
    
    private void onPathComplete(Path p) {
        if (p.error) return;
        path = p;
        currentWaypoint = 0;
    }
}

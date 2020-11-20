using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class Tracker : MonoBehaviour, Damager {
	
    [SerializeField] private float speed;
    [SerializeField] private float nextWaypointDistance = 3.0f;
    [SerializeField] private int damage;
    [SerializeField] private float knockbackForce;
    [SerializeField] private bool isFlipped;
	
    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rbody;
    private Seeker seeker;
    private Path path;
    private int currentWaypoint;
    private bool isTracking = true;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        InvokeRepeating(nameof(updatePath), 0f, .1f);
    }
    
    private void updatePath() {
        path = seeker.StartPath(transform.position, target.position, onPathComplete);
    }

    void FixedUpdate () {
        if (!isTracking) return;
        if(path == null || currentWaypoint >= path.vectorPath.Count) return;
        
        setVelocity();
        lookAtTarget();
        
        float distance = Vector2.Distance(rbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }

    private void setVelocity() {
        Vector2 currentWaypointPosition = path.vectorPath[currentWaypoint];
        Vector2 direction = (currentWaypointPosition - rbody.position).normalized;
        rbody.velocity =  Time.fixedDeltaTime * speed * direction;
    }

    private void onPathComplete(Path p) {
        if (p.error) return;
        path = p;
        currentWaypoint = 0;
    }

    private void lookAtTarget() {
        bool flipX;
        flipX = transform.InverseTransformPoint(target.position).x > 0.1;
        flipX = isFlipped ? !flipX : flipX;
        spriteRenderer.flipX = flipX;
    }

    public int getDamage() {
        return damage;
    }

    public float getKnockbackForce() {
        return knockbackForce;
    }

    public void start() {
        isTracking = true;
    }
    public void stop() {
        isTracking = false;
    }
}

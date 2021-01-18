using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Unbreakable.Enemy.Behaviour {
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

            float distance = Vector2.Distance(rbody.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                currentWaypoint++;
            }
            setVelocity();
        }
    
        private void setVelocity() {
            Vector2 currentWaypointPosition = path.vectorPath[currentWaypoint];
            Vector2 direction = currentWaypointPosition - rbody.position;
//        if(Math.Abs(direction.x) <= .2) return;
            rbody.velocity =  Time.deltaTime * speed * direction.normalized;
        }
    
        private IEnumerator updatePath() {
            while (true) {
                path = seeker.StartPath(rbody.position, target.position, onPathComplete);
                yield return new WaitForSeconds(.1f);
            }
        }
    
        private void onPathComplete(Path p) {
            if (p.error) return;
            path = p;
            currentWaypoint = 0;
        }
    }
}

using System.Collections;
using Pathfinding;
using Unbreakable.Util;
using UnityEngine;

namespace Unbreakable.Enemy.Behaviour {
    [CreateAssetMenu]
    public class TrackingBehaviour : EnemyBehaviour {
        
        [SerializeField] private float nextWaypointDistance = 3.0f;
        [SerializeField] [MinMaxSlider(0f, 300f)] private MinMax speedRange;
    
        private Seeker seeker;
        private Path path;
        private int currentWaypointIndex;
        private Rigidbody2D rbody;
        private float speed;
    
        public override void initialize(EnemyAI enemy, Transform target) {
            base.initialize(enemy, target);
            speed = speedRange.RandomValue;
            seeker = enemy.GetComponent<Seeker>();
            rbody = enemy.GetComponent<Rigidbody2D>();
            seeker.StartCoroutine(updatePath());
        }

        public override void update() {
            if(path == null || currentWaypointIndex >= path.vectorPath.Count) return;
            
            float distance = Vector2.Distance(rbody.position, path.vectorPath[currentWaypointIndex]);
            if (distance < nextWaypointDistance) {
                currentWaypointIndex++;
            }
            setVelocity();
        }
    
        private void setVelocity() {
            if(currentWaypointIndex >= path.vectorPath.Count) return;
            Vector2 currentWaypointPosition = path.vectorPath[currentWaypointIndex];
            Vector2 direction = currentWaypointPosition - rbody.position;
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
            currentWaypointIndex = 0;
        }
    }
}

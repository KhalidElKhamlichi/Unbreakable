using UnityEngine;

public class RocketLauncher : MonoBehaviour, FiringStrategy {
    
    [SerializeField] private float projectileSpread = 0.2f;
    [SerializeField] private float projSpeed = 500.0f;


    public void shoot(GameObject projectile, Transform emissionPoint) {
        GameObject projClone = Instantiate(projectile, emissionPoint.position, emissionPoint.rotation);
		
        Rigidbody2D projRbdy = projClone.GetComponent<Rigidbody2D>();

        projRbdy.AddForce((transform.right + new Vector3(0, Random.Range(-projectileSpread, projectileSpread), 0)) * projSpeed);
	
    }
        
}

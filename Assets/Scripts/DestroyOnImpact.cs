using UnityEngine;

public class DestroyOnImpact : MonoBehaviour {

    [SerializeField] private GameObject impactEffect;
		

    private void OnTriggerEnter2D(Collider2D other) {
        if(impactEffect) Instantiate(impactEffect, other.ClosestPoint(transform.position), transform.rotation);
        if (other.CompareTag("Player")) {
            
            Destroy(gameObject);
        }

    }
}

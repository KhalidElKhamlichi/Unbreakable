using UnityEngine;

public class Pistol : MonoBehaviour, FiringStrategy {
    
//    public GameObject flash;
//    public AudioClip clip;
    public float projectileSpread = 0.2f;
    public float projSpeed = 500.0f;
//    public float screenShakeAmnt;
//    public float screenShakeDur;
	
//    private AudioSource audioSource;
//    private ScreenShake sch;

    void Start () {
//        audioSource = GetComponent<AudioSource>();
//        sch = GetComponent<ScreenShake>();
    }

    public void shoot(GameObject projectile, Transform emissionPoint) {
//            GameObject flashClone = Instantiate(flash, emissionPoint.position, emissionPoint.rotation);
        GameObject projClone = Instantiate(projectile, emissionPoint.position, emissionPoint.rotation);
		
        Rigidbody2D projRbdy = projClone.GetComponent<Rigidbody2D>();

//			sch.Shake(screenShakeAmnt, screenShakeDur);
        projRbdy.AddForce((transform.right + new Vector3(0, Random.Range(-projectileSpread, projectileSpread), 0)) * projSpeed);

//            audioSource.PlayOneShot(clip);

//            Destroy(flashClone, 0.06f);

		
    }
        
}

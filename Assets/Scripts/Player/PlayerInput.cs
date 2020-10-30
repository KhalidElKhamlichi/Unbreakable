using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerInput : MonoBehaviour
{
    private PlayerManager playerManager;

    void Start () {
        playerManager = GetComponent<PlayerManager> ();
    }

    void Update() {
        Vector2 directionalInput = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerManager.setDirectionalInput(directionalInput);

        if (Input.GetButton("Fire1")) playerManager.attack();
    }
}

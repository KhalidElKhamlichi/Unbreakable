﻿using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerInput : MonoBehaviour
{
    private PlayerManager playerManager;

    void Start () {
        playerManager = GetComponent<PlayerManager> ();
    }

    void Update() {
        if(GameManager.isGamePaused) return;
        Vector2 directionalInput = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerManager.setDirectionalInput(directionalInput);

        if (Input.GetButton("Fire")) playerManager.attack();
        
        if (Input.GetButton("Drop")) playerManager.dropWeapon();
        
        if (Input.GetButton("Interact")) playerManager.interact	();
    }
}

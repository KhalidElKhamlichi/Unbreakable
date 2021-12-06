﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unbreakable.Player;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Unbreakable {
    public class CraftingStation : MonoBehaviour, Interactable {
        
        [SerializeField] private FloatVariable currencyAmount;
        [SerializeField] private List<GameObject> weapons;
        [SerializeField] private float timeToCraft;
        [SerializeField] private int cost;
        [SerializeField] private string textPrompt;
        [SerializeField] private GameObject craftingProgressUI;
        [SerializeField] private Image craftingProgressBar;
        [SerializeField] private GameObject interactionUI;

        private bool isCrafting;

        private void Awake() {
            interactionUI.GetComponentInChildren<TextMeshProUGUI>().text = textPrompt;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player") && !isCrafting) {
                interactionUI.SetActive(true);
                other.GetComponent<PlayerManager>().setInteractable(this);
            }
        }
    
        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                interactionUI.SetActive(false);
                other.GetComponent<PlayerManager>().setInteractable(null);
            }
        }

        public string getTextPrompt() {
            return isCrafting ? string.Empty : textPrompt;
        }

        public void interact() {
            if(isCrafting || currencyAmount.value < cost) return;
            isCrafting = true;
            craftingProgressUI.SetActive(true);
            currencyAmount.changeValue(currencyAmount.value-cost);
            StartCoroutine(craft());
        }
    
        private IEnumerator craft() {
            InvokeRepeating(nameof(updateCraftingProgressBar), 0.0f, 0.1f);
        
            yield return new WaitForSeconds(timeToCraft);
        
            isCrafting = false;
            craftingProgressUI.SetActive(false);
            craftingProgressBar.fillAmount = 0.0f;
            spawnWeapon();
            CancelInvoke();
        }

        private void spawnWeapon() {
            int randomIndex = Random.Range(0, weapons.Count);
            GameObject weapon = Instantiate(weapons[randomIndex], transform.position, Quaternion.identity);
            weapon.GetComponent<Collider2D>().enabled = false;
            weapon.transform.DOJump(transform.position - transform.up * 1.5f, 1, 1, .1f, false).onComplete += () => weapon.GetComponent<Collider2D>().enabled = true;;
        }
    
        private void updateCraftingProgressBar() {
            craftingProgressBar.fillAmount += 1/ (timeToCraft	/ .1f);
        }

    }
}

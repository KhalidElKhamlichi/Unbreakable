using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CraftingStation : MonoBehaviour, Interactable {
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private float delay;
    [SerializeField] private int cost;
    [SerializeField] private string textPrompt;
    [SerializeField] private GameObject craftingProgressUI;
    [SerializeField] private GameObject interactionUI;

    private bool isCrafting;
    private Image craftingProgressBar;

    private void Start() {
        craftingProgressBar = craftingProgressUI.transform.GetChild(0).GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if(!isCrafting) {
                interactionUI.GetComponentInChildren<TextMeshProUGUI>().text = textPrompt;
                interactionUI.SetActive(true);
                other.GetComponent<PlayerManager>().setInteractable(this);
            }
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
        if(isCrafting || GameManager.getCurrencyAmount() < cost) return;
        isCrafting = true;
        craftingProgressUI.SetActive(true);
        GameManager.reduceCurrency(cost);
        StartCoroutine(craft());
    }
    
    private IEnumerator craft()
    {
        InvokeRepeating(nameof(updateCraftingProgressBar), 0.0f, 0.1f);
        
        yield return new WaitForSeconds(delay);
        
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
        craftingProgressBar.fillAmount += 1/ (delay	/ .1f);
    }

}

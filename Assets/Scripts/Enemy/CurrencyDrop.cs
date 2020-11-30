using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDrop : MonoBehaviour {
    [SerializeField] private int amount;

    public int getCurrencyAmount() {
        return amount;
    }
}

using System;
using Unbreakable.Util;
using UnityEngine;

namespace Unbreakable.Enemy {
    public class CurrencyDrop : MonoBehaviour {
        [SerializeField] private FloatVariable currencyAmount;
        [SerializeField] private int amount;

        private void OnDestroy() {
            currencyAmount.changeValue(currencyAmount.value+amount);
        }
    }
}

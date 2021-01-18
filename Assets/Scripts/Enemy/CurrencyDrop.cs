using UnityEngine;

namespace Unbreakable.Enemy {
    public class CurrencyDrop : MonoBehaviour {
        [SerializeField] private int amount;

        public int getCurrencyAmount() {
            return amount;
        }
    }
}

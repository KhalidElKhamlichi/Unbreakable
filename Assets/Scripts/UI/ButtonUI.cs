using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unbreakable.UI {
    public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] Color onHoverTextColor;
        [SerializeField] Color defaultTextColor;

        private void OnEnable() {
            GetComponentInChildren<TextMeshProUGUI>().color  = defaultTextColor;
        }

        public void OnPointerEnter(PointerEventData eventData) {    
            GetComponentInChildren<TextMeshProUGUI>().color  = onHoverTextColor;
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            GetComponentInChildren<TextMeshProUGUI>().color  = defaultTextColor;
        }

    }
}
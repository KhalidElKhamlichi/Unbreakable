using System;
using UnityEngine;

namespace Unbreakable
{
    [CreateAssetMenu]
    public class FloatVariable : ScriptableObject {
        
        public float value {
            get;
            private set;
        }

        public event Action onChangedEvent;

        public void changeValue(float newValue) {
            value = newValue;
            onChangedEvent?.Invoke();
        }
    }
}

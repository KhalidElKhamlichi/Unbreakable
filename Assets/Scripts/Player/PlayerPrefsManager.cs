using UnityEngine;

namespace Unbreakable.Player
{
    [CreateAssetMenu]
    public class PlayerPrefsManager : ScriptableObject
    {
        [SerializeField] private string lastClearedWaveIndexKey;
        
        public void setLastClearedWaveIndex(int index) {
            PlayerPrefs.SetInt(lastClearedWaveIndexKey, index);
        }
        public int getLastClearedWaveIndex() {
            return PlayerPrefs.GetInt(lastClearedWaveIndexKey);
        }
    }
}

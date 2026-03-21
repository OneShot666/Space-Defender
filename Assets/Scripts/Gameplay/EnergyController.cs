using UnityEngine.UI;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Gameplay {
    public class EnergyController : MonoBehaviour {
        [SerializeField] private Slider energySlider;
        [SerializeField] private Image energyFill;
        [SerializeField] private Color minColor = Color.yellow;
        [SerializeField] private Color maxColor = Color.orange;

        void Start() {
            if (energySlider) {
                energySlider.maxValue = GetPlayerMaxEnergy();
                UpdateEnergy();
            }
        }

        void Update() {
            UpdateEnergy();
        }

        private float GetPlayerMaxEnergy() {
            return PlayerController.Instance.Player.MaxEnergy;
        }

        private float GetPlayerEnergy() {
            return PlayerController.Instance.Player.Energy;
        }

        private float GetPlayerRegen() {
            return PlayerController.Instance.Regen;
        }

        private void UpdateEnergy() {
            if (energySlider) {
                PlayerController.Instance.Player.Regenerate(GetPlayerRegen(), Time.deltaTime);
                energySlider.value = GetPlayerEnergy();
                float percent = energySlider.value / energySlider.maxValue;
                if (energyFill) energyFill.color = Color.Lerp(minColor, maxColor, percent);
            }
        }
    }
}
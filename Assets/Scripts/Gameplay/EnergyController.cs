using UnityEngine.UI;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Gameplay {
    public class EnergyController : MonoBehaviour {
        [SerializeField] private Slider energySlider;

        void Start() {
            if (energySlider) {
                energySlider.maxValue = GetPlayerMaxEnergy();
                energySlider.value = GetPlayerEnergy();
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

        public void UpdateEnergy() {
            if (energySlider) {
                PlayerController.Instance.Player.Regenerate(GetPlayerRegen(), Time.deltaTime);
                energySlider.value = GetPlayerEnergy();
            }
        }
    }
}
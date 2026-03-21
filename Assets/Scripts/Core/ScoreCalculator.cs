using UnityEngine;

namespace Core {
    public class ScoreCalculator {
        private int BaseScore => 10;
        public float Multiplier { get; private set; } = 1.0f;
        public readonly float MinMult = 1.0f;
        public readonly float MaxMult = 5.0f;

        public int Calculate(int score, int time=0) {
            return (int)(BaseScore * score * Multiplier);
        }

        public void IncreaseCombo(float comboCount=1) {
            Multiplier = Mathf.Min(Multiplier + comboCount, MaxMult);
        }

        public void DecayCombo(float comboCount=1) {
            Multiplier = Mathf.Max(Multiplier - comboCount, MinMult);
        }

        public void ResetMultiplier() {
            Multiplier = MinMult;
        }
    }
}

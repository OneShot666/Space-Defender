using NUnit.Framework;
using Core;

namespace Tests.EditMode {
    [TestFixture]
    public class ScoreCalculatorTests {
        private ScoreCalculator _calculator;

        [SetUp]
        public void SetUp() {
            _calculator = new ScoreCalculator(); 	                            // Init before test
        }

        [Test]
        public void Calculate_Normal_correctScore() {
            int score = _calculator.Calculate(5, 60);
            Assert.AreEqual(50, score);
        }

        [Test]
        public void Calculate_Limite_ScoreZero() {
            int score = _calculator.Calculate(0);
            Assert.AreEqual(0, score);
        }

        [Test]
        public void ApplyCombo_Normal_IncreasesMult() {
            _calculator.IncreaseCombo(3);
            Assert.AreEqual(4, _calculator.Multiplier);
        }

        [Test]
        public void ApplyCombo_Limite_UnchangedMult() {
            _calculator.IncreaseCombo(0);
            Assert.AreEqual(1, _calculator.Multiplier);
        }

        [Test]
        public void ApplyCombo_Limite_MaxMult() {
            _calculator.IncreaseCombo(99);
            Assert.AreEqual(_calculator.MaxMult, _calculator.Multiplier);
        }

        [Test]
        public void ResetMultiplier_Normal_MultOne() {
            _calculator.ResetMultiplier();
            Assert.AreEqual(1, _calculator.Multiplier);
        }
    }
}

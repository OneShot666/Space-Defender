using UnityEngine;
using Core;

/* * ==============================================================================
 * SPACE DEFENDER - TO DO LIST
 * ============================================================================== */
/* --- PART 1 : TESTS PLAN --- */
// Fill basics tests (Player, Enemy, ScoreCalculator)
// Make and justify 3 bonus tests
/* --- PART 2 : DEV TDD (Cycle Red-Green-Refactor) --- */
// Configuration NUnit (EditMode) & .asmdef
// Implement class Player (T01 à T06)
// Implement class Enemy (T07, T08)
// Implement class ScoreCalculator (T09 à T12)
// . Refactorization : Apply DRY & AAA principals
/* --- PART 3 : PIPELINE CI/CD (GitHub Actions) --- */
// Create .github/workflows/ci.yml file
// Configure Unity Secrets (LICENSE, EMAIL, PASSWORD)
// . Establish branch protection rules for branch main
// ! Automate extension CD for Build WebGL
// ! Add status badge in README.md
public class GameManager {
    private bool _isGameOver;
    private int _score;

    private ScoreCalculator _calculator;

    public static GameManager Instance { get; private set; }

    public void AddScore(int points) {
        if (!_isGameOver) {
            _score += _calculator.Calculate(points);
            Debug.Log("Score actuel : " + _score);
        }
    }

    public void TriggerGameOver() {
        _isGameOver = true;
        Debug.Log("Game Over !");
    }
}

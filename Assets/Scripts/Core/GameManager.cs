using UnityEngine;

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
// Refactorization : Apply DRY & AAA principals
/* --- PART 3 : PIPELINE CI/CD (GitHub Actions) --- */
// Create .github/workflows/ci.yml file
// Configure Unity Secrets (LICENSE, EMAIL, PASSWORD)
// Establish branch protection rules for branch main
// Automate extension CD for Build WebGL
// Add status badge in README.md
namespace Core {
    public class GameManager {
        public bool IsGameOver;

        public void TriggerGameOver() {
            IsGameOver = true;
            Time.timeScale = 0;                                                 // Pause game
        }
    }
}

# Space Defender

![CI Status](https://github.com/OneShot666/Space-Defender/actions/workflows/ci.yml/badge.svg)

&#x20;

A mini space shooter game in C#/Unity.

Developed using TDD and an automated CI/CD pipeline.



### 🚀 Présentation

Ce projet est un prototype de Shoot'em Up spatial développé sous Unity 2022.3+. L'objectif principal était de mettre en place une architecture découplée (Core/Gameplay), un système de score dynamique et une boucle de gameplay évolutive.


### 🛠 Architecture & Design Patterns

Le projet suit une séparation stricte entre la logique métier et l'implémentation Unity :

1. Dossier Core/ (Logique Pure)
Contient des classes C# standards, sans dépendance à MonoBehaviour.

Enemy & Player : Gèrent les points de vie et les statistiques.

ScoreCalculator : Logique mathématique du multiplicateur et du combo.

Avantage : Cette partie est entièrement testable via des Tests Unitaires (NUnit) sans lancer le moteur Unity.

2. Dossier Gameplay/ (Bridge & View)
Contient les scripts Unity qui font le pont entre le Core et le visuel.

EnemyController : Gère le mouvement, le sprite et appelle la logique Core.Enemy lors des impacts.

PlayerController : Gère les entrées (Input System) et la transformation de forme.

3. Design Patterns utilisés
Singleton Pattern : Utilisé pour les Managers (ScoreController, AudioManager) afin de centraliser l'accès aux données globales.

Observer-like (Bridge) : Les balles communiquent avec les contrôleurs d'ennemis pour déclencher les dégâts dans la logique Core.


### 🧪 Tests Unitaires

Le projet inclut une suite de tests dans le dossier Tests/EditMode. Ils valident :

- Le calcul correct des dégâts sur les entités Core.

- La logique du multiplicateur de score (incrémentation et limites).

- Le système de récompense (XP/Points) à la mort d'un ennemi.

Pour lancer les tests : Window > General > Test Runner dans Unity.


### 🎮 Fonctionnalités clés

Scrolling Infini : Système de repositionnement automatique des enfants d'un BackgroundManager pour un effet de boucle fluide.

Système de Combo Dynamique :

Multiplicateur de score allant de x1.0 à x5.0.

Délai de grâce de 2 secondes avant la diminution progressive du combo.

Super Forme : Transformation automatique du joueur (sprite et projectile) lorsque le multiplicateur atteint x4.0.

Persistance : Sauvegarde du meilleur score via PlayerPrefs.

Juicy Feedback :

Hit Flash : Les sprites deviennent blancs brièvement lors d'un dégât.

Audio Manager : Gestion des sons de tir aléatoires, explosions et musiques de fond.

### ⌨️ Contrôles

Souris : Viser (Rotation de la tourelle).

Clic Gauche : Tirer.


### 📂 Structure du projet

Assets/
 ├── Animations/    # Animations (bullets)
 ├── Font/          # Polices utilisées
 ├── Images/        # Sprites et Textures (Pixel Art)
 ├── Prefabs/       # Schema dans la scène
 ├── Scenes/        # Scène de jeu principale
 ├── Scripts/
 │    ├── Core/     # Logique C# Pure (Testable)
 │    └── Gameplay/ # MonoBehaviour & Unity Logic
 ├── Sounds/        # SFX et Musiques
 ├── Tests/
 │    └── EditMode/ # NUnit Tests

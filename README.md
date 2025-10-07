# Task 2: Singleton Implementation
## Studen Info
- Name: Todd Beatty
- ID: 01266370

## Pattern: Singleton
### Implementation
The Singleton pattern was implemented in the two classes, AudioManager and GameManager. I implemented it that way to make sure that only one instance of each appeared in the game. The "Awake()" method checks to see if another instance exists so that it is never created again. It makes sure that there is one place to control time, scoring, and audio to prevent conflicts.
### Game Integration
The singleton pattern is directly used by the gameplay by keeping track of central game systems like score, timer, and audio. All of the other scrips call these instances of these scripts in order to use them through the static instance methods. Both managers exist through all scenes which makes sure that data is easily transferrable across scenes seamlessly.
## Game Description
- Title: Bean Blaster
- Controls: WASD to move, Mouse Move to rotate camera, and Left Click to Shoot
- Objective: Collect as many coins as possible while shooting moving beans before the timer expires to earn a high score.

## Repository Stats
- Total Commits: 13
- Development Time: 6 Hours

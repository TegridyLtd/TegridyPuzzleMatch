# Tegridy Puzzle Match Controller
## About
The TegridyPuzzleMatch package provides a fun and challenging spacial awareness problem that can be used to deploy a variety of puzzles and works as a stand alone game with level selection / progession or levels can be called directly in your own scripts with easy insertion points. 
## Usage
We recommend taking a look at the example scene to get an idea of how the scene is composed and setup. The main component is the TegridyPuzzleMatch component and this is used to configure your levels once you have setup your GUI’s. The system comes with a basic level select menu that can be used for starting the levels, or the TegridyPuzzleMatch component can open them directly by for use as puzzles in an existing world by caling StartMatch() and passing it an integer that represents the index of the level you would like to load. If return2Menu is set to true the shipped gui will be opened upon the puzzle exiting otherwise the CustomCloseUp() funtion will be called which can be used to intergrate with your own systems and menu can be left null. 

![Tegridy](./0.png)

## Tegridy Puzzle Match Configuration

![Tegridy](./1.png)

- **Size** – Should be set to the number of levels you would like to have.
- **LevelIcon** – This should be set to a sprite that represents the level and will be used on the level select screen.
- **LevelName** – The name that should be displayed for the level.
- **UI** – This should be set to the GUI that will be used for the level and requires a correctly configured Level GUI.
- **Audio** – If these are not set the default game audio will be used instead.
- **Info** – Information about the previous games results.
- **DefaultAudio** – The default audioclips for use in the game.

![Tegridy](./2.png)

- **StartingScore** – The maximum score for solving the puzzle.
- **ClickDecrease** – The amount each click decreases the players score
- **TimeDecrease** – The amount each passing second decreases the players score.
- **Rounds** – The number of problems that need to be solved.
- **Difficulty** – The amount of colours to be used on the level, this should be set to at least two.
- **IncreaseDifficulty** – If set the difficulty will increase each round, once maximum colours have been reached, rotations will then be enabled.
- **RotateProblem/Solution** – If set the game objects will be rotated each round on  the level.
- **ResetColours** – If set after each round the problems colours will reset to the starting colour if not they will stay in the same configuration.
- **Timer** – If set the level will be timed.
- **MatchTime** – The amount of time the player has to solve the problem, should be set to a sane value in seconds.
- **ResetTime** – If set the matches timer will be restarted at the end of each round.

## Tegridy Puzzle Match Menu Configuration

![Tegridy](./3.png)

- **GameMenu** – This should be set to the Object that contains your menu’s GUI and a PuzzleMatchGUIMenu component.
- **Control** – This should be set to the object in your scene that contains the TregridyPuzzleMatch component.

## Menu GUI Configuration

![Tegridy](./4.png)

- **StartScreen** – An image that contains the level select screens objects.
- **Title** – If set used to display the games names.
- **Close** – Should be set to a button that closes the application.
- **LevelPrefab** – The prefab object that will be used to display the available levels.
- **PrefabSpacing** – The spacing between the prefabs objects. Should be set to the width+ of the level prefab.
- **Levels** – Content of a scroll view that contains the levelPrefab.
- **InfoScreen** – Should be set to an image that contains the title and description. It is used at the end of the match to display the results.
- **InfoTitle** – If set this will display the title set in the language controller.
- **InfoDescription** -If set this will display the description set in the language controller.

## Level GUI Configuration

![Tegridy](./5.png)

- **ProblemRect / SolutionRect** – A RectTranform that will be used to rotate the problem / solution objects if the option has been set for the level.
- **Problem / Solution** – These should both be equal in length and be set to the images in your scene that relate to the problem and solution, with each element relating to the other. Solution will always stay the same, problem will change when the user interacts with the images button.
- **Buttons** – This should be equal to the length of problems/solutions and contain the buttons used to change the images until they match the solution.
- **Close** – Closes the UI, will either return to the main menu or if return is set to false will run CustomCloseUp() instead from the TegridyPuzzleMatch controller.
- **GameInfo** – If set these will display information about the current game.

![Tegridy](./6.png)

- **Colours** – You will need to set at least 2 colours to be used for the level so the controller can function correctly, The amount of colours used will be decided by the level difficulty set in the controller.  
- **Rotations** – These should be set to the desired rotations that the level should use for both the problem and solution. Rotations will only be used when this is enabled for the level in the controller.

## Script / Class Descriptions
- **TegridyPuzzleMatch** – The main game controller, provided access to the levels and keeps track of the data. StartLevel() can be called to open a single level or you can use the menu component.
- **TegridyPuzzleMatchMenu** – Interface for loading levels, OpenLevelSelect() can be called to open the gui set in gameMenu in your own scripts.
- **TegridyPuzzleMatchGUI** - Used to keep track of the GUI elements
- **TegridyPuzzleMatchLanguage** – Provides easy access to all strings used in the GUI.
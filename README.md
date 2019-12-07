# Zenith
Zenith is a top-down perspective space shooter set in the 31st century. The player pilots a ship through space while being attacked by a variety of alien vessels. The objective of the game is to survive the multiple levels while taking down as many enemies as possible to make it to the final boss. Inspired by classic shoot-em-up games.

This project is in development to fulfill project requirements for Cps 209.

# Instructions
Begin the game by starting a new game, and choosing a difficulty level (easy, medium, hard). Once the game is started you use the left, right, up, down arrows to move around, and space to shoot. To save the current state of the game, press 's'; to load the saved state of a game, press 'l'. Make it through waves of enemies to victory. (Right now you must begin the game by pressing one of the arrow keys)

# Work Completed
* Serialization
* High score implementation
* Title, Help, Credits, and Start Game Screens
  * Back buttons
  * Options for starting a game
    * Name
    * Difficulty
* Sprites
* Mobile Version
* Game model object (lasers, asteroids, ships, enemies...)
* Game Model
  * Objects
    * Lasers
    * Asteroids
    * Enemy and Player Ships
    * Bosses
    * Sensors
  * Collisions
  

# Known Issues
* High score has been implemented...but not in the view.
* Mobile version is still a little bugged.
* The load and save functionality does not correctly work in the view nor in the buttons.
* The Highscore page does not work (it will crash the program).
* Mobile - The game dimensions are not working correctly; the game will only correctly work in the lower right-hand corner.
* The shop is currently empty and unusable.
* Not all needed instance variables of objects are serialized.
* If the user starts the game and returns to the main menu, and repeats that process, multiple tasks are ran on the World singleton class.

# Recordings

## Alpha

https://bju-my.sharepoint.com/:v:/r/personal/jgonz812_students_bju_edu/Documents/2019-11-22%2012-54-53.mkv?csf=1&e=Up3EfT

## Beta

Desktop: https://bju.hosted.panopto.com/Panopto/Pages/Viewer.aspx?id=950c85f3-1741-4a59-a4ea-ab1c0022bd8e

Mobile: https://bju.hosted.panopto.com/Panopto/Pages/Viewer.aspx?id=ab83ac72-b509-45b2-8afd-ab1c0028b0ca

# Expenses

| Team Member | Total time invested | Total time Left | Team member's journal |
| :------------- | :---------- | :----------- | :---------- |
| James Gonzales | 36 hrs | 24 hrs | https://github.com/CpS-209-Team3/Zenith/wiki/GonzalesJournal |
| Caedmon Evans | 39 hrs | 21 hrs | https://github.com/CpS-209-Team3/Zenith/wiki/EvansJournal |
| Le Bao | 27 hrs | 33 hrs | https://github.com/CpS-209-Team3/Zenith/wiki/BaoLeJournal |
| Steven Platt | 34 hrs | 26 hrs |  https://github.com/CpS-209-Team3/Zenith/wiki/PlattJournal |

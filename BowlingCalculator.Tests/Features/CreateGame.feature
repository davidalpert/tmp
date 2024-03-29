﻿Feature: CreateGame
	In order to track a bowling game
	As an employee
	I want to be able to create and configure a game.

Scenario: Game with one player
	Given I create a game with 1 player
	And I add the following players to the game:
		| name |
		| Mal  |
	When I list the players
	Then the list shall include the following players:
		| name |
		| Mal  |

Scenario: Game with three players
	Given I create a game with 3 players
	And I add the following players to the game:
		| name  |
		| Mal   |
		| Inara |
		| Wash  |
	When I list the players
	Then the list shall include the following players:
		| name  |
		| Mal   |
		| Inara |
		| Wash  |

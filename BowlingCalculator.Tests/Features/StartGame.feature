Feature: StartGame
	In order to begin bowling
	As a player
	I want to start the game

Scenario: Starting a game with one player
	Given a game has been set up with the following players:
		| name |
		| Mal  |
	When I press start
	Then the players should have the following scores:
		| name | score |
		| Mal  | 0     | 
	And it should be Mal's turn
	And the game should be ready to receive frame 1, ball 1

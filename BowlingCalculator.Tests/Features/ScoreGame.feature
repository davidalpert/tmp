Feature: ScoreGame
	In order to provide feedback to the players
	As a bowling lane
	I want to score the game as it happens

	Scenario: A game with one player and two balls
		Given a game has been set up with the following players:
			| name |
			| Mal  |
		When I record the following results:
		 	| turns |
		 	| 1,1   |
		Then the players should have the following scores:
			| name | score |
			| Mal  | 2     | 
		And it should be Mal's turn
		And the game should be ready to receive frame 2, ball 1
		 
		 
		 

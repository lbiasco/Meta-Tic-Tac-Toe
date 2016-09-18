# Meta-Tic-Tac-Toe
Tic-Tac-Toe game variant project made with Visual Studios for PSY 30676

A mid-semester project for the Programming for Video Game Development course (PSY 30676) at the University of Notre Dame.
This project was written in C# and compiled primarily out of forms in Visual Studios 2015. The game design is modeled after
Tic-Tac-Toe, but in the Meta variant, the game design is to switch Tic-Tac-Toe playing field with every move.

Meta Tic-Tac-Toe rules:
- Lay out nine games of Tic-Tac-Toe in a grid, similar to the design of normal Tic-Tac-Toe.
- The starting player (player 1) places an icon in one of the nine boxes of the center game.
- The following player (player 2) then must play in the normal Tic-Tac-Toe designated by the placement of player 1's icon.
   - Example: Player 1 places an X in the top-right position of the center game.
              Player 2 places an O in the bottom-left position of the top-right game.
              Player 1 places an X in the left position of the bottom-left game (etc, etc)
- Every time a player wins a smaller game (three icons in a row), they win that game and by extension, that game
  represents a larger icon in the grid of nine games.
- The first player to obtain three larger icons in a row wins the overall game.

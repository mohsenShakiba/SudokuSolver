# Solving Sudoku using Backtracking alogorithm in C#
This project contains the logic for solving a Sudoku puzzle using backtracking technique.

## Backtracking 
Backtracking is a technique that involves setting the value of each empty cell with a number and reach an unsolvable state we backtrack and change the numbers until we can solve the puzzle.

## Loading a puzzle
To load a puzzle use either one of existing puzzles in the `Examples` folder or create one according to these rules:
[] create a .txt file
[] each row must be in a new line
[] cells inside the row must be separate with ,
[] empty cells must be either empty space or a "-" character

``` C#
var loader = new FileLoader(@"..\..\..\Examples\easy_1.txt", 3);
var chart = loader.Load();
```

## Solving
To solve the puzzle use the `BackTrackSolver` class:

```C#
var solver = new BackTrackSolver();
var solvedChart = solver.Solve(chart);
```

## View Sudoku in Console
To view a Sudoku puzzle in console:

```C#
var presentation = new ConsolePresentation();
presentation.Present(solvedChart);
```


﻿using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.Formatters;
using BlueBoxes.WordSearchBuilder.Helpers;

namespace BlueBoxes.WordSearchBuilder.WordPlacers
{
    public abstract class WordPlacer
    {
        public abstract PlacedWord TryPlaceWord(string word, char[][] grid);
        public abstract Direction Direction { get; }
        
        public bool useSpanish { get; set; } = false;
        public IWordFormatter WordFormatter { get; set; } = new EnglishWordFormatter();
        public IWordFormatter WordFormatterSpanish { get; set; } = new SpanishWordFormatter();

        public static readonly char NullChar = '\0';

        public GridCell GetActualGridCell(GridCell startCell, int offset)
        {

            var col = startCell.Col + Direction.ToColDelta() * offset;
            var row = startCell.Row + Direction.ToRowDelta() * offset;
            return new GridCell(col, row);
        }

        public char? GetCellValue(GridCell startCell, int offset, char[][] grid)
        {
            Console.WriteLine($"offset {offset}");
            Console.WriteLine($"col {startCell.Col} row {startCell.Row}");
            var cell = GetActualGridCell(startCell, offset);
            Console.WriteLine($"result col {cell.Col} row {cell.Row}");

            try
            {
                return grid[cell.Col][cell.Row];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetCellValue(GridCell startCell, int offset, char[][] grid, char value)
        {
            var cell = GetActualGridCell(startCell, offset);
            grid[cell.Col][cell.Row] = value;
        }

        /// <summary>
        /// Finds an sets a valid location for the word to be placed
        /// </summary>
        /// <param name="xRange"></param>
        /// <param name="yRange"></param>
        /// <param name="word"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected PlacedWord FindWordLocation(IList<int> xRange, IList<int> yRange, string word, char[][] grid)
        {
            xRange = xRange.Shuffle();
            yRange = yRange.Shuffle();

            var placeFound = false;
            var startPos = new GridCell();

            var wordToPlace = (useSpanish ? WordFormatterSpanish.FormatWord(word) :  WordFormatter.FormatWord(word));

            //Find a Valid location
            foreach (var currentCol in xRange)
            {
                foreach (var currentRow in yRange)
                {
                    placeFound = TryPlaceWordLetters(wordToPlace, new GridCell(currentCol, currentRow), grid);
                    if (placeFound)
                    {
                        startPos = new GridCell(currentCol, currentRow);
                        break;
                    }
                }
                if (placeFound)
                    break;
            }

            //Set Word
            if (placeFound)
            {
                for (var i = 0; i < wordToPlace.Length; i++)
                {
                    SetCellValue(startPos, i, grid, wordToPlace[i]);
                }
                return new PlacedWord(word, wordToPlace.Length, Direction, startPos);
            }
            else
            {
                return PlacedWord.Empty;
            }
        }

        /// <summary>
        /// Tests if a word can be placed at a given location
        /// </summary>
        protected bool TryPlaceWordLetters(string wordLetters, GridCell start, char[][] grid)
        {
            for (var i = 0; i < wordLetters.Length; i++)
            {
                var cell = GetCellValue(start, i, grid);
                if (cell != NullChar && cell != wordLetters[i])
                    return false;
            }
            return true;
        }
    }
}
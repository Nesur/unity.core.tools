using System;

namespace Nesur.Core {
    /// <summary>
    /// Non-generic version for multiplayer network variables compatibility.
    /// </summary>
    public class GridSystem<T>{
        private readonly T[,] _grid;

        public GridSystem(int width, int height) {
            _grid = new T[width, height];
        }

        public T GetCellValue(int x, int y) {
            ValidateCellCoordinates(x, y);
            return _grid[x, y];
        }

        public void SetCellValue(int x, int y, T value) {
            ValidateCellCoordinates(x, y);
            _grid[x, y] = value;
        }

        public bool IsValidCell(int x, int y) {
            try {
                ValidateCellCoordinates(x, y);
                return true;
            }
            catch (IndexOutOfRangeException) {
                return false;
            }
        }

        private void ValidateCellCoordinates(int x, int y) {
            if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1)) {
                throw new IndexOutOfRangeException("Coordinates are out of bounds.");
            }
        }
    }
}
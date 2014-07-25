using System;
using System.Collections.Generic;

namespace Sudoku.Lib.Entities
{
    public class SudokuCell
    {
        private int positionX;
        private int positionY;
        private int value;

        public int PositionX
        {
            get
            {
                return this.positionX;
            }
        }

        public int PositionY
        {
            get
            {
                return this.positionY;
            }
        }

        public int SquareId
        {
            get
            {
                int previus = 3;
                if (this.positionX < 3)
                {
                    previus = 0;
                }
                if (this.positionX > 5)
                {
                    previus = 6;
                }

                if (this.positionY < 3)
                {
                    return previus + 0;
                }
                if (this.positionY > 5)
                {
                    return previus + 2;
                }

                return previus + 1;
            }
        }

        public ISet<int> PossibleValues { get; set; }

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value != 0 && !PossibleValues.Contains(value))
                {
                    throw new ArgumentException(string.Format("Not alowed value - {0}", value));
                }

                this.value = value;
            }
        }

        public SudokuCell(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;

            this.PossibleValues = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
    }
}

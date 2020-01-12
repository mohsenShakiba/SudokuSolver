using System.Collections.Generic;

namespace Sudoku
{
    public class RandomInputGenerator
    {
        private readonly List<Input> Inputs;

        public RandomInputGenerator()
        {
            Inputs = new List<Input>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            Inputs.Add(new Input());
                        }
                    }
                }
            }
        }
        
    }
}
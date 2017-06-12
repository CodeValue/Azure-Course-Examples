using Contract;

namespace AzureRelayDemo
{
    class ProblemSolver : IProblemSolver
    {
        public int AddNumbers(int a, int b)
        {
            return a + b;
        }
    }
}
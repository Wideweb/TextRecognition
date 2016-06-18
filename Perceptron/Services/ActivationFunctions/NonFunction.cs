using Perceptron.ServiceInterfaces;

namespace Perceptron.ActivationFunctions
{
    public class NonFunction : IActivationFunction
    {
        public double Compute(double x)
        {
            return x;
        }

        public double ComputeFirstDerivative(double x)
        {
            return 1;
        }
    }
}

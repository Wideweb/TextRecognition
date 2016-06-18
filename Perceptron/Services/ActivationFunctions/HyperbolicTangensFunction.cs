using System;
using Perceptron.ServiceInterfaces;

namespace Perceptron.ActivationFunctions
{
    public class HyperbolicTangensFunction : IActivationFunction
    {
        private readonly double alpha;

        public HyperbolicTangensFunction(double alpha)
        {
            this.alpha = alpha;
        }

        public double Compute(double x)
        {
            return (Math.Tanh(alpha * x));
        }

        public double ComputeFirstDerivative(double x)
        {
            double t = Math.Tanh(alpha * x);
            return alpha * (1 - t * t);
        }
    }
}

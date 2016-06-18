using System;
using Perceptron.ServiceInterfaces;

namespace Perceptron.Services.ActivationFunctions
{
    public class SigmoidFunction : IActivationFunction
    {
        private readonly double alpha;

        public SigmoidFunction(double alpha)
        {
            this.alpha = alpha;
        }

        public double Compute(double x)
        {
            double r = (1 / (1 + Math.Exp(-1 * alpha * x)));
            return r;
        }

        public double ComputeFirstDerivative(double x)
        {
            return alpha * this.Compute(x) * (1 - this.Compute(x));
        }
    }
}

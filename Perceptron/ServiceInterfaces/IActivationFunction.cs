namespace Perceptron.ServiceInterfaces
{
    public interface IActivationFunction
    {
        double Compute(double x);
        double ComputeFirstDerivative(double x);
    }
}

namespace Perceptron.ServiceInterfaces
{
    public interface IProximityMeasure
    {
        double Compute(double[] d, double[] y);
        double ComputePartialDerivative(double[] d, double[] y, int Index);
    }
}

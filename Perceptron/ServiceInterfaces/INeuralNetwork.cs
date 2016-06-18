using System.Collections.Generic;
using Perceptron.Services.NeuronNetwork;

namespace Perceptron.ServiceInterfaces
{
    public interface INeuralNetwork
    {
        List<Layer> Layers { get; set; }
        List<Link> InputLinks { get; set; }

        double[] ComputeOutput(double[] inputVector);
    }
}

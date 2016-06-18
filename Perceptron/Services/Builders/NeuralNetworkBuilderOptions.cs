using System.Collections.Generic;
using Perceptron.ServiceInterfaces;

namespace Perceptron.Services.Builders
{
    public class NeuralNetworkBuilderOptions
    {
        public List<int> Layers { get; set; }
        public IActivationFunction ActivationFunction;
    }
}

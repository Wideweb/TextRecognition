using System.Collections.Generic;
using Perceptron.ServiceInterfaces;

namespace Perceptron.Services.Builders
{
    public class ConvolutionalNeuralNetworkBuilderOptions
    {
        public OriginalImageOptions InputImageOptions { get; set; }
        public List<SignMapOptions> Layers { get; set; }
        public IActivationFunction ActivationFunction;
    }

    public class OriginalImageOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class SignMapOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Overlay { get; set; }
    }
}

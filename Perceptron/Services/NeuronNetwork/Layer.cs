using System.Collections.Generic;
using Perceptron.ServiceInterfaces;
using Perceptron.Services.Builders;

namespace Perceptron.Services.NeuronNetwork
{
    public class Layer
    {
        public double[] LastOutput { get; set; }
        public int InputDimension { get; set; }
        public List<Neuron> Neurons { get; set; }
        public OriginalImageOptions ImageOptions { get; set; }

        public Layer(int length, IActivationFunction activationFunction, double threshold = 1)
        {
            InputDimension = length;

            Neurons = new List<Neuron>();
            for (var i = 0; i < length; i++)
            {
                Neurons.Add(new Neuron{
                    ActivationFunction = activationFunction,
                    Threshold = threshold
                });
            }
        }

        public void Activate()
        {
            var output = new double[Neurons.Count];
            for (var i = 0; i < Neurons.Count; i++)
            {
                output[i] = Neurons[i].Activate();
            }

            LastOutput = output;
        }
    }
}

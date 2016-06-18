using System.Collections.Generic;
using Perceptron.ServiceInterfaces;

namespace Perceptron.Services.NeuronNetwork
{
    public class NeuronNetwork : INeuralNetwork
    {
        public List<Layer> Layers { get; set; }
        public List<Link> InputLinks { get; set; }

        public NeuronNetwork(List<Link> inputLinks, List<Layer> layers)
        {
            Layers = layers;
            InputLinks = inputLinks;
        }

        public double[] ComputeOutput(double[] inputVector)
        {
            SendSignalToTheFirstLayer(inputVector);
            ConductSignalOverLayers();
            return ReceiveSignalFormTheLastLayer();
        }

        private void SendSignalToTheFirstLayer(double[] inputVector)
        {
            for (var i = 0; i < inputVector.Length; i++)
            {
                InputLinks[i].SendImpulse(inputVector[i]);
            }
        }

        private void ConductSignalOverLayers()
        {
            Layers.ForEach(l => l.Activate());
        }

        private double[] ReceiveSignalFormTheLastLayer()
        {
            return Layers[Layers.Count - 1].LastOutput;
        }
    }
}

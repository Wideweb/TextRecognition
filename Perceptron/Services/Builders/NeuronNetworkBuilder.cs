using System;
using System.Collections.Generic;
using System.Linq;
using Perceptron.ActivationFunctions;
using Perceptron.Services.NeuronNetwork;

namespace Perceptron.Services.Builders
{
    public class NeuronNetworkBuilder
    {
        private readonly NeuralNetworkBuilderOptions options;
        private readonly Random random;

        private List<Layer> layers;
        private List<Link> inputLinks;

        public NeuronNetworkBuilder(NeuralNetworkBuilderOptions options)
        {
            this.options = options;
            random = new Random(104234);
        }

        public NeuronNetwork.NeuronNetwork Build()
        {
            BuildLayers();
            EstablishLinks();
            BuildInputLinks();

            return new NeuronNetwork.NeuronNetwork(inputLinks, layers);
        }

        private void BuildLayers()
        {
            layers = new List<Layer>();
            AddInputLayer();
            AddInnerLayers();
        }

        private void AddInputLayer()
        {
            layers.Add(new Layer(options.Layers[0], new NonFunction(), 0));
        }

        private void AddInnerLayers()
        {
            for (var i = 1; i < options.Layers.Count; i++)
            {
                layers.Add(new Layer(options.Layers[i], options.ActivationFunction));
            }
        }

        private void EstablishLinks()
        {
            for (var i = 0; i < layers.Count - 1; i++)
            {
                ConnectLayers(layers[i], layers[i + 1]);
            }
        }

        private void ConnectLayers(Layer inputLayer, Layer outputLayer)
        {
            foreach (var neuron in inputLayer.Neurons)
            {
                ConnectNeuronOutputLinksWithLayer(neuron, outputLayer);
            }

            NormalizeWeights(outputLayer);
        }

        private void ConnectNeuronOutputLinksWithLayer(Neuron neuron, Layer layer)
        {
            foreach (var outputNeuron in layer.Neurons)
            {
                var link = new Link
                {
                    Origin = neuron,
                    Destination = outputNeuron,
                    Weight = random.NextDouble() * 2 - 1
                };

                neuron.OutgoingLinks.Add(link);
                outputNeuron.IncomingLinks.Add(link);
            }
        }

        private void NormalizeWeights(Layer layer)
        {
            foreach (var neuron in layer.Neurons)
            {
                var length = Math.Sqrt(neuron.IncomingLinks.Sum(l => l.Weight*l.Weight));
                neuron.IncomingLinks.ForEach(l => l.Weight /= length);
            }
        }

        private void BuildInputLinks()
        {
            inputLinks = layers[0].Neurons.Select(neuron => new Link
            {
                Destination = neuron,
                Weight = 1
            }).ToList();
        }
    }
}

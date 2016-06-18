using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using Perceptron.ActivationFunctions;
using Perceptron.Services.NeuronNetwork;

namespace Perceptron.Services.Builders
{
    public class СonvolutionalNeuronNetworkBuilder
    {
        private readonly ConvolutionalNeuralNetworkBuilderOptions options;
        private readonly Random random;

        private List<Layer> layers;
        private List<Link> inputLinks;

        public СonvolutionalNeuronNetworkBuilder(ConvolutionalNeuralNetworkBuilderOptions options)
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
            layers.Add(new Layer(options.InputImageOptions.Width*options.InputImageOptions.Height,
                new NonFunction(), 0));
        }

        private void AddInnerLayers()
        {
            AddInnerLayer(options.InputImageOptions, options.Layers[0]);

            for (var i = 1; i < options.Layers.Count; i++)
            {
                AddInnerLayer(layers[i - 1].ImageOptions, options.Layers[i]);
            }
        }

        private void AddInnerLayer(OriginalImageOptions inputImageOptions, SignMapOptions signMapOptions)
        {
            var imageOptions = getImageOptions(inputImageOptions, signMapOptions);
            layers.Add(new Layer(imageOptions.Width * imageOptions.Height, options.ActivationFunction)
            {
                ImageOptions = imageOptions
            });
        }

        private OriginalImageOptions getImageOptions(OriginalImageOptions inputImageOptions, SignMapOptions signMapOptions)
        {
            return new OriginalImageOptions
            {
                Width = inputImageOptions.Width / signMapOptions.Width,
                Height = inputImageOptions.Height / signMapOptions.Height
            };
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
            foreach (var neuron in outputLayer.Neurons)
            {
                //ConnectNeuronOutputLinksWithLayer(neuron, outputLayer);
                for (var i = 0; i < inputLayer.ImageOptions.Height; i++)
                {
                    for (var j = 0; j < inputLayer.ImageOptions.Width; j++)
                    {
                        //inputLayer.Neurons[i * inputLayer.ImageOptions.Width + j]
                    }    
                }
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

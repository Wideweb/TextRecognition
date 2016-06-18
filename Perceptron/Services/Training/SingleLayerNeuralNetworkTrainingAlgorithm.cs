using System.Collections.Generic;
using System.Linq;
using Perceptron.ServiceInterfaces;
using Perceptron.Services.NeuronNetwork;

namespace Perceptron.Services.Training
{
    public class SingleLayerNeuralNetworkTrainingAlgorithm : ITrainingAlgorithm
    {
        private readonly INeuralNetwork neuralNetwork;
        private readonly TrainAlgorithmConfig config;

        public SingleLayerNeuralNetworkTrainingAlgorithm(INeuralNetwork neuralNetwork, TrainAlgorithmConfig config)
        {
            this.neuralNetwork = neuralNetwork;
            this.config = config;
        }

        public void Train(List<TrainingSample> trainingSamples)
        {
            for (var i = 0; i < config.Epoches; i++)
            {
                trainingSamples.ForEach(TrainBySample);
            }
        }

        private void TrainBySample(TrainingSample sample)
        {
            for (var i = 0; i < config.OneSampleRepeates; i++)
            {
                var output = neuralNetwork.ComputeOutput(sample.Sample);
                CorrectWeights(neuralNetwork.Layers.Last(), sample, output);
            }
        }

        private void CorrectWeights(Layer layer, TrainingSample sample, double[] output)
        {
            for (var i = 0; i < layer.InputDimension; i++)
            {
                var neuron = layer.Neurons[i];

                var dEdS = config.ProximityMeasure.ComputePartialDerivative(sample.Answer, output, i) 
                    * neuron.ActivationFunction.ComputeFirstDerivative(neuron.LastNET);


                for (var j = 0; j < neuron.IncomingLinks.Count; j++)
                {
                    var incomingLink = neuron.IncomingLinks[j];

                    incomingLink.dw = incomingLink.dw * config.InertialFactor + dEdS
                                      * sample.Sample[j]
                                      * config.TrainingSpeed;

                    incomingLink.Weight -= incomingLink.dw;
                }
            }
        }
    }
}

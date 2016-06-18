using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using Perceptron.ServiceInterfaces;
using Perceptron.Services.NeuronNetwork;
using Perceptron.Services.Training.EventArgs;

namespace Perceptron.Services.Training
{
    public class BackPropagationAlgorithm
    {
        private readonly TrainAlgorithmConfig config;
        private readonly INeuralNetwork neuralNetwork;

        public BackPropagationAlgorithm(INeuralNetwork neuralNetwork, TrainAlgorithmConfig config)
        {
            this.config = config;
            this.neuralNetwork = neuralNetwork;
        }

        public async Task Train(List<TrainingSample> trainingSamples)
        {
            await Task.Run(() =>
            {
                for (var i = 0; i < config.Epoches; i++)
                {
                    foreach (var sample in trainingSamples)
                    {
                        TrainSample(sample);
                    }
                }
            });
        }

        private void TrainSample(TrainingSample sample)
        {
            for (var i = 0; i < config.OneSampleRepeates; i++)
            {
                var output = neuralNetwork.ComputeOutput(sample.Sample);
                RiseErrorCalculationEvent(config.ProximityMeasure.Compute(sample.Answer, output), sample);
                SpreadErrorSignals(sample.Answer);
                CorrectWeights();
            }
        }

        private static double[] ParseOutput(IEnumerable<double> output)
        {
            var answer = output.Select((v, i) => new { Value = v, Index = i })
                .MaxBy(it => it.Value).Index;

            var result = new double[output.Count()];
            result[answer] = 1;

            return result;
        }

        private void RiseErrorCalculationEvent(double error, TrainingSample sample)
        {
            if (config.OnErrorCalculated != null)
            {
                config.OnErrorCalculated(this, new NeuralNetworkErrorEventArgs
                {
                    Error = error,
                    Image = Array.ConvertAll(sample.Sample, ToByte)
                });
            }
        }

        private byte ToByte(double v)
        {
            return (byte) v;
        }

        private void SpreadErrorSignals(double[] answer)
        {
            var length = neuralNetwork.Layers.Count;
            var layers = neuralNetwork.Layers;

            ComputeGradientForOutputNeurons(layers[length - 1], answer);
            for (var i = length - 2; i > 0; i--)
            {
                ComputeGradientForInnerNeurons(layers[i]);
            }
        }

        private void ComputeGradientForOutputNeurons(Layer layer, double[] answer)
        {
            for(var i = 0; i < layer.InputDimension; i++)
            {
                var neuron = layer.Neurons[i];
                var function = neuron.ActivationFunction;

                neuron.dEdS = 
                    config.ProximityMeasure.ComputePartialDerivative(answer, layer.LastOutput, i)
                    * function.ComputeFirstDerivative(neuron.LastNET);
            }
        }

        private void ComputeGradientForInnerNeurons(Layer layer)
        {
            for (var i = 0; i < layer.InputDimension; i++)
            {
                var neuron = layer.Neurons[i];
                var function = neuron.ActivationFunction;

                var d = function.ComputeFirstDerivative(neuron.LastNET);
                var e = InnerNeuronError(neuron);

                neuron.dEdS = d * e;
            }
        }

        private double InnerNeuronError(Neuron neuron)
        {
            double result = 0; 
            foreach (var outgoingLink in neuron.OutgoingLinks)
            {
                result += outgoingLink.Destination.dEdS
                    * outgoingLink.Weight;
            }

            return result;
        }

        private void CorrectWeights()
        {
            var length = neuralNetwork.Layers.Count;
            var layers = neuralNetwork.Layers;

            for (var i = 1; i < length; i++)
            {
                foreach (var neuron in layers[i].Neurons)
                {
                    foreach (var incomingLink in neuron.IncomingLinks)
                    {
                        var grad = neuron.dEdS * incomingLink.Origin.OUT;
                        var inertialMoment = incomingLink.dw * config.InertialFactor;

                        var correctedWeight = (1 - config.StimulatingFactor) * incomingLink.Weight - grad * config.TrainingSpeed + inertialMoment;

                        incomingLink.dw = correctedWeight - incomingLink.Weight;
                        incomingLink.Weight = correctedWeight;
                    }
                }
            }
        }
    }
}

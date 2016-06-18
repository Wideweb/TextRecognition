using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using NUnit.Framework;
using Perceptron.ActivationFunctions;
using Perceptron.ServiceInterfaces;
using Perceptron.Services.Builders;
using Perceptron.Services.Training;
using Perceptron.Services.Training.ErrorFunctions;

namespace Perceptron.Test
{
    [TestFixture]
    public class BackPropagationAlgorithmTests
    {
        private INeuralNetwork neuralNetwork;
        private NeuralNetworkBuilderOptions buildOptions;
        private TrainAlgorithmConfig trainOptions;

        [SetUp]
        public void FixtureSetup()
        {
            trainOptions = new TrainAlgorithmConfig
            {
                Epoches = 30,
                OneSampleRepeates = 5,
                InertialFactor = 0.1,
                StimulatingFactor = 1.0E-4,
                ProximityMeasure = new LeastSquareMethod(),
                TrainingSpeed = 1
            };
        }

        [Test]
        public void ShouldClassifyNoneIntersectingSamples()
        {
            buildOptions = new NeuralNetworkBuilderOptions
            {
                ActivationFunction = new HyperbolicTangensFunction(1),
                Layers = new List<int> { 9, 3 }
            };

            neuralNetwork = new NeuronNetworkBuilder(buildOptions).Build();

            var trainSamples = new List<TrainingSample>()
            {
                new TrainingSample
                {
                    Answer = new double[] {1, 0, 0},
                    Sample = new double[] {0, 0, 0, 1, 1, 1, 0, 0, 0}
                },
                new TrainingSample
                {
                    Answer = new double[] {0, 1, 0},
                    Sample = new double[] {1, 1, 0, 0, 0, 0, 0, 1, 1}
                },
                new TrainingSample
                {
                    Answer = new double[] {0, 0, 1},
                    Sample = new double[] {0, 0, 1, 0, 0, 0, 1, 0, 0}
                }
            };

            new BackPropagationAlgorithm(neuralNetwork, trainOptions).Train(trainSamples);

            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[0].Sample)), Is.EqualTo(0));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[1].Sample)), Is.EqualTo(1));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[2].Sample)), Is.EqualTo(2));
        }

        [Test]
        public void ShouldClassifyIntersectingSamples()
        {
            buildOptions = new NeuralNetworkBuilderOptions
            {
                ActivationFunction = new HyperbolicTangensFunction(1),
                Layers = new List<int> { 9, 3 }
            };

            neuralNetwork = new NeuronNetworkBuilder(buildOptions).Build();

            var trainSamples = new List<TrainingSample>()
            {
                new TrainingSample
                {
                    Answer = new double[] {1, 0, 0},
                    Sample = new double[] {1, 1, 1, 1, 0, 0, 0, 0, 0}
                },
                new TrainingSample
                {
                    Answer = new double[] {0, 1, 0},
                    Sample = new double[] {0, 0, 1, 1, 1, 1, 0, 0, 0}
                },
                new TrainingSample
                {
                    Answer = new double[] {0, 0, 1},
                    Sample = new double[] {0, 0, 0, 0, 1, 1, 1, 1, 1}
                }
            };

            new BackPropagationAlgorithm(neuralNetwork, trainOptions).Train(trainSamples);

            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[0].Sample)), Is.EqualTo(0));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[1].Sample)), Is.EqualTo(1));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[2].Sample)), Is.EqualTo(2));
        }

        [Test]
        public void ShouldClassifyNestedSamples()
        {
            buildOptions = new NeuralNetworkBuilderOptions
            {
                ActivationFunction = new HyperbolicTangensFunction(1),
                Layers = new List<int> { 9, 2 }
            };

            neuralNetwork = new NeuronNetworkBuilder(buildOptions).Build();

            var trainSamples = new List<TrainingSample>()
            {
                new TrainingSample
                {
                    Answer = new double[] {1, 0},
                    Sample = new double[] {1, 1, 1, 1, 1, 1, 1, 1, 1}
                },
                new TrainingSample
                {
                    Answer = new double[] {0, 1},
                    Sample = new double[] {1, 1, 1, 1, 0, 0, 0, 0, 0}
                }
            };

            new BackPropagationAlgorithm(neuralNetwork, trainOptions).Train(trainSamples);

            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[0].Sample)), Is.EqualTo(0));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(trainSamples[1].Sample)), Is.EqualTo(1));
        }

        private static int ParseOutput(IEnumerable<double> output)
        {
            return output.Select((v, i) => new { Value = v, Index = i })
                .MaxBy(it => it.Value).Index;
        }
    }
}

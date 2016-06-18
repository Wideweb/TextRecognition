using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Moq;
using MoreLinq;
using NUnit.Framework;
using Perceptron.ActivationFunctions;
using Perceptron.ServiceInterfaces;
using Perceptron.Services.Builders;
using Perceptron.Services.TextRecognizer;
using Perceptron.Services.Training;
using Perceptron.Services.Training.ErrorFunctions;

namespace Perceptron.Test
{
    [TestFixture]
    public class TextRecognizerTests
    {
        private const string SamplesPath = @"C:\Users\Alckevich\Desktop\Arial16x16photo";
        private const string SampleExtenstion = @".bmp";

        private TextRecognizer textRecognizer;

        [SetUp]
        public void FixtureSetup()
        {
        }

        [Test]
        public void ShouldClassifyTwoSymbols()
        {
            var options = new TextRecognizerBuilderOptions
            {
                AlphabetCapacity = 2,
                SamplesPath = @"C:\Users\Alckevich\Desktop\Arial16x16photo"
            };

            textRecognizer = new TextRecognizerBuilder(options).Build().Result;

            Assert.That(textRecognizer.Parse(GetSymbolImage('A')), Is.EqualTo('A'));
            Assert.That(textRecognizer.Parse(GetSymbolImage('B')), Is.EqualTo('B'));
        }

        [Test]
        public void Should()
        {
            var trainOptions = new TrainAlgorithmConfig
            {
                Epoches = 500,
                OneSampleRepeates = 5,
                InertialFactor = 0.1,
                StimulatingFactor = 1.0E-4,
                ProximityMeasure = new LeastSquareMethod(),
                TrainingSpeed = 0.1
            };

            var buildOptions = new NeuralNetworkBuilderOptions
            {
                ActivationFunction = new HyperbolicTangensFunction(1),
                Layers = new List<int> { 256, 10, 3 }
            };

            var neuralNetwork = new NeuronNetworkBuilder(buildOptions).Build();

            var ASample = GetSample('A', 0,  3);
            var LSample = GetSample('L', 1, 3);
            var WSample = GetSample('W', 2, 3);

            new BackPropagationAlgorithm(neuralNetwork, trainOptions).Train(new List<TrainingSample>
            {
                ASample, LSample, WSample
            });

            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(ASample.Sample)), Is.EqualTo(0));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(LSample.Sample)), Is.EqualTo(1));
            Assert.That(ParseOutput(neuralNetwork.ComputeOutput(WSample.Sample)), Is.EqualTo(2));
        }

        private TrainingSample GetSample(char symbol, int answer, int alphabetCapacity)
        {
            return new TrainingSample
            {
                Answer = GetAnswerForSymbolIndex(answer, alphabetCapacity),
                Sample = ImageHelper.GetVector(GetSymbolImage(symbol))
            };
        }

        private Bitmap GetSymbolImage(char symbol)
        {
            return ImageHelper.ChangeImageSize(
                new Bitmap(Image.FromFile(Path.Combine(SamplesPath, symbol + SampleExtenstion))), 16, 16);
        }

        private double[] GetAnswerForSymbolIndex(int i, int alphabetCapacity)
        {
            var answer = new double[alphabetCapacity];
            answer[i] = 1;

            return answer;
        }

        private static int ParseOutput(IEnumerable<double> output)
        {
            return output.Select((v, i) => new { Value = v, Index = i })
                .MaxBy(it => it.Value).Index;
        }
    }
}

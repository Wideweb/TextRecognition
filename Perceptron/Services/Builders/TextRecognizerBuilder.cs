using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Perceptron.ActivationFunctions;
using Perceptron.Services.ActivationFunctions;
using Perceptron.Services.TextRecognizer;
using Perceptron.Services.Training;
using Perceptron.Services.Training.ErrorFunctions;

namespace Perceptron.Services.Builders
{
    public class TextRecognizerBuilder
    {
        private const string SymbolPathFormat = @"{0}\{1}.bmp";

        private readonly TextRecognizerBuilderOptions options;

        public TextRecognizerBuilder(TextRecognizerBuilderOptions options)
        {
            this.options = options;
        }

        public async Task<TextRecognizer.TextRecognizer> Build()
        {
            var buildOptions = new NeuralNetworkBuilderOptions
            {
                ActivationFunction = new SigmoidFunction(1),
                Layers = new List<int> { 16 * 16, 5, options.AlphabetCapacity }
            };
            var neuronNetwork = new NeuronNetworkBuilder(buildOptions).Build();

            var trainOptions = new TrainAlgorithmConfig
            {
                Epoches = 1000,
                OneSampleRepeates = 3,
                InertialFactor = 0.1,
                StimulatingFactor = 1.0E-4,
                ProximityMeasure = new LeastSquareMethod(),
                TrainingSpeed = 0.01,
                OnErrorCalculated = options.OnRecognizeErrorCalculated
            };

            await new BackPropagationAlgorithm(neuronNetwork, trainOptions)
                .Train(GetTrainingSamples().ToList());

            return new TextRecognizer.TextRecognizer(neuronNetwork);
        }

        private IEnumerable<TrainingSample> GetTrainingSamples()
        {
            for (var i = 0; i < options.AlphabetCapacity; i++)
            {
                var symbol = (char) ('A' + i);

                yield return new TrainingSample
                {
                    Answer = GetAnswerForSymbolIndex(i),
                    Sample = ImageHelper.GetVector(ImageHelper.ChangeImageSize(new Bitmap(Image.FromFile(GetSymbolPath(symbol))), 16, 16))
                };
            }

            /*var reader = new XlsSymbolReader(@"C:\Users\Alckevich\Desktop\Train.csv", options.AlphabetCapacity);
            return reader.GetSymbols().Take(30);*/

            //return await Task.Run(() => new CsvTrainSampleParser(@"C:\Users\Alckevich\Desktop\Train.csv").Parse().Take(1000).ToList());
        }

        private double[] GetAnswerForSymbolIndex(int i)
        {
            var answer = new double[options.AlphabetCapacity];
            answer[i] = 1;

            return answer;
        }

        private string GetSymbolPath(char symbol)
        {
            return string.Format(SymbolPathFormat,
                    options.SamplesPath, 
                    symbol);
        }
    }
}

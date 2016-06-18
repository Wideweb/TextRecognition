using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MoreLinq;
using Perceptron.ServiceInterfaces;

namespace Perceptron.Services.TextRecognizer
{
    public class TextRecognizer
    {
        private const int SymbolHeight = 16;
        private const int SymbolWidth = 16;

        private readonly INeuralNetwork neuralNetwork;

        public TextRecognizer(INeuralNetwork neuralNetwork)
        {
            this.neuralNetwork = neuralNetwork;
        }

        public int Parse(Bitmap symbol)
        {
            return
                ParseOutput(
                    neuralNetwork.ComputeOutput(
                        ImageHelper.GetVector(ImageHelper.ChangeImageSize(symbol, SymbolWidth, SymbolHeight))));
        }

        private static int ParseOutput(IEnumerable<double> output)
        {
            return output.Select((v, i) => new {Value = v, Index = i})
                .MaxBy(it => it.Value).Index;
        }
    }
}

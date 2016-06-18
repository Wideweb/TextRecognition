using System;
using Perceptron.Services.Training.EventArgs;

namespace Perceptron.Services.Builders
{
    public class TextRecognizerBuilderOptions
    {
        public string SamplesPath { get; set; }
        public int AlphabetCapacity { get; set; }
        public Action<object, NeuralNetworkErrorEventArgs> OnRecognizeErrorCalculated { get; set; }  
    }
}

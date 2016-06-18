using System;
using Perceptron.ServiceInterfaces;
using Perceptron.Services.Training.EventArgs;

namespace Perceptron.Services.Training
{
    public class TrainAlgorithmConfig
    {
        public long Epoches { get; set; }
        public long OneSampleRepeates { get; set; }
        public IProximityMeasure ProximityMeasure { get; set; }
        public double TrainingSpeed { get; set; }
        public double InertialFactor { get; set; }
        public double StimulatingFactor { get; set; }
        public Action<object, NeuralNetworkErrorEventArgs> OnErrorCalculated { get; set; } 
    }
}

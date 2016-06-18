using System.Collections.Generic;
using Perceptron.Services.Training;

namespace Perceptron.ServiceInterfaces
{
    interface ITrainingAlgorithm
    {
        void Train(List<TrainingSample> trainingSamples);
    }
}

using System;
using System.Linq;
using CsvHelper.Configuration;
using Perceptron.Services.Training;

namespace Perceptron.Services.TextRecognizer.Mappers
{
    public sealed class TrainSampleCsvMap : CsvClassMap<TrainingSample>
    {
        private const long AlphabetCapacity = 2;

        public TrainSampleCsvMap()
        {
            Map(m => m.Answer).ConvertUsing(row => GetAnswerForSymbolIndex(row.GetField<int>(0)));
            Map(m => m.Sample).ConvertUsing(row => Array.ConvertAll(row.CurrentRecord.Skip(1).ToArray(), double.Parse).Select(x => x > 0 ? 1.0 : 0).ToArray());
        }

        private static double[] GetAnswerForSymbolIndex(int i)
        {
            var answer = new double[AlphabetCapacity];

            if (i < AlphabetCapacity)
            {
                answer[i] = 1;
            }

            return answer;
        }
    }
}

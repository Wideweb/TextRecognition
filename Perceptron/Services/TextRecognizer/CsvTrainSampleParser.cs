using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using Perceptron.Services.TextRecognizer.Mappers;
using Perceptron.Services.Training;

namespace Perceptron.Services.TextRecognizer
{
    public class CsvTrainSampleParser
    {
        private readonly string fileName;

        public CsvTrainSampleParser(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<TrainingSample> Parse()
        {
            using (var reader = new CsvReader(new StreamReader(fileName)))
            {
                reader.Configuration.RegisterClassMap<TrainSampleCsvMap>();
                while (reader.Read())
                {
                    var record = reader.GetRecord<TrainingSample>();
                    if (record.Answer[0] > 0.5 || record.Answer[1] > 0.5)
                        yield return record;
                }
            }
        } 
    }
}

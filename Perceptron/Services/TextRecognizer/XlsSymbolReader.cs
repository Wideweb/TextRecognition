using System;
using System.Collections.Generic;
using Perceptron.Services.Training;
using Excel = Microsoft.Office.Interop.Excel;

namespace Perceptron.Services.TextRecognizer
{
    public class XlsSymbolReader : IDisposable
    {
        private readonly Excel.Application xlApplication;
        private readonly Excel.Workbook xlWorkBook;
        private readonly Excel.Worksheet xlWorkSheet;
        private readonly Excel.Range range;
        private readonly long alphabetCapacity;

        private bool isDisposed;

        public XlsSymbolReader(string fileName, long alphabetCapacity)
        {
            this.alphabetCapacity = alphabetCapacity;

            xlApplication = new Excel.Application();
            xlWorkBook = xlApplication.Workbooks.Open(fileName, ReadOnly:true);

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Item[1];
            range = xlWorkSheet.UsedRange;

            isDisposed = false;
        }
        
        public IEnumerable<TrainingSample> GetSymbols()
        {
            for (var i = 2; i < range.Rows.Count; i++)
            {
                yield return GetTrainSample(i);
            }
        }

        private TrainingSample GetTrainSample(int row)
        {
            var columnsCount = range.Columns.Count;
            var result = new double[columnsCount - 1];
            
            for (var i = 2; i < range.Columns.Count; i++)
            {
                result[i - 2] = GetCellValue(row, i);
            }

            return new TrainingSample
            {
                Answer = GetAnswerForSymbolIndex(GetCellValue(row, 1)),
                Sample = result
            };
        }

        private int GetCellValue(int row, int column)
        {
            return (int) (range.Cells[row, column] as Excel.Range).Value2;
        }

        private double[] GetAnswerForSymbolIndex(int i)
        {
            var answer = new double[alphabetCapacity];
            answer[i] = 1;

            return answer;
        }
    
        public void Dispose()
        {
            if (isDisposed) return;

            xlWorkBook.Close(true, null, null);
            xlApplication.Quit();

            ReleaseComObject(xlWorkSheet);
            ReleaseComObject(xlWorkBook);
            ReleaseComObject(xlApplication);
            isDisposed = true;
        }

        private void ReleaseComObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}

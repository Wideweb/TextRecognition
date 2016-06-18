using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Perceptron.Services.Builders;
using Perceptron.Services.TextRecognizer;
using Perceptron.Services.Training.EventArgs;

namespace View
{
    public partial class Index : Form
    {
        private TextRecognizer textRecognizer;
        public delegate void EventDelegate(object sender, EventArgs e);

        public Index()
        {
            InitializeComponent();

            errorChart.Series.Add("Error");
        }

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Train(sender, e).Wait();
            });
        }

        private async Task Train(object sender, EventArgs e)
        {
            var options = new TextRecognizerBuilderOptions
            {
                AlphabetCapacity = 5,
                SamplesPath = @"C:\Users\Alckevich\Desktop\Arial16x16photo",
                OnRecognizeErrorCalculated = OnRecognizeErrorCalculated
            };

            textRecognizer = await new TextRecognizerBuilder(options).Build();
        }

        private void ClearButtton_Click(object sender, EventArgs e)
        {
            SymbolDrawBox.Clear();
        }

        private void RecognizeButton_Click(object sender, EventArgs e)
        {
            /*var samples = new CsvTrainSampleParser(@"C:\Users\Alckevich\Desktop\Train.csv").Parse().Skip(10).Take(100).ToList();
            for (var i = 0; i < 10; i++)
            {*/
                SymbolLabel.Text = "" + (char)('A' + textRecognizer.Parse(SymbolDrawBox.GetImage()));
            /*}*/
        }

        private byte ToByte(double v)
        {
            return (byte) v;
        }

        private Image GetImage(byte[] arr)
        {
            var bmp = new Bitmap(28, 28);
            for (var i = 0; i < 28; i++)
            {
                for (var j = 0; j < 28; j++)
                {
                    var color = arr[i * 28 + j] > 0 ? Color.Black : Color.White;
                    bmp.SetPixel(j, i, color);
                }
            }

            return bmp;
        }

        private void OnRecognizeErrorCalculated(object sender, NeuralNetworkErrorEventArgs eventArgs)
        {
            //var errorArgs = eventArgs as NeuralNetworkErrorEventArgs;
            BeginInvoke(new EventDelegate((s, e) =>
            {
            errorChart.Series["Error"].Points.AddY((e as NeuralNetworkErrorEventArgs).Error);
            

            //trainSymbolPictureBox.Image = GetImage(errorArgs.Image);

            }), sender, eventArgs);
        }
    }
}

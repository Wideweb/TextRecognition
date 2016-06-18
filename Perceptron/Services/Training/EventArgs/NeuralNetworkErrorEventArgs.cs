namespace Perceptron.Services.Training.EventArgs
{
    public class NeuralNetworkErrorEventArgs : System.EventArgs
    {
        public double Error { get; set; }
        public byte[] Image { get; set; }
    }
}

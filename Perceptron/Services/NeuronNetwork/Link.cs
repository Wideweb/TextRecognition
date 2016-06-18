namespace Perceptron.Services.NeuronNetwork
{
    public class Link
    {
        public double dw { get; set; }

        public double Weight { get; set; }
        public Neuron Origin { get; set; }
        public Neuron Destination { get; set; }

        public Link()
        {
            Weight = 0;
        }

        public void SendImpulse(double impulse)
        {
            Destination.ReceiveImpulse(impulse * Weight);
        }
    }
}

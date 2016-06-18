using System.Collections.Generic;
using Perceptron.ServiceInterfaces;

namespace Perceptron.Services.NeuronNetwork
{
    public class Neuron
    {
        public List<Link> OutgoingLinks { get; set; }
        public List<Link> IncomingLinks { get; set; }

        public double dEdS { get; set; }
        public double LastNET { get; set; }
        public double NET { get; set; }
        public double OUT { get; set; }

        public IActivationFunction ActivationFunction { get; set; }
        public double Threshold { get; set; }

        public Neuron()
        {
            OutgoingLinks = new List<Link>();
            IncomingLinks = new List<Link>();
        }

        public double Activate()
        {
            OUT = ActivationFunction.Compute(NET + Threshold);
            LastNET = NET;
            NET = 0;
            OutgoingLinks.ForEach(l => l.SendImpulse(OUT));

            return OUT;
        }

        public void ReceiveImpulse(double impulse)
        {
            NET += impulse;
        }
    }
}

namespace DeepNN
{
    public class Connection
    {
        private Node InNode { get; set; }
        private Node OutNode { get; set; }
        public double Weight { get; set; }
        public double DeltaWeight { get; set; }

        public Connection(Node inNode, Node outNode)
        {
            InNode = inNode;
            OutNode = outNode;
            Weight = Network.GetRandom();
        }
    }
}
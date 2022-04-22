namespace DeepNN
{
    [Serializable()]
    public class Connection
    {
        public Node InNode { get; set; }
        public Node OutNode { get; set; }
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
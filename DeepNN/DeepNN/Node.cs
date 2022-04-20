using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepNN
{
    public class Node
    {
        public List<Connection> InConnections { get; set; }
        public List<Connection> OutConnections { get; set; }
        public double Bias { get; set; }
        public double DeltaBias { get; set; }
        public double Gradient { get; set; }
        public double Value { get; set; }
        public Node()
        {
            InConnections = new List<Connection>();
            OutConnections = new List<Connection>();
            OutConnections = new List<Connection>();
            Bias = Network.GetRandom();
        }
        public Node(IEnumerable<Node> inNodes) : this()
        {
            foreach (Node node in inNodes)
            {
                Connection connection = new Connection(node, this);
                node.OutConnections.Add(connection);
                InConnections.Add(connection);
            }
        }
        public double CalculateValue()
            => Value = Sigmoid.Output(InConnections.Sum(a => a.Weight * a.InNode.Value) + Bias);

    }
}

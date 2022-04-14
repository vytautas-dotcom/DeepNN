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
<<<<<<< HEAD
            OutConnections = new List<Connection>();
=======
            OutConnection = new List<Connection>();
>>>>>>> a74e4d3f8c7b767eca3fa098631131ae55a5b68c
            Bias = Network.GetRandomBias();
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
    }
}

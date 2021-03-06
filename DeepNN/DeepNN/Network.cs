namespace DeepNN
{
    [Serializable()]
    public class Network
    {
        public List<Node> InputLayer { get; set; }
        public List<List<Node>> HiddenLayers { get; set; }
        public List<Node> OutputLayer { get; set; }
        public double LearnRate { get; set; }
        public double Momentum { get; set; }

        private static readonly Random random = new Random();
        public Network()
        {
            InputLayer = new List<Node>();
            HiddenLayers = new List<List<Node>>();
            OutputLayer = new List<Node>();
            LearnRate = 0.0;
            Momentum = 0.0;
        }
        public Network(int inputSize, int[] hiddenSize, int outputSize, 
                       double? learnRate = null, double? momentum = null)
        {
            InputLayer = new List<Node>();
            HiddenLayers = new List<List<Node>>();
            OutputLayer = new List<Node>();
            LearnRate = learnRate ?? 0.25;
            Momentum = momentum ?? 0.5;


            for (int i = 0; i < inputSize; i++)
                InputLayer.Add(new Node());


            var firstHiddenLayer = new List<Node>();
            for (int i = 0; i < hiddenSize[0]; i++)
                firstHiddenLayer.Add(new Node(InputLayer));

            HiddenLayers.Add(firstHiddenLayer);

            for (int i = 1; i < hiddenSize.Length; i++)
            {
                var hiddenLayer = new List<Node>();
                for (var j = 0; j < hiddenSize[i]; j++)
                    hiddenLayer.Add(new Node(HiddenLayers[i - 1]));
                HiddenLayers.Add(hiddenLayer);
            }

            for (int i = 0; i < outputSize; i++)
                OutputLayer.Add(new Node(HiddenLayers.Last()));
        }
        internal static double GetRandom()
            => 2 * random.NextDouble() - 1;
        public void Train(List<Data> data, int epochs)
        {
            for (int i = 0; i < epochs; i++)
            {
                foreach (var item in data)
                {
                    ForwardPropagate(item.Values);
                    BackwardPropagate(item.Targets);
                }
            }

        }
        public void Train(List<Data> data, double minError)
        {
            var error = 1.0;
            var epochs = 0;
            while(error > minError && epochs < int.MaxValue)
            {
                var errors = new List<double>();
                foreach (var item in data)
                {
                    ForwardPropagate(item.Values);
                    BackwardPropagate(item.Targets);
                    errors.Add(CalculateError(item.Targets));
                }
                error = errors.Average();
                epochs++;
            }
        }

        private double CalculateError(params double[] targets)
        {
            int count = 0;
            return OutputLayer.Sum(a => Cost.Error(a.Value, targets[count++]));
        }

        private void ForwardPropagate(double[] values)
        {
            var index = 0;
            InputLayer.ForEach(node => node.Value = values[index++]);
            HiddenLayers.ForEach(layer => layer.ForEach(node => node.CalculateValue()));
            OutputLayer.ForEach(node => node.CalculateValue());
        }
        private void BackwardPropagate(double[] targets)
        {
            var index = 0;
            OutputLayer.ForEach(node => node.CalculateGradient(targets[index++]));
            HiddenLayers.Reverse();
            HiddenLayers.ForEach(layer => layer.ForEach(node => node.CalculateGradient()));
            HiddenLayers.ForEach(layer => layer.ForEach(node => node.UpdateWeights(LearnRate, Momentum)));
            HiddenLayers.Reverse();
            OutputLayer.ForEach(node => node.UpdateWeights(LearnRate, Momentum));
        }
        public void Test(List<Data> data)
        {
            foreach (var item in data)
            {
                ForwardPropagate(item.Values);
            }
            OutputLayer.ForEach(x => Console.WriteLine(x.Value));
        }
        //-------------------train for directions--------------------
        public void Train_Directions(List<(double[], double[])> data, int epochs)
        {
            for (int i = 0; i < epochs; i++)
            {
                foreach (var item in data)
                {
                    ForwardPropagate(item.Item1);
                    BackwardPropagate(item.Item2);
                }
            }

        }
        public void Train_Directions(List<(double[], double[])> data, double minError)
        {
            var error = 1.0;
            var epochs = 0;
            while (error > minError && epochs < int.MaxValue)
            {
                var errors = new List<double>();
                foreach (var item in data)
                {
                    ForwardPropagate(item.Item1);
                    BackwardPropagate(item.Item2);
                    errors.Add(CalculateError(item.Item2));
                }
                error = errors.Average();
                epochs++;
            }
        }
        public void Test_Directions(List<(double[], double[], string)> data)
        {
            int count = 0;
            List<double[]> allCalculatedOutputs = new List<double[]>();
            double[] calculatedOutput;
            foreach (var item in data)
            {
                int i = 0;
                calculatedOutput = new double[item.Item2.Length];
                ForwardPropagate(item.Item1);
                OutputLayer.ForEach(x => calculatedOutput[i++] = x.Value);
                allCalculatedOutputs.Add(calculatedOutput);
            }
            for (int i = 0; i < data.Count; i++)
            {
                var arr1 = data[i].Item2;
                var arr2 = MaxToOne(allCalculatedOutputs[i]);
                if (arr1.SequenceEqual(arr2))
                    count++;
            }
            double accuracy = (double)count / data.Count;
            Console.WriteLine("Accuracy " + accuracy);
        }
        private double[] MaxToOne(double[] val)
        {
            double[] convertedArray = new double[val.Length];
            double biggestVal = 0;
            int indexOfMax = 0;

            for (int i = 0; i < val.Length; i++)
            {
                if (val[i] > biggestVal)
                {
                    biggestVal = val[i];
                    indexOfMax = i;
                }
            }
            for (int i = 0; i < val.Length; i++)
            {
                if (i == indexOfMax)
                {
                    convertedArray[i] = 1;
                }
                else
                {
                    convertedArray[i] = 0;
                }
            }
            return convertedArray;
        }
    }
}
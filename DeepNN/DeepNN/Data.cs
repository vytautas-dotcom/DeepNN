namespace DeepNN
{
    public class Data
    {
        public double[] Values { get; set; }
        public double[] Targets { get; set; }
        public Data(double[] values)
        {
            Values = values;
        }
        public Data(double[] values, double[] targets)
        {
            Values=values;
            Targets = targets;
        }
    }
    public class DataList : List<Data>
    {
        public void Add(double[] values)
            => this.Add(new Data(values));
        public void Add(double[] values, double[] targets)
            => this.Add(new Data(values, targets));

        private void EnterNumbers(int numInput, int numOutput)
        {
            List<string> values = new List<string>();
            List<string> targets = new List<string>();

            for (int i = 0; i < numInput; i++)
            {
                Console.WriteLine("enter value and press enter:");
                string value = Console.ReadLine();
                values.Add(value);
            }
            for (int i = 0; i < numOutput; i++)
            {
                Console.WriteLine("enter target eparated by comma");
                string target = Console.ReadLine();
                targets.Add(target);
            }

            double[] arrValues = new double[numInput];
            double[] arrTargets = new double[numOutput];

            foreach (var item in values)
            {
                arrValues[values.IndexOf(item)] = Convert.ToDouble(item);
            }
            foreach (var item in targets)
            {
                arrTargets[targets.IndexOf(item)] = Convert.ToDouble(item);
            }

            this.Add(arrValues, arrTargets);
        }

        public void EnterSamples(int numSamples, int numInput, int numOutput)
        {
            for (int i = 0; i < numSamples; i++)
            {
                EnterNumbers(numInput, numOutput);
            }
        }
    }
}
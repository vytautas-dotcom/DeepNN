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
            string values = "";
            string targets = "";

            for (int i = 0; i < numInput; i++)
            {
                Console.WriteLine("enter values eparated by comma");
                values += Console.ReadLine();
            }
            for (int i = 0; i < numOutput; i++)
            {
                Console.WriteLine("enter targets eparated by comma");
                targets += Console.ReadLine();
            }

            string[] arrValues = values.Split(',');
            string[] arrTargets = targets.Split(',');

            List<double> listValues = new List<double>();
            List<double> listTargets = new List<double>();

            foreach (var item in arrValues)
            {
                listValues.Add(Convert.ToDouble(item));
            }
            foreach (var item in arrTargets)
            {
                listTargets.Add(Convert.ToDouble(item));
            }

            this.Add(listValues.ToArray(), listValues.ToArray());
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
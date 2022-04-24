using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DeepNN
{
    public class DirectionsData
    {
        
        public double[] Values { get; set; }
        public (string, double[]) LabelsTargets;
        public DirectionsData(double[] values)
        {
            Values = values;
        }
        public DirectionsData(double[] values, double[] targets, string label)
        {
            Values = values;
            LabelsTargets.Item1 = label;
            LabelsTargets.Item2 = targets;
        }
    }
    public class DirectionsDataList : List<DirectionsData>
    {
        public Dictionary<string, double[]> InitialList { get; set; }
        Random rand;
        public DirectionsDataList()
        {
            InitialList = new Dictionary<string, double[]>();
            InitialList.Add("up", new double[] { 1, 0, 0, 0 });
            InitialList.Add("right", new double[] { 0, 1, 0, 0 });
            InitialList.Add("down", new double[] { 0, 0, 1, 0 });
            InitialList.Add("left", new double[] { 0, 0, 0, 1 });

            rand = new Random();
        }

        public void Add(double[] values)
            => this.Add(new DirectionsData(values));
        public void Add(double[] values, double[] targets, string label)
            => this.Add(new DirectionsData(values, targets, label));

        public void GetPictures(string directoryName)
        {
            try
            {
                foreach (var item in InitialList)
                {
                    
                    string DirectoryName = @$"../../../{directoryName}/" + item.Key;
                    
                    string[] dirs = Directory.GetFiles(DirectoryName);
                    
                    foreach (string dir in dirs)
                    {

                        Bitmap image = new Bitmap(dir, true);
                        double[] values = new double[image.Width * image.Height];

                        for (int x = 0; x < image.Width; x++)
                        {
                            for (int y = 0; y < image.Height; y++)
                            {
                                Color pixelColor = image.GetPixel(x, y);
                                values[image.Width * x + y] = Math.Abs(pixelColor.GetBrightness() - 1);
                            }
                        }
                        this.Add(values, item.Value, item.Key);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        public List<(double[], double[], string)> ShuffleData(List<(double[], double[], string)> sequence)
        {
            for (int i = 0; i < sequence.Count; i++)
            {
                int index = rand.Next(i, sequence.Count);
                var temp = sequence[index];
                sequence[index] = sequence[i];
                sequence[i] = temp;
            }
            return sequence;
        }
        public void SplitData(DirectionsDataList data, out List<(double[], double[], string)> trainData, out List<(double[], double[], string)> testData)
        {
            List<(double[], double[], string)> list = new List<(double[], double[], string)>();
            int totalRows = data.Count;
            int trainRows = (int)(totalRows * 0.80);
            int testRows = totalRows - trainRows;

            trainData = new List<(double[], double[], string)>();
            testData = new List<(double[], double[], string)>();

            List<(double[], double[], string)> temp = new List<(double[], double[], string)>();

            foreach (var item in data)
            {
                temp.Add((item.Values, item.LabelsTargets.Item2, item.LabelsTargets.Item1));
            }
            list = ShuffleData(temp);



            for (int i = 0; i < trainRows; i++)
                trainData.Add(list[i]);
            for (int i = 0; i < testRows; i++)
                testData.Add(list[i+trainRows]);
        }

    }
}

using System.Runtime.Serialization.Formatters.Binary;

namespace DeepNN
{
    public class NetworkManager
    {
        private int _numInputParameters { get; set; }
        private int[] _hiddenNodes { get; set; }
        private int _numOutputParameters { get; set; }
        public Network _network { get; set; }
        public DataList _dataSetList;
        public DataList _testDataSetList;
        public string FileName = "";
        public NetworkManager CreateNetwork(int input, int output, params int[] hidden)
        {
            _numInputParameters = input;
            _hiddenNodes = hidden;
            _numOutputParameters = output;

            _network = new Network(_numInputParameters, _hiddenNodes, _numOutputParameters);

            return this;
        }
        public NetworkManager GetTrainingDataFromUser(int trainingSamplesNum)
        {
            _dataSetList = new DataList();
            _dataSetList.EnterSamples(trainingSamplesNum, _numInputParameters, _numOutputParameters);
            return this;
        }
        public NetworkManager TrainNetworkToMinimumError(double error)
        {
            _network.Train(data: _dataSetList, minError: error);
            return this;
        }
        public NetworkManager TrainNetworkOverNumberOfEpochs(int numEpochs)
        {
            _network.Train(data: _dataSetList, epochs: numEpochs);
            return this;
        }
        public NetworkManager SerializeNetwork(string fileName)
        {
            FileName = @$"../../../{fileName}.bin";
            Stream stream = File.Create(FileName);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, _network);
            stream.Close();

            return this;
        }

        public NetworkManager GetTestingDataFromUser(int testingSamplesNum)
        {
            _testDataSetList = new DataList();
            _testDataSetList.EnterSamples(testingSamplesNum, _numInputParameters, null);
            return this;
        }
        public void TestNetworkToMinimum()
        {
            Network network = new Network();
            if (File.Exists(FileName))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                network = (Network)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
                network.Test(data: _testDataSetList);
            }
            else
            {
                Console.WriteLine("Network not found.");
            }
        }
    }
}

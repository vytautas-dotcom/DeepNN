namespace DeepNN
{
    public class Program
    {
        static void Main()
        {
            NetworkManager mgr = new NetworkManager();

            mgr.CreateNetwork(input: 3, output: 1, hidden: new[]{ 3, 2})
                .GetTrainingDataFromUser(trainingSamplesNum: 3)
                .TrainNetworkToMinimumError(error: 0.005)
                .GetTestingDataFromUser(testingSamplesNum: 1)
                .TestNetworkToMinimum();
        }
    }
}
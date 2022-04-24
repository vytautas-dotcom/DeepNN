namespace DeepNN
{
    public class Program
    {
        static void Main()
        {
            NetworkManager mgr = new NetworkManager();

            //mgr.CreateNetwork(input: 3, output: 1, hidden: new[]{ 3, 3})
            //    .GetTrainingDataFromUser(trainingSamplesNum: 3)
            //    .TrainNetworkToMinimumError(error: 0.005)
            //    .SerializeNetwork(fileName: "nn_3i_1o_3_3h_3tS_0005e")
            //    .GetTestingDataFromUser(testingSamplesNum: 1)
            //    .TestNetworkToMinimum();
            mgr.CreateNetwork(input: 784, output: 4, hidden: new[] { 16, 16 })
                .GetTrainingDataFromFile()
                .TrainNetworkToMinimumError_Directions(0.005)
                .SerializeNetwork(fileName: "nn_784i_4o_3_3h_32tS_005e_Directions")
                .TestNetworkToMinimum_Directions();
        }
    }
}
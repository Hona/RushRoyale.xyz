namespace RushRoyale.DataMiner;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await Miner.MineAsync();
            
        Console.WriteLine("Done, press any key to exit...");
        Console.ReadLine();
    }
}
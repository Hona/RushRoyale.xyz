using BenchmarkDotNet.Attributes;

namespace RushRoyale.DataMiner;

public class Benchmark
{
    [Benchmark]
    public async Task DataMine()
    {
        await Miner.MineAsync(true);
    }
}
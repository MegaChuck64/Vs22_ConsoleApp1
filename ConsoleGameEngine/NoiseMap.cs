using SimplexNoise;
namespace ConsoleGameEngine;
public static class NoiseMap
{
    public static float[,] Generate(int length, int width, float scale, int seed = -1)
    {
        Noise.Seed = seed == -1 ? new Random().Next(-10_000, 10_000) : seed;
        return Noise.Calc2D(length, width, scale);
    }
}


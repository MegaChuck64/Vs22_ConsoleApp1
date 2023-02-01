using SimplexNoise;
namespace ConsoleGameEngine;
public static class NoiseMap
{

    public static float[,] Generate(int width, int height, float scale, int offsetX = 0, int offsetY = 0, float zoom = 1f, int seed = -1)
    {
        Noise.Seed = seed == -1 ? new Random().Next(-10_000, 10_000) : seed;
        float[,] array = new float[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                array[i, j] = Noise.CalcPixel2D((int)((i + offsetX)/zoom), (int)((j + offsetY)/zoom), scale);
            }
        }

        return array;
    }
}


using System;

namespace Licenta.API.Helpers
{
    public static class LevenshteinDistance
    {
        public static int Compute(string firstString, string secondString)
        {
            int firstLength = firstString.Length;
            int secondLength = secondString.Length;
            int[,] distance = new int[firstLength + 1, secondLength + 1];

            if (firstLength == 0)
            {
                return secondLength;
            }

            if (secondLength == 0)
            {
                return firstLength;
            }

            for (int i = 1; i <= firstLength; i++)
            {
                for (int j = 1; j <= secondLength; j++)
                {
                    int cost = (secondString[j - 1] == firstString[i - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }
            return distance[firstLength, secondLength];
        }
    }
}

using System;
using System.IO;
using System.Linq;

public class LogConverter
{
    public static void Convert(string inputPath, string outputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        string[] newLines = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split('|');
            string httpMethod = fields[3].Split(' ')[0].TrimStart('"');
            string statusCode = fields[1];
            string uriPath = fields[3].Split(' ')[1];
            string timeTaken = fields[4];
            string responseSize = fields[0];
            string cacheStatus = GetCacheStatus(fields[2]);

            newLines[i] = $"{httpMethod} {statusCode} {uriPath} {timeTaken} {responseSize} {cacheStatus}";
        }

        File.WriteAllLines(outputPath, newLines);
    }

    public static string GetCacheStatus(string status)
    {
        switch (status)
        {
            case "HIT":
                return "HIT";
            case "MISS":
                return "MISS";
            case "INVALIDATE":
                return "REFRESH_HIT";
            default:
                return "UNKNOWN";
        }
    }
}

using System.Text;

string TakeLevel(string origin, int level, int[] levels)
{
    string output = string.Empty;

    foreach (var item in levels) {
        int current = (level * levels.Length - 1) + item;

        if (current < origin.Length)
            output += origin[current];
    }

    return output;
}

string Encrypt(string origin, int[] levels) {
    int maxLevel = levels.Length;
    StringBuilder output = new StringBuilder();
    for (int i = 0; i < Math.Ceiling(origin.Length / (float)levels.Length); i++) {
        output.Append(TakeLevel(origin, i, levels));
    }

    return output.ToString();
}

string Decrypt(string text, int[] levels)
{
    int cols = levels.Length;
    int rows = text.Length / cols;
    if (text.Length % cols != 0) rows += 1;

    var textMatrix = new char[rows, cols];
    int index = 0;

    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols && index < text.Length; j++) {
            int pom = levels[j] - 1;
            textMatrix[i, pom] = text[index];
            index++;
        }
    }

    string output = string.Empty;
    
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            if (textMatrix[i, j] != 0)
                output += textMatrix[i, j];
        }
    }

    return output;
}

// string origin = Console.ReadLine() ?? string.Empty).ToUpper();
string origin = "CRYPTOGRAPHYOSA";

// int[] levels = (Console.ReadLine() ?? "0").Split('-').Select(int.Parse).ToArray();
int[] levels = new[] { 3, 1, 4, 2 };

string output = Encrypt(origin, levels);

Console.WriteLine(output);

Console.WriteLine(Decrypt(output, levels));
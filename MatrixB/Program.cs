using System.Text;

(int initIndex, int index)[] MatchLettersWithIndex(string key)
{
    var temp = key.Select((x, index) => new { T = x, InitOrder = index }).OrderBy(x => x.T)
        .Select((x, index) => new { T = x.T, InitOrder = x.InitOrder, Order = index }).OrderBy(x => x.InitOrder).ToList();

    return temp.Select(x => (x.InitOrder + 1, x.Order + 1)).ToArray();
}

string TakeLevel(string origin, int level, int maxLevel)
{
    string output = string.Empty;

    for (int i = level - 1; i < origin.Length; i += maxLevel) {
        if (i >= origin.Length)
            break;

        output += origin[i];
    }

    return output;
}

string Encrypt(string origin, (int initIndex, int index)[] levels)
{
    int maxLevel = levels.Length;

    var dict = new Dictionary<int, string>();

    for (int i = 0; i < levels.Length; i++) {
        dict.Add(levels[i].index, TakeLevel(origin, levels[i].initIndex, levels.Length));
    }

    StringBuilder output = new StringBuilder();

    foreach (var value in dict.OrderBy(x => x.Key)) {
        output.Append(value.Value);
    }

    return output.ToString();
}

// string origin = (Console.ReadLine() ?? string.Empty).ToUpper();
// string key = Console.ReadLine() ?? "A";

string origin = "FRANCJA";
string key = "BCADA";
var levels = MatchLettersWithIndex(key);

string output = Encrypt(origin, levels);

Console.Clear();
Console.WriteLine(origin);
Console.WriteLine(key);

Console.WriteLine(output);

Console.WriteLine(Decrypt(output, levels));

string Decrypt(string origin, (int initIndex, int index)[] levels)
{
    int minHeight = origin.Length / levels.Length;
    int maxHeight = origin.Length % levels.Length > 0 ? minHeight + 1 : minHeight;

    var tokens = new string[levels.Length];
    
    int lastSlice = 0;

    foreach (var item in levels.OrderBy(x => x.index)) {
        tokens[item.initIndex - 1] = origin.Substring(lastSlice, item.initIndex <= origin.Length % levels.Length ? maxHeight : minHeight);

        lastSlice += item.initIndex <= origin.Length % levels.Length ? maxHeight : minHeight;
    }

    int index = 0;
    string output = string.Empty;
    
    for (int i = 0; i < origin.Length; i++) {
        output += tokens[i % tokens.Length][index];
        if ((i + 1) % levels.Length == 0)
            index++;
    }

    return output;
}
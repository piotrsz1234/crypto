using System.Text;

string TakeLevel(string origin, int level, int maxLevels)
{
    string output = String.Empty;
    int direction = 1;
    int current = 1;

    for (int i = 0; i < origin.Length; i++) {
        if (current == level)
            output += origin[i];

        if ((current - 1) % (maxLevels - 1) == 0 && i > 0)
            direction *= -1;

        current += direction;
    }

    return output;
}

string Encrypt(string origin, int level)
{
    var builder = new StringBuilder();

    for (int i = 0; i < level; i++) {
        builder.Append(TakeLevel(origin, i + 1, level));
    }

    return builder.ToString();
}

string Decrypt(string origin, int levels)
{
    char[,] rail = new char[levels, origin.Length];
 
    // filling the rail matrix to distinguish filled
    // spaces from blank ones
    for (int i = 0; i < levels; i++)
        for (int j = 0; j < origin.Length; j++)
            rail[i, j] = '\n';
 
    // to find the direction
    bool dirDown = true;
    int row = 0, col = 0;
 
    // mark the places with '*'
    for (int i = 0; i < origin.Length; i++) {
        // check the direction of flow
        if (row == 0)
            dirDown = true;
        if (row == levels - 1)
            dirDown = false;
 
        // place the marker
        rail[row, col++] = '*';
 
        // find the next row using direction flag
        if (dirDown)
            row++;
        else
            row--;
    }
 
    // now we can construct the fill the rail matrix
    int index = 0;
    for (int i = 0; i < levels; i++)
        for (int j = 0; j < origin.Length; j++)
            if (rail[i, j] == '*' && index < origin.Length)
                rail[i, j] = origin[index++];
 
    // create the result string
    string result = "";
    row = 0;
    col = 0;
 
    // iterate through the rail matrix
    for (int i = 0; i < origin.Length; i++) {
        // check the direction of flow
        if (row == 0)
            dirDown = true;
        if (row == levels - 1)
            dirDown = false;
 
        // place the marker
        if (rail[row, col] != '*')
            result += rail[row, col++];
 
        // find the next row using direction flag
        if (dirDown)
            row++;
        else
            row--;
    }
    return result;
}

// string origin = (Console.ReadLine() ?? string.Empty).ToUpper();
string origin = "FRANCJA";

// int level = int.Parse(Console.ReadLine() ?? "0");
int level = 3;

string output = Encrypt(origin, level);

Console.WriteLine(output);

Console.WriteLine(Decrypt(output, level));
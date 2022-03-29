using System.Text;

internal class Engine
{
    private String solution;
    private List<String> words;
    private List<String> guesses;

    private static readonly char HIT = 'O';
    private static readonly char MISS = 'Z';
    private static readonly char MISSING = 'X';

    private Random random;

    public Engine(String solution)
    {
        this.solution = solution.ToLower();
        this.words = File.ReadAllLines("dictionary.txt").ToList();
        this.guesses = new List<string>();
        random = new Random();
    }

    public String Execute()
    {
        guesses.Add("salet".ToLower());

        for (int i = 0; i < 6; i++)
        {
            if (guesses.Last().Equals(solution))
            {
                return GenerateOutput();
            }
            GenerateGuess();
        }
        return GenerateOutput();
    }

    private void GenerateGuess()
    {
        String guess = words[random.Next(words.Count)];
        guesses.Add(guess);
    }

    private String GenerateOutput()
    {
        String attempts = (guesses.Last().Equals(solution)) ? (guesses.Count.ToString()) : "X";
        String header = String.Format("Wordle 001 {0}/6", attempts);
        StringBuilder outputStringBuilder = new StringBuilder(header);

        outputStringBuilder.AppendLine(header);

        for (int i = 0; i < guesses.Count; i++)
        {
            outputStringBuilder.AppendLine(GenerateLine(guesses[i]));
        }

        return outputStringBuilder.ToString();
    }

    private String GenerateLine(String line)
    {
        char[] result = new char[5];

        char[] guess = line.ToCharArray();
        char[] target = solution.ToCharArray();

        for (int i = 0; i < target.Length; i++)
        {
            if(guess[i] == target[i])
            {
                result[i] = HIT;
                continue;
            } 
            if (target.Contains(guess[i]))
            {
                result[i] = MISS;
                continue;
            }
            result[i] = MISSING;
        }

        return new string(result);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dingo
{
    internal class DingoService
    {
        private String solution;
        private List<String> words;
        private List<String> guesses;

        private static readonly char HIT = 'O';
        private static readonly char MISS = 'Z';
        private static readonly char UNKNOWN = 'X';

        private GuessService guessService;

        public DingoService(String solution)
        {
            this.solution = solution.ToLower();
            this.words = File.ReadAllLines("dictionary.txt").ToList();
            this.guesses = new List<string>();
            guessService = new GuessService(ref words, ref guesses);
        }

        public String Execute()
        {
            guesses.Add("salet".ToLower());

            for (int i = 0; i < 5; i++)
            {
                if (guesses.Last().Equals(solution))
                {
                    return GenerateOutput();
                }
                NextGuess();
            }
            return GenerateOutput();
        }

        private void NextGuess()
        {
            words.Remove(guesses.Last());

            // build state
            GuessService.State[] results = new GuessService.State[5];
            results[0] = calculateState(solution[0], guesses.Last()[0]);
            results[1] = calculateState(solution[1], guesses.Last()[1]);
            results[2] = calculateState(solution[2], guesses.Last()[2]);
            results[3] = calculateState(solution[3], guesses.Last()[3]);
            results[4] = calculateState(solution[4], guesses.Last()[4]);

            string nextGuess = guessService.Next(results);

            guesses.Add(nextGuess);
        }

        private GuessService.State calculateState(char expected, char actual)
        { 
            if (actual == expected)
            {
                return GuessService.State.Known;
            }
            if (solution.Contains(actual))
            {
                return GuessService.State.Identified;
            }
            return GuessService.State.Unknown;
        }

        private String GenerateOutput()
        {
            String attempts = (guesses.Last().Equals(solution)) ? (guesses.Count.ToString()) : "X";
            String header = String.Format("Wordle 001 {0}/6", attempts);
            StringBuilder outputStringBuilder = new StringBuilder();

            outputStringBuilder.AppendLine(header);

            for (int i = 0; i < guesses.Count; i++)
            {
                outputStringBuilder.Append(GenerateLine(guesses[i]));
                outputStringBuilder.Append(guesses[i].ToString() + "\n");
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
                if (guess[i] == target[i])
                {
                    result[i] = HIT;
                    continue;
                }
                if (target.Contains(guess[i]))
                {
                    result[i] = MISS;
                    continue;
                }
                result[i] = UNKNOWN;
            }

            return new string(result);
        }
    }
}

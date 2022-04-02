using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dingo
{
    internal class GuessService
    {
        private List<string> words;
        private List<char> discountedLetters;
        private HashSet<char> knownLetters;
        private char[] knownPlacements;
        private List<String> guesses;

        Random r = new Random();

        public enum State { Known, Identified, Unknown }

        public GuessService(ref List<String> words, ref List<string> guesses)
        {
            this.words = words;
            this.guesses = guesses;
            this.discountedLetters = new List<char>();
            this.knownLetters = new HashSet<char>();
            this.knownPlacements = new char[5] {'#', '#', '#', '#', '#'};
        }

        public string Next(State[] results)
        {

            // data collection
            for (int i = 0; i < results.Length; i++)
            {
                switch (results[i])
                {
                    case State.Known:
                        knownPlacements[i] = guesses.Last()[i];
                        break;
                    case State.Identified:
                        knownLetters.Add(guesses.Last()[i]);
                        break;
                    case State.Unknown:
                        discountedLetters.Add(guesses.Last()[i]);
                        break;
                }
            }

            // data correction
            HashSet<String> wordsToRemove = new HashSet<string>();

            for(int i = 0; i < words.Count; i++)
            {
                for(int j = 0; j < discountedLetters.Count; j++)
                {
                    if (words[i].Contains<char>(discountedLetters[j]))
                    {
                        wordsToRemove.Add(words[i]);
                    }
                }
            }
            
            foreach(string word in words)
            {
                foreach (char c in knownLetters)
                {
                    if (knownLetters.Count > 0 && !word.Contains<char>(c))
                    {
                        wordsToRemove.Add(word);
                    }
                }
            }

            foreach(string word in words)
            {
                for(int i=0; i<5; i++)
                {
                    if ((!knownPlacements[i].Equals('#')) && (knownPlacements[i] != word[i]))
                    {
                        wordsToRemove.Add(word);
                    }
                }
            }

            words.RemoveAll(word => wordsToRemove.Contains(word));

            return words[r.Next(words.Count)];
        }
    }
}

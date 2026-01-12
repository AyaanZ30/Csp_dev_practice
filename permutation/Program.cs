using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Xsl;

namespace Variables
{
    class TypecastDemo
    {
        static readonly Random rng = new Random();
        public static void Run(string str)
        {
            List<char> letters = new List<char>();

            foreach(var ch in str.ToLower())
            {
                if(char.IsLetter(ch))
                {
                    letters.Add(ch);
                }
            }

            BigInteger count = GetPermutationsCount(letters);
            Shuffle(letters);

            string jumbled = new string(letters.ToArray());

            System.Console.WriteLine($"Jumbled word : {jumbled} (TOTAL : {count} permutations possible)");
        }

        static void Shuffle<T>(List<T> list)
        {
            for(int i = list.Count - 1 ; i >= 0 ; i--)
            {
                int j = rng.Next(i+1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        static BigInteger GetPermutationsCount(List<char> letters)
        {
            BigInteger numerator = getFactorial(letters.Count);
            Dictionary<char, int> charFreq = getCharFrequencies(letters);
            BigInteger denominator = 1;

            foreach(int c in charFreq.Values)
            {
                denominator *= getFactorial(c);
            }

            return (numerator / denominator);
        }

        static BigInteger getFactorial(BigInteger len)
        {
            if(len <= 1) return 1;
            return (len * getFactorial(len-1));
        }

        static Dictionary<char, int> getCharFrequencies(IEnumerable<char> letters)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach(var ch in letters)
            {
                if(freq.ContainsKey(ch)) freq[ch]++;
                else freq[ch] = 1;
            }
            return freq;
        }
    };

    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Enter a word : ");
            string word = Console.ReadLine();
            
            System.Console.WriteLine($"Original word : {word.ToLower()}");

            TypecastDemo.Run(word);
        }
    }
}
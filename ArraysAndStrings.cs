namespace Chapter03
{
    public class ArraysAndStrings
    {
        // 1. Copying an array manually using a loop
        public static void CopyArray()
        {
            int[] original = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] copy = new int[original.Length];

            for (int i = 0; i < original.Length; i++)
            {
                copy[i] = original[i];
            }

            Console.WriteLine("Original: " + string.Join(", ", original));
            Console.WriteLine("Copy:     " + string.Join(", ", copy));
        }

        // 2. Simple list manager with add, remove, and clear
        public static void ListManager()
        {
            var list = new List<string>();

            while (true)
            {
                Console.WriteLine("Enter command (+ item, - item, or -- to clear):");
                string input = Console.ReadLine();

                if (input == "--") list.Clear();
                else if (input.StartsWith("+ ")) list.Add(input.Substring(2));
                else if (input.StartsWith("- ")) list.Remove(input.Substring(2));

                Console.WriteLine("Current list: " + string.Join(", ", list));
            }
        }

        // 3. Find all prime numbers in a given range
        public static int[] FindPrimesInRange(int start, int end)
        {
            List<int> primes = new();
            for (int i = Math.Max(2, start); i <= end; i++)
            {
                bool isPrime = true;
                for (int j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) primes.Add(i);
            }
            return primes.ToArray();
        }

        // 4. Rotate array k times and sum the result
        public static void RotateAndSum()
        {
            int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int k = int.Parse(Console.ReadLine());
            int n = array.Length;
            int[] sum = new int[n];

            for (int r = 1; r <= k; r++)
            {
                int[] rotated = new int[n];
                for (int i = 0; i < n; i++)
                {
                    rotated[(i + r) % n] = array[i];
                }
                for (int i = 0; i < n; i++)
                {
                    sum[i] += rotated[i];
                }
            }
            Console.WriteLine(string.Join(" ", sum));
        }

        // 5. Find longest sequence of equal elements
        public static void LongestEqualSequence(int[] nums)
        {
            int maxLen = 1, currLen = 1, bestStart = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1]) currLen++;
                else currLen = 1;

                if (currLen > maxLen)
                {
                    maxLen = currLen;
                    bestStart = i - maxLen + 1;
                }
            }
            Console.WriteLine(string.Join(" ", nums.Skip(bestStart).Take(maxLen)));
        }

        // 6. Find the most frequent number (leftmost if tie)
        public static void MostFrequentNumber(int[] nums)
        {
            var freq = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (!freq.ContainsKey(num)) freq[num] = 0;
                freq[num]++;
            }
            int maxCount = freq.Values.Max();
            int result = nums.First(x => freq[x] == maxCount);

            Console.WriteLine($"The number {result} is the most frequent (occurs {maxCount} times)");
        }

        // 7. Reverse string: Method 1 - Using char array
        public static string ReverseWithCharArray(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        // 7. Reverse string: Method 2 - Using loop
        public static string ReverseWithLoop(string input)
        {
            string reversed = "";
            for (int i = input.Length - 1; i >= 0; i--)
                reversed += input[i];
            return reversed;
        }

        // 8. Reverse words in a sentence, keep punctuation
        public static void ReverseWordsInSentence(string sentence)
        {
            char[] separators = ".,:;=()&[]\"'\\/!? ".ToCharArray();
            var words = new List<string>();
            var tokens = new List<string>();

            int index = 0;
            while (index < sentence.Length)
            {
                if (separators.Contains(sentence[index]))
                {
                    tokens.Add(sentence[index].ToString());
                    index++;
                }
                else
                {
                    int start = index;
                    while (index < sentence.Length && !separators.Contains(sentence[index]))
                        index++;
                    words.Add(sentence.Substring(start, index - start));
                    tokens.Add("#");
                }
            }

            words.Reverse();
            int wordIndex = 0;
            foreach (var token in tokens)
            {
                if (token == "#") Console.Write(words[wordIndex++]);
                else Console.Write(token);
            }
            Console.WriteLine();
        }

        // 9. Extract palindromes from text
        public static void ExtractPalindromes(string input)
        {
            var words = input.Split(new[] { ' ', ',', '.', '?', '!', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var palindromes = new HashSet<string>();
            foreach (var word in words)
            {
                string lower = word.ToLower();
                if (lower.Length > 1 && lower.SequenceEqual(lower.Reverse()))
                    palindromes.Add(word);
            }
            var sorted = palindromes.OrderBy(x => x).ToArray();
            Console.WriteLine(string.Join(", ", sorted));
        }

        // 10. Parse URL
        public static void ParseUrl(string url)
        {
            string protocol = "", server = "", resource = "";

            if (url.Contains("://"))
            {
                var parts = url.Split(new[] { "://" }, 2, StringSplitOptions.None);
                protocol = parts[0];
                url = parts[1];
            }

            int slashIndex = url.IndexOf("/");
            if (slashIndex >= 0)
            {
                server = url.Substring(0, slashIndex);
                resource = url.Substring(slashIndex + 1);
            }
            else
            {
                server = url;
            }

            Console.WriteLine($"[protocol] = \"{protocol}\"");
            Console.WriteLine($"[server] = \"{server}\"");
            Console.WriteLine($"[resource] = \"{resource}\"");
        }
    }
}

using System;
using System.Collections.Generic;

namespace Longest_Valid_Parentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LongestValidParentheses("))))())()()(()"));
        }

        public static int LongestValidParentheses(string s)
        {
            if (s.Length <= 1) return 0;

            int half = s.Length / 2;
            string left = s.Substring(0, half);
            string right = s.Substring(half, s.Length - half);

            int answer = Math.Max(LongestValidParentheses(left), LongestValidParentheses(right));

            int[] longestIncompletePrefix = LongestIncompletePrefixes(right.ToCharArray(), ')');
            char[] leftArr = left.ToCharArray();
            Array.Reverse(leftArr);
            int[] longestIncompleteSuffix = LongestIncompletePrefixes(leftArr, '(');

            int minLength = Math.Min(longestIncompletePrefix.Length, longestIncompleteSuffix.Length);
            for (int i = 0; i < minLength; ++i)
            {
                if (longestIncompleteSuffix[i] != 0 && longestIncompletePrefix[i] != 0)
                    answer = Math.Max(answer, longestIncompleteSuffix[i] + longestIncompletePrefix[i]);
            }
            return answer;
        }
        public static int[] LongestIncompletePrefixes(char[] s, char spare)
        {
            int[] results = new int[s.Length + 1];
            int sum = 0;
            int numberOfSpareElements = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                sum += s[i] == spare ? -1 : +1;
                if (sum < 0)
                {
                    ++numberOfSpareElements;
                    sum = 0;
                }
                if (sum == 0) results[numberOfSpareElements] = i + 1;
            }
            return results;
        }
    }
}


public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int count = 0;
        int max = 0;

        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
            {
                count++;
            }
        }

        max = count;

        for (int i = k; i < s.Length; i++)
        {
            if (IsVowel(s[i]))
            {
                count++;
            }

            if (IsVowel(s[i - k]))
            {
                count--;
            }

            if (count > max)
            {
                max = count;
            }
        }

        return max;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' ||
               c == 'e' ||
               c == 'i' ||
               c == 'o' ||
               c == 'u';
    }
}
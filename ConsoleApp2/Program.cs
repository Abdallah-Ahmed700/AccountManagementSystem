namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();

            try
            {
                CheckForVowels(input);
                Console.WriteLine("The string contains vowels.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        static void CheckForVowels(string text)
        {
            string vowels = "aeiouAEIOU";
            for (int i = 0; i < text.Length; i++)
            {
                if (vowels.Contains(text[i]))
                {
                    return; // Vowel found, exit method
                }
            }

            throw new Exception("The string does not contain any vowels.");

        }
    }
}

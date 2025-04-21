namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();

            char askAddNumber;
            bool repeatNumber = true; 
            do
            {
                Console.Write("Enter Numper : ");
                int num = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < numbers.Count; i++)  
                {
                    if (numbers[i] == num)
                    {
                        Console.WriteLine("\n>>> The number exists. Enter another number. <<<\n");
                        repeatNumber = false;
                        break;
                    }
                }

                if (repeatNumber == true)
                {
                    numbers.Add(num);
                    Console.WriteLine($"\n>>> Number Is Add {numbers[^1]} <<<\n");
                    Console.Write(">>> [");
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        Console.Write($" {numbers[i]} ");
                    }
                    Console.Write("] <<<\n");
                    numbers.TrimExcess();
                }

                
                Console.Write("\nWould you like to enter another number  ? ( y  or  n ) : ");
                askAddNumber = Convert.ToChar(Console.ReadLine().ToUpper());

            } while (askAddNumber == 'Y');

        }
    }
}

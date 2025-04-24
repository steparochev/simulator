using System;
public class N
{
    public static void Main(string[] a)
    {
        Console.Write("Число 1-5: ");
        if (int.TryParse(Console.ReadLine(), out int n) && n >= 1 && n <= 5)
            Console.WriteLine(n switch
            {
                1 => "один",
                2 => "два",
                3 => "три",
                4 => "четыре",
                5 => "пять",
            });
    }

}

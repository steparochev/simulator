using System;
public class S
{
    public static void Main(string[] a)
    {
        Console.WriteLine("Ввод (стоп - выход):");
        while (true)
        {
            string i = Console.ReadLine()!;
            if (i.ToLower() == "стоп")
            {
                Console.WriteLine("Завершено.");
                break;
            }
            Console.WriteLine("Ввели: " + i);
        }
    }
}
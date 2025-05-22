using System;
public class M
{
    public static void Main(string[] a)
    {
        Random r = new();

        IEnumerable<int> numbers = Enumerable.Range(0, 11);

      
        int[] nums = numbers.Select(i => r.Next(1, 101)).ToArray();

    
        Console.WriteLine("Случайные числа: " + string.Join(", ", nums));

        
        Console.WriteLine("Макс: " + nums.Max());
    }
}
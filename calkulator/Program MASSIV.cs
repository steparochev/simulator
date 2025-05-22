using System;

public class MASSIV 
{
    public static void Main(string[] args)
    {

        int[] numbers = new int[100];


        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1;
        }
        Console.WriteLine("Массив заполнен числами от 1 до 100:");
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i] + " ");
        }
     
    }
}
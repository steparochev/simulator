using System;

public class MASSIV 
{
    public static void Main(string[] args)
    {
        // Создаем массив целых чисел размером 100
        int[] numbers = new int[100];

        // Заполняем массив числами от 1 до 100
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1; // Важно: массив начинается с 0, а нам нужно с 1
        }

        // Выводим массив в консоль (можно убрать, если не нужно)
        Console.WriteLine("Массив заполнен числами от 1 до 100:");
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i] + " ");
        }
     
    }
}
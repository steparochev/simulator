using System;

public class Q
{
    public static void Main(string[] args)
    {
        int[,,] mas = {
            { { 1, 2 }, { 3, 4 } },
            { { 4, 5 }, { 6, 7 } },
            { { 7, 8 }, { 9, 10 } },
            { { 10, 11 }, { 12, 13 } }
        };

        Console.Write("{");

        
        for (int i = 0; i < 4; i++) 
        {
            Console.Write("{");
            for (int j = 0; j < 2; j++) 
            {
                Console.Write("{" + mas[i, j, 0] + " , " + mas[i, j, 1] + "}"); 

                if (j == 0) 
                {
                    Console.Write(" , ");
                }
            }
            if (i < 3)
            {
                Console.Write("}");
            }
            Console.Write("}");
        }
    }
}
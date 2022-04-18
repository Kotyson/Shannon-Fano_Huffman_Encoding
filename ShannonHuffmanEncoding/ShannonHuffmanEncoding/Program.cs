using System;
using System.Collections.Generic;

namespace ShannonHuffmanEncoding
{
    struct Node
    {
        public string codeNum;
        public double probability;
        public char sourceChar;

        public Node(string codeNum, double probability, char sourceChar)
        {
            this.codeNum = codeNum;
            this.probability = probability;
            this.sourceChar = sourceChar;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Node> nodes = new List<Node>();
            int numberOfNodes = 0;
            Console.Write("How many characters do you want in input word?: ");
            try
            {
                numberOfNodes = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Input is not number value!!! {e.Message}");
                return;
            }
            
            Console.WriteLine(numberOfNodes);
            for (int i = 0; i < numberOfNodes; i++)
            {
                double probability = 0;
                char sourceChar = 'A';
                Console.Write($"\nWhat is the {i + 1}. source char?: ");
                sourceChar = Convert.ToChar(Console.ReadLine().Substring(0,1));
                Console.Write("\nWhat is it's probability?: ");
                probability = Convert.ToDouble(Console.ReadLine());
                nodes.Add(new Node("", probability, sourceChar));
            }

            foreach (Node item in nodes)
            {
                Console.WriteLine($"{item.sourceChar} p: {item.probability}");
            }

            nodes.Sort((a, b) => b.probability.CompareTo(a.probability)); // sort big to low

        }

        static void Shannon_Fano(Node[] nodes)
        {

        }
    }
}

using System;
using System.Collections.Generic;

namespace ShannonHuffmanEncoding
{
    class Node
    {
        public string codeWord { get; set; }
        public double probability;
        public char sourceChar;

        public Node(string codeWord, double probability, char sourceChar)
        {
            this.codeWord = codeWord;
            this.probability = probability;
            this.sourceChar = sourceChar;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> alphabet = new Dictionary<char, int>();
            List<Node> nodes = new List<Node>();
            string inputWord;

            Console.Write("Write your input word: ");
            inputWord = Console.ReadLine();
            for (int i = 0; i < inputWord.Length; i++)
            {
                char sourceChar = inputWord[i];
                if (alphabet.ContainsKey(sourceChar))
                {
                    alphabet[sourceChar] += 1;
                }
                else
                {
                    alphabet.Add(sourceChar, 1);
                }
            }
            foreach (KeyValuePair<char, int> kvp in alphabet)
            {
                Console.WriteLine($"Key: {kvp.Key} Value: {kvp.Value}");
                double probability = (double)kvp.Value / (double)inputWord.Length;
                nodes.Add(new Node("", probability, kvp.Key));
            }
            foreach (Node item in nodes)
            {
                Console.WriteLine($"{item.sourceChar} p: {item.probability}");
            }
            //int numberOfNodes = 0;
            //Console.Write("How many characters do you want in input word?: ");
            //try
            //{
            //    numberOfNodes = Convert.ToInt32(Console.ReadLine());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Input is not number value!!! {e.Message}");
            //    return;
            //}

            //Console.WriteLine(numberOfNodes);
            //for (int i = 0; i < numberOfNodes; i++)
            //{
            //    double probability = 0;
            //    char sourceChar = 'A';
            //    Console.Write($"\nWhat is the {i + 1}. source char?: ");
            //    sourceChar = Convert.ToChar(Console.ReadLine().Substring(0, 1));
            //    Console.Write("\nWhat is it's probability?: ");
            //    probability = Convert.ToDouble(Console.ReadLine());
            //    nodes.Add(new Node("", probability, sourceChar));
            //}
            //nodes.Add(new Node("", 0.3, 'A'));
            //nodes.Add(new Node("", 0.25, 'B'));
            //nodes.Add(new Node("", 0.2, 'C'));
            //nodes.Add(new Node("", 0.12, 'D'));
            //nodes.Add(new Node("", 0.08, 'E'));
            //nodes.Add(new Node("", 0.05, 'F'));

            double prob = 0;
            foreach (Node item in nodes)
                prob += item.probability;

            //if (prob !>= 1)
            //    throw new Exception("Probability must be equal to 1");

            nodes.Sort((a, b) => b.probability.CompareTo(a.probability)); // sort big to low
            Shannon_Fano(nodes);
            FullDraw(nodes);
        }

        static void Shannon_Fano(List<Node> nodes)
        {
            if (nodes.Count == 1)
                return;

            double prob_first_half = 0;
            double min_dif = 0;
            int split_index = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                prob_first_half += nodes[i].probability;
                double prob_second_half = 0;
                for (int j = i+1; j < nodes.Count; j++)
                {
                    prob_second_half += nodes[j].probability;
                }
                min_dif = i == 0 ? Math.Abs(prob_first_half - prob_second_half) : min_dif;

                if (Math.Abs(prob_first_half - prob_second_half) < min_dif)
                {
                    min_dif = Math.Abs(prob_first_half - prob_second_half);
                }
                else if (Math.Abs(prob_first_half - prob_second_half) > min_dif)
                {
                    split_index = i-1;
                    break;
                }
            }

            List<Node> first_half = new List<Node>();
            for (int i = 0; i < split_index + 1; i++)
            {
                nodes[i].codeWord += "1";
                first_half.Add(nodes[i]);
            }

            List<Node> second_half = new List<Node>();
            for (int i = split_index + 1; i < nodes.Count; i++)
            {
                nodes[i].codeWord += "0";
                second_half.Add(nodes[i]);
            }
            Console.WriteLine("--------------------------------------------");
            foreach (Node item in first_half)
            {
                Console.WriteLine($"{item.sourceChar} p: {item.probability} b: {item.codeWord}");
            }

            foreach (Node item in second_half)
            {
                Console.WriteLine($"{item.sourceChar} p: {item.probability} b: {item.codeWord}");
            }
            Console.WriteLine("--------------------------------------------");
            Shannon_Fano(first_half);
            Shannon_Fano(second_half);
        }

        static double AverageLengthOfCodeWord(List<Node> nodes)
        {
            double averageLength = 0;
            foreach (var item in nodes)
                averageLength += item.probability * item.codeWord.Length;
            return averageLength;
        }

        static double Effectivity(List<Node> nodes)
        {
            double H = 0;
            foreach (var item in nodes)
                H += item.probability * Math.Log2(item.probability);
            double n = (-H / AverageLengthOfCodeWord(nodes)) * 100;
            return n;
        }

        static void FullDraw(List<Node> nodes)
        {
            Console.WriteLine("{0,-10} {1, 8:N1} {2, 10}", "Input word", "Probability", "Encoding");
            foreach (var item in nodes)
            {
                Console.WriteLine("{0,-10} {1, 8:N3} {2, 10}", item.sourceChar, item.probability, item.codeWord);
            }
            Console.WriteLine($"AverageLength: {AverageLengthOfCodeWord(nodes)}");
            Console.WriteLine($"Effectivity: {Effectivity(nodes)} %");
            
        }
    }
}

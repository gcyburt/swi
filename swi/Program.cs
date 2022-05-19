using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace swi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "input.json";
            string jsonString = File.ReadAllText(fileName);

            //initalizing dict with deserializes json data from input.json
            Dictionary<string, Dictionary<string, dynamic>> equationsDicts = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, dynamic>>>(jsonString);

            //dict for results, sorting will be easier
            Dictionary<string, double> notSortedResult = new Dictionary<string, double>();

            foreach (KeyValuePair<string, Dictionary<string, dynamic>> obj in equationsDicts)
            {
                List<dynamic> operatorAndOperands = new List<dynamic>(3);
                string objectName = obj.Key;
                foreach (KeyValuePair<string, dynamic> pair in obj.Value)
                {
                    operatorAndOperands.Add(pair.Value);
                }
                string operation = operatorAndOperands[0].ToString();
                switch (operation)
                {
                    case "add":
                        try
                        {
                            dynamic res = operatorAndOperands[1].GetDouble() + operatorAndOperands[2].GetDouble();
                            notSortedResult.Add(objectName, res);
                        }
                        catch (ArithmeticException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "sub":
                        try
                        {
                            dynamic res = operatorAndOperands[1].GetDouble() - operatorAndOperands[2].GetDouble();
                            notSortedResult.Add(objectName, res);
                        }
                        catch (ArithmeticException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "mul":
                        try
                        {
                            dynamic res = operatorAndOperands[1].GetDouble() * operatorAndOperands[2].GetDouble();
                            notSortedResult.Add(objectName, res);
                        }
                        catch (ArithmeticException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case "sqrt":
                        try
                        {
                            dynamic res = Math.Sqrt(operatorAndOperands[1].GetDouble());
                            notSortedResult.Add(objectName, res);
                        }
                        catch (ArithmeticException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid operation name!");
                        break;
                }
            }
            IOrderedEnumerable<KeyValuePair<string, double>> sortedResult = notSortedResult.OrderBy(kvp => kvp.Value);

            List<string> stringToFile = new List<string>();
            foreach (KeyValuePair<string, double> el in sortedResult)
            {
                Console.WriteLine(el.Key + ": " + el.Value);
                stringToFile.Add(el.Key + ": " + el.Value);
            }
            File.WriteAllLines("output.txt", stringToFile);
            Console.ReadLine();
        }

    }
}

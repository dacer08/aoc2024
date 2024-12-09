//using System;
//using System.Data;
//using System.Text;

//namespace AdventOfCode;

//public class D07
//{
//    private int Count = 0;

//    //private static readonly Func<int, int, int> Add = (x, y) => x + y;

//    //private static readonly Func<int, int, int> Multiply = (x, y) => x * y;

//    private List<string> GetOperations(int count, List<string> numbers)
//    {
//        var operations = new List<string>();
//        double len = Math.Pow(2, count);
//        for (int i = 0; i <= len - 1; i++)
//        {
//            var builder = new StringBuilder();
//            string str = Convert.ToString(i, 2).PadLeft(count, '0');
//            builder.Append(numbers[0]);
//            for (int j = 0; j < str.Length; j++)
//            {
//                if (str[j] == '0')
//                {
//                    builder.Append('+');
//                }
//                else
//                {
//                    builder.Append('*');
//                }
//                builder.Append(numbers[j + 1]);
//            }
//            operations.Add(builder.ToString());
//        }

//        return operations;
//    }


//    private void Explore(int result, List<string> numbers)
//    {
//        var operations = GetOperations(numbers.Count - 1, numbers);
//        foreach (var operation in operations)
//        {
//            var current = (int)(new DataTable().Compute(operation, null));
//            if (current == result)
//            {
//                Count += result;
//                return;
//            }
//        }
//    }

//    public void Run()
//    {

//        for (var i = 0; i < D07Data.Results.Count; i++)
//        {
//            Explore(D07Data.Results[i], D07Data.Numbers[i]);
//        }
//        Console.WriteLine(Count);
//    }
//}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StackTest
{
    class Program
    {

        //题目：
        //输入两个整数序列，第一个序列表示栈的压入顺序，请判断第二个序列是否可能为该栈的弹出顺序。假设压入栈的所有数字均不相等。例如序列1,2,3,4,5是某栈的压入顺序，序列4,5,3,2,1是该压栈序列对应的一个弹出序列，但4,3,5,1,2就不可能是该压栈序列的弹出序列。（注意：这两个序列的长度是相等的）

        //解题思路
        //1.当栈的长度很大时，一次性全部压进去，再出栈：5 4 3 2 1
        //2.当栈的长度小于序列长度，比如 1 2 3 4都顺利入栈，但是满了，那么要先出栈一个，才能入栈，那么就是先出4，然后压入5，随后再全部出栈：4 5 3 2 1

        static void Main(string[] args)
        {

            Console.WriteLine("请输入入栈序列【注:请切换至英文输入法】，Eg：1,2,3");
            List<int> push = ConvertStringToIntList(Console.ReadLine());

            Console.WriteLine("请输入目标出栈序列，Eg：1,2,3");
            List<int> pop = ConvertStringToIntList(Console.ReadLine());

            if (push.Count < 1 || pop.Count < 1 || pop.Count != push.Count)
            {
                Console.WriteLine("当前目标序列是否为对应出栈序列: 不是");
            }

            string pStr = string.Join(",", push);
            string popStr = string.Join(",", pop);
            Console.WriteLine($"当前入栈序列：{pStr} \n指定出栈序列： {popStr}");

            int stackLength = 0;
            var ret = IsPopOrder(push, pop, out stackLength);
            string retStr = ret ? "是" : "不是";

            Console.WriteLine($"当前目标序列是否为对应出栈序列：{retStr}，且栈长度为 {stackLength};");
            Console.ReadKey();
        }

        public static bool IsPopOrder(List<int> push, List<int> pop, out int stackLength)
        {
            int l = push.Count;
            bool ret = false;
            stackLength = 0;
            Stack s = new Stack();

            if (push != null && push.Count > 0 && pop != null && pop.Count == pop.Count)
            {
                int p = 0;
                var value = pop[p];
                for (int i = 0; i < l; i++)
                {
                    if (push[i] != value)
                    {
                        s.Push(push[i]);
                        Console.WriteLine($"入栈： {push[i]}");
                    }
                    else
                    {
                        Console.WriteLine($"入栈： {push[i]}\n");
                        Console.WriteLine($"出栈： {push[i]}");

                        stackLength = s.Count + 1;

                        ++p;
                        var curValue = pop[p];
                        while (s.Count > 0 && (int)s.Peek() == curValue)
                        {
                            //栈顶元素与出站序列当前值相等
                            Console.WriteLine($"出栈： {s.Peek()}");
                            s.Pop();

                            ++p;
                            if (p <= l - 1)
                                curValue = pop[p];
                        }
                        value = curValue;

                    }
                }
                if (s.Count < 1)
                    ret = true;
            }
            return ret;
        }

        private static List<int> ConvertStringToIntList(string s)
        {
            List<int> ret = new List<int>();
            if (!string.IsNullOrEmpty(s))
            {
                var temp = s.Split(',').ToList();
                temp.ForEach(t =>
                {
                    int i;
                    if (!string.IsNullOrEmpty(t) && int.TryParse(t, out i))
                    {
                        ret.Add(i);
                    }
                });
            }
            return ret;
        }
    }
}

using System.Collections.Generic;
using System;

namespace DotNetUtilities
{
    public class Combinations
    {
            public static List<List<string>> Combo1(List<string> set, List<string> result)
            {
                if (result.Count == set.Count)
                {
                    return new List<List<string>> { result };
                }
                else
                {                
                    List<List<string>> accumulator = new List<List<string>>();

                    foreach(string member in set)
                    {
                        List<string> temp = new List<string>(result);
                        temp.Add(member);
                        accumulator.AddRange( Combo1(set, temp ));
                    }

                    return accumulator;
                }
            }

            public static List<String> Combo2(List<String> set, String result)
            {
                if (set.Count == result.Length)
                {
                    return new List<String> { result };
                }
                else
                {
                    List<String> accumulator = new List<String>();

                    foreach(String member in set)
                    {
                        String temp = String.Concat(result, member);
                        accumulator.AddRange( Combo2(set, temp ));
                    }

                    return accumulator;
                }
            }
    }

}
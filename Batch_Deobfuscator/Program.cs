using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Batch_Deobfuscator
{
    class Program
    {
        public static string key;
        public static string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string finalResult;
        public static string finalfinalResult;
        static void Main(string[] args)
        {
            if(args[0] == null)
            {
                Console.Write(@"Please use like this: Batch_Deobfuscator.exe C:\blabla\myfile.bat");
                Console.ReadKey();
                Environment.Exit(0);
            }
            string path = args[0];
            string content = File.ReadAllText(path);
            key = FindKey(content);
            Console.Title = "JsBatchDeobfuscator v1.6 By [DrayNeur]";
            content = Regex.Split(content, @"cls")[1].Replace("@%ab9:~14,1%%ab9:~12,1%%ab9:~17,1%%ab9:~24,1% %ab9:~24,1%%ab9:~15,1%%ab9:~15,1%", "");
            string[] lines = content.Split(
                new[] { "\r\n", "\r", "\n" },StringSplitOptions.None
            );
            string[] chars;
            for (int i = 0; i < lines.Length; i++)
            {
                chars = Regex.Split(lines[i], @"%" + key + ":~"); ;
                for (int o = 0; o < chars.Length; o++)
                {
                    if (o == 0)
                    {
                        chars[o] = "ENTER";
                    } else
                    {
                        chars[o] = chars[o].Replace(",1%", "");
                    }
                    if (chars[o] == "ENTER")
                    {
                        finalResult = finalResult + "\n";
                        //chars[o].Contains(":") || chars[o].Contains("/") || chars[o].Contains("(") || chars[o].Contains(")") || chars[o].Contains("@") || chars[o].Contains(@"\") || chars[o].Contains("<") || chars[o].Contains(">") || chars[o].Contains("_") || chars[o].Contains("-") || chars[o].Contains(".") || chars[o].Contains('"') || chars[o].Contains(' ')
                    }
                    else if (ContainChar(chars[o]))
                    {
                        string b = string.Empty;
                        string chars_without_number;
                        int val = 0;

                        for (int m = 0; m < chars[o].Length; m++)
                        {
                            if (Char.IsDigit(chars[o][m]))
                            {
                                b += chars[o][m];
                            }
                        }
                        if (b.Length > 0)
                        {
                            val = int.Parse(b);
                        }
                        chars_without_number = chars[o].Replace(val.ToString(), "");
                        finalResult = finalResult + GetChar(val);
                        finalResult = finalResult + chars_without_number;
                    } else
                    {
                        finalResult = finalResult + GetChar(int.Parse(chars[o]));
                    }
                    
                }
            }
            finalfinalResult = "\n@echo off\n" + finalResult; 
            Console.Write(finalfinalResult);
            Console.ReadKey();
        }
        public static char GetChar(int charactere)
        {
            char result;
            result = alphabet[charactere];
            return result;
        }
        private static string FindKey(string contents)
        {
            string[] result = Regex.Split(contents, @"Set ");
            return Regex.Split(result[1], @"=")[0];
        }
        public static bool ContainChar(string all)
        {
            for (int m = 0; m < all.Length; m++)
            {
                if (Char.IsDigit(all[m]))
                {
                    return true;
                    break;
                }
            }
            return false;
        }
    }
}

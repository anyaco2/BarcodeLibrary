﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary
{
    /// <summary>
    ///     Класс-Сервис для работы с штрих-кодами
    /// </summary>
    public static class BarcodeHelper
    {
        #region Help

        /// <summary>
        /// <para>
        /// ████████████████████████████████████████████████████████████
        /// ██ ▌█▐█▌█▌▐▐█  █▐▌▌█▐ ██  ▌ ▌▌▌█  █ █▐▐█▌▌▐▌██▐ ▐  ▌▐█ ▐▐ ██
        /// ██ ▌█▐█▌█▌▐▐█  █▐▌▌█▐ ██  ▌ ▌▌▌█  █ █▐▐█▌▌▐▌██▐ ▐  ▌▐█ ▐▐ ██
        /// ██ ▌█▐█▌█▌▐▐█  █▐▌▌█▐ ██  ▌ ▌▌▌█  █ █▐▐█▌▌▐▌██▐ ▐  ▌▐█ ▐▐ ██
        /// ██ ▌█▐█▌█▌▐▐█  █▐▌▌█▐ ██  ▌ ▌▌▌█  █ █▐▐█▌▌▐▌██▐ ▐  ▌▐█ ▐▐ ██
        /// ████████████████████████████████████████████████████████████
        ///                           Example
        /// </para>
        /// </summary>
        public static string GetCode(string text)
        {
            
            StringBuilder code = new StringBuilder(Frame);
            List<int> patterns = new List<int>();

            // Определяем алгоритм кодирования
            bool isNumeric = true;
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i]))
                {
                    isNumeric = false;
                    break;
                }
            }

            if (isNumeric && text.Length % 2 == 0 && text[0] != '0')
            {
                // Числовой режим кодирования
                patterns.Add(StartNumbers);
                code.Append(Patterns[StartNumbers]);
                EncodeNumbers(text, patterns, code);
            }
            else
            {
                // Текстовый режим кодирования
                patterns.Add(StartText);
                code.Append(Patterns[StartText]);
                EncodeText(text, patterns, code);
            }

            // Добавляем контрольный паттерн
            patterns.Add(CalculateCheckPattern(patterns));
            code.Append(Patterns[patterns[patterns.Count - 1]]);

            // Добавляем паттерн остановки и конец штрихкода
            code.Append(Patterns[Stop]);
            code.Append(Frame);

            // Преобразовываем строку из "0" и "1" в строку штрихов
            var tmp = code.ToString();
            code = new StringBuilder();
            foreach (var x in tmp.Chunk(2)) {
                code.Append(Bars[Convert.ToInt32(x,2)]);
            }
            // Формируем полный многострочный штрихкод
            return BuildFullCode(code.ToString());
        }

        private static void EncodeNumbers(string text, List<int> patterns, StringBuilder code)
        {
            for (int i = 0; i < text.Length; i += 2)
            {
                string pair = text.Substring(i, 2);
                patterns.Add(Array.IndexOf(NumberSymbols, pair));
                code.Append(Patterns[patterns[patterns.Count - 1]]);
            }
        }
        private static IEnumerable<string> Chunk(this string a, int b)
        {
            return Enumerable.Range(0, a.Length / b)
                .Select(i => a.Substring(i * b, b));
        }

        private static void EncodeText(string text, List<int> patterns, StringBuilder code)
        {
            foreach (char c in text)
            {
                patterns.Add(Array.IndexOf(TextSymbols, c.ToString()));
                code.Append(Patterns[patterns[patterns.Count - 1]]);
            }
        }

        private static int CalculateCheckPattern(List<int> patterns)
        {
                int sum = patterns[0];
                for (int i = 1; i < patterns.Count; i++)
                {
                    sum += i * patterns[i];
                }

                return sum % 103;
        }

        private static string BuildFullCode(string code)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(new string('█', code.Length));
            for (int i = 0; i < Height; i++)
            {
                sb.AppendLine(code);
            }

            sb.AppendLine(new string('█', code.Length));
            return sb.ToString();
        }



        /// <summary>   
        ///  Высота штрихкода (в строках)
        /// </summary>
        public const int Height = 1;

        /// <summary>
        /// Для получения рамки штрихкода по краям
        /// </summary>
        private const string Frame = "0000";

        /// <summary>
        ///    Допустимые варианты штрихов
        /// </summary>
        public static readonly char[] Bars = { '█', '▌', '▐', ' ' };

        /// <summary>
        ///     Начальный номер паттерна для текстовой строки
        /// </summary>
        private const int StartText = 104;

        /// <summary>
        ///     Начальный номер паттерна для числовой строки
        /// </summary>
        private const int StartNumbers = 105;

        /// <summary>
        ///     Переключить в числовой режим кодирования
        /// </summary>
        private const int SwitchToNumbers = 99;

        /// <summary>
        ///     Переключить в текстовый режим кодирования
        /// </summary>
        private const int SwitchToText = 100;

        /// <summary>
        ///     Номер паттерна завершения
        /// </summary>
        private const int Stop = 108;

        /// <summary>
        ///     Доступные паттерны
        /// </summary>
        private static readonly string[] Patterns = {
        "11011001100", "11001101100", "11001100110", "10010011000", "10010001100",
        "10001001100", "10011001000", "10011000100", "10001100100", "11001001000",
        "11001000100", "11000100100", "10110011100", "10011011100", "10011001110",
        "10111001100", "10011101100", "10011100110", "11001110010", "11001011100",
        "11001001110", "11011100100", "11001110100", "11101101110", "11101001100",
        "11100101100", "11100100110", "11101100100", "11100110100", "11100110010",
        "11011011000", "11011000110", "11000110110", "10100011000", "10001011000",
        "10001000110", "10110001000", "10001101000", "10001100010", "11010001000",
        "11000101000", "11000100010", "10110111000", "10110001110", "10001101110",
        "10111011000", "10111000110", "10001110110", "11101110110", "11010001110",
        "11000101110", "11011101000", "11011100010", "11011101110", "11101011000",
        "11101000110", "11100010110", "11101101000", "11101100010", "11100011010",
        "11101111010", "11001000010", "11110001010", "10100110000", "10100001100",
        "10010110000", "10010000110", "10000101100", "10000100110", "10110010000",
        "10110000100", "10011010000", "10011000010", "10000110100", "10000110010",
        "11000010010", "11001010000", "11110111010", "11000010100", "10001111010",
        "10100111100", "10010111100", "10010011110", "10111100100", "10011110100",
        "10011110010", "11110100100", "11110010100", "11110010010", "11011011110",
        "11011110110", "11110110110", "10101111000", "10100011110", "10001011110",
        "10111101000", "10111100010", "11110101000", "11110100010", "10111011110",
        // 100+
        "10111101110", "11101011110", "11110101110", "11010000100", "11010010000",
        "11010011100", "11000111010", "11010111000", "1100011101011"};
        /// <summary>
        ///     Разрешенные символы
        /// </summary>
        private static readonly string[] TextSymbols = {
        " ","!","\"","#","$","%","&","'","(",")",
        "*","+",",","-",".","/","0","1","2","3",
        "4","5","6","7","8","9",":",";","<","=",
        ">","?","@","A","B","C","D","E","F","G",
        "H","I","J","K","L","M","N","O","P","Q",
        "R","S","T","U","V","W","X","Y","Z","[",
        "\\","]","^","_","`","a","b","c","d","e",
        "f","g","h","i","j","k","l","m","n","o",
        "p","q","r","s","t","u","v","w","x","y",
        "z","{","|","|","~"
    };
        /// <summary>
        ///     Разрешенные пары числовых строк
        /// </summary>
        private static readonly string[] NumberSymbols = {
        "00","01","02","03","04","05","06","07","08","09",
        "10","11","12","13","14","15","16","17","18","19",
        "20","21","22","23","24","25","26","27","28","29",
        "30","31","32","33","34","35","36","37","38","39",
        "40","41","42","43","44","45","46","47","48","49",
        "50","51","52","53","54","55","56","57","58","59",
        "60","61","62","63","64","65","66","67","68","69",
        "70","71","72","73","74","75","76","77","78","79",
        "80","81","82","83","84","85","86","87","88","89",
        "90","91","92","93","94","95","96","97","98","99"
    };
        #endregion
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decrypting
{
    public class Decrypter
    {
        /// <summary>
        /// Получить список используемых в тексте символов.
        /// </summary>
        public List<string> GetUsedSymbols(string text)
            => GetUsedSymbols(new string[] { text });

        /// <summary>
        /// Получить список используемых в нескольких текстах символов.
        /// </summary>
        public List<string> GetUsedSymbols(IEnumerable<string> texts)
            => texts.SelectMany(s => s.Select(c => c))
                .Distinct()
                .Select(c => c.ToString())
                .OrderBy(s => s)
                .ToList();

        /// <summary>
        /// Получить частоту встречаемости символов в тексте.
        /// </summary>
        public List<Tuple<string, double>> GetFrequencies(string text)
        {
            var usedSymbols = GetUsedSymbols(text);
            var n = text.Length;
            var result = new List<Tuple<string, double>>();
            foreach (var symbol in usedSymbols)
            {
                var symbolCount = text.Where(s => s.ToString() == symbol)
                    .Sum(s => s);
                result.Add(Tuple.Create(symbol, symbolCount / Convert.ToDouble(n)));
            }
            return result;
        }

        /// <summary>
        /// Длина зашифрованного блока.
        /// </summary>
        private const int BlockLength = 16;

        /// <summary>
        /// Перемешивание (2-й ключ).
        /// </summary>
        private readonly int[] MoveArray = new int[] 
        {
            11, 5, 2, 6,
            12, 1, 15, 14,
            8, 3, 0, 7,
            9, 4, 13, 10
        };

        /// <summary>
        /// Замена символов (1-й ключ).
        /// </summary>
        public readonly Dictionary<string, string> Symbols = new Dictionary<string, string>
        {
            { "BM", "" },

            { "DH", "д" },
            { "HD", "м" },
            { "AJ", "0" },
            { "JA", "н" },
            { "IK", "о" },
            { "ON", "к" },
            { "NO", "д" },

            { "GF", "т" },
            { "EM", "е" },
            { "AN", "а" },
            { "CC", "ф" },
            { "FO", "9" },
            { "DC", "1" },

            { "FN", "З" },
            { "NF", "ь" },
            { "HH", "п" },
            { "AA", " " },
            { "HC", "р" },

            { "IO", "и" },
            { "HF", "Н" },
            { "PD", "л" },
            { "MF", "ы" },
            { "FH", "в" },
            { "KK", "б" },
            { "JE", "," },

            { "GN", "." },
            { "NB", "ж" },
            { "CM", "с" },
            { "CK", "ч" },
            { "BE", "я" },
            { "IF", "ё" },
            { "OO", "у" },
            { "CP", "ц" },
            { "HI", "э" },
            { "DD", "г" },

            { "EI", "й" },
            { "OE", "ш" },
            { "AF", "з" },
            { "ED", "щ" },
            { "GA", "3" },
            { "PH", "5" },
            { "DI", "%" },
            { "GH", "7" },
            { "DL", "-" },
            { "OP", "2" },

            { "AL", "*" },
        };

        private int Sum = 0;

        /// <summary>
        /// Расшифровать текст.
        /// </summary>
        public string Analyze(string text)
        {
            var blocksCount = text.Length / BlockLength;
            var sb = new StringBuilder(text.Length / 2);
            for (int i = 0; i < blocksCount; i++)
            {
                var block = AnalizeBlock(text.Substring(i * BlockLength, BlockLength));
                sb.Append(block);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Расшифровать блок (16 символов) текста.
        /// </summary>
        private string AnalizeBlock(string text)
        {
            var afterMove = new char[BlockLength];
            for (int i = 0; i < BlockLength; i++)
                afterMove[i] = text[MoveArray[i]];
            var stringAfterMove = new string(afterMove);

            var result = new StringBuilder(BlockLength / 2);
            for (int i = 0; i < BlockLength; i += 2)
            {
                var encrypted = stringAfterMove.Substring(i, 2);
                if (!Symbols.ContainsKey(encrypted))
                {
                    Symbols.Add(encrypted, Sum.ToString());
                    Sum++;
                }
                result.Append(Symbols[encrypted]);
            }
            return result.ToString();
        }
    }
}

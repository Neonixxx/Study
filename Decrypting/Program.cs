using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Decrypting
{
    class Program
    {
        public static string EncryptedText { get; } = "MDEDMFMFPEAHHANAIFICAHKOHAHHGHMEIFJAAKAOAAHIIHMCJFIHOCKAFINHGOOIHFAHONAC" +
            "HIDGHHNANEHKAMCOIJNEJAAAOKGFKAFNNIAAIADPFKIKKHKHIIDDKPCHAMCNEFKNOBANEAMCADHEHNCABHCAHHOIAKACBOANHNMIIEFGGOIIKAOFHI" +
            "DJIDAAGNAACKAFJHAIOJKIPFCHMNMDHEAAGJOOGABKMOEFIECIAHHHKNPOOEDKOIFBAGAAHOHANFDCAAAMIJEJIAJDAMAOHAOEJIFMJDHAFAHAJMDA" +
            "PHMEPKAKKOADIIAIIANOCAAFNAJMMGAGAAFGEDEHNKMMFOECPJKIPDIAJAOHAAIAPDOIDHCMKAMDEIAAFAKIFMINNMKHAANCCGFGANHNHAFNFHNAAA" +
            "AACKJACOACAHKNIIDHAMGMMAFACCMJEEAAMOEAACMFJAAHNJOIAKBEOKEABNKIKICHAKOPAANNCAOAINAJBNOCAMNEHACEADKKGODDAOCNDAHFFHAA" +
            "AMGOKKFAIIOIENCHIIIMAAKFEAHJEGAAHAECOLIDHIODAINAAKCFHAMNGFOAIIMCIAIKFDOOKGFPAMOOAEICADONHJMHJEAAJFJOAAAEIANJMODHEF" +
            "JBAKAMNJAIGJOOHOHAKFDHAIHMIHEJIEFDANHOHAHAOFOIJDHAAAHEJAOAPNMEIMAFCMFOGHNEEACHIEIKDDOKKPFHJNAAADMEKAFAJIKJHKOIECIB" +
            "ADKMNJAHHJKIAOHANFDFAADMIHEJHFHOOMCDIIAEAAEOGOPKCODFIHNIIAAAMMPADHDFJHOFEIAJHNDNOADDAIAAAAFAAKOKLKNAIAAIIADPCMMAMF" +
            "FKAEDGCPNGAMFEFKHAJGNIEGNODAGPAMNIOAKEAIPOEKADDFAMEDKGIIKKAOCDAAKNPAEAIOMCANMHOFFAFIAGOAKINKADNMNFPGMCKBME";

        public static List<Example> Examples { get; } = new List<Example>
        {
            new Example { Text = "Западном фронте", EncryptedText = "JHAOKNNANIDFHHNAGCCAMACFJEMAHBKI" },
            //                                                                  --   --     - - --    --   --     - - --
            new Example { Text = "Запа   дном   _фро   нте-", EncryptedText = "JHAO KNNA NIDF HHNA   GCCA MACF JEMA HBKI" },
            new Example { Text = "1919", EncryptedText = "BCFM MCOM BBMD DBOF" }
        };

        public static IEnumerable<string> Texts => Examples.Select(e => e.Text);

        public static IEnumerable<string> EncryptedTexts => Examples.Select(e => e.EncryptedText).Union(new string[] { EncryptedText });

        static void Main(string[] args)
        {
            var decrypter = new Decrypter();

            Console.WriteLine(decrypter.Analyze(Examples[0].EncryptedText));

            var result2 = decrypter.Analyze(EncryptedText);
            Console.WriteLine();
            Console.WriteLine(result2);
            using (var sw = new StreamWriter("output.txt"))
                sw.WriteLine(result2);


                Console.WriteLine();
            Console.WriteLine();
            foreach (var item in decrypter.Symbols)
                Console.WriteLine($"{item.Key} - {item.Value}");

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind

    
{
    class Program
       
    {
        static private Random _Random = new Random();
        private static char[] _Code = getCode();
        static void Main(string[] args)
        {
            playGame();
        }
        static char[] getCode()
        {
            char[] code = new char[5];
            
            int number =  _Random.Next(00000, 99999);
            code = number.ToString().PadLeft(5, '0').ToArray();

            return code ;
        }

        static void playGame()
        {

            string numberAnswer = "";
            char[] numberAnswerTab;
            char[] returnTab = new char[getCode().Length];
            string returnString = new string('_', _Code.Length);



            do
            {
                if(returnTab[0] != '\0')
                {
                    returnString = new string(returnTab);
                }
               
                Console.WriteLine(returnString);
                do
                {
                    Console.WriteLine("Choisissez un code à 5 chiffres :");
                    numberAnswer = Console.ReadLine();
                }
                while (numberAnswer.Length != 5);

                numberAnswerTab = numberAnswer.ToCharArray(0, numberAnswer.Length);

                for (int i = 0; i < numberAnswerTab.Length; i++)
                {
                    if(numberAnswerTab[i] == _Code[i])
                    {
                        returnTab[i]= numberAnswerTab[i];
                    }
                    else
                    {
                        returnTab[i] = '_';
                    }
                }


            }
            while (numberAnswerTab != _Code);

        }
       
        
    }
}

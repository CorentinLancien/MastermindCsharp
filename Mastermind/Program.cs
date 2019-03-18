using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind

    
{
    class Program
       
    {
        #region references
        //Je réference ici, toutes les variables qui me seront utiles dans plusieurs fonctions, en l'occurence mon
        // Random ainsi que la génération de mon Code .
        static private Random _Random = new Random();
        private static char[] _Code = getCode();
        #endregion
        static void Main(string[] args)
        
        {
            playGame();
        }

        #region Methods
        //Je range par Methodes, ce que j'ai besoin de dévelloper pour la fonction Main

       
        static char[] getCode()
        //Cette methode sert à me retourner un tableau de charactères, tableau qui me servira de génération de mon code
        //plus tard, dans la fonction du jeu .
        {
            char[] code = new char[5];

            int number = _Random.Next(00000, 99999);
            //Grace a PadLeft, j'oblige de mettre un 0 tant que la chaine de caractere n'est pas égale à 5 de longueur
            //ce qui permet de generer mon nombre aléatoire grâce au _Random apeller précédemment
            code = number.ToString().PadLeft(5, '0').ToArray();

            return code;
        }

        static void playGame()
        {
            #region références
            string numberAnswer = ""; //Le chiffre entrer par l'utilisateur
            char[] numberAnswerTab; // le tableau de caractere, qui contiendra la chaine entrer par le joueur
            char[] returnTab = new char[_Code.Length]; //Le tableau de caractere qui me servira d'affichage
            string returnString = new string('_', _Code.Length); //La chaine qui me servira a afficher mon tableau d'affichage
            char[] repertoire = new char[_Code.Length];//le repertoire me permettant de savoir si un chiffre a l'intérieure d'une réponse
                                                       //à déjà été vérifier
            bool findStar = false;// un boolean me permettant de savoir si l'on à écris un étoile à la place souhaiter dans le tableau d'affichage
            bool error = false; // un boolean qui me permet de savoir si le chiffre actuelle est déjà dans le repertoire
            bool endOfGame = true; //un boolean qui me permet de savoir si le jeu est fini
            int countLife = 8; // mon compteur de vie
            #endregion


            do
            {
                endOfGame = true;
                Console.Clear();
                for (int i = 0; i < repertoire.Length; i++)
                {
                    repertoire[i] = 'a';
                }
                if (returnTab[0] != '\0')
                {
                    returnString = new string(returnTab);
                }

                Console.WriteLine(returnString);
                do
                {
                    Console.WriteLine("Choisissez un code à 5 chiffres : ");
                    Console.WriteLine($"Vous n'avez plus que : {countLife} essais");
                    numberAnswer = Console.ReadLine();
                }
                while (numberAnswer.Length != 5);

                numberAnswerTab = numberAnswer.ToCharArray(0, numberAnswer.Length);

                for (int i = 0, j = 0; i < numberAnswerTab.Length; i++)
                {
                    if (numberAnswerTab[i] == _Code[i])
                    {
                        returnTab[i] = numberAnswerTab[i];
                        repertoire[j] = numberAnswerTab[i];
                        j++;
                    }
                    else
                    {
                        returnTab[i] = '_';
                    }
                }
                for (int i = 0; i < numberAnswerTab.Length; i++)
                {
                    error = false;
                    for (int k = 0; k < repertoire.Length && repertoire != null; k++)
                    {
                        if (repertoire[k] == numberAnswerTab[i])
                        {
                            error = true;
                        }
                    }
                    for (int j = 0; j < _Code.Length && error == false; j++)
                    {
                        if (_Code[j] == numberAnswerTab[i])
                        {
                            returnTab[i] = '*';
                            j = _Code.Length;
                            findStar = true;

                        }
                        if (findStar == false)
                        {
                            returnTab[i] = '_';
                        }


                        repertoire[i] = numberAnswerTab[i];
                    }

                }
                for (int i = 0; i < _Code.Length; i++)
                {
                    if (numberAnswerTab[i] != _Code[i])
                    {
                        endOfGame = false;
                    }
                }
                countLife--;
            }
            while (endOfGame == false && countLife > 0);
            if (countLife > 0)
            {
                Console.WriteLine($"Bravo vous avez gagné !!!! il ne vous restait plus que : {countLife + 1} essais");
            }
            else
            {
                Console.WriteLine("Vous avez perdu !!! Il ne reste plus d'éssais");
            }
            
            Console.ReadLine();
        }


    }
    #endregion

}

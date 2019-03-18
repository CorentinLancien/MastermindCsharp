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
                endOfGame = true; //On admet d'abord que le jeu est terminé
                Console.Clear();

                //Cette boucle permet de réinistialisé le repertoire à chaque nouveau tour de jeu
                for (int i = 0; i < repertoire.Length; i++)
                {
                    repertoire[i] = 'a';
                }

                // Si le tableau d'affichage n'est pas composé de 0, alors la chaine afficher sera égale à la concaténation
                // des valeurs du tableau returnTab
                if (returnTab[0] != '\0')
                {
                    returnString = new string(returnTab);
                }

                Console.WriteLine(returnString);

                //cette boucle do while, nous permet de vérifier si l'utilisateur rentre un code de 5 chiffre
                //Tant que ça n'est pas le cas, on repose la question
                do
                {
                    Console.WriteLine("Choisissez un code à 5 chiffres : ");
                    Console.WriteLine($"Vous n'avez plus que : {countLife} essais");
                    numberAnswer = Console.ReadLine();
                }
                while (numberAnswer.Length != 5);

                //Chaque tour, on récupère la valeur entrée par le joueur et on la découpe pour la stockée dans un tableau de char
                numberAnswerTab = numberAnswer.ToCharArray(0, numberAnswer.Length);

                //On test d'abord si le chiffre est à la bonne place
                for (int i = 0, j = 0; i < numberAnswerTab.Length; i++)
                {
                    //Si oui, alors on affiche le chiffre du code
                    if (numberAnswerTab[i] == _Code[i])
                    {
                        returnTab[i] = numberAnswerTab[i];
                        //Si c'est le cas, alors dans le repertoire, on indique à la machine, de ne plus tester ce chiffre du code
                        repertoire[j] = numberAnswerTab[i];
                        j++;
                    }
                    //Sinon, on réaffiche un underscore 
                    else
                    {
                        returnTab[i] = '_';
                    }
                }

                //Enfin, on regarde si le chiffre du code est dans le code secret mais n'est pas à la bonne place
                for (int i = 0; i < numberAnswerTab.Length; i++)
                {
                    //On initialise erreur a faut puisqu'on part du principe qu'il n'y a pas d'erreures
                    error = false;
                    //On parcoure le répertoire, et si le chiffre du code s'y trouve, alors erreur passe a faux .
                    for (int k = 0; k < repertoire.Length && repertoire != null; k++)
                    {
                        if (repertoire[k] == numberAnswerTab[i])
                        {
                            error = true;
                        }
                    }

                    //l'on entre dans cette boucle, qui s'il n'y a pas d'erreure, c'est à dire qui si le chiffre à la place i
                    //ne se trouve pas dans le répertoire
                    //Ensuite, on parcoure le code, pour voir si le chiffre du code proposé sy trouve quelque part
                    for (int j = 0; j < _Code.Length && error == false; j++)
                    {
                        //On hypothèse qu'il n'y a pas d'étoile trouvé pour le chiffre du code à la place i
                        findStar = false;
                        //Si oui, alors l'on affiche une étoile . Le boolean findStar passe à true et j est agale à la taille
                        //du tableau pour en sortir directement
                        if (_Code[j] == numberAnswerTab[i])
                        {
                            returnTab[i] = '*';
                            j = _Code.Length;
                            findStar = true;

                        }
                        //Si aucune étoile n'a été afficher pour le chiffre à la place i, alors l'on réaffiche l'underscore
                        if (findStar == false)
                        {
                            returnTab[i] = '_';
                        }

                        //Enfin, on stocke ce chiffre dans le répertoire, afin de ne pas le retester plus tard
                        repertoire[i] = numberAnswerTab[i];
                    }

                }

                //Dernière chose, on teste si le tableau de la réponse du joueur est égale à celui du Code en parcourant respectivement
                //ceux-ci
                for (int i = 0; i < _Code.Length; i++)
                {
                    //Si il y a une différence entre les deux tableaux, alors la fin du jeu n'est pas vrai sinon, le jeu est fini
                    if (numberAnswerTab[i] != _Code[i])
                    {
                        endOfGame = false;
                    }
                }
                //On enleve une vie au joueur
                countLife--;
            }
            //Si il lui reste des vies, le joueur continue, sinon le jeu est terminé, est si le boolean endOfGame est vrai, alors
            //le jeu est terminé aussi
            while (endOfGame == false && countLife > 0);
            //Si l'on sort par la première condition, alors c'est que le joueur à gagné .
            if (countLife > 0)
            {
                Console.WriteLine($"Bravo vous avez gagné !!!! il ne vous restait plus que : {countLife + 1} essais");
            }
            //Sinon, c'est que le joueur à perdu
            else
            {
                Console.WriteLine("Vous avez perdu !!! Il ne reste plus d'essais . . . ");
            }
            
            Console.ReadLine();
        }


    }
    #endregion

}

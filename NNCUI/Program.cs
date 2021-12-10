using System.Globalization;
using System;
using NNLib;

namespace NNCUI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isOver = false;
            while(!isOver){

                Console.Write("Veuillez entrer votre numéro de registre national : ");
                string number = Console.ReadLine();

                if(NationalNumber.IsValid(number)){

                    string gender = NationalNumber.getGender(number);
                    int age = NationalNumber.getAge(number);
                    Console.WriteLine($"Bonjour {gender}, Vous avez {age} ans.");
                    isOver = true;

                } else{

                    Console.WriteLine("Numéro invalide. Veuillez réessayer");
                }
            }
        }
    }
}

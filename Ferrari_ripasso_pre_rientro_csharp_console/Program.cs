using Ferrari_ripasso_pre_rientro_csharp_form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferrari_ripasso_pre_rientro_csharp_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region dichiarazioni
            funzioni_form functions = new funzioni_form();
            string fileName = "ferrari.csv";
            string fileNameTemp = "ferrari_temp.csv";
            int recordLen = 260;
            Random rnd = new Random();
            #endregion
            if (!functions.checkFixedLen(fileName))
                functions.createFixedLen(fileName, fileNameTemp);
            functions.subChar(fileName, fileNameTemp);
            bool keepGoing = true;
            string basicOptions = "Opzioni:\n1 - Aggiunta Miovalore\n2 - Conta Campi\n3 - Calcola Lunghezza Campi/Record\n4 - Aggiunta Record in coda\n5 - Visualizza Campi\n6 - Ricerca Record\n7 - Modifica Record\n8 - Cancella Record\n0 - Chiusura Programma\n";
            do
            {
                Console.Clear();
                Console.WriteLine(basicOptions);
                string option;
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        if (!functions.checkMyValue(fileName))
                            functions.createMyValue(fileName, fileNameTemp);
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nel file sono già presenti i campi Miovalore e Cancellazione Logica");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                        }
                        break;
                    case "2":
                        int numFields = functions.countFields(fileName);
                        Console.Clear();
                        
                        Console.Write("Numero campi: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(numFields);
                        Console.ResetColor();
                        Console.WriteLine("\nPremere un tasto per continuare\n");
                        Console.ReadKey();
                        break;
                    case "3":
                        int fields = functions.countFields(fileName);
                        string[] maxLen = functions.maxLen(fileName, fields);
                        Console.Clear();
                        Console.Write($"Il record più lungo è ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" {maxLen[fields]} ");
                        Console.ResetColor();
                        Console.Write($"composto da {maxLen[fields].Length} caratteri\n");
                        for (int i = 0; i < fields; i++)
                        {
                            Console.Write($"Il campo {i} più lungo è");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write ($" {maxLen[i]} ");
                            Console.ResetColor();
                            Console.Write ($"composto da {maxLen[i].Length} caratteri\n");
                        }
                        Console.WriteLine("\nPremere un tasto per continuare\n");
                        Console.ReadKey();
                        break;
                    case "4":
                        int numFieldsATQ = functions.countFields(fileName);
                        string[] inputs = new string[numFieldsATQ];
                        string[] fieldsNames = functions.searchFieldsNames(fileName);
                        for (int i = 0; i < 9; i++)
                        {
                            Console.Clear();
                            Console.WriteLine($"Inserire i dati del campo \"{fieldsNames[i]}\" da aggiungere\n");
                            inputs[i] = Console.ReadLine();
                        }
                        if (functions.checkMyValue(fileName))
                        {
                            inputs[9] = rnd.Next(10, 21).ToString();
                            inputs[10] = "0";
                        }
                        if (!functions.checkInputLen(numFieldsATQ, inputs))
                        {
                            if (!functions.checkMyValue(fileName))
                            {
                                Console.Clear();
                                Console.WriteLine("Errore: uno o più campi sono vuoti o troppo lunghi, la lunghezza totale non deve superare i 242 caratteri");
                                Console.WriteLine("\nPremere un tasto per continuare\n");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Errore: uno o più campi sono vuoti o troppo lunghi, la lunghezza totale non deve superare i 245 caratteri");
                                Console.WriteLine("\nPremere un tasto per continuare\n");
                                Console.ReadKey();
                            }
                        }
                        else if (!functions.checkInputChars(numFieldsATQ, inputs))
                        {
                            Console.Clear();
                            Console.WriteLine("Uno o più campi contengono caratteri non validi");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" \n';', '#', '\\'");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            functions.addToQueue(fileName, numFieldsATQ, inputs);
                        }
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Inserire numero del primo campo\n");
                        string firstFieldS = Console.ReadLine();
                        int firstField;
                        if (firstFieldS.Length == 1 && firstFieldS[0] > 47 && firstFieldS[0] < 58)
                            firstField = int.Parse(firstFieldS);
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il campo inserito non è valido");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("Inserire numero del primo campo\n");
                        string secondFieldS = Console.ReadLine();
                        int secondField;
                        if (secondFieldS.Length == 1 && secondFieldS[0] > 47 && secondFieldS[0] < 58)
                            secondField = int.Parse(secondFieldS);
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il campo inserito non è valido");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("Inserire numero del primo campo\n");
                        string thirdFieldS = Console.ReadLine();
                        int thirdField;
                        if (thirdFieldS.Length == 1 && thirdFieldS[0] > 47 && thirdFieldS[0] < 58)
                            thirdField = int.Parse(thirdFieldS);
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il campo inserito non è valido");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                            break;
                        }
                        int numFieldsVIS = functions.countFields(fileName);
                        int[] checkEd = new int[numFieldsVIS];
                        for (int i = 0; i < numFieldsVIS; i++)
                            checkEd[i] = 0;
                        if (firstField == 0 || secondField == 0 || thirdField == 0) checkEd[0] = 1;
                        if (firstField == 1 || secondField == 1 || thirdField == 1) checkEd[1] = 1;
                        if (firstField == 2 || secondField == 2 || thirdField == 2) checkEd[2] = 1;
                        if (firstField == 3 || secondField == 3 || thirdField == 3) checkEd[3] = 1;
                        if (firstField == 4 || secondField == 4 || thirdField == 4) checkEd[4] = 1;
                        if (firstField == 5 || secondField == 5 || thirdField == 5) checkEd[5] = 1;
                        if (firstField == 6 || secondField == 6 || thirdField == 6) checkEd[6] = 1;
                        if (firstField == 7 || secondField == 7 || thirdField == 7) checkEd[7] = 1;
                        if (firstField == 8 || secondField == 8 || thirdField == 8) checkEd[8] = 1;
                        if (numFieldsVIS > 9)
                            if (firstField == 9 || secondField == 9 || thirdField == 9) checkEd[9] = 1;
                        else
                            if (firstField == 9 || secondField == 9 || thirdField == 9)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;  
                                Console.WriteLine("Il campo Miovalore non è presente nel file");
                                Console.ResetColor();
                                Console.WriteLine("\nPremere un tasto per continuare\n");
                                Console.ReadKey();
                                break;
                            }
                        string[] names = functions.searchFieldsNames(fileName);
                        Console.Clear();
                        using (StreamReader csvReader = File.OpenText(fileName))
                        {
                            string lineFromFile;
                            lineFromFile = csvReader.ReadLine();
                            while ((lineFromFile = csvReader.ReadLine()) != null)
                            {
                                string[] fieldsVIS = lineFromFile.Split(';');
                                if (numFieldsVIS == 11)
                                {
                                    if (fieldsVIS[10] == "0")
                                    {
                                        Console.WriteLine();
                                        for (int i = 0; i < numFieldsVIS; i++)
                                        {
                                            if (checkEd[i] == 1)
                                            {
                                                Console.Write($"{names[i]}: ");
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.Write($"{fieldsVIS[i]} ");
                                                Console.ResetColor();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    for (int i = 0; i < numFieldsVIS; i++)
                                    {
                                        if (checkEd[i] == 1)
                                        {
                                            Console.Write($"{names[i]}: ");
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.Write($"{fieldsVIS[i]} ");
                                        }
                                    }
                                }
                            }
                            csvReader.Close();
                            Console.WriteLine("\n\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                        }
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Inserire Identificatore in OpenStreetMap\n");
                        string idOSM = Console.ReadLine();
                        Tuple<string, int> RecordAndPosition = functions.searchPosition(fileName, idOSM, false);
                        if (RecordAndPosition.Item2 == -1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il record cercato non è presente");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Il record ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(RecordAndPosition.Item1);
                            Console.ResetColor();
                            Console.Write($"è presente in posizione {RecordAndPosition.Item2}\n");
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                        }
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("Inserire Identificatore in OpenStreetMap\n");
                        string idOSMMOD = Console.ReadLine();
                        Tuple<string, int> RecordAndPositionMOD = functions.searchPosition(fileName, idOSMMOD, false);
                        if (RecordAndPositionMOD.Item2 == -1)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Il record cercato non è presente");
                            Console.ResetColor();
                            Console.WriteLine("\nPremere un tasto per continuare\n");
                            Console.ReadKey();
                        }
                        else
                        {
                            int numFieldsMOD = functions.countFields(fileName);
                            string[] inputsMOD = new string[numFieldsMOD];
                            string[] fieldsNamesMOD = functions.searchFieldsNames(fileName);
                            for (int i = 0; i < 9; i++)
                            {
                                Console.Clear();
                                Console.WriteLine($"Inserire i dati del campo \"{fieldsNamesMOD[i]}\" da aggiungere\n");
                                inputsMOD[i] = Console.ReadLine();
                            }
                            if (functions.checkMyValue(fileName))
                            {
                                inputsMOD[9] = rnd.Next(10, 21).ToString();
                                inputsMOD[10] = "0";
                            }
                            if (!functions.checkInputLen(numFieldsMOD, inputsMOD))
                            {
                                if (!functions.checkMyValue(fileName))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Errore: uno o più campi sono vuoti o troppo lunghi, la lunghezza totale non deve superare i 242 caratteri");
                                    Console.WriteLine("\nPremere un tasto per continuare\n");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Errore: uno o più campi sono vuoti o troppo lunghi, la lunghezza totale non deve superare i 245 caratteri");
                                    Console.WriteLine("\nPremere un tasto per continuare\n");
                                    Console.ReadKey();
                                }
                            }
                            else if (!functions.checkInputChars(numFieldsMOD, inputsMOD))
                            {
                                Console.Clear();
                                Console.WriteLine("Uno o più campi contengono caratteri non validi");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" \n';', '#', '\\'");
                                Console.ResetColor();
                                Console.WriteLine("\nPremere un tasto per continuare\n");
                                Console.ReadKey();
                            }
                            else
                            {
                                functions.modifyRecord(fileName, numFieldsMOD, inputsMOD, RecordAndPositionMOD.Item2, recordLen);
                            }
                        }
                        break;
                    case "8":
                        bool valid;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Opzioni cancellazione\n1 - Cancella record\n2 - Recupera record\n3 - Ricompatta database\n");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    valid = true;
                                    if (!functions.checkMyValue(fileName))
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Nel file non sono presenti i campi Miovalore e Cancellazione Logica");
                                        Console.ResetColor();
                                        Console.WriteLine("\nPremere un tasto per continuare\n");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Inserire Identificatore in OpenStreetMap\n");
                                        string idOSMDEL = Console.ReadLine();
                                        Tuple<string, int> RecordAndPositionDEL = functions.searchPosition(fileName, idOSMDEL, false);
                                        if (RecordAndPositionDEL.Item2 == -1)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Il record cercato non è presente");
                                            Console.ResetColor();
                                            Console.WriteLine("\nPremere un tasto per continuare\n");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            int numFieldsDEL = functions.countFields(fileName);
                                            functions.deleteRecord(fileName, numFieldsDEL, RecordAndPositionDEL.Item1, RecordAndPositionDEL.Item2, recordLen);
                                        }
                                    }
                                    break;
                                case "2":
                                    valid = true;
                                    if (!functions.checkMyValue(fileName))
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Nel file non sono presenti i campi Miovalore e Cancellazione Logica");
                                        Console.ResetColor();
                                        Console.WriteLine("\nPremere un tasto per continuare\n");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Inserire Identificatore in OpenStreetMap\n");
                                        string idOSMDEL = Console.ReadLine();
                                        Tuple<string, int> RecordAndPositionDEL = functions.searchPosition(fileName, idOSMDEL, true);
                                        if (RecordAndPositionDEL.Item2 == -1)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Il record cercato non è presente");
                                            Console.ResetColor();
                                            Console.WriteLine("\nPremere un tasto per continuare\n");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            int numFieldsDEL = functions.countFields(fileName);
                                            functions.recoverRecord(fileName, numFieldsDEL, RecordAndPositionDEL.Item1, RecordAndPositionDEL.Item2, recordLen);
                                        }
                                    }
                                    break;
                                case "3":
                                    valid = true;
                                    if (!functions.checkMyValue(fileName))
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Nel file non sono presenti i campi Miovalore e Cancellazione Logica");
                                        Console.ResetColor();
                                        Console.WriteLine("\nPremere un tasto per continuare\n");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        bool validCOMP;
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Cancellare definitivamente i record eliminati\n1 - Si\n0 - No\n");
                                            switch (Console.ReadLine())
                                            {
                                                case "1":
                                                    validCOMP = true;
                                                    functions.permDelete(fileName, fileNameTemp);
                                                    break;
                                                case "0":
                                                    validCOMP = true;
                                                    break;
                                                default:
                                                    validCOMP = false;
                                                    Console.WriteLine("\nOpzione non valida\nPremere un tasto per continuare\n");
                                                    Console.ReadKey();
                                                    break;
                                            }
                                        } while (!validCOMP);
                                    }
                                    break;
                                default:
                                    valid = false;
                                    Console.WriteLine("\nOpzione non valida\nPremere un tasto per continuare\n");
                                    Console.ReadKey();
                                    break;
                            }
                        } while (!valid);
                        break;
                    case "0":
                        bool validEXIT;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Chiudere il programma?\n1 - Si\n0 - No\n");
                            switch(Console.ReadLine())
                            {
                                case "1":
                                    validEXIT = true;
                                    keepGoing = false;
                                    break;
                                case "0":
                                    validEXIT = true;
                                    keepGoing = true;
                                    break;
                                default:
                                    validEXIT = false;
                                    Console.WriteLine("\nOpzione non valida\nPremere un tasto per continuare\n");
                                    Console.ReadKey();
                                    break;
                            }   
                        }while(!validEXIT);
                        break;
                    default:
                        Console.WriteLine("\nOpzione non valida\nPremere un tasto per continuare\n");
                        Console.ReadKey();
                        break;
                }
            } while (keepGoing);
        }



    }
}
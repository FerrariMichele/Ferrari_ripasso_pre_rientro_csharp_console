using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferrari_ripasso_pre_rientro_csharp_form
{
    internal class funzioni_form
    {
        public bool checkFixedLen(string fileName)
        {
            bool fixedLen = false;
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                lineFromFile = csvReader.ReadLine();
                if (lineFromFile.Contains("##"))
                    fixedLen = true;
                csvReader.Close();
            }
            return fixedLen;
        }
        public void createFixedLen(string fileName, string fileNameTemp)
        {
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                using (StreamWriter csvWriter = new StreamWriter(fileNameTemp))
                {
                    while ((lineFromFile = csvReader.ReadLine()) != null)
                    {
                        lineFromFile += ";";
                        lineFromFile = lineFromFile.PadRight(256) + "##";
                        csvWriter.WriteLine(lineFromFile);
                    }
                    csvWriter.Close();
                }
                csvReader.Close();
            }
            File.Delete(fileName);
            File.Move(fileNameTemp, fileName);
            File.Delete(fileNameTemp);
        }
        public bool checkMyValue(string fileName)
        {
            bool myValue = false;
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                lineFromFile = csvReader.ReadLine();
                if (lineFromFile.Contains("Miovalore;Cancellazione logica"))
                    myValue = true;
                csvReader.Close();
            }
            return myValue;
        }
        public void createMyValue(string fileName, string fileNameTemp)
        {
            string lineFromFile;
            Random rnd = new Random();
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                using (StreamWriter csvWriter = new StreamWriter(fileNameTemp))
                {
                    lineFromFile = csvReader.ReadLine();
                    string[] fields = lineFromFile.Split(';');
                    lineFromFile = "";
                    for (int i = 0; i < fields.Length - 1; i++)
                        lineFromFile += fields[i] + ";";
                    lineFromFile += "Miovalore;Cancellazione logica;";
                    lineFromFile = lineFromFile.PadRight(256) + "##";
                    csvWriter.WriteLine(lineFromFile);
                    while ((lineFromFile = csvReader.ReadLine()) != null)
                    {
                        fields = lineFromFile.Split(';');
                        lineFromFile = "";
                        for (int i = 0; i < fields.Length - 1; i++)
                            lineFromFile += fields[i] + ";";
                        lineFromFile += rnd.Next(10,21) + ";0;";
                        lineFromFile = lineFromFile.PadRight(256) + "##";
                        csvWriter.WriteLine(lineFromFile);
                    }
                    csvWriter.Close();
                }
                csvReader.Close();
            }
            File.Delete(fileName);
            File.Move(fileNameTemp, fileName);
            File.Delete(fileNameTemp);
        }
        public int countFields(string fileName)
        {
            int count = 0;
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                lineFromFile = csvReader.ReadLine();
                string[] fields = lineFromFile.Split(';');
                count = fields.Length;
                csvReader.Close();
            }
            return count - 1;
        }
        public string[] maxLen(string fileName, int numField)
        {
            string[] maxLenFields = new string[numField + 1];
            for (int i = 0; i < numField + 1; i++)
                maxLenFields[i] = "";
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                csvReader.ReadLine();
                while ((lineFromFile = csvReader.ReadLine()) != null)
                {
                    string[] fields = lineFromFile.Split(';');
                    lineFromFile = "";
                    for (int i = 0; i < numField; i++)
                    {
                        if (fields[i].Length > maxLenFields[i].Length)
                            maxLenFields[i] = fields[i];
                        lineFromFile += fields[i] + ";";
                    }
                    if (lineFromFile.Length > maxLenFields[numField].Length)
                        maxLenFields[numField] = lineFromFile;
                }
                csvReader.Close();
            }
            return maxLenFields;
        }
        public bool checkInputLen(int numField, string[] elements)
        {
            string lineToFile = "";
            for (int i = 0; i < numField; i++)
            {
                if (elements[i].Length == 0)
                    return false;  
                lineToFile += elements[i] + ";";
            }
            if(numField == 11)
                if (lineToFile.Length < 256)
                    return true;
                else return false;
            else
                if (lineToFile.Length < 251)
                    return true;
            return false;
        }
        public bool checkInputChars(int numField, string[] elements)
        {
            for(int i = 0; i < numField; i++)
            {
                if (elements[i].Contains(';') || elements[i].Contains('\\') || elements[i].Contains('#'))
                    return false;
            }  
            return true;
        }
        public void addToQueue(string fileName, int numField, string[] elements)
        {
            string lineToFile = "";
            for (int i = 0; i < numField; i++)
                lineToFile += elements[i] + ";";
            lineToFile = lineToFile.Replace('\uFFFD', '\'');
            lineToFile = lineToFile.PadRight(256) + "##";
            using (FileStream csvRanWriter = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(lineToFile);
                csvRanWriter.Seek(0, SeekOrigin.End);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public string[] searchFieldsNames(string fileName)
        {
            string[] fieldsNames = new string[countFields(fileName)];
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                lineFromFile = csvReader.ReadLine();
                string[] fields = lineFromFile.Split(';');
                for (int i = 0; i < fieldsNames.Length; i++)
                {
                    fieldsNames[i] = fields[i];
                }
                csvReader.Close();
            }
            return fieldsNames;
        }
        public Tuple<string, int> searchPosition(string fileName, string identifier, bool deleted)
        {
            Tuple<string, int> RecordAndPosition; 
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                string lineFromFile;
                int position = 0;
                if (countFields(fileName) == 11)
                {
                    while ((lineFromFile = csvReader.ReadLine()) != null)
                    {
                        string[] fields = lineFromFile.Split(';');
                        if (fields[6] == identifier)
                        {
                            if (((fields[10] == 0.ToString() && !deleted) || (fields[10] == 1.ToString() && deleted == true)) && position != 0)
                            {
                                csvReader.Close();
                                RecordAndPosition = new Tuple<string, int>(lineFromFile, position);
                                return RecordAndPosition;
                            }
                        }
                        position++;
                    }
                }
                else
                {
                    while ((lineFromFile = csvReader.ReadLine()) != null)
                    {
                        string[] fields = lineFromFile.Split(';');
                        if (fields[6] == identifier && position != 0)
                        {
                            csvReader.Close();
                            RecordAndPosition = new Tuple<string, int>(lineFromFile, position);
                            return RecordAndPosition;
                        }
                        position++;
                    }
                }
                csvReader.Close();
            }
            return new Tuple<string, int>("", -1);
        }
        public void subChar(string fileName, string fileNameTemp)
        {
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                using (StreamWriter csvWriter = new StreamWriter(fileNameTemp))
                {
                    while ((lineFromFile = csvReader.ReadLine()) != null)
                    {
                        lineFromFile = lineFromFile.Replace('\uFFFD', '\'');
                        csvWriter.WriteLine(lineFromFile);
                    }
                    csvWriter.Close();
                }
                csvReader.Close();
            }
            File.Delete(fileName);
            File.Move(fileNameTemp, fileName);
            File.Delete(fileNameTemp);
        }
        public void modifyRecord(string fileName, int numFields, string[] elements, int position, int recordLen)
        {
            string lineToFile = "";
            for (int i = 0; i < numFields; i++)
                lineToFile += elements[i] + ";";
            lineToFile = lineToFile.PadRight(256) + "##";
            lineToFile = lineToFile.Replace('\uFFFD', '\'');
            using (FileStream csvRanWriter = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(lineToFile);
                csvRanWriter.Seek(position * recordLen, SeekOrigin.Current);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public void deleteRecord(string fileName, int numFields, string elements, int position, int recordLen)
        {
            string lineToFile = "";
            string[] fields = elements.Split(';');
            for (int i = 0; i < numFields - 1; i++)
                lineToFile += fields[i] + ";";
            lineToFile += "1;";
            lineToFile = lineToFile.PadRight(256) + "##";
            using (FileStream csvRanWriter = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(lineToFile);
                csvRanWriter.Seek(position * recordLen, SeekOrigin.Current);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public void recoverRecord(string fileName, int numFields, string elements, int position, int recordLen)
        {
            string lineToFile = "";
            string[] fields = elements.Split(';');
            for (int i = 0; i < numFields - 1; i++)
                lineToFile += fields[i] + ";";
            lineToFile += "0;";
            lineToFile = lineToFile.PadRight(256) + "##";
            using (FileStream csvRanWriter = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(lineToFile);
                csvRanWriter.Seek(position * recordLen, SeekOrigin.Current);
                csvRanWriter.Write(bytes, 0, bytes.Length);
                csvRanWriter.Close();
            }
        }
        public void permDelete(string fileName, string fileNameTemp)
        {
            string lineFromFile;
            using (StreamReader csvReader = File.OpenText(fileName))
            {
                using (StreamWriter csvWriter = new StreamWriter(fileNameTemp))
                {
                    lineFromFile = csvReader.ReadLine();
                    csvWriter.WriteLine(lineFromFile);
                    while ((lineFromFile = csvReader.ReadLine()) != null)
                    {
                        string[] fields = lineFromFile.Split(';');
                        if (fields[10] == "0")
                        {
                            csvWriter.WriteLine(lineFromFile);
                        }
                    }
                    csvWriter.Close();
                }
                csvReader.Close();
            }
            File.Delete(fileName);
            File.Move(fileNameTemp, fileName);
            File.Delete(fileNameTemp);
        }
    }
} 
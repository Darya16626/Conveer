using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Data;
using System.Xml.Linq;

namespace Conveer
{
    internal class FileClass
    {
        public FileClass()
        {
            Console.WriteLine("Введите путь до файла (вместе в названием), который вы хотите открыть");
            Console.WriteLine("---------------------------------------------------------------------");
            string path = Console.ReadLine();
            string ext = path.Substring(path.LastIndexOf('.'));
            string[] Text3 = new string[0];
            string json = "";
            string txt = "";
            int r = 0;
            List <Fig> Text = new List<Fig>();

            if (ext == ".xml")
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Fig>));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                  Text = (List<Fig>)xml.Deserialize(fs);
                }
                foreach (Fig f in Text)
                {
                    txt = txt + f.Name + "\n" + f.FigId1 + "\n" + f.FigId1 + "\n";
                    Array.Resize(ref Text3, r + 3);
                    Text3[r] = f.Name;
                    Text3[r + 1] = f.FigId1;
                    Text3[r + 2] = f.FigId2;
                    r = r + 3;
                }
            }
            if (ext == ".json")
            {
                json = File.ReadAllText(path);
                Text = JsonConvert.DeserializeObject<List<Fig>>(json);
                foreach (Fig f in Text) 
                { 
                    txt = txt + f.Name + "\n" + f.FigId1 + "\n" + f.FigId1 + "\n";
                    Array.Resize(ref Text3, r + 3);
                    Text3[r] = f.Name;
                    Text3[r + 1] = f.FigId1;
                    Text3[r + 2] = f.FigId2;
                    r = r + 3;
                }
            }
            if (ext == ".txt")
            {
                txtOpen(path);
                Text3 = File.ReadAllLines(path);
                for (int i = 0; i < Text3.Length - 2; i+=3)
                {
                    Text.Add(new Fig { Name = Text3[i], FigId1 = Text3[i + 1], FigId2 = Text3[i + 2] });
                }
                
            }

            Console.Clear();
            Console.WriteLine("Сейчас значения можно отредактировать. Завершить - Enter");
            Console.WriteLine("---------------------------------------------------------------------");
            foreach (Fig f in Text)
            {
                Console.WriteLine(f.Name);
                Console.WriteLine(f.FigId1);
                Console.WriteLine(f.FigId2);
            }

            ConsoleKeyInfo click;
            Redact curs = new Redact(2, Text3.Length);
            string[] Text4 = curs.Show(Text3);
            Text.Clear();
            for (int i = 0; i < Text4.Length - 2; i += 3)
            {
                Text.Add(new Fig { Name = Text4[i], FigId1 = Text4[i + 1], FigId2 = Text4[i + 2] });
            }

            Console.Clear();
            Console.WriteLine("Сохранить файл в одном из трех форматов (txt, json, xml) - F1. Закрыть - Esc");
            Console.WriteLine("---------------------------------------------------------------------");
            foreach (Fig f in Text)
            {
                Console.WriteLine(f.Name);
                Console.WriteLine(f.FigId1);
                Console.WriteLine(f.FigId2);
                txt = txt + f.Name + "\n" + f.FigId1 + "\n" + f.FigId1 + "\n";
            }

            do
            {
                click = Console.ReadKey();
                if (click.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    Console.WriteLine("Введите путь до файла (вместе с названием), куда вы хотите сохранить текст");
                    Console.WriteLine("---------------------------------------------------------------------");
                    string pathsave = Console.ReadLine();
                    string ext2 = pathsave.Substring(pathsave.LastIndexOf('.'));
                    
                    if (ext2 == ".txt")
                    {
                        File.WriteAllText(pathsave, txt);
                    }
                    if (ext2 == ".json")
                    {
                        json = JsonConvert.SerializeObject(Text);
                        File.WriteAllText(pathsave, json);
                    }
                    if (ext2 == ".xml")
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(List<Fig>));
                        using (FileStream fs = new FileStream(pathsave, FileMode.OpenOrCreate))
                        {
                            xml.Serialize(fs, Text);
                        }
                    }
                    Console.WriteLine("Успешно сохранено! Ура, я это сделала!");
                }
                if (click.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
            }
            while (click.Key != ConsoleKey.F1);
        }
        static string txtOpen(string path)
        {
            string txt = File.ReadAllText(path);
            return txt;
        }
    }
}

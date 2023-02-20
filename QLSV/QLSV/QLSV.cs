using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Linq;

namespace QLSV
{
    public class SV :IComparable<SV>
    {

        public string MSSV { get; set; }
        public string Name { get; set; }
        public string LSH { get; set; }
        public DateTime NS { get; set; }
        public double DTB { get; set; }
        public bool Gender { get; set; }
        public bool Img { get; set; }
        public bool Files { get; set; }
        public bool CCCD { get; set; }

        public SV(string mSSV, string name, string lSH, DateTime nS, double dTB, bool gender, bool img, bool files, bool cCCD)
        {
            MSSV = mSSV;
            Name = name;
            LSH = lSH;
            NS = nS;
            DTB = dTB;
            Gender = gender;
            Img = img;
            Files = files;
            CCCD = cCCD;
        }
        public SV(object[] parameters)
        {
            MSSV = (string)parameters[0];
            Name = (string)parameters[1];
            LSH = (string)parameters[2];
            NS = (DateTime)parameters[3];
            DTB = (double)parameters[4];
            Gender = (bool)parameters[5];
            Img = (bool)parameters[6];
            Files = (bool)parameters[7];
            CCCD = (bool)parameters[8];
        }

        public SV(string input)
        {
            // MSSV|NAME|LSH|NS(dd/mm/yyyy)|DTB|GENDER|IMG|FILES|CCCD
            string[] separator = { "|" };
            string[] fields = input.Split(separator, StringSplitOptions.None);
            MSSV = fields[0];
            Name = fields[1];
            LSH = fields[2];
            NS = Convert.ToDateTime(fields[3]);
            DTB = Convert.ToDouble(fields[4]);
            Gender = Convert.ToBoolean(fields[5]);
            Img = Convert.ToBoolean(fields[6]);
            Files = Convert.ToBoolean(fields[7]);
            CCCD = Convert.ToBoolean(fields[8]);
        }

        public override string ToString()
        {
            return MSSV + "|" + Name + "|" + LSH + "|" + NS.ToString("dd/MM/yyyy") + "|" + DTB.ToString() + "|" + Gender.ToString() + "|" + Img.ToString() + "|" + Files.ToString() + "|" + CCCD.ToString();
        }

        public int CompareTo(SV other)
        {
            throw new NotImplementedException();
        }
    }

    public class SVList
    {
        Comparison<SV> sortMethod;
        public static int byName(SV former, SV latter)
        {
            return former.Name.CompareTo(latter.Name);
        }
        public static int byGrade(SV former, SV latter)
        {
            return former.DTB.CompareTo(latter.DTB);
        }
        public static int byClass(SV former, SV latter)
        {
            return former.LSH.CompareTo(latter.LSH);
        }

        public List<SV> Items { get; set; }
        public SVList()
        {
            Items = new List<SV>();
        }
        public void Sort(string sortOption)
        {
            if (sortOption == "Theo ten") sortMethod += new Comparison<SV>(byName);

            if (sortOption == "Theo DTB") sortMethod += new Comparison<SV>(byGrade);

            if (sortOption == "Theo lop sinh hoat") sortMethod += new Comparison<SV>(byClass);
            Items.Sort(sortMethod);
        }
    }
    public class QLSV
    {
        public DataTable Table { get; set; }
        private string path;
        private static QLSV _database;
        public static QLSV Database
        {
            get
            {
                if (_database == null)
                    _database = new QLSV();
                return _database;
            }
            set { }
        }
        private QLSV()
        {
            Table = new DataTable();

            Table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV", typeof(string)),
                new DataColumn("Ho ten", typeof(string)),
                new DataColumn("Lop sinh hoat", typeof(string)),
                new DataColumn("Ngay sinh", typeof(DateTime)),
                new DataColumn("DTB", typeof(Double)),
                new DataColumn("Gender", typeof(Boolean)),
                new DataColumn("Anh", typeof(Boolean)),
                new DataColumn("Hoc ba", typeof(Boolean)),
                new DataColumn("CCCD", typeof(Boolean))
            });

            path = Environment.CurrentDirectory + "/Record.txt";
            LoadFile();
        }

        private void LoadFile()
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Close();
                }
            }
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                    AddSV(new SV(sr.ReadLine()));
                sr.Close();
            }
        }
        private void SaveFile()
        {
            using (StreamWriter sw = new StreamWriter(path,false))
            {
                for (int index = 0; index < Table.Rows.Count; index++)
                {
                    sw.WriteLine(Table.Rows[index].ToString());
                }
                sw.Close(); 
            }
        }
        public int Exist(string MSSV)
        {
            for (int index = 0; index < Table.Rows.Count; index++)
            {
                if ((string)Table.Rows[index].ItemArray[0] == MSSV)
                {
                    return index;
                }
            }
            return -1;
        }
        public void AddSV(SV item)
        {
            Table.Rows.Add(item.MSSV, item.Name, item.LSH, item.NS, item.DTB, item.Gender, item.Img, item.Files, item.CCCD);
        }
        public void RemoveSV(SV item)
        {
            for (int index = 0; index < Table.Rows.Count; index++)
            {
                if ((string)Table.Rows[index].ItemArray[0] == item.MSSV)
                {
                    Table.Rows.RemoveAt(index);
                    return;
                }
            }
        }
        public void RemoveRangeSV(params SV[] items)
        {
            foreach (SV i in items)
            {
                RemoveSV(i);
            }
        }
        public void Update(SV item)
        {
            for (int index = 0; index < Table.Rows.Count; index++)
            {
                if ((string)Table.Rows[index].ItemArray[0] == item.MSSV)
                {
                    Table.Rows[index].ItemArray[1] = item.Name;
                    Table.Rows[index].ItemArray[2] = item.LSH;
                    Table.Rows[index].ItemArray[3] = item.NS;
                    Table.Rows[index].ItemArray[4] = item.DTB;
                    Table.Rows[index].ItemArray[5] = item.Gender;
                    Table.Rows[index].ItemArray[6] = item.Img;
                    Table.Rows[index].ItemArray[7] = item.Files;
                    Table.Rows[index].ItemArray[8] = item.CCCD;
                    return;
                }
            }
        }
    }
}

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Windows.Forms;

namespace QLSV
{
    public partial class MainForm : Form
    {
        public delegate SVList GetData(string searchString, string classFilterOption );
        public delegate SVList SortList(string sortOption);
        public GetData load;
        public SortList sort;
        
        public MainForm()
        {
            InitializeComponent();

            InitializeClassFilter();
            InitializeSortOptions();
            InitializeDataView();

            load += FilterData;
            sort += LocalSort;
        }

        private void InitializeSortOptions()
        {
            comboBoxSortOption.Items.AddRange(new String[] { "Theo ten", "Theo lop sinh hoat", "Theo DTB" });
        }

        private void InitializeClassFilter()
        {
            List<string> ClassFilter = new List<string>();
            ClassFilter.Add("All");
            string temp;
            for (int index = 0; index < QLSV.Database.Table.Rows.Count; index++)
            {
                temp = (string)QLSV.Database.Table.Rows[index].ItemArray[2];
                if (ClassFilter.IndexOf(temp) == -1)
                {
                    ClassFilter.Add(temp);
                }
            }
            comboBoxLSH.DataSource = ClassFilter;
        }

        private void InitializeDataView()
        {
            datashow.DataSource = QLSV.Database.Table;
        }

        private SVList FilterData(string searchString, string classFilterOption)
        {
            SVList result = new SVList();
            if (classFilterOption == "All" || classFilterOption == null)
            {
                string tempname = "";
                for (int index = 0; index < QLSV.Database.Table.Rows.Count; index++)
                {
                    tempname = (string)QLSV.Database.Table.Rows[index].ItemArray[1];
                    if (tempname.Contains(searchString))
                    result.Items.Add(new SV(QLSV.Database.Table.Rows[index].ItemArray));
                }
            } 

            else if (classFilterOption != "" && classFilterOption != "All" )
            {
                string temp = "";
                string tempname = "";
                for (int index = 0; index < QLSV.Database.Table.Rows.Count; index++)
                {
                    temp = (string)QLSV.Database.Table.Rows[index].ItemArray[2];
                    tempname = (string)QLSV.Database.Table.Rows[index].ItemArray[1];
                    if (temp == classFilterOption || tempname.Contains(searchString))
                        result.Items.Add(new SV(QLSV.Database.Table.Rows[index].ItemArray));
                }
            }
            return result;
        }

        private SVList LoadRows(DataGridViewRowCollection listOfRows)
        {
            SVList result = new SVList();
            foreach (DataGridViewRow row in listOfRows)
            {
                if (!row.IsNewRow)
                {
                    string mssv = row.Cells[0].Value.ToString();
                    string name = row.Cells[1].Value.ToString();
                    string lsh = row.Cells[2].Value.ToString();
                    DateTime ns = DateTime.Parse(row.Cells[3].Value.ToString());
                    double dtb = double.Parse(row.Cells[4].Value.ToString());
                    bool gender = bool.Parse(row.Cells[5].Value.ToString());
                    bool img = bool.Parse(row.Cells[6].Value.ToString());
                    bool files = bool.Parse(row.Cells[7].Value.ToString());
                    bool cccd = bool.Parse(row.Cells[8].Value.ToString());

                    // Create a new SV object using the cell values
                    result.Items.Add(new SV(mssv, name, lsh, ns, dtb, gender, img, files, cccd));
                }
            }
            return result;
        }

        private SVList LocalSort(string sortOption)
        {
            SVList result = LoadRows(datashow.Rows);
            result.Sort(sortOption);
            return result;
        }

        private void buttonSearch_Click(object sender, System.EventArgs e)
        {
            string classFilterOption = comboBoxLSH.SelectedItem.ToString();
            datashow.DataSource = load(textBoxSearch.Text, classFilterOption ).Items;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DetailForm AddForm = new DetailForm();
            AddForm.ShowDialog();
            //defining
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            DetailForm EditForm = new DetailForm();
            EditForm.ShowDialog();
            //defining
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (comboBoxSortOption.SelectedItem== null) return;
            datashow.DataSource = sort(comboBoxSortOption.SelectedItem.ToString()).Items;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLSV
{
    public partial class DetailForm : Form
    {
        public DetailForm()
        {
            InitializeComponent();

            InitializeClassFilter();
        }
        private void InitializeClassFilter()
        {
            List<string> ClassFilter = new List<string>();
            ClassFilter.Add("");
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

        public void ShowForm(string MSSV = "")
        {
            if (MSSV == "")
            {
                //Add mode
                this.ShowDialog();
            }
            else
            {
                SV target = QLSV.Database.GetSV(MSSV);
                if (target == null)
                {
                    MessageBox.Show("Something error!");
                    return;
                }
                else
                {
                    //prepare to show in edit mode
                    textBoxMSSV.Enabled = false;
                    textBoxMSSV.Text = target.MSSV;
                    textBoxName.Text = target.Name;
                    textBoxDTB.Text = target.DTB.ToString();
                    comboBoxLSH.Text = target.LSH;
                    dateTimePickerNS.Value = target.NS;
                    if (target.Gender)
                    {
                        radioButtonNam.Checked = true;
                    }
                    else radioButtonNu.Checked = true;
                    checkBoxImg.Checked = target.Img;
                    checkBoxFile.Checked = target.Files;
                    checkBoxCCCD.Checked = target.CCCD;
                    this.ShowDialog();
                }
            }
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                SV pendingUpdate = new SV(
                textBoxMSSV.Text,
                textBoxName.Text,
                comboBoxLSH.Text,
                dateTimePickerNS.Value,
                Convert.ToDouble(textBoxDTB.Text),
                radioButtonNam.Checked,
                checkBoxImg.Checked,
                checkBoxFile.Checked,
                checkBoxCCCD.Checked
            );
                QLSV.Database.Update(pendingUpdate);
                this.Dispose();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Vui long nhap day du cac truong con thieu");
            }
        }
    }
}

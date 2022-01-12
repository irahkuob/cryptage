using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
namespace cryptage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo Files = new DirectoryInfo(
            Application.StartupPath.ToString() + @"\Files");
            foreach (FileInfo file in Files.GetFiles())
            {
                File.Delete(file.FullName); //حذف جميع الملفات القديمة
            }


        }
    }
}

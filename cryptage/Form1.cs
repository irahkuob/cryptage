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
       
        void CanEncrypt()
        {
            if (listView1.Items.Count > 0 & File.Exists(textBox1.Text))
                Encrypt.Enabled = true;
            else
                Encrypt.Enabled = false;
            Remove.Enabled = false;
        }
        void RarFiles()
        {
            var f = File.Create(Application.StartupPath + "\\RarFiles.bat");
            f.Close();
            StreamWriter FileW = File.AppendText(Application.StartupPath +"\\RarFiles.bat");
        FileW.WriteLine("Rar a Archive Files");
          FileW.Close();
           Process p = new Process();
        ProcessStartInfo pInfo = new ProcessStartInfo(f.Name);
        p.StartInfo = pInfo;
           p.Start(); 
            p.WaitForExit();

    }
        void BatEncrypt()
        {
            var f = File.Create(Application.StartupPath + "\\Encrypt.bat");
            f.Close();
            StreamWriter FileW = File.AppendText(Application.StartupPath +
            "\\Encrypt.bat");
            FileW.WriteLine("copy /b image.jpg + archive.rar encrypted.jpg");
            FileW.Close();
            Process.Start(f.Name);
        }
        void BatDecrypt(string path)
        {
            var f = File.Create(Application.StartupPath + "\\Decrypt.bat");
            f.Close();
            StreamWriter FileW = File.AppendText(Application.StartupPath +"\\Decrypt.bat");
            FileW.WriteLine("ren \"{0}\" DecryptedFiles.rar", path);
            FileW.Close();
            Process.Start(f.Name);
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
             if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] Files = open.FileNames;
                foreach (string FileItem in Files)
                {
                    FileInfo file = new FileInfo(FileItem);
                    ListViewItem L;
                    L = listView1.Items.Add(file.Name);
                    long size = file.Length;
                    string Unit = "Bytes";
                    if (size < 1e6) //1e6  تعني1x10^6
                    {
                        Unit = " KB";
                        size /= 1024;
                    }
                    else if (size > 1e6 & size < 1e9)
                    {
                        Unit = " MB";
                        size /= 1048576;
                    }
                    L.SubItems.Add(size.ToString() + Unit);
                    L.SubItems.Add(file.FullName);
                }
            }
            CanEncrypt();
        }
        

        private void ListView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            foreach (ListViewItem L in listView1.Items)
            {
                if (L.Selected)
                    Remove.Enabled = true;
                else
                    Remove.Enabled = false;
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem L in listView1.Items)
                if (L.Selected)
                    L.Remove();
            CanEncrypt();
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            string fullpath;
            foreach (ListViewItem Item in listView1.Items)
            {
            fullpath = Application.StartupPath + @"\Files\" +
            Item.SubItems[0].Text;
                File.Copy(Item.SubItems[2].Text, fullpath);
            }
            File.Copy(textBox1.Text, Application.StartupPath + "\\image.jpg");
            RarFiles();
            BatEncrypt();
            MessageBox.Show("Files encrypted successfully!", "FilesToJPG ByEng27");
            Process.Start(Application.StartupPath);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG File | *.jpg";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = open.FileName;
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG File | *.jpg";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BatDecrypt(open.FileName);
                MessageBox.Show("Files decrypted successfully!", "FilesToJPG By Eng27");
                Process.Start(open.FileName.Replace(open.SafeFileName, "DecryptedFiles.rar"));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo Files = new DirectoryInfo(
            Application.StartupPath.ToString() + @"\Files");
            foreach (FileInfo file in Files.GetFiles())
            {
                File.Delete(file.FullName); //حذف جميع الملفات القديمة
            }

            File.Delete(Application.StartupPath + "\\image.jpg");
            File.Delete(Application.StartupPath + "\\archive.rar");
            File.Delete(Application.StartupPath + "\\DecryptedFiles.rar");
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            CanEncrypt();

        }
    }
    }



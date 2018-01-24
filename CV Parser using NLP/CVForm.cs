﻿using CV_Parser_using_NLP.Data;
using CV_Parser_using_NLP.Dependency;
using CV_Parser_using_NLP.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CV_Parser_using_NLP
{
    public partial class CVForm : Form
    {
        string directory;
        bool updateData = false;
        public CVForm()
        {
            InitializeComponent();
            Enabled = false;
            Helper.InitializeDependencies(LoadingPanel, this);     
            if (updateData)
            {
                Helper.UpdateData();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string skills = skillsTextbox.Text;
            string education = educationQualificationTextbox.Text;
            string experience = experienceTextBox.Text;

            if (skills != "" && education != "" && experience != "")
            {
                Parser.InitializeRequirementData(skills, education, experience);                
            }
            else Helper.ShowError("All fields are required.");

            if (Directory.Exists(cvDirectoryTextbox.Text)) {

                DirectoryInfo dir = new DirectoryInfo(directory);
                FileInfo[] PDFFiles = dir.GetFiles("*.pdf");
                
                if (!(PDFFiles.Length == 0))
                { 


                                    /*****************/
                    /************/CVParserUsingNLP();/*************/
                                    /*****************/


                }

                else if (PDFFiles.Length == 0)
                {
                    Helper.ShowError("No CVs found in the directory. Please make sure all CVs are in .PDF format ");
                }

                else {
                    Helper.ShowError("There was an unexpected error. Please try again.");
                }

            }
            else Helper.ShowError("Directory doesn't exist.");
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    directory = folderBrowserDialog.SelectedPath;
                }
                cvDirectoryTextbox.Text = directory;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Parser parser = new Parser(directory);
            }
        }

        private void CVParserUsingNLP()
        {
            Parser.GetPDFFiles();
        }
    }
}

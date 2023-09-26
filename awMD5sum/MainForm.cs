/*
 * Created by SharpDevelop.
 * User: jason_su
 * Date: 11/19/2021
 * Time: AM 11:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Resources;


namespace awMD5sum
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		string spath = "";
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public string checkMD5(string filename)
		{
			String ret ="error	" + filename;
		    using (var md5 = MD5.Create())
		    {
		        using (var stream = File.OpenRead(filename))
		        {
		            var hash = md5.ComputeHash(stream);
		            ret = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant() + "	" + Path.GetFileName(filename);
		            return ret;
		        }
		    }
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			OpenFileDialog choofdlog = new OpenFileDialog();
			choofdlog.Filter = "All Files (*.*)|*.*";
			choofdlog.FilterIndex = 1;
			//choofdlog.Multiselect = true;
			
			if (choofdlog.ShowDialog() == DialogResult.OK)    
			{     
				string sFileName = choofdlog.FileName; 
				//string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true  
				
				this.spath = Path.GetFullPath (sFileName);
				textBox1.Text = checkMD5(sFileName);				
			}
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = "Custom Description"; 
			
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				string list = "";
				this.spath = fbd.SelectedPath;
				
			    string [] fileEntries = Directory.GetFiles(fbd.SelectedPath);
			    for (int i = 0; i!= fileEntries.Length; i++)
			    {
			    	list += checkMD5(fileEntries[i]) + "\r\n";
			    }
			    textBox1.Text = list;
			}
		}
		void MainFormLoad(object sender, EventArgs e)
		{
	
		}
		void Button3Click(object sender, EventArgs e)
		{
			if (this.spath.Length > 0 && textBox1.Text.Length > 0)
			{
				try
				{
					File.WriteAllText(this.spath + "\\md5.txt", textBox1.Text);
					MessageBox.Show("Done!!", "");
				}
				catch{
					MessageBox.Show("Write md5.txt failed!!", "Warning");
				}			
			}
			else
				MessageBox.Show("There is no content!!", "Warning");
		}
	}
}

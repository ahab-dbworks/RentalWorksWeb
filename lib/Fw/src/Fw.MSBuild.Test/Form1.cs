using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fw.MSBuildTasks;
using Fw.Json.ValueTypes;
using System.Xml.Serialization;
using System.IO;

namespace Fw.MSBuild.Test
{
    public partial class Form1 : Form
    {
        //------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }
        //------------------------------------------------------------------------------
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = dialogSelectPath.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //------------------------------------------------------------------------------
        private void dialogSelectPath_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                txtPath.Text = dialogSelectPath.FileName;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //------------------------------------------------------------------------------
        private void btnDebug_Click(object sender, EventArgs e)
        {
            FwJSAppBuilder config;
            
            Cursor.Current = Cursors.WaitCursor;
            config = new FwJSAppBuilder();
            config.BuildEngine = new VirtualBuildEngine();
            config.Publish = false;
            config.Version = "0.0.0.0";
            string projectPath = Path.GetDirectoryName(txtPath.Text);
            string pathVersion = Path.Combine(projectPath, "version.txt");
            if (File.Exists(pathVersion))
            {
                config.Version = File.ReadAllText(pathVersion);
            }
            string sourcePath = Path.GetDirectoryName(projectPath);
            string solutionPath = Path.GetDirectoryName(sourcePath);
            
            // to support project hosted under WebApi
            if (solutionPath.EndsWith("\\src"))
            {
                solutionPath = Path.GetDirectoryName(solutionPath);
            }
            
            config.Build(txtPath.Text, solutionPath);
            Cursor.Current = Cursors.Default;
        }
        //------------------------------------------------------------------------------
        private void btnRelease_Click(object sender, EventArgs e)
        {
            FwJSAppBuilder config;
            
            Cursor.Current = Cursors.WaitCursor;
            config = new FwJSAppBuilder();
            config.BuildEngine = new VirtualBuildEngine();
            config.Publish = true;
            config.Version = "0.0.0.0";
            string projectPath = Path.GetDirectoryName(txtPath.Text);
            string pathVersion = Path.Combine(projectPath, "version.txt");
            if (File.Exists(pathVersion))
            {
                config.Version = File.ReadAllText(pathVersion);
            }
            string sourcePath = Path.GetDirectoryName(projectPath);
            string solutionPath = Path.GetDirectoryName(sourcePath);

            // to support project hosted under WebApi
            if (solutionPath.EndsWith("\\src"))
            {
                solutionPath = Path.GetDirectoryName(solutionPath);
            }

            config.Build(txtPath.Text, solutionPath);
            Cursor.Current = Cursors.Default;
        }
        //------------------------------------------------------------------------------
        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            FwApplicationSchema.Load(txtPath.Text);
        }
        //------------------------------------------------------------------------------
        public static T DeserializeFile<T>(string filename)
        {
            string xml = File.ReadAllText(filename);
            return (T)DeserializeString<T>(xml);
        }
        //------------------------------------------------------------------------------
        public static T DeserializeString<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            T obj = (T)serializer.Deserialize(reader);
            return obj;
        }
        //------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //------------------------------------------------------------------------------

        
    }
}

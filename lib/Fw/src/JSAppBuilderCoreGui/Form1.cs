using JSAppBuilderCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace JSAppBuilderGui
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //------------------------------------------------------------------------------
        private void btnDebug_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (File.Exists(txtPath.Text))
                {
                    FwJSAppBuilder config = new FwJSAppBuilder();
                    config.Publish = false;
                    string projectPath = Path.GetDirectoryName(txtPath.Text);
                    string pathVersion = Path.Combine(projectPath, "version.txt");
                    config.Version = "0.0.0.0";
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
                }
                else
                {
                    MessageBox.Show($"File doesn't exist: \"{txtPath.Text}\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to build Release: \"{txtPath.Text}\"\n{ex.Message + ex.StackTrace}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        //------------------------------------------------------------------------------
        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (File.Exists(txtPath.Text))
                {
                    FwJSAppBuilder config = new FwJSAppBuilder();
                    config.Publish = true;
                    string projectPath = Path.GetDirectoryName(txtPath.Text);
                    string pathVersion = Path.Combine(projectPath, "version.txt");
                    config.Version = "0.0.0.0";
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
                }
                else
                {
                    MessageBox.Show($"File doesn't exist: \"{txtPath.Text}\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to build Release: \"{txtPath.Text}\"\n{ex.Message + ex.StackTrace}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        //------------------------------------------------------------------------------
        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txtPath.Text))
                {
                    FwJSAppBuilder jsAppBuilderConfig = this.DeserializeFile<FwJSAppBuilder>(txtPath.Text);
                }
                else
                {
                    MessageBox.Show($"File doesn't exist: \"{txtPath.Text}\"");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Unable to deserialize: \"{txtPath.Text}\"\n{ex.Message + ex.StackTrace}");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        //------------------------------------------------------------------------------
        public T DeserializeFile<T>(string filename)
        {
            string xml = File.ReadAllText(filename);
            return (T)DeserializeString<T>(xml);
        }
        //------------------------------------------------------------------------------
        public T DeserializeString<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml);
            T obj = (T)serializer.Deserialize(reader);
            return obj;
        }
        //------------------------------------------------------------------------------
    }
}

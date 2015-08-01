using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SpaceEngineersDSModlistGenerator
{
    public partial class Form1 : Form
    {
        private SpaceEngineersDedicatedServerconfigModlistReader ModlistReader;

        AboutBox1 aboutBox;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ModlistReader = new SpaceEngineersDedicatedServerconfigModlistReader(openFileDialog1.FileName);
                ModlistReader.parseCfg();
                listBox1.Items.Clear();
                int chunkCount = ModlistReader.getChunkCount();
                for (int i = 0; i < chunkCount; i++)
                {
                    listBox1.Items.Add("Modliste " + (i+1).ToString() + "/" + chunkCount.ToString());
                }                
            }
        }

        private void öffenenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int chunkId = listBox1.SelectedIndex;
            textBox1.Text = (new ModlistChunkView()).renderChunk(ModlistReader.getChunk(chunkId), chunkId, ModlistReader.getChunkCount()).ToString();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            aboutBox = new AboutBox1();

            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length > 0)
            {
                this.Text = ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BrewMLLib;
using BrewMLLib.DAL;
using BrewMLLib.Migrations;
using BrewMLLib.Models;


namespace FormTesting
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        private static MDIParent1 instance = null;

        public MDIParent1()
        {
            InitializeComponent();
        }

        public static MDIParent1 getInstance()
        {
            if (instance != null)
                return instance;
            else
                return new MDIParent1();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        public void CreateNewPlant(String name)
        {
            PlantForm newForm = new PlantForm();
            newForm.Text = name;
            newForm.MdiParent = this;

            /*
            Thread t = new Thread(new ThreadStart(newForm.DoYourBusiness));
            t.IsBackground = true;
            t.Start();*/
            newForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                ReadFile(FileName);
            }
        }


        private void ReadFile(String fname)
        {
            FluentEQType types = new FluentEQType();
            Plant p = new Plant();
            BrewDBContext db = new BrewDBContext();
            BrewML b = new BrewML();
            FluentPlant pd = new FluentPlant();
            FluentRecipe rec = new FluentRecipe();


            string[] lines = System.IO.File.ReadAllLines(@fname);
            for(int i=0; i<lines.Length; i++)
            {
                String input = lines[i];
                String[] words = input.Split(' ');
                for(int ii=1; ii<words.Length;ii++)
                    words[ii] = words[ii].Replace('_', ' ');
                



                if (words[0] == "Add_EQType")
                    types.AddEQType(words[1]);
                else if (words[0] == "Add_Plant")
                {
                    pd.AddPlant(words[1]);
                    this.CreateNewPlant(words[1]);
                }
                else if (words[0] == "Add_Recipe")
                    rec.AddRecipe(words[1]);
                else if (words[0] == "Plant")
                {
                    FluentPlant fp = (FluentPlant)pd.ForPlant(words[1]);
                    do
                    {
                        i++;
                        String tempinput = lines[i];
                        String[] tempWords = tempinput.Split(' ');
                        for (int ii = 1; ii < tempWords.Length; ii++)
                            tempWords[ii] = tempWords[ii].Replace('_', ' ');

                        if (tempWords[0] == "Add_ControlLoop")
                            fp.HasLoops().AddControlLoop(tempWords[1], tempWords[2]).SetSetPoint(Convert.ToInt32(tempWords[3]));
                        else if (tempWords[0] == "Update_ControlLoop")
                            fp.HasLoops(tempWords[1]).SetSetPoint(Convert.ToInt32(tempWords[2]));
                        else if (tempWords[0] == "Add_Unit")
                            fp.HasUnits().AddUnit(tempWords[1]);

                    } while (lines[i] != "--------------------------------------------");
                }
                else if (words[0] == "Recipe")
                {
                    FluentRecipe fr = (FluentRecipe)rec.ForRecipe(words[1]);
                    do
                    {
                        i++;
                        String tempinput = lines[i];
                        String[] tempWords = tempinput.Split(' ');
                        for (int ii = 1; ii < tempWords.Length; ii++)
                            tempWords[ii] = tempWords[ii].Replace('_', ' ');

                        if (tempWords[0] == "SetBrandDescription")
                            rec.SetBrandDescription(tempWords[1]);
                        else if (tempWords[0] == "SetQualityTarget")
                            rec.SetQualityTarget(tempWords[1]);
                        else if (tempWords[0] == "Add_Operation")
                            rec.HasRecOperations().AddOperation(tempWords[1]).SetSetPoint(Convert.ToInt32(tempWords[2]));

                    } while (lines[i] != "--------------------------------------------");
                }

            }
        }


        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            MDIParent1.getInstance().WindowState = FormWindowState.Maximized;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            
        }

    }
}

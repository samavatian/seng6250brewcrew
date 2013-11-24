using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

using BrewMLLib;
using BrewMLLib.DAL;
using BrewMLLib.Models;


namespace FormTesting
{
    public partial class PlantForm : Form
    {
        // This delegate enables asynchronous calls for setting the text property
        delegate void SetTextCallback(string text);
        delegate void Refresher();

        public PlantForm()
        {
            InitializeComponent();
        }
        
        public static Point LastPointForTheForm;
        private String name;
        private static Object Lock = new object();
        private FluentPlant FP;
        private FluentRecipe FR;
        BrewDBContext contx = new BrewDBContext();



        private void PlantForm_Load(object sender, EventArgs e)
        {
            FluentPlant pd = new FluentPlant();
            FluentRecipe rec = new FluentRecipe();
            name = this.Text;
            FP = (FluentPlant) pd.ForPlant(name);


            ShowPlant(FP.GetPlant());

            this.StartPosition = FormStartPosition.Manual;
            this.Location = LastPointForTheForm;
            LastPointForTheForm.X += this.Width;
            if (!IsOnScreen(this))
            {
                LastPointForTheForm.X = 0;
                LastPointForTheForm.Y += 400;
                this.Location = LastPointForTheForm;
                LastPointForTheForm.X += this.Width;
            }
            


            this.Refresh();

            //this.DoYourBusiness();
            Lock = new Object();

        }


        private void ShowPlant(Plant p)
        {
            //Get all equipments and loops to find out the suitable size of this form

            List<Unit> UList = contx.Plants.SelectMany(g => g.Units).Where(g => g.PlantID == p.PlantID).ToList();
            this.Width = UList.Count * 200;
            this.Height = 400 ;
            this.pictureBox1.Width = this.Width;
            this.pictureBox1.Height = this.Height;

            // Create pen and brush
            Pen blackPen = new Pen(Color.Black, 3);
            Pen whitePen = new Pen(Color.White, 3);
            Pen redPen = new Pen(Color.Red, 3);
            SolidBrush brush = new SolidBrush(Color.Black);
            Font font = new System.Drawing.Font("Arial", 11);
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics gr = Graphics.FromImage(bmp);


            //For each unit, draw a tank (red rectangle)
            Unit[] UArray = UList.ToArray();
            for(int i=0; i<UList.Count; i++)
            {
                // Draw a rectangle to put each tank into a frame
                gr.FillRectangle(brush, new Rectangle(i * 200, 0, 200, 400));
                gr.DrawRectangle(whitePen, new Rectangle(i * 200, 0, 200, 400));

                //Draw the unit ??
                gr.FillRectangle(new SolidBrush(Color.Red), new Rectangle(i * 200 + 50, 100, 100, 150));
                gr.DrawString(UArray[i].UnitName, font, brush, i * 200 + 60, 110);

                //then draw the loops ??
                Unit u = UArray[i];
                List<BrewMLLib.EQControlLoop> ConList = contx.Units.SelectMany(g => g.UnitLoops).Where(g => g.UnitID == u.UnitID).ToList();
                BrewMLLib.EQControlLoop[] CList = ConList.ToArray();
                int top=0, left=0;
                for (int j = 0; j < CList.Length; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Parent = this.pictureBox1;
                    //this.Controls.Add(pic);
                    left = (i * 200 + 40) + (j % 2) * 110;
                    top = 100 + (j / 2) * 50;
                    pic.Location = new Point(left, top);
                    pic.Width = pic.Height = 10;
                    pic.Visible = true;
                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pic, CList[j].EquipName);

                    Bitmap bmp2 = new Bitmap(10, 10);
                    Graphics gg = Graphics.FromImage(bmp2);
                    gg.DrawRectangle(whitePen, new Rectangle(0,0,10,10));
                    pic.Image = bmp2;
                }

            }



            this.pictureBox1.Image = bmp;
        }



        private void PlantForm_Activated(object sender, EventArgs e)
        {
           
        }

        [STAThread]
        public void DoYourBusiness()
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += new ElapsedEventHandler(Report);
            t.Enabled = true;

            /*
            for (int i = 0; i < 10; i++)
            {
                if (this.Text == "")
                    this.Text = name;
                else
                    this.Text = "";
                Thread.Sleep(1000);
            }
            */
        }


        private void Report(object source, ElapsedEventArgs e)
        {
            lock (Lock)
            {
                
                if (this.Text == "")
                    this.setText(name);
                else
                    this.setText("");
            }
        }

        private void setText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.Text = text;
            }
        }


        public bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Rectangle formRectangle = new Rectangle(form.Left, form.Top, form.Width, form.Height);

                if (screen.WorkingArea.Contains(formRectangle))
                {
                    return true;
                }
            }

            return false;
        }

        

        
    }
}

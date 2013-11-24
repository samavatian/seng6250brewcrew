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

using FormTesting;
using BrewMLLib;
using BrewMLLib.DAL;
using BrewMLLib.Models;

namespace FormTesting
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initialize the position of the first form
            PlantForm.LastPointForTheForm = new Point(0, 0);
            MDIParent1 MDIForm = new MDIParent1();
            MDIForm.Show();

            /*
            BrewML b = new BrewML();

            BrewDBContext db = new BrewDBContext();


            Plant p = new Plant();
            FluentPlant pd = new FluentPlant();

            FluentEQType types = new FluentEQType();
            String PlantName;

            PlantName = "Little Jakes";
            pd.AddPlant(PlantName);
            MDIForm.CreateNewPlant(PlantName);

            PlantName = "Big Jakes";
            pd.AddPlant(PlantName);
            MDIForm.CreateNewPlant(PlantName);

            PlantName = "Huge Jakes";
            pd.AddPlant(PlantName);
            MDIForm.CreateNewPlant(PlantName);

            */


            //MessageBox.Show("Wait For Them To Finish !");
            
            Application.Run(MDIForm);
           
            /*
            Form[] flist = MDIForm.MdiChildren;
            foreach (Form f in flist)
            {
                f.Activate();
                f.Refresh();
                
            }
             */
        }
    }
}

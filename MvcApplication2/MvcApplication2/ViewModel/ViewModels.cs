using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MvcApplication2.Models;

namespace MvcApplication2.ViewModel
{
    public class ViewModels
    {
    }

    public class ViewModelPlants
    {
        public Plant Plant { get; set; }
        public List<Plant> Plants { get; set; }

    }

    public class ViewModelEQControlLoops
    {
        public List<EQControlLoop> Loops { get; set; }

    }

    public class ViewPlantDetails
    {

        public Plant Plant { get; set; }
        public List<EQControlLoop> EQControlLoops { get; set; }
        public List<EQAuxilary> EQAuxilarys { get; set; }
        public List<EQVessel> EQVessels { get; set; }



    }

}
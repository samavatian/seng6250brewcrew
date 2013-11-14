using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrewMLLib.Models;

namespace BrewMLLib.DAL
{


    public interface IPlantActions
    {
        Plant GetPlant();

        IPlantActions InPlant(string a);

        IControlLoopActions AddLoop();

        IPlantActions Final();
    }

    public interface IControlLoopActions
    {

        IControlLoopActions WithName(string name);
        IControlLoopActions AsType(string name);

        IControlLoopActions Final();
    }

    public class PlantActions : IPlantActions
    {

        private Plant _plant;
        BrewDBContext cont = new BrewDBContext();

        public Plant GetPlant()
        {
            return _plant;
        }

        public IPlantActions InPlant(string name)
        {

            _plant = cont.Plants.FirstOrDefault(p => p.PlantName == name);
            
            return this;

        }
        public IControlLoopActions AddLoop()
        {


            IControlLoopActions loopact = new ControlLoopActions(this.GetPlant());


            return loopact;

        }

        public IPlantActions Final()
        {

            cont.SaveChanges();
            return this;
        }
        

    }
    public class ControlLoopActions : IControlLoopActions
    {
        private EQControlLoop _loop;
        BrewDBContext cont = new BrewDBContext();


        public ControlLoopActions(Plant p)
        {
            //EQType ty = new EQType();
            //ty.TypeDescription = "asdf";

            //cont.EQTypes.Add(ty);
            //cont.SaveChanges();

            _loop = new EQControlLoop();
          
            _loop.PlantID = p.PlantID;
            //_loop.EQTypeID = ty.EQTypeID;


        }
        //public IControlLoopActions Create(IPlantActions f)
        //{

        //    _loop = new EQControlLoop();
        //    _loop.Plant = f.GetPlant();
        //    _loop.EQType = new EQType();
        //    return this;

        //}

        public IControlLoopActions WithName(string name)
        {
            _loop.EquipName = name;
            return this;
        }

        public IControlLoopActions AsType(string name)
        {
            
            _loop.EQTypeID = cont.EQTypes.FirstOrDefault(g=>g.TypeDescription==name).EQTypeID;
            return this;
        }

        public IControlLoopActions Final()
        {
            cont.EQControlLoops.Add(_loop);

            cont.SaveChanges();
            return this;
        }
    }

}






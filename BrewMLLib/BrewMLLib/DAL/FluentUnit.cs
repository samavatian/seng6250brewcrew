using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//using BrewMLLib.DAL;
//using BrewMLLib.Models;

namespace BrewMLLib
{


    public interface IEQUnitOperation
    {

        IEQUnitOperation AddEQUnitOperation(string Name, string UnitName);
        IEQUnitOperation ForEQUnitOperation(string Name);

        IEQUnitOperation SetUnitForThisOp(string Name);


    }

    public interface IFluentUnit
    {


        IFluentUnit AddUnit(string UnitName);
        IFluentUnit ForUnit(string Name);

        Unit GetUnit();
        Plant GetPlant();

        IFluentUnit SetUnitName(string Name);
        //IFluentUnit SetUnitDesc(string Description);
        //IFluentRecipe SetQualityTarget(string QTarget);


        IFluentControlLoop HasLoops();
        //IFluentControlLoop AttachControlLoop(string Name);


        //        IFluentUnit HasUnitAux();
        //        IFluentUnit HasUnitVessels();

        //IFluentUnit HasCurrentBatch();
        IFluentUnit HasCurrentEQOperation();
        IFluentUnit HasAvailableOperations();


        //IFluentUnit AttachToPlant(string PlantName);
    }


    public class FluentUnit : IFluentUnit
    {

        private Unit _unit;
        private Plant _plant;
        private List<EQControlLoop> _loops;
        private IFluentPlant _parent;
        //private bool _isnew;

        BrewDBContext contx = new BrewDBContext();


        public FluentUnit()
        {

            //return this;

        }


        public FluentUnit(Plant plant)
        {


            _plant = plant;
            


        }



        public FluentUnit(Plant plant, string unitname)
        {

            //Plant plant = parent.GetPlant();
            //Plant plant = parent.GetPlant();
            _plant = plant; 
                //contx.Plants.FirstOrDefault(g => g.PlantID == plant.PlantID);

            

//            _unit = contx.Units.FirstOrDefault(g => g.UnitName == Name);
            _unit = contx.GetUnitByPlantByName(_plant.PlantName, unitname);
        }


        public Unit GetUnit()
        {

            return this._unit;
        }

        public Plant GetPlant()
        {

            return this._plant;

        }
        public IFluentUnit SetUnitName(string Name)
        {
            this._unit.UnitName = Name;
            return this;

        }


        public IFluentUnit ForUnit(string unitname)
        {
            //_unit = contx.Units.FirstOrDefault(g => g.UnitName == s);
            //_plant = contx.Plants.FirstOrDefault(g => g.Units.Contains(_unit));
            //_unit = contx.
            _unit = contx.GetUnitByPlantByName(_plant.PlantName, unitname);

            return this;

        }

        public IFluentUnit AddUnit(string UnitName)
        {


            _unit = contx.CreateUnit(_plant.PlantID, UnitName);

            //////////Plant p = _parent.GetPlant();
            ////////_unit = contx.Units.FirstOrDefault(l => l.UnitName == UnitName);
            //////////_plant = contx.Plants.FirstOrDefault(p => p.PlantName == PlantName);

            //////////_isnew = false;
            ////////if (_unit == null & _plant !=null)
            ////////{

            ////////    _isnew = false;

            ////////    _unit = new Unit();

            ////////    //if (_plant != null)
            ////////        _unit.Plant = contx.Plants.FirstOrDefault(h => h.PlantID == _plant.PlantID);
            ////////    _unit.UnitName = UnitName;
            ////////    _unit.AvailableOperations = new List<EQUnitOperation>();
            ////////    _unit.InputUnits = new List<Unit>();
            ////////    _unit.OutputUnits = new List<Unit>();
            ////////    _unit.UnitAux = new List<EQAuxilary>();
            ////////    _unit.UnitLoops = new List<EQControlLoop>();
            ////////    _unit.UnitVessels = new List<EQVessel>();

            ////////    contx.Units.Add(_unit);
            ////////    contx.SaveChanges();
            ////////    //if (_plant != null)
            ////////        contx.Plants.FirstOrDefault(g => g.PlantID == _unit.Plant.PlantID).Units.Add(contx.Units.FirstOrDefault(e => e.UnitName == _unit.UnitName));
            ////////    contx.SaveChanges();

            ////////}
            //////////else
            //////////{
            //////////    if (_plant != null)
            //////////        _unit.Plant = contx.Plants.FirstOrDefault(h => h.PlantID == _plant.PlantID);

            //////////    if (_plant != null)
            //////////        contx.Plants.FirstOrDefault(g => g.PlantID == _unit.Plant.PlantID).Units.Add(contx.Units.FirstOrDefault(e => e.UnitName == _unit.UnitName));

            //////////    contx.SaveChanges();

            //////////}
            ////////_unit = contx.Units.FirstOrDefault(l => l.UnitName == UnitName);
            return this;

        }


        public IFluentControlLoop HasLoops()
        {

            IFluentControlLoop eq = new FluentControlLoop(this._unit,this._plant);

           

            return eq;


        }

        //public IFluentControlLoop AttachLoop(string name)
        //{


        //}

        public IFluentUnit HasCurrentEQOperation()
        {
            _loops = contx.Units.FirstOrDefault(u => u.UnitID == this._unit.UnitID).UnitLoops.ToList();

            return this;

        }
        public IFluentUnit HasAvailableOperations()
        {
            _loops = contx.Units.FirstOrDefault(u => u.UnitID == this._unit.UnitID).UnitLoops.ToList();
            return this;
        }



    }

}

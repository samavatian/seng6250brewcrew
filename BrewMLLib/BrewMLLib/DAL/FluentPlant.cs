using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using BrewMLLib.Models;

namespace BrewMLLib
{

    /// <summary>
    /// comment to test git hub
    //// comment to test git hub stacy
    //// comment add second change stacy
//          you dog this is working
    //
    /// </summary>
    public interface IFluentPlant
    {
       

        IFluentPlant ForPlant(string Name);
        IFluentPlant AddPlant(string Name);
        
        Plant GetPlant();
        

        IFluentControlLoop HasLoops();
        IFluentControlLoop HasLoops(string Name);
        //IFluentControlLoop HasLoopsInUnit(string UnitName);


        IFluentUnit HasUnits();
        IFluentUnit HasUnits(string Name);
        //IFluentUnit AttachUnit(string Name);

        //int Final();

        string GetMessage();

    }


    public interface IFluentEQType
    {

        IFluentEQType AddEQType(string Name);
        IFluentEQType ForEQType(string Name);

        EQType GetEQType(string Name);

        //int Final();

        string GetMessage();

    }

    public interface IFluentControlLoop
    {

        IFluentControlLoop ForControlLoop(string Name);
        IFluentControlLoop AddControlLoop(string Name, string EqType="");
        //IFluentControlLoop AddControlLoop(string Name);

        float? GetSetPoint();

        IFluentControlLoop SetSetPoint(float SetPoint);
        IFluentControlLoop HasType(string EqType);

        //IFluentControlLoop AttachToUnit(string Unit);


        //int Final();

        string GetMessage();

    }





    /// <summary>
    /// 
    /// </summary>
    public class FluentPlant : IFluentPlant
    {
        //private bool _isnew;
        private Plant _plant;
      
        private BrewDBContext contx = new BrewDBContext();

        private string _message;

        public string GetMessage() { return _message; }

        public Plant GetPlant()
        {
            return _plant;
        }

        public IFluentPlant ForPlant(string name)
        {

            try
            {
                
//                _plant = contx.Plants.FirstOrDefault(p => p.PlantName == name);
                _plant = contx.GetPlantByPlantName(name);

                _message = "";

            }
            catch (Exception ex)
            {
                _message = _message + ex.ToString();
            }

            return this;

        }

        public IFluentPlant AddPlant(string name)
        {

           
//            _plant = contx.Plants.FirstOrDefault(p => p.PlantName == name);
            Plant plant = contx.GetPlantByPlantName(name);
            if (plant == null)
            {
                try
                {

                    plant = new Plant();
                    plant.PlantName = name;
                    plant.BrewPubMenu = "default";
                    plant.PlantAddress = "default";
                    plant.PlantLocation = "Down under Default";

                    //plant.PlantAux = new List<EQAuxilary>();
                    //plant.PlantLoops = new List<EQControlLoop>();
                    //plant.PlantVessels = new List<EQVessel>();

                    //plant.Units = new List<Unit>();

                    //contx.Plants.Add(_plant);
                    //contx.SaveChanges();
                    _plant = contx.EQAddPlant(plant);

//                    _plant = contx.Plants.FirstOrDefault(g => g.PlantName == name);
                    //_plant = contx.GetPlantByPlantName(name);
                
                }
                catch (Exception ex)
                {
                    _message = _message + ex.ToString();
                }
            }

            return this;


        }
        

        public IFluentControlLoop HasLoops()
        {
            IFluentControlLoop eq = new FluentControlLoop(this._plant);

            _message = _message + eq.GetMessage();

            return eq;

        }
        public IFluentControlLoop HasLoops(string name)
        {
            IFluentControlLoop eq = new FluentControlLoop(this._plant, name);

            _message = _message + eq.GetMessage();

            return eq;

        }


        public IFluentUnit HasUnits()
        {
            IFluentUnit u = new FluentUnit(this._plant);

            //_message = _message + eq.GetMessage();

            return u;

        }
        public IFluentUnit HasUnits(string unitname)
        {
            IFluentUnit u = new FluentUnit(this._plant, unitname);

            //_message = _message + eq.GetMessage();

            return u;

        }
        //public IFluentControlLoop HasLoopsInUnit(string name)
        //{

        //    Unit u = new Unit();
        //    u = contx.Plants.FirstOrDefault(g => g.PlantID == this._plant.PlantID).Units.FirstOrDefault(s => s.UnitName == name);

        //    IFluentControlLoop eq = new FluentControlLoop(this, name);


        //    //IFluentUnit u = new FluentUnit(u, name);

        //    //_message = _message + eq.GetMessage();

        //    return eq;

        //}

        ////public int Final()
        ////{
        ////    try
        ////    {
        ////        if (_isnew)
        ////        {

        ////            contx.Plants.Add(_plant);

        ////        }
        ////        contx.SaveChanges();

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        _message = _message + ex.ToString();
        ////    }
        ////    return 1;
        ////}
        

    }




    /// <summary>
    /// 
    /// </summary>
    public class FluentControlLoop : IFluentControlLoop
    {
        private EQControlLoop _loop;
        private Plant _plant;
        private Unit _unit;
        //IFluentPlant _parentplant;
        //IFluentUnit _parentunit;
       
        BrewDBContext contx = new BrewDBContext();

        private string _message;

        public string GetMessage() { return _message; }

        public FluentControlLoop(Plant parent)
        {
            //_plant = parent.GetPlant();
            _plant = parent;
            //_isnew = false;
            _message = "";

        }
        public FluentControlLoop(Plant plant, string name)
        {
            try
            {
                //Plant plant = parent;
                //_plant = parent.GetPlant();
//                _plant = contx.Plants.FirstOrDefault(g => g.PlantID == plant.PlantID);
                //_plant = contx.GetPlantByPlantID(plant.PlantID);
                _plant = plant;
                _message = "";
                //_loop = contx.EQControlLoops.FirstOrDefault(g => g.EquipName == name);

                //var test = contx.EQControlLoops
                //    .Where(b => b.Plant.PlantID == plant.PlantID)
                //    .Where(c => c.EquipName == name)
                //    .FirstOrDefault();

                _loop = contx.GetLoopByPlantIDAndName(_plant.PlantID, name);



                    

            }
            catch (Exception ex)
            {
                _message = _message + ex.ToString();

            }

        }

        public FluentControlLoop(Unit parentunit, Plant parentplant)
        {
            //_parentplant = parentplant;
            //_parentunit = parentunit;
            //_parent = parent;
            //_unit = parentunit.GetUnit();
            //_plant = parentplant.GetPlant();
            //_parent = parent;
            _unit = parentunit;
            _plant = parentplant;

          

        }
        //public FluentControlLoop(Plant plant, IFluentUnit parent)
        //{
            
        //    _parentunit = parent;
        //    //_parent = plant;
        //    _plant = plant;
        //    _unit = parent.GetUnit();
        //    //_parent = parent;


        //}

        //public FluentControlLoop(IFluentUnit parent, string Name)
        //{


        //    _plant = parent.GetPlant();
        //    _parentunit = parent;
        //    Unit u = _parentunit.GetUnit();

        //    _loop = contx.EQControlLoops.FirstOrDefaul


        //}

        //public FluentControlLoop(IFluentUnit parent)
        //{

        //    try
        //    {

        //        Plant plant = parent.GetPlant();
        //        _plant = contx.Plants.FirstOrDefault(g => g.PlantID == plant.PlantID);
        //        Unit u = parent.GetUnit();
        //        //_parent = parent;
        //        _isnew = false;
        //        _message = "";

        //        //_loop = contx.EQControlLoops.FirstOrDefault(g => g.);
        //        _loop = contx.EQControlLoops.FirstOrDefault(g => g.Unit.UnitID == u.UnitID);



        //    }
        //    catch (Exception ex)
        //    {
        //        _message = _message + ex.ToString();

        //    }


        //}

        public IFluentControlLoop ForControlLoop(string s)
        {
            //_loop = contx.EQControlLoops.FirstOrDefault(g => g.EquipName == s);
            _loop = contx.GetEQControlLoopsByPlantByName(_plant, s);

            //_loop = contx.get
            return this;

        }

////////        public IFluentControlLoop AddControlLoop(string LoopName, string EqDescription)
//////        public IFluentControlLoop AddControlLoop(string LoopName, string EqDescription)
//////        {
//////            //Plant p = _parent.GetPlant();
////////            Plant p = _plant;
//////            //_plant = _plant.GetPlant();
//////            _loop = _plant.PlantLoops.FirstOrDefault(g => g.EquipName == LoopName);
   
//////        AddControlLoop(LoopName

//////            //contx.AddEQControlLoopToPlant(

//////            if (_loop == null)
//////            {


//////                _loop = contx.CreateNewLoop(LoopName, EqDescription, _plant, _unit);


//////            }



//////            ////        _loop = new EQControlLoop();
                    
//////            ////        _loop.AssetTag = "111";
//////            ////        _loop.Description = "default";
//////            ////        _loop.EnableTrending = false;
//////            ////        _loop.EquipName = LoopName;

//////            ////        _loop.EQType = contx.EQTypes.FirstOrDefault(f => f.TypeDescription == EqDescription);
//////            ////        //_loop.Plant = _parent.GetPlant();
//////            ////        _loop.Plant = contx.Plants.FirstOrDefault(e => e.PlantID == p.PlantID);

//////            ////        contx.EQControlLoops.Add(_loop);
//////            ////        contx.SaveChanges();
//////            ////        contx.Plants.FirstOrDefault(g => g.PlantID == p.PlantID).PlantLoops.Add(contx.EQControlLoops.FirstOrDefault(e=>e.BaseEquipID == _loop.BaseEquipID));
//////            ////        //p.PlantLoops.Add(_loop);
//////            ////        contx.SaveChanges();

//////            ////        if (_unit != null)
//////            ////        {
//////            ////            //contx.Plants.FirstOrDefault(e => e.PlantID == p.PlantID).Units.FirstOrDefault(p => p.UnitID == _unit.UnitID).UnitLoops.Add(contx.EQControlLoops.FirstOrDefault(l => l.EquipName == LoopName));

//////            ////            contx.Units.FirstOrDefault(u => u.UnitID == _unit.UnitID).UnitLoops.Add(contx.EQControlLoops.FirstOrDefault(e => e.BaseEquipID == _loop.BaseEquipID));
//////            ////            contx.SaveChanges();


//////            ////        }

//////            ////    }
//////            ////    catch (Exception ex)
//////            ////    {
//////            ////        _message = _message + ex.ToString();

//////            ////    }
//////            ////}
//////            return this;


//////        }



        /// <summary>
        /// add to unit
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public IFluentControlLoop AddControlLoop(string name, string type="")
        {
            if (_plant != null)
            {

                _loop = contx.Plants.FirstOrDefault(g => g.PlantID == _plant.PlantID).PlantLoops.FirstOrDefault(x => x.EquipName == name);

                if (_loop == null)
                {
                    _loop = contx.CreateNewLoop(name, type, _plant, _unit);

                }

            }

            if (_loop != null)
            {
                if (_unit != null)
                {
                    _loop = contx.AddEQControlLoopToUnit(_unit.UnitID, name);

                }


            }
            return this;


        }



        public IFluentControlLoop SetSetPoint(float sp)
        {
            //_loop.EquipName = name;
            _loop.SetPoint = sp;
            contx.SaveChanges();
            return this;
        }

        public float? GetSetPoint()
        {

            return _loop.SetPoint;
        }
        public IFluentControlLoop HasType(string name)
        {
            
            //_loop.EQTypeID = cont.EQTypes.FirstOrDefault(g=>g.TypeDescription==name).EQTypeID;

            _loop.EQType = contx.EQTypes.FirstOrDefault(g => g.TypeDescription == name);

            return this;
        }


        //IFluentControlLoop AttachToUnit(string UnitName)
        //{

        //    //////_loop.EquipName = name;
        //    ////_loop.SetPoint = sp;
        //    ////contx.SaveChanges();
        //    ////return this;

        //    Unit u = new Unit();
        //    u = contx.Units.FirstOrDefault(g => g.UnitName == UnitName);

        //    if (u != null)
        //    {

        //        this._loop.u

        //    }


        //    return this;

        //}

        //////public int Final()
        //////{
            
        //////    if (_isnew)
        //////    {
        //////        //contx.EQControlLoops.Add(_loop);

        //////        Plant plantx = _parent.GetPlant();
        //////        plantx.PlantLoops.Add(_loop);
        //////    }
        //////    else
        //////    {

        //////        //cont.SaveChanges();
        //////    }
            
        //////    contx.SaveChanges();
        //////    //return this._parent;
        //////    return 1;
        //////}
    }

    public class FluentEQType : IFluentEQType
    {
        private bool _isnew;
        private EQType _eqtype;
        private BrewDBContext contx = new BrewDBContext();

        private string _message;


        public string GetMessage() { _message = "wut?"; return _message; }

        public IFluentEQType AddEQType(string s)
        {

            _isnew = false;
            EQType type = contx.EQTypes.FirstOrDefault(g => g.TypeDescription == s);

//            if (contx.EQTypes.FirstOrDefault(g => g.TypeDescription == s) == null)
            if (type == null)
            {

                _isnew = true;

                _eqtype = new EQType();
                _eqtype.TypeDescription = s;

                contx.EQTypes.Add(_eqtype);
                contx.SaveChanges();
            }

            return this;
        }

        public IFluentEQType ForEQType(string s)
        {
            return this;
        }

        public EQType GetEQType(string s)
        {

            return _eqtype;
        }

        //////public int Final()
        //////{


        //////    if (_isnew)
        //////    {

        //////        contx.EQTypes.Add(_eqtype);
        //////        contx.SaveChanges();

        //////    }
        //////    //return this;
        //////    return 1;
        //////}

    }



}






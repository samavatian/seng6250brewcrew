using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrewMLLib.Models;

namespace BrewMLLib.DAL
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
       

        IFluentPlant ForPlant(string a);
        IFluentPlant AddPlant(string a);
        
        Plant GetPlant();

        IFluentControlLoop HasLoops();
        IFluentControlLoop HasLoops(string name);

        //int Final();

        string GetMessage();

    }


    public interface IFluentEQType
    {

        IFluentEQType AddEQType(string s);
        IFluentEQType ForEQType(string s);

        EQType GetEQType(string s);

        //int Final();

        string GetMessage();

    }

    public interface IFluentControlLoop
    {

        IFluentControlLoop ForControlLoop(string s);
        IFluentControlLoop AddControlLoop(string s, string t);

        float? GetSetPoint();

        IFluentControlLoop SetSetPoint(float sp);
        IFluentControlLoop HasType(string name);

        //int Final();

        string GetMessage();

    }



    /// <summary>
    /// 
    /// </summary>
    public class FluentPlant : IFluentPlant
    {
        private bool _isnew;
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

                _isnew = false;
                _plant = contx.Plants.FirstOrDefault(p => p.PlantName == name);
                _message = "";
                //if (_plant == null)
                //{
                //    AddPlant(name);

                //}

            }
            catch (Exception ex)
            {
                _message = _message + ex.ToString();
            }

            return this;

        }

        public IFluentPlant AddPlant(string name)
        {

            _isnew = false;
            _plant = contx.Plants.FirstOrDefault(p => p.PlantName == name);
            if (_plant == null)
            {
                try
                {
                    _isnew = true;
                    _plant = new Plant();
                    _plant.PlantName = name;
                    _plant.BrewPubMenu = "default";
                    _plant.PlantAddress = "default";
                    _plant.PlantAux = new List<EQAuxilary>();
                    _plant.PlantLoops = new List<EQControlLoop>();
                    _plant.PlantVessels = new List<EQVessel>();

                    _plant.Units = new List<Unit>();

                    contx.Plants.Add(_plant);
                    contx.SaveChanges();
                }
                catch (Exception ex)
                {
                    _message = _message + ex.ToString();
                }
            }

            return this;


        }
        //public IFluentControlLoop AddLoop(string name)
        //{


        //    IFluentControlLoop loopact = new ControlLoopActions(this.GetPlant(),name);


        //    return loopact;

        //}

        public IFluentControlLoop HasLoops()
        {
            IFluentControlLoop eq = new FluentControlLoop(this);

            _message = _message + eq.GetMessage();

            return eq;

        }
        public IFluentControlLoop HasLoops(string name)
        {
            IFluentControlLoop eq = new FluentControlLoop(this, name);

            _message = _message + eq.GetMessage();

            return eq;

        }


        public int Final()
        {
            try
            {
                if (_isnew)
                {

                    contx.Plants.Add(_plant);

                }
                contx.SaveChanges();

            }
            catch (Exception ex)
            {
                _message = _message + ex.ToString();
            }
            return 1;
        }
        

    }




    /// <summary>
    /// 
    /// </summary>
    public class FluentControlLoop : IFluentControlLoop
    {
        private EQControlLoop _loop;
        private Plant _plant;
        IFluentPlant _parent;
        private bool _isnew;
        BrewDBContext contx = new BrewDBContext();

        private string _message;

        public string GetMessage() { return _message; }

        public FluentControlLoop(IFluentPlant parent)
        {
            _plant = parent.GetPlant();
            _parent = parent;
            _isnew = false;
            _message = "";

        }
        public FluentControlLoop(IFluentPlant parent, string name)
        {

            try
            {

                Plant plant = parent.GetPlant();
                _plant = contx.Plants.FirstOrDefault(g => g.PlantID == plant.PlantID);

                _parent = parent;
                _isnew = false;
                _message = "";

                //_loop = contx.Plants.FirstOrDefault(g => g.PlantID == _plant.PlantID).PlantLoops.FirstOrDefault(g => g.EquipName == name);

                //_loop = plant.PlantLoops.FirstOrDefault(g => g.EquipName == name);
                _loop = contx.EQControlLoops.FirstOrDefault(g => g.EquipName == name);


                //if (_loop == null)
                //{
                //    _isnew = true;
                //    _loop = new EQControlLoop();
                //    _loop.EquipName = name;
                //    //_loop.PlantID = _plant.PlantID;
                //    _loop.PlantID = _parent.GetPlant().PlantID;
                //}
            }
            catch (Exception ex)
            {
                _message = _message + ex.ToString();

            }


        }
        public IFluentControlLoop ForControlLoop(string s)
        {
            _loop = contx.EQControlLoops.FirstOrDefault(g => g.EquipName == s);

            return this; 

        }

        public IFluentControlLoop AddControlLoop(string s, string t)
        {
            Plant p = _parent.GetPlant();
            _loop = p.PlantLoops.FirstOrDefault(g => g.EquipName == s);
            _isnew = false;
            if (_loop == null)
            {
                try
                {
                    _isnew = true;

                    _loop = new EQControlLoop();
                    
                    _loop.AssetTag = "111";
                    _loop.Description = "default";
                    _loop.EnableTrending = false;
                    _loop.EquipName = s;
                   
                    _loop.EQType = contx.EQTypes.FirstOrDefault(f => f.TypeDescription == t);
                    //_loop.Plant = _parent.GetPlant();
                    _loop.Plant = contx.Plants.FirstOrDefault(e => e.PlantID == p.PlantID);

                    contx.EQControlLoops.Add(_loop);
                    contx.SaveChanges();
                    contx.Plants.FirstOrDefault(g => g.PlantID == p.PlantID).PlantLoops.Add(contx.EQControlLoops.FirstOrDefault(e=>e.BaseEquipID == _loop.BaseEquipID));
                    //p.PlantLoops.Add(_loop);
                    contx.SaveChanges();
                }
                catch (Exception ex)
                {
                    _message = _message + ex.ToString();

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

        public int Final()
        {
            
            if (_isnew)
            {
                //contx.EQControlLoops.Add(_loop);

                Plant plantx = _parent.GetPlant();
                plantx.PlantLoops.Add(_loop);
            }
            else
            {

                //cont.SaveChanges();
            }
            
            contx.SaveChanges();
            //return this._parent;
            return 1;
        }
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

        public int Final()
        {


            if (_isnew)
            {

                contx.EQTypes.Add(_eqtype);
                contx.SaveChanges();

            }
            //return this;
            return 1;
        }

    }



}






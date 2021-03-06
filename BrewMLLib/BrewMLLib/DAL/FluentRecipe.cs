﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using BrewMLLib.Models;

namespace BrewMLLib
{
    //class FluentRecipe
    //{
    //}


    /// <summary>
    /// 
    /// </summary>
    public interface IFluentRecipe
    {
        

        IFluentRecipe AddRecipe(string Name);
        IFluentRecipe ForRecipe(string Name);

        MasterRecipe GetRecipe();

        IFluentRecipe SetBrandName(string BrandName);
        IFluentRecipe SetBrandDescription(string BrandDescription);
        IFluentRecipe SetQualityTarget(string QTarget);

        IFluentRecipeOperations HasRecOperations();
        IFluentRecipeOperations HasRecOperations(string RecOperation);

        IFluentRecipe HasIngredients(string Name, float AmountToAdd);

        //IFluentRecipe InPlant(string s);

        //IFluentRecipe Final();
    }

    public interface IFluentRecipeOperations
    {
        

        IFluentRecipeOperations AddOperation(string Name);
        IFluentRecipeOperations ForOperation(string Name);

        RecUnitOperation GetRecUnitOperation();

        IFluentRecipeOperations SetSetPoint(float SetPoint);

        IFluentTransitions HasTransitions();

        IFluentRecipeOperations AddAllowedUnits(string UnitName);
        IFluentRecipeOperations AddAllowedUnits(string OpName, string UnitName);

        //IFluentUnit HasAllowedUnits();

        //IFluentRecipe Final();
    }


    public interface IFluentTransitions
    {
        

//        IFluentTransitions AddTranstion(string Name);
//        IFluentTransitions AddTransition(string Name, string loopname, Operants op, float SetPoint);
        IFluentTransitions AddTransition(string Name);
        IFluentTransitions ForTransition(string Name);

        IFluentTransitions GetTransition();

        IFluentTransitions SetName(string Name);
        IFluentTransitions SetTime(int Time);



        IFluentTransitions HasLoop(string LoopName);

        //IFluentRecipe Final();


    }


    public class FluentRecipe : IFluentRecipe
    {

        private MasterRecipe _recipe;
        private BrewDBContext contx = new BrewDBContext();

        //private bool _isnew;
        

        public MasterRecipe GetRecipe()
        {
            return this._recipe;
        }


        public IFluentRecipe AddRecipe(string s)
        {
            //FluentRecipe recactions = new FluentRecipe();

            _recipe = contx.CreateMasterRecipe(s);

//            _recipe = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == s);
            //_isnew = false;

            //if (_recipe == null)
            //{
            //    _isnew = true;
            //    _recipe = new MasterRecipe();

            //    _recipe.BrandDescription = "default";
            //    _recipe.BrandName = s;
            //    _recipe.Ingredients = new List<Ingredient>();
            //    _recipe.RecOperations = new List<RecUnitOperation>();
            //    _recipe.QaulityTargets = "default";

            //    _recipe.Plants = new List<Plant>();

            //    contx.MasterRecipes.Add(_recipe);
            //    contx.SaveChanges();

            //}

            return this;

        }

        public IFluentRecipe ForRecipe(string s)
        {

            _recipe = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == s);
            return this;

        }


        public IFluentRecipe SetBrandName(string s)
        {

            _recipe.BrandName = s;
            contx.SaveChanges();
            return this;

        }
        public IFluentRecipe SetBrandDescription(string s)
        {
            _recipe.BrandDescription = s;
            contx.SaveChanges();
            return this;


        }
        public IFluentRecipe SetQualityTarget(string s)
        {
            _recipe.QaulityTargets = s;
            contx.SaveChanges();
            return this;

        }


        public IFluentRecipeOperations HasRecOperations()
        {

            
            IFluentRecipeOperations op = new FluentRecipeOperations(this._recipe);

            

            return op;

        }
        public IFluentRecipeOperations HasRecOperations(string s)
        {


            IFluentRecipeOperations op = new FluentRecipeOperations(this._recipe,s);



            return op;

        }
        public IFluentRecipe HasIngredients(string s, float amount)
        {


            return this;
        }

        //public IFluentRecipe InPlant(string s)
        //{

        //    Plant plant = contx.Plants.FirstOrDefault(g => g.PlantName == s);

        //    if (plant != null)
        //    {
        //        _recipe.Plants.Add(plant);

        //    }

        //    return this;
        //}


        //public IFluentRecipe Final()
        //{

        //    if (_isnew)
        //    {
        //        contx.MasterRecipes.Add(_recipe);
        //    }
        //    //else
        //    //{

        //    //    //cont.SaveChanges();
        //    //}
        //    contx.SaveChanges();
        //    return this;
        //}
    }




    /// <summary>
    ///  Recipe Operations
    /// </summary>
    public class FluentRecipeOperations : IFluentRecipeOperations
    {


        //private IFluentRecipe _parent;
        private RecUnitOperation _operation;
        private MasterRecipe _recipe;
        private BrewDBContext contx = new BrewDBContext();
        private bool _isnew;



        public FluentRecipeOperations(MasterRecipe parent, string s)
        {
           
            //MasterRecipe rec = parent;
            _recipe = parent;
//            _operation =_parent.GetRecipe().RecOperations.FirstOrDefault(g=>g.OperationName==s);
            //_operation = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == rec.BrandName).RecOperations.FirstOrDefault(e => e.OperationName == s);
            _operation = _recipe.RecOperations.FirstOrDefault(g => g.OperationName == s);

            //if (_operation == null)
            //{
            //    //_operation = new RecUnitOperation();

            //    AddOperation(s);

            //}
        }
        public FluentRecipeOperations(MasterRecipe parent)
        {
            _recipe = parent;
        }


        public RecUnitOperation GetRecUnitOperation()
        {

            return _operation;
        }



        public IFluentRecipeOperations AddOperation(string Name)
        {


            _operation = contx.AddRecUnitOperation(_recipe,  Name);

            
            //_operation = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == s);
            //MasterRecipe rec = _parent.GetRecipe();
            //_operation = rec.RecOperations.FirstOrDefault(g => g.OperationName == s);

            //_isnew = false;

            //if (_operation == null)
            //{

            //    _isnew = true;
            //    _operation = new RecUnitOperation();

            //    _operation.OperationName = s;
            //    _operation.SetPoint = 0;
            //    _operation.Transitions = new List<Transition>();
            //    _operation.AllowedUnits = new List<Unit>();


            //    contx.RecUnitOperations.Add(_operation);
            //    contx.SaveChanges();

            //    contx.MasterRecipes.FirstOrDefault(f => f.BrandName == rec.BrandName).RecOperations.Add(_operation);

            //    contx.SaveChanges();
            //}

            return this;
        }

        public IFluentRecipeOperations ForOperation(string Name)
        {

            _operation = contx.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == _recipe.MasterRecipeID).RecOperations.FirstOrDefault(s => s.OperationName == Name);

//            _operation = contx.RecUnitOperations.FirstOrDefault(g => g.OperationName == s);

            return this;
        }

        public IFluentRecipeOperations SetSetPoint(float f)
        {
            _operation.SetPoint = f;
            contx.SaveChanges();
            return this;
        }

        public IFluentTransitions HasTransitions()
        {
            FluentTransition tran = new FluentTransition(_operation);
 
            return tran;
        }

        //public IFluentUnit HasAllowedUnits()
        //{

        //    FluentUnit u = new FluentUnit();
        //    return u;
        //}
        public IFluentRecipeOperations AddAllowedUnits(string OpName, string UnitName)
        {

           
                contx.AddUnitToRecUnitOperation(_operation, UnitName);
            
            return this;
        }
        public IFluentRecipeOperations AddAllowedUnits(string UnitName)
        {

            //RecUnitOperation op = _operation;
            if (_recipe.RecOperations == null)
            {
                return this;
            }
            else
            {
                //foreach (RecUnitOperation op in _recipe.RecOperations.ToList())
                {
                    //_operation = _recipe.RecOperations.FirstOrDefault();
                    contx.AddUnitToRecUnitOperation(_operation, UnitName);
                }
            }
            return this;
        }

        //public IFluentRecipe Final()
        //{

        //    if (_isnew)
        //    {
        //        //contx.MasterRecipes.FirstOrDefault(g=>g.RecOperations.Add(_operation);

        //        //contx.RecUnitOperations.Add(_operation);
        //        //contx.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == _parent.GetRecipe().MasterRecipeID).RecOperations.Add(_operation);

        //        MasterRecipe rec = _parent.GetRecipe();

        //        rec.RecOperations.Add(_operation);
        //        //contx.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == rec.MasterRecipeID).RecOperations.Add(_operation);

        //    }
        //    else
        //    {

        //        //cont.SaveChanges();
        //    }
        //    contx.SaveChanges();
        //    return this._parent;
        //}
    }

    public class FluentTransition: IFluentTransitions
    {

        //private IFluentRecipeOperations _parent;
        private RecUnitOperation _operation;
        private Transition _transition;
        private BrewDBContext contx = new BrewDBContext();
        private bool _isnew;



        public FluentTransition(RecUnitOperation op)
        {
            _operation = op;


        }
        public FluentTransition(RecUnitOperation op, string Name)
        {
            _transition = contx.RecUnitOperations.FirstOrDefault(i => i.RecUnitOperationID == op.RecUnitOperationID).Transitions.FirstOrDefault(s => s.TransitionName == Name);


        }



        public IFluentTransitions AddTransition(string TranName)
        {
            //RecUnitOperation r = _parent.GetRecUnitOperation();

            _transition = contx.Transitions.FirstOrDefault(g => g.TransitionName == TranName);

            //if (_transition == null)
            //{

            //    _isnew = true;

            //    _transition.TransitionName = s;
                
            //    _transition.loop = contx.EQControlLoops.FirstOrDefault(k => k.EquipName == loopname);
            //    _transition.operant = op;
            //    //_transition.SetPoint = 



            //}

            return this;

        }
        public IFluentTransitions ForTransition(string s)
        {


            return this;
        }

        public IFluentTransitions GetTransition()
        {



            return this;
        }

        public IFluentTransitions SetName(string s)
        {
        
            
            return this;
        }
        public IFluentTransitions SetTime(int i)
        {


            return this;

        }

        public IFluentTransitions SetOperant(Operants o)
        {

            return this;

        }

        public IFluentTransitions HasLoop(string s)
        {

            return this;

        }

        //IFluentRecipe Final();


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BrewMLLib.Models;

namespace BrewMLLib.DAL
{
    //class FluentRecipe
    //{
    //}


    /// <summary>
    /// 
    /// </summary>
    public interface IFluentRecipe
    {
        

        IFluentRecipe AddRecipe(string s);
        IFluentRecipe ForRecipe(string s);

        MasterRecipe GetRecipe();

        IFluentRecipe SetBrandName(string s);
        IFluentRecipe SetBrandDescription(string s);
        IFluentRecipe SetQualityTarget(string s);

        IFluentRecipeOperations HasRecOperations();
        IFluentRecipeOperations HasRecOperations(string s);

        IFluentRecipe HasIngredients(string s, float amount);

        //IFluentRecipe InPlant(string s);

        //IFluentRecipe Final();
    }

    public interface IFluentRecipeOperations
    {
        

        IFluentRecipeOperations AddOperation(string s);
        IFluentRecipeOperations ForOperation(string s);

        RecUnitOperation GetRecUnitOperation();

        IFluentRecipeOperations SetSetPoint(float f);

        IFluentRecipeOperations HasTransitions(string s);
        IFluentRecipeOperations HasAllowedUnits(string s);

        //IFluentRecipe Final();
    }


    public interface IFluentTransitions
    {
        

        IFluentTransitions AddTranstion(string s);
        IFluentTransitions ForTranstion(string s);

        Transition GetTranstion();

        IFluentTransitions SetName(string s);
        IFluentTransitions SetTime(int i);

        IFluentTransitions SetOperant(Operants o);

        IFluentTransitions HasLoop(string s);

        //IFluentRecipe Final();


    }


    public class FluentRecipe : IFluentRecipe
    {

        private MasterRecipe _recipe;
        private BrewDBContext contx = new BrewDBContext();

        private bool _isnew;
        

        public MasterRecipe GetRecipe()
        {
            return this._recipe;
        }


        public IFluentRecipe AddRecipe(string s)
        {
            //FluentRecipe recactions = new FluentRecipe();

            _recipe = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == s);
            _isnew = false;

            if (_recipe == null)
            {
                _isnew = true;
                _recipe = new MasterRecipe();

                _recipe.BrandDescription = "default";
                _recipe.BrandName = s;
                _recipe.Ingredients = new List<Ingredient>();
                _recipe.RecOperations = new List<RecUnitOperation>();
                _recipe.QaulityTargets = "default";

                _recipe.Plants = new List<Plant>();

                contx.MasterRecipes.Add(_recipe);
                contx.SaveChanges();

            }

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

            
            IFluentRecipeOperations op = new FluentRecipeOperations(this);

            

            return op;

        }
        public IFluentRecipeOperations HasRecOperations(string s)
        {


            IFluentRecipeOperations op = new FluentRecipeOperations(this,s);



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


        public IFluentRecipe Final()
        {

            if (_isnew)
            {
                contx.MasterRecipes.Add(_recipe);
            }
            //else
            //{

            //    //cont.SaveChanges();
            //}
            contx.SaveChanges();
            return this;
        }
    }


    public class FluentRecipeOperations : IFluentRecipeOperations
    {


        private IFluentRecipe _parent;
        private RecUnitOperation _operation;
        private BrewDBContext contx = new BrewDBContext();
        private bool _isnew;



        public FluentRecipeOperations(IFluentRecipe parent, string s)
        {
            _isnew = false;
            _parent = parent;
            MasterRecipe rec = _parent.GetRecipe();

//            _operation =_parent.GetRecipe().RecOperations.FirstOrDefault(g=>g.OperationName==s);
            //_operation = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == rec.BrandName).RecOperations.FirstOrDefault(e => e.OperationName == s);
            _operation = rec.RecOperations.FirstOrDefault(g => g.OperationName == s);

            //if (_operation == null)
            //{
            //    //_operation = new RecUnitOperation();

            //    AddOperation(s);

            //}
        }
        public FluentRecipeOperations(IFluentRecipe parent)
        {
            _isnew = false;
            _parent = parent;
            MasterRecipe rec = _parent.GetRecipe();
        }


        public RecUnitOperation GetRecUnitOperation()
        {

            return _operation;
        }



        public IFluentRecipeOperations AddOperation(string s)
        {

            //_operation = contx.MasterRecipes.FirstOrDefault(g => g.BrandName == s);
            MasterRecipe rec = _parent.GetRecipe();
            _operation = rec.RecOperations.FirstOrDefault(g => g.OperationName == s);

            _isnew = false;

            if (_operation == null)
            {

                _isnew = true;
                _operation = new RecUnitOperation();

                _operation.OperationName = s;
                _operation.SetPoint = 0;
                _operation.Transitions = new List<Transition>();
                _operation.AllowedUnits = new List<Unit>();
                

                contx.RecUnitOperations.Add(_operation);
                contx.SaveChanges();

                contx.MasterRecipes.FirstOrDefault(f => f.BrandName == rec.BrandName).RecOperations.Add(_operation);

                contx.SaveChanges();
            }

            return this;
        }

        public IFluentRecipeOperations ForOperation(string s)
        {
            _operation = contx.RecUnitOperations.FirstOrDefault(g => g.OperationName == s);

            return this;
        }

        public IFluentRecipeOperations SetSetPoint(float f)
        {
            _operation.SetPoint = f;
            contx.SaveChanges();
            return this;
        }

        public IFluentRecipeOperations HasTransitions(string s)
        {
            
            return this;
        }
        public IFluentRecipeOperations HasAllowedUnits(string s)
        {
            return this;
        }


        public IFluentRecipe Final()
        {

            if (_isnew)
            {
                //contx.MasterRecipes.FirstOrDefault(g=>g.RecOperations.Add(_operation);

                //contx.RecUnitOperations.Add(_operation);
                //contx.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == _parent.GetRecipe().MasterRecipeID).RecOperations.Add(_operation);

                MasterRecipe rec = _parent.GetRecipe();

                rec.RecOperations.Add(_operation);
                //contx.MasterRecipes.FirstOrDefault(g => g.MasterRecipeID == rec.MasterRecipeID).RecOperations.Add(_operation);

            }
            else
            {

                //cont.SaveChanges();
            }
            contx.SaveChanges();
            return this._parent;
        }
    }


}

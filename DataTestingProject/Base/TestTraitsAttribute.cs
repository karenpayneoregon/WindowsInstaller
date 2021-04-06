using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTestingProject.Base
{
    public enum Trait
    {
        ReadStudentsXml
    }
    /// <summary>
    /// Declarative class for using Trait enum about for traits on test method.
    /// </summary>
    public class TestTraitsAttribute : TestCategoryBaseAttribute
    {
        private readonly Trait[] _traits;

        public TestTraitsAttribute(params Trait[] traits)
        {
            this._traits = traits;
        }

        public override IList<string> TestCategories
        {
            get
            {
                var traitStrings = new List<string>();

                foreach (var trait in _traits)
                {
                    string value = Enum.GetName(typeof(Trait), trait);
                    traitStrings.Add(value);
                }

                return traitStrings;
            }
        }
    }

}
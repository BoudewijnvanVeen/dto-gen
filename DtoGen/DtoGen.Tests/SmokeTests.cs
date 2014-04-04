using System;
using System.Collections.Generic;
using DtoGen.Core;
using DtoGen.Core.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DtoGen.Tests
{
    [TestClass]
    public class SmokeTests
    {

        //[TestMethod]
        //public void Sample1()
        //{
        //    var result = Transform.Create<SampleClass1>("SampleClassDTO")
        //        .Remove(c => c.Prop2)
        //        .Rename(c => c.Prop1, "Property1")
        //        .ConvertTo<int>(c => c.Prop3)
        //        .SyncCollection<SampleClass1, string>(c => c.Prop5, c => c.Prop1, "Length")
        //        .ReplaceItemsInCollection<SampleClass1, string>(c => c.Prop6)
        //        .Generate();
        //}
        
    }


    

    public class SampleClass1
    {

        public int Prop1 { get; set; }

        public double? Prop2 { get; set; }

        public string Prop3 { get; set; }

        public DateTime? Prop4 { get; set; }

        public List<SampleClass1> Prop5 { get; set; }

        public List<SampleClass1> Prop6 { get; set; }

    }

}

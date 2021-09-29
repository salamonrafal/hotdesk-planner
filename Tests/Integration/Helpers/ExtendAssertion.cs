using System.Collections.Generic;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Integration.Helpers
{
    public static class ExtendAssertion
    {
        public static void ShouldBeOn(this List<Reservation>? actualValues, List<string> testValues)
        {
            actualValues.Should ().HaveCount (testValues.Count);
            
            foreach (var actualValue in actualValues!)
                Assert.IsTrue (testValues.Exists (x => x.Contains (actualValue.Id.ToString ())));
        } 
        
        public static void ShouldBeOn(this List<Desk>? actualValues, List<string> testValues)
        {
            actualValues.Should ().HaveCount (testValues.Count);
            
            foreach (var actualValue in actualValues!)
                Assert.IsTrue (testValues.Exists (x => x.Contains (actualValue.Id.ToString ())));
        } 
        public static void ShouldBeOn(this List<User>? actualValues, List<string> testValues)
        {
            actualValues.Should ().HaveCount (testValues.Count);
            
            foreach (var actualValue in actualValues!)
                Assert.IsTrue (testValues.Exists (x => x.Contains (actualValue.Id.ToString ())));
        } 
    }
}
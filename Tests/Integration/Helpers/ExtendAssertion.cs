using System.Collections.Generic;
using Core.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Integration.Helpers
{
    public static class ExtendAssertion
    {
        public static void ShouldBeOn(this List<Reservation>? actualValue, List<string> testValues)
        {
            actualValue.Should ().HaveCount (testValues.Count);
            
            foreach (var testValue in actualValue!)
                Assert.IsTrue (testValues.Exists (x => x.Contains (testValue.Id.ToString ())));
        } 
        
        public static void ShouldBeOn(this List<Desk>? actualValue, List<string> testValues)
        {
            actualValue.Should ().HaveCount (testValues.Count);
            
            foreach (var testValue in actualValue!)
                Assert.IsTrue (testValues.Exists (x => x.Contains (testValue.Id.ToString ())));
        } 
        public static void ShouldBeOn(this List<User>? actualValue, List<string> testValues)
        {
            actualValue.Should ().HaveCount (testValues.Count);
            
            foreach (var testValue in actualValue!)
                Assert.IsTrue (testValues.Exists (x => x.Contains (testValue.Id.ToString ())));
        } 
    }
}
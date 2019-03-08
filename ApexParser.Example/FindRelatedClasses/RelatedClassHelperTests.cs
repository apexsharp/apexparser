using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ApexSharpDemo.FindRelatedClasses
{
    [TestFixture]
    public class RelatedClassHelperTests
    {
        [Test]
        public void InvalidRelatedClasses()
        {
            Assert.DoesNotThrow(() =>
            {
                Assert.IsFalse(RelatedClassHelper.IsRelated(null, null));
                Assert.IsFalse(RelatedClassHelper.IsRelated(null, string.Empty));
                Assert.IsFalse(RelatedClassHelper.IsRelated(string.Empty, null));
                Assert.IsFalse(RelatedClassHelper.IsRelated(string.Empty, string.Empty));
                Assert.IsFalse(RelatedClassHelper.IsRelated(null, "   "));
                Assert.IsFalse(RelatedClassHelper.IsRelated("   ", null));
                Assert.IsFalse(RelatedClassHelper.IsRelated("   ", "  "));
            });
        }

        private string SampleClass { get; } = @"
            @ThatsPerfect('IsTest')
            global class SomethingGreat {
                static void main() {
                    GreatUtility x = null;
                    Object y = new CoolStuff('Hello'); // don't use BadStuff
                }

                public Something Wonderful { get; set; }

                public SomethingGreat(WhatElse x) {
                    Wonderful = new AwesomeStuff(x);
                }
            }";

        [Test]
        public void RelatedClassUseCases()
        {
            // positives
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "GreatUtility")); // variable
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "CoolStuff")); // expression
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "Object")); // variable
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "Something")); // property
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "AwesomeStuff")); // expression
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "ThatsPerfect")); // annotation
            Assert.IsTrue(RelatedClassHelper.IsRelated(SampleClass, "WhatElse")); // parameter

            // incomplete matches
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "Great")); // partial name
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "Utility")); // partial name
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "Awesome")); // partial name
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "Perfect")); // partial name

            // negatives
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "IsTest")); // string constant
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "global")); // keyword
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "Hello")); // string constant
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "BadStuff")); // commented out
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "main")); // method name
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "SomethingGreat")); // itself
            Assert.IsFalse(RelatedClassHelper.IsRelated(SampleClass, "Else")); // property name
        }
    }
}

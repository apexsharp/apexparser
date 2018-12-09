using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApexSharp.ApexParser.Toolbox;
using NUnit.Framework;

namespace ApexSharp.ApexParser.Tests.Toolbox
{
    [TestFixture]
    public class SmartEnumerableTests
    {
        [Test]
        public void SmartEnumerableForNullDoesntThrowExceptions()
        {
            var enumerable = default(string[]);
            Assert.DoesNotThrow(() =>
            {
                var smart = enumerable.AsSmartEnumerable();
                Assert.NotNull(smart);
                Assert.IsFalse(smart.Any());

                smart = enumerable.AsSmart();
                Assert.NotNull(smart);
                Assert.IsFalse(smart.Any());
            });
        }

        [Test]
        public void SmartEnumerableWorksForEmptyCollection()
        {
            var enumerable = new string[0];
            Assert.DoesNotThrow(() =>
            {
                var smart = enumerable.AsSmartEnumerable();
                Assert.NotNull(smart);
                Assert.IsFalse(smart.Any());

                smart = enumerable.AsSmart();
                Assert.NotNull(smart);
                Assert.IsFalse(smart.Any());
            });
        }

        [Test]
        public void SmartEnumerableWorksForCollectionWithASingleElement()
        {
            var enumerable = new string[] { "Hello" };
            Assert.DoesNotThrow(() =>
            {
                var smart = enumerable.AsSmartEnumerable();
                Assert.NotNull(smart);
                Assert.IsTrue(smart.Any());
                Assert.AreEqual(1, smart.Count());

                foreach (var item in smart)
                {
                    Assert.IsTrue(item.IsFirst);
                    Assert.IsTrue(item.IsLast);
                    Assert.AreEqual(0, item.Index);
                    Assert.AreEqual("Hello", item.Value);
                }
            });
        }

        [Test]
        public void SmartEnumerableWorksForCollectionWithMultipleElement()
        {
            var enumerable = new string[] { "Hello", "Cruel", "World" };
            Assert.DoesNotThrow(() =>
            {
                var smart = enumerable.AsSmartEnumerable();
                Assert.NotNull(smart);
                Assert.IsTrue(smart.Any());
                Assert.AreEqual(3, smart.Count());

                var items = smart.ToArray();
                var enumerator = smart.GetEnumerator();
                Assert.IsTrue(enumerator.MoveNext());

                var item = enumerator.Current;
                Assert.AreEqual("Hello", item.Value);
                Assert.AreEqual(0, item.Index);
                Assert.IsTrue(item.IsFirst);
                Assert.IsFalse(item.IsLast);
                Assert.IsTrue(enumerator.MoveNext());

                item = enumerator.Current;
                Assert.AreEqual("Cruel", item.Value);
                Assert.AreEqual(1, item.Index);
                Assert.IsFalse(item.IsFirst);
                Assert.IsFalse(item.IsLast);
                Assert.IsTrue(enumerator.MoveNext());

                item = enumerator.Current;
                Assert.AreEqual("World", item.Value);
                Assert.AreEqual(2, item.Index);
                Assert.IsFalse(item.IsFirst);
                Assert.IsTrue(item.IsLast);
                Assert.IsFalse(enumerator.MoveNext());
            });
        }
    }
}

using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests

    {
        [Test]
        public void Test_EmptyConstructor()
        {

            var nums = new Collection<int>();

            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.AreEqual(nums.ToString(), "[]");
        }
        [Test]

        public void Test_Collection_ConstructorSingleItem()
        {
            var nums = new Collection<int>(5);
            Assert.That(nums.ToString(), Is.EqualTo("[5]"));

        }
        [Test]

        public void Test_Collection_ConstructorMultipleItem()
        {
            var nums = new Collection<int>(10, 20, 30, 40);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40]"));

        }

        [Test]

        public void Test_Collection_Add()
        {

            var nums = new Collection<int>(5);
            nums.Add(6);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 6]"));
        }
        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }
        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");
            // Act
            var item0 = names[0];
            var item1 = names[1];
            // Assert
            Assert.That(item0, Is.EqualTo("Peter"));
            Assert.That(item1, Is.EqualTo("Maria"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var names = new Collection<string>("Bob", "Joe");
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }
        [Test]
        public void TestCollectionInsertAtStart()
        {
            var nums = new Collection<int>(new int[] { });
            nums.AddRange(20, 30, 40);
            nums.InsertAt(0, 10);

            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40]"));
        }
        [Test]
        public void TestCollectionInsertAtEnd()
        {
            var nums = new Collection<int>(new int[] { });
            nums.AddRange(10, 20, 30);
            nums.InsertAt(3, 40);

            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 40]"));
        }
        [Test]
        public void TestExchangeMiddle()
        {
            var nums = new Collection<int>(new int[] { });
            nums.AddRange(10, 20, 30, 40, 50, 60, 70);
            nums.Exchange(3, 4);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30, 50, 40, 60, 70]"));
        }
        [Test]
        public void TestRemoveAtStart()
        {
            var nums = new Collection<int>(new int[] { });
            nums.AddRange(10, 20, 30, 40);
            nums.RemoveAt(0);
            Assert.That(nums.ToString(), Is.EqualTo("[20, 30, 40]"));
        }

        [Test]
        public void TestRemoveAtEnd()
        {
            var nums = new Collection<int>(new int[] { });
            nums.AddRange(10, 20, 30, 40);
            nums.RemoveAt(3);
            Assert.That(nums.ToString(), Is.EqualTo("[10, 20, 30]"));
        }


        [Test]
         public void Test_Collection_ToStringNestedCollections()
            {
                var names = new Collection<string>("Teddy", "Gerry");
                var nums = new Collection<int>(10, 20);
                var dates = new Collection<DateTime>();
                var nested = new Collection<object>(names, nums, dates);
                string nestedToString = nested.ToString();
                Assert.That(nestedToString,
                  Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));
            }

            [Test]
            [Timeout(1000)]
            public void Test_Collection_1MillionItems()
            {
                const int itemsCount = 1000000;
                var nums = new Collection<int>();
                nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
                Assert.That(nums.Count == itemsCount);
                Assert.That(nums.Capacity >= nums.Count);
                for (int i = itemsCount - 1; i >= 0; i--)
                    nums.RemoveAt(i);
                Assert.That(nums.ToString() == "[]");
                Assert.That(nums.Capacity >= nums.Count);
            }
           

     }
}


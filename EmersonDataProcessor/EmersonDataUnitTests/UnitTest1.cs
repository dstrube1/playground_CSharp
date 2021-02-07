using System;
using EmersonDataProcessor;
using EmersonDataProcessor.model;
using EmersonDataProcessor.model.foo1;
using EmersonDataProcessor.model.foo2;
using NUnit.Framework;

namespace EmersonDataUnitTests
{
    [TestFixture]
    public class Foo1DataReaderTests
    {
        private Foo1DataReader reader;

        [SetUp]
        public void Setup()
        {
            reader = Foo1DataReader.Instance;
        }

        [Test]
        public void Read_InputIsNull_ReturnNull()
        {
            var result = reader.Read(null);

            Assert.IsNull(result, "Reader passed null should return null.");
        }

        [Test]
        public void Read_InputIsNotNull_ReturnNotNull()
        {
            IFoo foo = new Foo1();
            var result = reader.Read(foo);

            Assert.IsNotNull(result,
                "Reader passed not null should return not null.");
        }
    }

    [TestFixture]
    public class Foo2DataReaderTests
    {
        private Foo2DataReader reader;

        [SetUp]
        public void Setup()
        {
            reader = Foo2DataReader.Instance;
        }

        [Test]
        public void Read_InputIsNull_ReturnNull()
        {
            var result = reader.Read(null);

            Assert.IsNull(result, "Reader passed null should return null.");
        }

        [Test]
        public void Read_InputIsNotNull_ReturnNotNull()
        {
            IFoo foo = new Foo2();
            var result = reader.Read(foo);

            Assert.IsNotNull(result,
                "Reader passed not null should return not null.");
        }
    }

    [TestFixture]
    public class ProgramTests
    {
        //private Program program;

        [SetUp]
        public void Setup()
        {
            //program = new Program();
            /*
             * No setup needed - all Program methods are static
             */
        }

        [Test]
        public void Main_InputIsNull_ThrowException()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                delegate { Program.Main(null); });
            Assert.That(exception.Message, Is.EqualTo("Null args"),
                "Program Main passed null should throw exception.");
        }

        [Test]
        public void Main_InputIsNotNull_DoesNotThrowException()
        {
            Assert.DoesNotThrow(delegate { Program.Main(new string[0]); },
                "Program Main passed not null should not throw exception.");
        }

        [Test]
        public void ProcessFoo_Input1IsNull_ReturnFalse()
        {
            Assert.False(Program.ProcessFoo(null, 1),
                "ProcessFoo passed first parameter null should return false");
        }

        [Test]
        public void ProcessFoo_Input2IsInvalid_ReturnFalse()
        {
            //Less than minimum fooType
            Assert.False(Program.ProcessFoo(new string[0], 0),
                "ProcessFoo passed second parameter less than minimum fooType should return false");
            //Footype = anything negative
            Assert.False(Program.ProcessFoo(new string[0], -1),
                "ProcessFoo passed second parameter fooType = anything negative should return false");
            //Greater than maximum fooType
            Assert.False(Program.ProcessFoo(new string[0], 3),
                "ProcessFoo passed second parameter greater than maximum fooType should return false");

        }

        [Test]
        public void ProcessFoo_InputsValid_ReturnTrue()
        {
            //Valid 1
            Assert.True(Program.ProcessFoo(new string[0], 1),
                "ProcessFoo passed second parameter valid fooType should return true");
            //Valid 2
            Assert.True(Program.ProcessFoo(new string[0], 2),
                "ProcessFoo passed second parameter valid fooType should return true");

            //TODO: Not tested: 1st param points to 2 alternate input files 

            //All others invalid
        }
    }
}
using System.Collections.Generic;
using NUnit.Framework;
using RouletteGame.Legacy;

namespace RouletteGame.Test.Unit
{
	[TestFixture]
	public class StandardFieldFactoryUnitTest
	{
		private IFieldFactory _uut;
		private List<Field> _list;

		[SetUp]
		public void Init()
		{
			_uut = new StandardFieldFactory();
			_list = _uut.CreateFields();
		}

		[Test]
		public void CreateFields_NumberOfTotalFields_Is37()
		{
			Assert.That(_list.Count, Is.EqualTo(37));
		}

		[TestCase(0, Field.Green)]
		[TestCase(1, Field.Red)]
		[TestCase(2, Field.Black)]
		[TestCase(3, Field.Red)]
		[TestCase(14, Field.Red)]
		[TestCase(35, Field.Black)]
		[TestCase(36, Field.Red)]
		public void CreateFields_ColorOfFields_AreCorrect(int number, uint color)
		{
			Assert.That(_list[number].Color, Is.EqualTo(color));
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(10)]
		[TestCase(17)]
		[TestCase(36)]
		public void CreateFields_NumberOfFieldsIsSameAsIndex_AreCorrect(int number)
		{
			Assert.That(_list[number].Number, Is.EqualTo(number));
		}
	}
}

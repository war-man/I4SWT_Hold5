using System.Collections.Generic;
using System.Threading;
using NSubstitute;
using NUnit.Framework;
using RouletteGame.Legacy;

namespace RouletteGame.Test.Unit
{
	[TestFixture]
	public class RouletteUnitTest
	{
		private IFieldFactory _fieldFactory;
		private IRandomizer _randomizer;
		private Roulette _uut;


		[Test]
		public void Spin_RandomizerReceived_RandomizerNextIsReceived()
		{
			_fieldFactory = Substitute.For<IFieldFactory>();
			_fieldFactory.CreateFields().Returns(new List<Field> { new Field(0, Field.Green) });
			_randomizer = Substitute.For<IRandomizer>();
			_uut = new Roulette(_fieldFactory, _randomizer);

			_uut.Spin();
			_randomizer.Received().NextRandom(Arg.Any<int>(), Arg.Any<int>());
		}

		[TestCase((uint)0, (uint)0)]
		[TestCase((uint)3, (uint)3)]
		[TestCase((uint)5, (uint)5)]
		public void GetResult_ResultNunmberMatchingSpin_ResultNumberMatchesSpin(uint spinResult, uint fieldNumber)
		{
			_fieldFactory = Substitute.For<IFieldFactory>();
			_fieldFactory.CreateFields().Returns(new List<Field>
			{
				new Field(0, Field.Green),
				new Field(1, Field.Red),
				new Field(2, Field.Black),
				new Field(3, Field.Red),
				new Field(4, Field.Black),
				new Field(5, Field.Red)

			});

			_randomizer = Substitute.For<IRandomizer>();
			_randomizer.NextRandom(Arg.Any<int>(), Arg.Any<int>()).Returns(spinResult);

			_uut = new Roulette(_fieldFactory, _randomizer);
			_uut.Spin();

			Assert.That(_uut.GetResult().Number, Is.EqualTo(fieldNumber));
		}

		[TestCase((uint)0, Field.Green)]
		[TestCase((uint)3, Field.Red)]
		[TestCase((uint)4, Field.Black)]
		[TestCase((uint)5, Field.Red)]
		public void GetResult_ResultColorMatchingSpin_ResultColorMatchesSpin(uint spinResult, uint color)
		{
			_fieldFactory = Substitute.For<IFieldFactory>();
			_fieldFactory.CreateFields().Returns(new List<Field>
			{
				new Field(0, Field.Green),
				new Field(1, Field.Red),
				new Field(2, Field.Black),
				new Field(3, Field.Red),
				new Field(4, Field.Black),
				new Field(5, Field.Red)

			});

			_randomizer = Substitute.For<IRandomizer>();
			_randomizer.NextRandom(Arg.Any<int>(), Arg.Any<int>()).Returns(spinResult);

			_uut = new Roulette(_fieldFactory, _randomizer);
			_uut.Spin();

			Assert.That(_uut.GetResult().Color, Is.EqualTo(color));
		}
	}
}
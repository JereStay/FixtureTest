using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTest.Tests
{
    public class FixtureTestTests
    {
        [Fact]
        public void InitializedWithReferenceToFixtureBehaviors()
        {
            var uut = new FixtureTest();
            var expectedBehaviors = uut.Fixture.Behaviors;

            var actualBehaviors = uut.Behaviors;

            actualBehaviors.Should().BeSameAs(expectedBehaviors);
        }

        [Fact]
        public void InitializedWithReferenceToFixtureCustomizations()
        {
            var uut = new FixtureTest();
            var expectedCustomizations = uut.Fixture.Customizations;

            var actualCustomizations = uut.Customizations;

            actualCustomizations.Should().BeSameAs(expectedCustomizations);
        }

        [Fact]
        public void InitializedWithReferenceToFixtureResidueCollectors()
        {
            var uut = new FixtureTest();
            var expectedResidueCollectors = uut.Fixture.ResidueCollectors;

            var actualResidueCollectors = uut.ResidueCollectors;

            actualResidueCollectors.Should().BeSameAs(expectedResidueCollectors);
        }

        [Fact]
        public void BuildCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);

            _ = uut.Build<ICloneable>();

            mockIFixture.Received().Build<ICloneable>();
        }

        [Fact]
        public void CustomizeWithCustomizationCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);
            var mockICustomization = Substitute.For<ICustomization>();

            _ = uut.Customize(mockICustomization);

            mockIFixture.Received().Customize(mockICustomization);
        }
        
        [Fact]
        public void CustomizeWithFuncCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);
            var mockICustomizationFunc =
                Substitute.For<
                    Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>>();

            uut.Customize(mockICustomizationFunc);

            mockIFixture.Received().Customize(mockICustomizationFunc);
        }

        [Fact]
        public void CreateCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);

            uut.Create<ICloneable>();

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void CreateManyCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            mockIFixture.Create(null, null)
                .ReturnsForAnyArgs(Substitute.For<IEnumerable<ICloneable>>());
            var uut = new FixtureTest(mockIFixture);

            uut.CreateMany<ICloneable>();

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void CreateManyWithCountCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            mockIFixture.Create(null, null)
                .ReturnsForAnyArgs(Substitute.For<IEnumerable<ICloneable>>());
            var uut = new FixtureTest(mockIFixture);

            uut.CreateMany<ICloneable>(4);

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void FreezeCountCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);

            uut.Freeze<ICloneable>();

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void FreezeWithFuncCountCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);
            Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>
                composerTransformation = _ => Substitute.For<ISpecimenBuilder>();

            uut.Freeze(composerTransformation);

            mockIFixture.Received()
                .Customize(Arg.Any<Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>>());
        }

        [Fact]
        public void InjectCountCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new FixtureTest(mockIFixture);

            uut.Inject(Substitute.For<ICloneable>());

            mockIFixture.Received()
                .Customize(Arg.Any<Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>>());
        }
    }
}

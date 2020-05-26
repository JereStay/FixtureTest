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
    public class AutoFixtureTestTests
    {
        [Fact]
        public void InitializedWithReferenceToFixtureBehaviors()
        {
            var uut = new AutoFixtureTest();
            var expectedBehaviors = uut.Fixture.Behaviors;

            var actualBehaviors = uut.Behaviors;

            actualBehaviors.Should().BeSameAs(expectedBehaviors);
        }

        [Fact]
        public void InitializedWithReferenceToFixtureCustomizations()
        {
            var uut = new AutoFixtureTest();
            var expectedCustomizations = uut.Fixture.Customizations;

            var actualCustomizations = uut.Customizations;

            actualCustomizations.Should().BeSameAs(expectedCustomizations);
        }

        [Fact]
        public void InitializedWithReferenceToFixtureResidueCollectors()
        {
            var uut = new AutoFixtureTest();
            var expectedResidueCollectors = uut.Fixture.ResidueCollectors;

            var actualResidueCollectors = uut.ResidueCollectors;

            actualResidueCollectors.Should().BeSameAs(expectedResidueCollectors);
        }

        [Fact]
        public void GetOmitAutoPropertiesGetsFixtureOmitAutoProperties()
        {
            var uut = new AutoFixtureTest();
            uut.Fixture.OmitAutoProperties = true;
            var expectedOmitAutoProperties = uut.Fixture.OmitAutoProperties;

            var actualOmitAutoProperties = uut.OmitAutoProperties;

            actualOmitAutoProperties.Should().Be(expectedOmitAutoProperties);
        }

        [Fact]
        public void SetOmitAutoPropertiesSetsFixtureOmitAutoProperties()
        {
            var uut = new AutoFixtureTest();

            uut.OmitAutoProperties = true;

            uut.OmitAutoProperties.Should().Be(uut.Fixture.OmitAutoProperties);
        }

        [Fact]
        public void GetRepeatCountGetsFixtureRepeatCount()
        {
            var uut = new AutoFixtureTest();
            uut.Fixture.RepeatCount = 4;
            var expectedRepeatCount = uut.Fixture.RepeatCount;

            var actualRepeatCount = uut.RepeatCount;

            actualRepeatCount.Should().Be(expectedRepeatCount);
        }

        [Fact]
        public void SetRepeatCountSetsFixtureRepeatCount()
        {
            var uut = new AutoFixtureTest();

            uut.RepeatCount = 4;

            uut.RepeatCount.Should().Be(uut.Fixture.RepeatCount);
        }

        [Fact]
        public void CreateCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var mockICloneable = Substitute.For<ICloneable>();
            var mockISpecimenContext = Substitute.For<ISpecimenContext>();
            var uut = new AutoFixtureTest(mockIFixture);

            _ = uut.Create(mockICloneable, mockISpecimenContext);

            mockIFixture.Received().Create(mockICloneable, mockISpecimenContext);
        }

        [Fact]
        public void BuildCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);

            _ = uut.Build<ICloneable>();

            mockIFixture.Received().Build<ICloneable>();
        }

        [Fact]
        public void CustomizeWithCustomizationCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);
            var mockICustomization = Substitute.For<ICustomization>();

            _ = uut.Customize(mockICustomization);

            mockIFixture.Received().Customize(mockICustomization);
        }
        
        [Fact]
        public void CustomizeWithFuncCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);
            var mockICustomizationFunc =
                Substitute.For<
                    Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>>();

            uut.Customize(mockICustomizationFunc);

            mockIFixture.Received().Customize(mockICustomizationFunc);
        }

        [Fact]
        public void CreateCallsWrappedIFixtureWithExtension()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);

            uut.Create<ICloneable>();

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void CreateManyCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            mockIFixture.Create(null, null)
                .ReturnsForAnyArgs(Substitute.For<IEnumerable<ICloneable>>());
            var uut = new AutoFixtureTest(mockIFixture);

            uut.CreateMany<ICloneable>();

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void CreateManyWithCountCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            mockIFixture.Create(null, null)
                .ReturnsForAnyArgs(Substitute.For<IEnumerable<ICloneable>>());
            var uut = new AutoFixtureTest(mockIFixture);

            uut.CreateMany<ICloneable>(4);

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void FreezeCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);

            uut.Freeze<ICloneable>();

            mockIFixture.Received().Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Fact]
        public void FreezeWithFuncCountCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);
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
            var uut = new AutoFixtureTest(mockIFixture);

            uut.Inject(Substitute.For<ICloneable>());

            mockIFixture.Received()
                .Customize(Arg.Any<Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>>());
        }

        [Fact]
        public void RegisterCallsWrappedIFixture()
        {
            var mockIFixture = Substitute.For<IFixture>();
            var uut = new AutoFixtureTest(mockIFixture);

            uut.Register(() => Substitute.For<ICloneable>());

            mockIFixture.Received()
                .Customize(Arg.Any<Func<ICustomizationComposer<ICloneable>, ISpecimenBuilder>>());
        }
    }
}

using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using System;
using System.Collections.Generic;

namespace FixtureTest
{
    public class AutoFixtureTest : IFixture
    {
        public AutoFixtureTest() : this(new Fixture()) { }

        public AutoFixtureTest(IFixture fixture) => Fixture = fixture;

        public IFixture Fixture { get; }
        public IList<ISpecimenBuilderTransformation> Behaviors => Fixture.Behaviors;
        public IList<ISpecimenBuilder> Customizations => Fixture.Customizations;
        public bool OmitAutoProperties
        {
            get => Fixture.OmitAutoProperties;
            set => Fixture.OmitAutoProperties = value;
        }
        public int RepeatCount
        {
            get => Fixture.RepeatCount;
            set => Fixture.RepeatCount = value;
        }
        public IList<ISpecimenBuilder> ResidueCollectors => Fixture.ResidueCollectors;

        public ICustomizationComposer<T> Build<T>() => Fixture.Build<T>();

        public object Create(object request, ISpecimenContext context) => Fixture.Create(request, context);

        public IFixture Customize(ICustomization customization) => Fixture.Customize(customization);

        public void Customize<T>(Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation) => Fixture.Customize(composerTransformation);

        public T Create<T>() => Fixture.Create<T>();

        public IEnumerable<T> CreateMany<T>() => Fixture.CreateMany<T>();

        public IEnumerable<T> CreateMany<T>(int count) => Fixture.CreateMany<T>(count);

        public T Freeze<T>() => Fixture.Freeze<T>();

        public T Freeze<T>(Func<ICustomizationComposer<T>, ISpecimenBuilder> composerTransformation) => Fixture.Freeze<T>(composerTransformation);

        public void Inject<T>(T item) => Fixture.Inject(item);
    }
}
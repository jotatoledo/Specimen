# Specimen

[![Build status](https://ci.appveyor.com/api/projects/status/o8d5sn59sds2oar7/branch/master?svg=true)](https://ci.appveyor.com/project/jotatoledo/specimen/branch/master)
[![NuGet](http://img.shields.io/nuget/v/Specimen.svg?logo=nuget)](https://www.nuget.org/packages/Specimen/)

Basic contract and abstract implementation of the specification pattern.

## Installation

`Specimen` can be installed using the NuGet command line or the NuGet Package Manager in Visual Studio.

```bash
PM> Install-Package Specimen
```

## Example

First, you will need to add the following using statement:

```csharp
using Specimen;
```

### Creating specification classes

To create your own specification classes, there are 2 options:

- _Implement_ the `ISpecification<T>` interface:

```csharp
public class PositiveIntegerSpecification : ISpecification<int> 
{
    public Expression<Func<int, bool>> Predicate => value => value > 0;
    
    public bool IsSatisfiedBy(int value)
    {
        // Ideally the compiled expression should be cached
        var compiled = this.Predicate.Compile();
        return compiled(value);
    }
}

public class GreaterThanSpecification : ISpecification<int> 
{
    private readonly int threshold int;
    
    public LargerThanSpecification(int threshold)
    {
        this.threshold = threshold;
    }

    public Expression<Func<int, bool>> Predicate => value => value > this.threshold;
    
    public bool IsSatisfiedBy(int value)
    {
        // Ideally the compiled expression should be cached
        var compiled = this.Predicate.Compile();
        return compiled(value);
    }
}

public class MultipleOfSpecification : ISpecification<int> 
{
    private readonly int baseNumber int;
    
    public MultipleOfSpecification(int baseNumber)
    {
        this.baseNumber = baseNumber;
    }

    public Expression<Func<int, bool>> Predicate => value => value % this.baseNumber == 0;
    
    public bool IsSatisfiedBy(int value)
    {
        // Ideally the compiled expression should be cached
        var compiled = this.Predicate.Compile();
        return compiled(value);
    }
}
```

- _Extend_ the `SpecificationBase<T>` abstract class:

```csharp
public class PositiveIntegerSpecification : SpecificationBase<int> 
{
    public override Expression<Func<int, bool>> Predicate => value => value > 0;
}

public class GreaterThanSpecification : SpecificationBase<int> 
{
    private readonly int threshold int;
    
    public LargerThanSpecification(int threshold)
    {
        this.threshold = threshold;
    }

    public override Expression<Func<int, bool>> Predicate => value => value > this.threshold;
}

public class MultipleOfSpecification : ISpecification<int> 
{
    private readonly int baseNumber int;
    
    public MultipleOfSpecification(int baseNumber)
    {
        this.baseNumber = baseNumber;
    }

    public override Expression<Func<int, bool>> Predicate => value => value % this.baseNumber == 0;
}
```

The `SpecificationBase<T>` class already implements the `ISpecification<T>#IsSatisfiedBy` method in a cache-aware manner.

### Logical operators

Logical operations like _negation, conjunction (AND) and disjunction (OR)_ can be applied on `ISpecification<T>` instances as follows:

#### Negation

```csharp
var positive = new PositiveIntegerSpecification();

var negativeOrZero = positive.Negate();

Assert(negativeOrZero.IsSatisfiedBy(-10));
Assert(negativeOrZero.IsSatisfiedBy(-5));
Assert(negativeOrZero.IsSatisfiedBy(0));
Assert(negativeOrZero.IsSatisfiedBy(5)); // Raises error
```

#### Conjunction

```csharp
var positive = new PositiveIntegerSpecification();
var multipleOfTwo = new MultipleOfSpecification(2);
var largerThanFive = new GreaterThanSpecification(5);

var positiveMultipleOfTwoLargerThanFive = positive.And(multipleOfTwo, largerThanFive);

Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(-10)); // Raises error
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(0)); // Raises error
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(15)); // Raises error
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(20));
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(25)); // Raises error
```

#### Disjunction

```csharp
var positive = new PositiveIntegerSpecification();
var multipleOfTwo = new MultipleOfSpecification(2);
var largerThanFive = new GreaterThanSpecification(5);

var positiveMultipleOfTwoLargerThanFive = positive.Or(multipleOfTwo, largerThanFive);

Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(-10)); // Raises error
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(0)); // Raises error
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(15));
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(20));
Assert(positiveMultipleOfTwoLargerThanFive.IsSatisfiedBy(25));
```

### Working with LINQ

Support for LINQ expressions/functions is provided by the library, allowing to use `ISpecification<T>` instances as inpuf of `Where` invocations:

```csharp
var source = new []{-10, 5, 0, -5, 20 };
var positive = new PositiveIntegerSpecification();

var filtered = source.Where(positive); // Yields [5, 20]

Assert(positiveMultipleOfTwoLargerThanFive.Count() == 2);
```

## Other projects

Check out some of my other C# projects:

- [DotNetFunctional.Maybe](https://github.com/dotnetfunctional/Maybe): An Option type monad for C# with LINQ support and rich fluent syntax.
- [DotNetFunctional.Try](https://github.com/dotnetfunctional/Try): The Try monad (Error/Exceptional monad) for C# with LINQ support and rich fluent syntax.
using CleanArchitecture.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        IDictionary<string, string[]> actual = new ValidationException().Errors;

        _ = actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        List<ValidationFailure> failures =
        [
                new ValidationFailure("Age", "must be over 18"),
        ];

        IDictionary<string, string[]> actual = new ValidationException(failures).Errors;

        _ = actual.Keys.Should().BeEquivalentTo(new string[] { "Age" });
        _ = actual["Age"].Should().BeEquivalentTo(new string[] { "must be over 18" });
    }

    [Test]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        List<ValidationFailure> failures =
        [
                new ValidationFailure("Age", "must be 18 or older"),
            new ValidationFailure("Age", "must be 25 or younger"),
            new ValidationFailure("Password", "must contain at least 8 characters"),
            new ValidationFailure("Password", "must contain a digit"),
            new ValidationFailure("Password", "must contain upper case letter"),
            new ValidationFailure("Password", "must contain lower case letter"),
        ];

        IDictionary<string, string[]> actual = new ValidationException(failures).Errors;

        _ = actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

        _ = actual["Age"].Should().BeEquivalentTo(new string[]
        {
                "must be 25 or younger",
                "must be 18 or older",
        });

        _ = actual["Password"].Should().BeEquivalentTo(new string[]
        {
                "must contain lower case letter",
                "must contain upper case letter",
                "must contain at least 8 characters",
                "must contain a digit",
        });
    }
}

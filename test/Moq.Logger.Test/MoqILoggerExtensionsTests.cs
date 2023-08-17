// Copyright © 2023 Rune Gulbrandsen.
// All rights reserved. Licensed under the MIT License; see LICENSE.txt.
using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KISS.Moq.Logger.Test
{
    public class MoqILoggerExtensionsTests
    {
        [Fact]
        public void TestVerifyExtThrowsArgumentNullOnMissingMock()
        {
            Action act = () => MockILoggerExtensions.VerifyExt<ILogger>(null!, l => l.LogWarning("Test"), Times.Never, null!);

            act.Should().Throw<ArgumentNullException>().WithParameterName("mock");
        }

        [Fact]
        public void TestVerifyExtThrowsArgumentNullOnMissingExpression()
        {
            Action act = () => new Mock<ILogger>().VerifyExt(null!, Times.Never, null!);

            act.Should().Throw<ArgumentNullException>().WithParameterName("expression");
        }

        [Fact]
        public void TestVerifyExtThrowsNotSupportedExceptionOnNonExtensionMethods()
        {
            Mock<ILogger> mock = new();

            Action act = () => mock.VerifyExt(l => l.BeginScope("Hello World"), Times.Once);
            act.Should().Throw<NotSupportedException>().WithMessage("Use Mock.Verify methods for non-extension methods.");

            act = () => mock.VerifyExt(l => l.IsEnabled(LogLevel.Error), Times.Once);
            act.Should().Throw<NotSupportedException>().WithMessage("Use Mock.Verify methods for non-extension methods.");

            act = () => mock.VerifyExt(l => l.Log(LogLevel.Debug, 0, "Hello World", null, null!));
            act.Should().Throw<NotSupportedException>().WithMessage("Use Mock.Verify methods for non-extension methods.");
        }

        [Fact]
        public void TestVerifyExtThrowsNotSupportedExceptionOnUnknownILoggerExtensionMethod()
        {
            Mock<ILogger> mock = new();
            Action act = () => mock.VerifyExt(l => l.Foo(), Times.Once);
            act.Should().Throw<NotSupportedException>().WithMessage("The Foo extension method is not supported by VerifyExt.");
        }

        [Fact]
        public void TestVerifyExtThrowsNotSupportedExceptionOnUnknownILoggerLogExtensionMethod()
        {
            Mock<ILogger> mock = new();
            Action act = () => mock.VerifyExt(l => l.LogTrivia("This is a trivia"), Times.Once);
            act.Should().Throw<NotSupportedException>().WithMessage("The log extension method LogTrivia is not supported by VerifyExt.");
        }

        [Fact]
        [SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "This is intended to test invalid usage.")]
        public void TestVerifyExtThrowsInvalidOperationExceptionIfMoqItMethodsAreUsedForParametersOnExtensionMethodMessageParameter()
        {
            Mock<ILogger> mock = new();

            Action act = () => mock.VerifyExt(l => l.BeginScope(It.IsAny<string>(), "World"));

            act.Should().Throw<InvalidOperationException>().WithMessage("Moq It methods is not supported while verifying BeginScope.");
        }

        [Fact]
        public void TestVerifyExtThrowsInvalidOperationExceptionIfMoqItMethodsAreUsedForParametersOnExtensionMethodParamsParameter()
        {
            Mock<ILogger> mock = new();

            Action act = () => mock.VerifyExt(l => l.BeginScope("Hello {World}", It.IsAny<object?[]>()));

            act.Should().Throw<InvalidOperationException>().WithMessage("Moq It methods is not supported while verifying BeginScope.");
        }

        [Fact]
        public void TestVerifyExtOnBeginScopeExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.BeginScope("Hello {You}", "World");

            mock.VerifyExt(l => l.BeginScope("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogTraceExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogTrace("Hello {You}", "World");

            mock.VerifyExt(l => l.LogTrace("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogDebugExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogDebug("Hello {You}", "World");

            mock.VerifyExt(l => l.LogDebug("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogInformationExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogInformation("Hello {You}", "World");

            mock.VerifyExt(l => l.LogInformation("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogWarningExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogWarning("Hello {You}", "World");

            mock.VerifyExt(l => l.LogWarning("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogErrorExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogError("Hello {You}", "World");

            mock.VerifyExt(l => l.LogError("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogCriticalExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogCritical("Hello {You}", "World");

            mock.VerifyExt(l => l.LogCritical("Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtOnLogExtensionMethod()
        {
            Mock<ILogger> mock = new();

            mock.Object.Log(LogLevel.Debug, "Hello {You}", "World");

            mock.VerifyExt(l => l.Log(LogLevel.Debug, "Hello {You}", "World"));
        }

        [Fact]
        public void TestVerifyExtUsingMessageOnly()
        {
            Mock<ILogger> mock = new();

            Uri url = new("https://some.url/");

            int value = 10;

            mock.Object.LogInformation("I want to log the number {Number}", value);

            mock.VerifyExt(l => l.LogInformation("I want to log the number 10"));
        }

        [Fact]
        public void TestVerifyExtUsingValueTypeAsParameter()
        {
            Mock<ILogger> mock = new();

            int value = 10;

            mock.Object.LogInformation("I want to log the number {Number}", value);

            mock.VerifyExt(l => l.LogInformation("I want to log the number {Number}", value));
        }

        [Fact]
        public void TestVerifyExtUsingReferenceTypeAsParameters()
        {
            Mock<ILogger> mock = new();

            Uri url = new("https://some.url/");

            mock.Object.LogInformation("This is the url {Url}", url);

            mock.VerifyExt(l => l.LogInformation("This is the url {Url}", url));
        }

        [Fact]
        public void TestVerifyExtThrowsMockExceptionWithExtensionName()
        {
            Mock<ILogger> mock = new();

            Uri url = new("https://some.url/");

            mock.Object.LogInformation("This is the url {Url}", url);

            Action act = () => mock.VerifyExt(l => l.LogInformation("This is the url {Url}", "https://someother.url/"), Times.Once());

            act.Should().Throw<MockException>().WithMessage("*LogInformation*");
        }

        [Fact]
        public void TestVerifyExtHonorsTimesConstraint()
        {
            Mock<ILogger> mock = new();

            mock.Object.LogInformation("Hello World!");
            mock.Object.LogInformation("Hello World!");

            Action act = () => mock.VerifyExt(l => l.LogInformation("Hello World!"), Times.Exactly(1));

            act.Should().Throw<MockException>().WithMessage("*on the mock exactly 1 times, but was 2 times*");
        }

        [Fact]
        public void TestVerifyExtAddsFailMessageToMockException()
        {
            Action act = () => new Mock<ILogger>().VerifyExt(l => l.LogInformation("Hello World!"), "This is a custom fail message.");

            act.Should().Throw<MockException>().WithMessage("This is a custom fail message.*");
        }
    }
}

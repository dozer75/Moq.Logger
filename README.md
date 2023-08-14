# KISS.Moq.Logger

## ABOUT

The KISS.Moq.Logger NuGet package contains a set of extension methods to ease verification of
[Microsoft Logging extensions](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging)
using [Moq](https://github.com/moq/moq).

## INTRODUCTION

Verifying extension methods using Moq is not possible due to how the Moq framework and extension method works.

Since the Microsoft Logging extensions heavily relies on extension methods it is cumbersome to compose Verify 
operations against `ILogger` since it requires you to moq the `ILogger.Log` rather than the extension 
methods themselves.

This NuGet package adds a set of verify extension methods called `VerifyExt` that makes it possible to
verify the `ILogger` extension methods in the same way as other methods.

## USAGE

The usage is simple:

1. Install the latest version of `Kiss.Moq.Logger` using NuGet.

2. Create a `Mock` instance for the `ILogger` or `ILogger<TCategoryName>` interface you want to mock.

		Mock<ILogger> loggerMock = new Mock<ILogger>();

3. Execute the code you want to test

	   loggerMock.Object.LogInformation("Hello {You}", "World");

4. Add a `using statement` for the `KISS.Moq.Logger` namespace

       using KISS.Moq.Logger;

5. Use the `VerifyExt` method to verify the `Mock<ILogger>` instance.

	   loggerMock.VerifyExt(logger => logger.LogInformation("Hello {You}", "World");

> **NOTE:** The `VerifyExt` is only supported on extension methods in the `Microsoft.Extensions.Logging.LoggerExtensions` 
extension class.

## BUILD

You can build this project in any environment that supports .NET development. If you plan to do changes and
create pull requests refer to the CONTRIBUTING.md document for how to do this.
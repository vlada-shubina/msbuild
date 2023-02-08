// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Build.UnitTests;
using Microsoft.Build.UnitTests.Shared;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Build.Bootstrap.Tests
{
    public class MiscTests
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly TestEnvironment _environment;

        public MiscTests(ITestOutputHelper output)
        {
            _testOutput = output;
            _environment = TestEnvironment.Create(_testOutput);
        }

        [Fact]
        public void CanResolveSdkUsingBootstrapExe()
        {
            string projContents =
                    """
                    <Project Sdk="Microsoft.NET.Sdk">
                      <PropertyGroup>
                        <TargetFramework>net7.0</TargetFramework>
                      </PropertyGroup>
                    </Project>
                    """;

            TransientTestFile project = _environment.CreateFile("testProject.csproj", projContents);
            string msbuildPath = @"TODO: resolve path \artifacts\bin\bootstrap\net472\MSBuild\Current\Bin\MSBuild.exe";

            string output = RunnerUtilities.RunProcessAndGetOutput(msbuildPath, project.Path + " -r", out bool success, shellExecute: false, _testOutput);
            success.ShouldBeTrue();
        }

        [Fact]
        public void CanResolveSdkUsingBootstrapDll()
        {
            string projContents =
                    """
                    <Project Sdk="Microsoft.NET.Sdk">
                      <PropertyGroup>
                        <TargetFramework>net7.0</TargetFramework>
                      </PropertyGroup>
                    </Project>
                    """;

            TransientTestFile project = _environment.CreateFile("testProject.csproj", projContents);
            string msbuildPath = @"TODO: resolve path \artifacts\bin\bootstrap\net7.0\MSBuild\MSBuild.dll";

            string output = RunnerUtilities.RunProcessAndGetOutput(EnvironmentProvider.GetDotnetExePath(), msbuildPath + " " + project.Path + " -r", out bool success, shellExecute: false, _testOutput);
            success.ShouldBeTrue();
        }
    }
}

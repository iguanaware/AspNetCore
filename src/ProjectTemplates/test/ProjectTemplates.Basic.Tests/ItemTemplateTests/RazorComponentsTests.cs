// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Templates.Test.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Templates.Items.Test
{
    public class RazorComponentsTest
    {
        public RazorComponentsTest(ProjectFactoryFixture projectFactory, ITestOutputHelper output)
        {
            ProjectFactory = projectFactory;
            Output = output;
        }

        public Project Project { get; set; }

        public ProjectFactoryFixture ProjectFactory { get; }
        public ITestOutputHelper Output { get; }

        [Fact]
        public async Task RazorComponentsItemTemplate()
        {
            Project = await ProjectFactory.GetOrCreateProject("razorcomponentitem", Output);

            var createResult = await Project.RunDotNetNewAsync("razorcomponent --name Different");
            Assert.True(0 == createResult.ExitCode, ErrorMessages.GetFailedProcessMessage("create", Project, createResult));

            Project.AssertFileExists("Different.razor", shouldExist: true);
            Assert.Contains("<h3>Different</h3>", Project.ReadFile("Different.razor"));
        }
    }
}

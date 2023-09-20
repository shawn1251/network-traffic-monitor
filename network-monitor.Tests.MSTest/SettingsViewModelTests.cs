﻿using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using network_monitor.Contracts.Services;
using network_monitor.Models;
using network_monitor.ViewModels;

namespace network_monitor.Tests.MSTest;

[TestClass]
public class SettingsViewModelTests
{
    public SettingsViewModelTests()
    {
    }

    [TestMethod]
    public void TestSettingsViewModel_SetCurrentTheme()
    {
        var mockThemeSelectorService = new Mock<IThemeSelectorService>();
        mockThemeSelectorService.Setup(mock => mock.GetCurrentTheme()).Returns(AppTheme.Light);
        var mockAppConfig = new Mock<IOptions<AppConfig>>();
        var mockSystemService = new Mock<ISystemService>();
        var mockApplicationInfoService = new Mock<IApplicationInfoService>();

        var settingsVm = new SettingsViewModel(mockAppConfig.Object, mockThemeSelectorService.Object, mockSystemService.Object, mockApplicationInfoService.Object);
        settingsVm.OnNavigatedTo(null);

        Assert.AreEqual(AppTheme.Light, settingsVm.Theme);
    }

    [TestMethod]
    public void TestSettingsViewModel_SetCurrentVersion()
    {
        var mockThemeSelectorService = new Mock<IThemeSelectorService>();
        var mockAppConfig = new Mock<IOptions<AppConfig>>();
        var mockSystemService = new Mock<ISystemService>();
        var mockApplicationInfoService = new Mock<IApplicationInfoService>();
        var testVersion = new Version(1, 2, 3, 4);
        mockApplicationInfoService.Setup(mock => mock.GetVersion()).Returns(testVersion);

        var settingsVm = new SettingsViewModel(mockAppConfig.Object, mockThemeSelectorService.Object, mockSystemService.Object, mockApplicationInfoService.Object);
        settingsVm.OnNavigatedTo(null);

        Assert.AreEqual($"network_monitor - {testVersion}", settingsVm.VersionDescription);
    }

    [TestMethod]
    public void TestSettingsViewModel_SetThemeCommand()
    {
        var mockThemeSelectorService = new Mock<IThemeSelectorService>();
        var mockAppConfig = new Mock<IOptions<AppConfig>>();
        var mockSystemService = new Mock<ISystemService>();
        var mockApplicationInfoService = new Mock<IApplicationInfoService>();

        var settingsVm = new SettingsViewModel(mockAppConfig.Object, mockThemeSelectorService.Object, mockSystemService.Object, mockApplicationInfoService.Object);
        settingsVm.SetThemeCommand.Execute(AppTheme.Light.ToString());

        mockThemeSelectorService.Verify(mock => mock.SetTheme(AppTheme.Light));
    }
}

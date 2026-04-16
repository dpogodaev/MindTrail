using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Common.Utilities;

namespace MindTrail.Common.Tests.UtilityTests;

/// <summary>
/// Tests for <see cref="ElapsedTimeMeter"/> class.
/// </summary>
[TestClass]
public class ElapsedTimeMeterTests
{
    #region Constructor

    /// <summary>
    /// Test for the constructor of class <see cref="ElapsedTimeMeter"/>.
    /// </summary>
    [TestMethod]
    public void Constructor_AutoStartupIsEnabled_TimerIsActive()
    {
        // Act
        var timer = new ElapsedTimeMeter(true);

        // Assert
        Assert.IsTrue(timer.IsActive);
    }

    /// <summary>
    /// Test for the constructor of class <see cref="ElapsedTimeMeter"/>.
    /// </summary>
    [TestMethod]
    public void Constructor_AutoStartupIsDisabled_TimerIsNotActive()
    {
        // Act
        var timer = new ElapsedTimeMeter();

        // Assert
        Assert.IsFalse(timer.IsActive);
    }

    #endregion

    #region Start

    /// <summary>
    /// Test for <see cref="ElapsedTimeMeter.Start"/> method.
    /// </summary>
    [TestMethod]
    public void Start_TimerIsNotActive_StartsTimer()
    {
        // Arrange
        var timer = new ElapsedTimeMeter();

        // Act
        timer.Start();
        DoSomethingInMs(1);

        // Assert
        Assert.IsTrue(timer.IsActive);
        Assert.AreNotEqual(0, timer.ElapsedTimeInMs);
        Assert.AreNotEqual(0, timer.TotalElapsedTimeInMs);
    }

    /// <summary>
    /// Test for <see cref="ElapsedTimeMeter.Start"/> method.
    /// </summary>
    [TestMethod]
    public void Start_TimerIsActive_ThrowsException()
    {
        // Arrange
        var timer = new ElapsedTimeMeter(true);

        // Act
        void StartTimer() => timer.Start();

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(StartTimer);
        Assert.AreEqual("The time meter has already been started.", exception.Message);
        Assert.IsTrue(timer.IsActive);
    }

    #endregion

    #region Stop

    /// <summary>
    /// Test for <see cref="ElapsedTimeMeter.Stop"/> method.
    /// </summary>
    [TestMethod]
    public void Stop_TimerIsActive_StopsTimer()
    {
        // Arrange
        var timer = new ElapsedTimeMeter(true);

        DoSomethingInMs(1);

        var elapsedTimeInMs = timer.ElapsedTimeInMs;
        var totalElapsedTimeInMs = timer.TotalElapsedTimeInMs;

        // Act
        timer.Stop();

        // Assert
        Assert.IsFalse(timer.IsActive);
        Assert.AreEqual(0, timer.ElapsedTimeInMs);
        Assert.AreEqual(0, timer.TotalElapsedTimeInMs);
        Assert.AreNotEqual(0, elapsedTimeInMs);
        Assert.AreNotEqual(0, totalElapsedTimeInMs);
    }

    /// <summary>
    /// Test for <see cref="ElapsedTimeMeter.Stop"/> method.
    /// </summary>
    [TestMethod]
    public void Stop_TimerIsNotActive_ThrowsException()
    {
        // Arrange
        var timer = new ElapsedTimeMeter();

        // Act
        void StopTimer() => timer.Stop();

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(StopTimer);
        Assert.AreEqual("The time meter was not started.", exception.Message);
        Assert.IsFalse(timer.IsActive);
    }

    #endregion

    #region Restart

    /// <summary>
    /// Test for <see cref="ElapsedTimeMeter.Restart"/> method.
    /// </summary>
    [TestMethod]
    public void Restart_TimerIsActive_RestartsTimer()
    {
        // Arrange
        var timer = new ElapsedTimeMeter(true);

        DoSomethingInSeconds(1);

        var elapsedTimeInSecondsBeforeReset = timer.ElapsedTimeInMs / 1000;
        var totalElapsedTimeInSecondsBeforeReset = timer.TotalElapsedTimeInMs / 1000;

        // Act
        timer.Restart();

        var statusAfterReset = timer.IsActive;
        var elapsedTimeInSecondsAfterReset = timer.ElapsedTimeInMs / 1000;
        var totalElapsedTimeInSecondsAfterReset = timer.TotalElapsedTimeInMs / 1000;

        DoSomethingInSeconds(1);

        var elapsedTimeInSecondsBeforeStop = timer.ElapsedTimeInMs / 1000;
        var totalElapsedTimeInSecondsBeforeStop = timer.TotalElapsedTimeInMs / 1000;

        timer.Stop();

        // Assert
        Assert.AreEqual(1, elapsedTimeInSecondsBeforeReset);
        Assert.AreEqual(1, totalElapsedTimeInSecondsBeforeReset);

        Assert.IsTrue(statusAfterReset);
        Assert.AreEqual(0, elapsedTimeInSecondsAfterReset);
        Assert.AreEqual(1, totalElapsedTimeInSecondsAfterReset);

        Assert.AreEqual(1, elapsedTimeInSecondsBeforeStop);
        Assert.AreEqual(2, totalElapsedTimeInSecondsBeforeStop);
    }

    /// <summary>
    /// Test for <see cref="ElapsedTimeMeter.Restart"/> method.
    /// </summary>
    [TestMethod]
    public void Restart_TimerIsNotActive_ThrowsException()
    {
        // Arrange
        var timer = new ElapsedTimeMeter();

        // Act
        void PauseTimer() => timer.Restart();

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(PauseTimer);
        Assert.AreEqual("The time meter is not active.", exception.Message);
        Assert.IsFalse(timer.IsActive);
    }

    #endregion

    private static void DoSomethingInMs(int millisecondsNumber)
    {
        Thread.Sleep(millisecondsNumber);
    }

    private static void DoSomethingInSeconds(int secondsNumber)
    {
        Thread.Sleep(secondsNumber * 1000);
    }
}
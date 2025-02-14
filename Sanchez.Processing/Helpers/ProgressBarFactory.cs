﻿using Sanchez.Processing.Models;
using Sanchez.Processing.Services;
using ShellProgressBar;

namespace Sanchez.Processing.Helpers;

public static class ProgressBarFactory
{
    public static IProgressBar NewProgressBar(RenderOptions options, int maxTicks, string? initialMessage = null)
    {
        if (options.Quiet || options.Verbose) return new NullProgressBar();
            
        var progressBarOptions = DefaultOptions();

        try
        {
            return new ProgressBar(maxTicks, initialMessage, progressBarOptions);
        }
        catch (IOException)
        {
            // Unable to get console buffer, so return a no-op progress bar
            return new NullProgressBar();
        }
    }

    public static ProgressBarOptions DefaultOptions()
    {
        return new()
        {
            ProgressCharacter = '─',
            ProgressBarOnBottom = true,
            ForegroundColor = ConsoleColor.Cyan,
            ForegroundColorDone = ConsoleColor.Blue
        };
    }
}
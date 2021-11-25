package com.jetbrains.rider.plugins.coachsharp.options

import com.jetbrains.rider.settings.simple.SimpleOptionsPage

class CoachSharpOptionsPage : SimpleOptionsPage("CoachSharp Options", "CoachSharpOptionsPage") {
    override fun getId(): String {
        return "CoachSharpOptionsPage"
    }
}
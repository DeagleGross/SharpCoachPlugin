// Copyright 2000-2020 JetBrains s.r.o. and other contributors. Use of this source code is governed by the Apache 2.0 license that can be found in the LICENSE file.

package com.jetbrains.rider.plugins.coachsharp.toolwindows.mappingresults;

import com.intellij.openapi.wm.ToolWindow;
import com.google.gson.Gson;
import java.io.*;
import javax.swing.*;
import java.util.Calendar;
import java.nio.file.*;

public class MappingResultsWindow {
  private JPanel myToolWindowContent;
  private ContentManager contentManager;

  public MappingResultsWindow(ToolWindow toolWindow) {
    contentManager = new ContentManager();
    myToolWindowContent = contentManager.getContent();
  }
  public JPanel getContent() {
    return myToolWindowContent;
  }
}
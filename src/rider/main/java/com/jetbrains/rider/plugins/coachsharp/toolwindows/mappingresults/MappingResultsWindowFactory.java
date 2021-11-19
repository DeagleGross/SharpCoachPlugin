// Copyright 2000-2020 JetBrains s.r.o. and other contributors. Use of this source code is governed by the Apache 2.0 license that can be found in the LICENSE file.

package com.jetbrains.rider.plugins.coachsharp.toolwindows.mappingresults;

import com.intellij.openapi.project.Project;
import com.intellij.openapi.wm.ToolWindow;
import com.intellij.openapi.wm.ToolWindowFactory;
import com.intellij.ui.content.Content;
import com.intellij.ui.content.ContentFactory;
import org.jetbrains.annotations.NotNull;

import java.io.File;

public class MappingResultsWindowFactory implements ToolWindowFactory {

  /**
   * Create the tool window content.
   *
   * @param project    current project
   * @param toolWindow current tool window
   */
  public void createToolWindowContent(@NotNull Project project, @NotNull ToolWindow toolWindow) {
    drawToolWindow(project, toolWindow);
    setupContentWatcher(project, toolWindow);
  }
  
  public static void drawToolWindow(Project project, ToolWindow toolWindow){
    MappingResultsWindow myToolWindow = new MappingResultsWindow(toolWindow);
    ContentFactory contentFactory = ContentFactory.SERVICE.getInstance();
    Content content = contentFactory.createContent(myToolWindow.getContent(), "", false);
    toolWindow.getContentManager().addContent(content);
  }
  
  private void setupContentWatcher(Project project, ToolWindow toolWindow){
      // new FileWatcher(new File("F:\\Projects\\SharpCoachPlugin\\design_builder_app\\test.json"), project, toolWindow).start();
  }

}
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

  public MappingResultsWindow(ToolWindow toolWindow) {
    initializeWindow();
  }

  public void initializeWindow() {
    // initialize components of page
    myToolWindowContent = new JPanel();
    JLabel pageText = new JLabel();
    
    // fill in results to design
//     MappingResult results = LoadResults();
//     pageText.setText(results.b);

    Gson g = new Gson();
    
    Person person = g.fromJson("{\"name\": \"John\"}", Person.class);
    var res = person.name; //John
    
    System.out.println(g.toJson(person)); // {"name":"John"}

    String fileName = Path.of("test.txt").toAbsolutePath().toString();

    pageText.setText(fileName);
    
    // add text to page
    myToolWindowContent.add(pageText);
  }

  public JPanel getContent() {
    return myToolWindowContent;
  }
  
  private MappingResult LoadResults() {
    var deserialized = JsonOperator.Deserialize("F:\\Projects\\Baraholka\\test.json");
    return (MappingResult)deserialized; 
  }

}
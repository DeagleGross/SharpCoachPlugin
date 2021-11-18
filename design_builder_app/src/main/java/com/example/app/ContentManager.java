package com.example.app;

import javax.swing.*;
import java.awt.*;

public class ContentManager {
    public JPanel getContent(){
        JPanel wholeWindow = new JPanel();
        wholeWindow.setLayout(new BoxLayout(wholeWindow, BoxLayout.Y_AXIS));

        var mappingResults = JsonOperator.DeserializeToMappingResults("F:\\Projects\\Baraholka\\test.json");

        if (mappingResults == null) {
            // showing just that we didn't succeed in reading results of an operation
            wholeWindow.add(new JTextField("Mapping results could not load properly..."));
        }
        else {
            for (var currentOperationResult : mappingResults) {
                // displaying info about single mapping operation

                var operationPanel = new JPanel();
                operationPanel.setLayout(new BoxLayout(operationPanel, BoxLayout.Y_AXIS));

                var generalInfoPanel = new JPanel(new FlowLayout());
                generalInfoPanel.add(new JLabel("[" + currentOperationResult.operationDate + "]"));
                generalInfoPanel.add(new JLabel("Mapping FROM '" + currentOperationResult.inputClassName + "'"));
                generalInfoPanel.add(new JLabel("Mapping TO '" + currentOperationResult.outputClassName + "'"));

                var detailsInfoPanel = new JPanel();
                generalInfoPanel.add(new JLabel("text text text"));

                operationPanel.add(generalInfoPanel);
                operationPanel.add(detailsInfoPanel);

                wholeWindow.add(operationPanel);
            }
        }

        return wholeWindow;
    }
}

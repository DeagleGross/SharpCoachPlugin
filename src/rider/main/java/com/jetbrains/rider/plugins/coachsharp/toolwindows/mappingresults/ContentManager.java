package com.jetbrains.rider.plugins.coachsharp.toolwindows.mappingresults;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;

public class ContentManager {
    public JPanel getContent(){
        JPanel wholeWindow = new JPanel();
        wholeWindow.setLayout(new BoxLayout(wholeWindow, BoxLayout.Y_AXIS));

        var mappingResults = JsonOperator.DeserializeToMappingResults("F:\\Projects\\SharpCoachPlugin\\design_builder_app\\test.json");

        if (mappingResults == null) {
            // showing just that we didn't succeed in reading results of an operation
            wholeWindow.add(new JTextField("Mapping results could not load properly..."));
        }
        else {
            for (var operationResult : mappingResults) {
                // displaying info about single mapping operation

                var operationPanel = new JPanel();
                operationPanel.setLayout(new BoxLayout(operationPanel, BoxLayout.Y_AXIS));
                operationPanel.setBorder(new LineBorder(Color.BLACK, 1));

                // add operation main description
                addOperationInfo(operationPanel, operationResult);

                // add table of failed to map properties
                addOperationFails(operationPanel, operationResult);

                wholeWindow.add(operationPanel);
            }
        }

        return wholeWindow;
    }

    private void addOperationInfo(JPanel panel, MappingResult mappingResult) {
        var innerPanel = new JPanel();
        innerPanel.add(new JLabel("[" + mappingResult.operationDate + "]"));
        innerPanel.add(new JLabel("'" + mappingResult.inputClassName + "'"));
        innerPanel.add(new JLabel("to"));
        innerPanel.add(new JLabel("'" + mappingResult.outputClassName + "'"));

        panel.add(innerPanel);
    }

    private void addOperationFails(JPanel panel, MappingResult mappingResult) {
        var noInputFails = (mappingResult.failedInputProperties == null || mappingResult.failedInputProperties.length == 0);
        var noOutputFails = (mappingResult.failedOutputProperties == null || mappingResult.failedOutputProperties.length == 0);

        if (noInputFails && noOutputFails) {
            panel.add(new JLabel("Mapping was successful"));
            return;
        }

        // just to avoid NRE
        if (mappingResult.failedInputProperties == null) mappingResult.failedInputProperties = new String[0];
        if (mappingResult.failedOutputProperties == null) mappingResult.failedOutputProperties = new String[0];

        // forming table data array
        var maxFailedProperties = Math.max(mappingResult.failedInputProperties.length, mappingResult.failedOutputProperties.length);
        var minFailedProperties = Math.min(mappingResult.failedInputProperties.length, mappingResult.failedOutputProperties.length);
        var data = new String[maxFailedProperties][2];
        for (int i = 0; i < minFailedProperties; i++){
            data[i][0] = mappingResult.failedInputProperties[i];
            data[i][1] = mappingResult.failedInputProperties[i];
        }
        if (mappingResult.failedInputProperties.length > minFailedProperties)
            for (int i = minFailedProperties + 1; i < maxFailedProperties; i++) data[i][0] = mappingResult.failedInputProperties[i];
        if (mappingResult.failedOutputProperties.length > minFailedProperties)
            for (int i = minFailedProperties + 1; i < maxFailedProperties; i++) data[i][0] = mappingResult.failedOutputProperties[i];

        var innerTable = new JTable(data, new String[] { mappingResult.inputClassName, mappingResult.outputClassName });

        panel.add(innerTable);
    }
}